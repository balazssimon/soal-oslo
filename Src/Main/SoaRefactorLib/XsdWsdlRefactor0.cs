using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using SoaMetaModel;

namespace SoaMM
{
    public class XsdWsdlRefactor
    {
        XNamespace xmlNS = "http://www.w3.org/2001/XMLSchema";
        XNamespace wsdlNS = "http://schemas.xmlsoap.org/wsdl/";
        XNamespace soap12NS = "http://schemas.xmlsoap.org/wsdl/soap12/";
        XNamespace soapNS = "http://schemas.xmlsoap.org/wsdl/soap/";

        public string path = @"..\..\wsdl\";
        //public string fileName = "HelloNamespace.wsdl";
        public string fileName = "t.wsdl";

        public bool loging = true;
        private List<Collective> readedElements = new List<Collective>();
        private List<string> faultList = new List<string>();

        private class Collective
        {
            public string name { get; set; }
            public ObjectTypes objectType { get; set; }
            public XNamespace ownNS { get; set; }
            public XElement definition { get; set; }
            public bool isReady { get; set; }
            public SoaObject objRef { get; set; }

            public Collective()
            {
                this.name = "";
                this.ownNS = null;
                this.definition = null;
                this.isReady = false;
                this.objRef = null;
            }

        }

        enum ObjectTypes
        {
            service,
            binding,
            porttype,
            message,

            enumType,
            complexType,

            exceptionType,
            structType,
            arrayType
        }

        public void Run()
        {
            Read();
            Declare();
            Linking();
        }

        private void Read()
        {
            try
            {

                // Beolvasandó dokumentumok listája
                LinkedList<XDocument> documentList = new LinkedList<XDocument>();

                // Imprortálások bekérése
                XDocument Doc = XDocument.Load(@path + fileName);

                // Rekurzív függvény az importálások kifejtésére
                CheckImports(documentList, Doc, path);

                //--------------------------------------------------------------------

                // Az összes fájl feldolgozása
                foreach (XDocument xd in documentList)
                {
                    XNamespace targetNS = "";
                    XNamespace defNS = "";
                    try
                    {
                        targetNS = (string)xd.Element("definitions").Attribute("targetNamespace");
                        defNS = "";
                    }
                    catch { }
                    try
                    {
                        targetNS = (string)xd.Element("schema").Attribute("targetNamespace");
                        defNS = "";
                    }
                    catch { }
                    try
                    {
                        targetNS = (string)xd.Element(xmlNS + "schema").Attribute("targetNamespace");
                        defNS = xmlNS;
                    }
                    catch { }
                    try
                    {
                        targetNS = (string)xd.Element(wsdlNS + "definitions").Attribute("targetNamespace");
                        defNS = wsdlNS;
                    }
                    catch { }

                    // services
                    var services = from c in xd.Descendants(wsdlNS + "service")
                                   select c;

                    foreach (XElement xe in services)
                    {
                        Collective coll = new Collective()
                        {
                            name = (string)xe.Attribute("name"),
                            definition = xe,
                            objectType = ObjectTypes.service,
                            ownNS = targetNS
                        };

                        readedElements.Add(coll);
                    }

                    // binding
                    var bindings = from c in xd.Descendants(wsdlNS + "binding")
                                   select c;

                    foreach (XElement xe in bindings)
                    {
                        Collective coll = new Collective()
                        {
                            name = (string)xe.Attribute("name"),
                            definition = xe,
                            objectType = ObjectTypes.binding,
                            ownNS = targetNS
                        };

                        readedElements.Add(coll);
                    }

                    // porttype
                    var portTypes = from c in xd.Descendants(wsdlNS + "portType")
                                    select c;

                    foreach (XElement xe in portTypes)
                    {
                        Collective coll = new Collective()
                        {
                            name = (string)xe.Attribute("name"),
                            definition = xe,
                            objectType = ObjectTypes.porttype,
                            ownNS = targetNS
                        };

                        readedElements.Add(coll);
                    }

                    // message
                    var messages = from c in xd.Descendants(wsdlNS + "message")
                                   select c;

                    foreach (XElement xe in messages)
                    {
                        Collective coll = new Collective()
                        {
                            name = (string)xe.Attribute("name"),
                            definition = xe,
                            objectType = ObjectTypes.message,
                            ownNS = targetNS
                        };

                        readedElements.Add(coll);
                    }

                    //TODO: <type> és <schema> ellenőrzése a targetNamespace miatt!!!                    

                    // simpleTypes
                    var simpleTypes = from c in xd.Descendants(xmlNS + "simpleType")
                                      select c;

                    foreach (XElement xe in simpleTypes)
                    {
                        Collective coll = new Collective()
                        {
                            name = (string)xe.Attribute("name"),
                            definition = xe,
                            objectType = ObjectTypes.enumType,
                            ownNS = targetNS
                        };

                        readedElements.Add(coll);
                    }

                    // complexTypes
                    var complexTypes = from c in xd.Descendants(xmlNS + "complexType")
                                       select c;

                    foreach (XElement xe in complexTypes)
                    {
                        Collective coll = new Collective()
                        {
                            name = (string)xe.Attribute("name"),
                            definition = xe,
                            objectType = ObjectTypes.complexType,
                            ownNS = targetNS

                        };

                        readedElements.Add(coll);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        // Rekurzív fv az importok feldolgozására
        private void CheckImports(LinkedList<XDocument> documentList, XDocument doc, string path)
        {
            documentList.AddLast(doc);
            var files = from c in doc.Descendants(xmlNS + "import")
                         select c.Attribute("schemaLocation");

            foreach (string file in files)
            {
                XDocument import = XDocument.Load(@path + file);
                Wr("Import file: " + file);
                CheckImports(documentList, import, path);
            }
        }

        // Deklarálja az objektumokat
        private void Declare()
        {
            // service
            var services = from c in readedElements
                           where c.objectType == ObjectTypes.service
                           select c;

            Wr("Declare services:");
            foreach (Collective service in services)
            {
                Endpoint ep = new Endpoint()
                {
                    Name = (string)service.definition.Attribute("name")
                };
                service.objRef = ep;
                Wr("   " + ep.Name);
            }

            // binding
            var bindings = from c in readedElements
                           where c.objectType == ObjectTypes.binding
                           select c;

            Wr("Declare bindings:");
            foreach (Collective binding in bindings)
            {
                Binding bg = new Binding()
                {
                    Name = (string)binding.definition.Attribute("name")
                };
                binding.objRef = bg;
                Wr("   " + bg.Name);
            }
            // porttype
            var portTypes = from c in readedElements
                            where c.objectType == ObjectTypes.porttype
                            select c;

            Wr("Declare porttypes:");
            foreach (Collective porttype in portTypes)
            {
                Interface ifa = new Interface()
                {
                    Name = (string)porttype.definition.Attribute("name")
                };
                porttype.objRef = ifa;
                Wr("   " + ifa.Name);
            }

            // simpleTypes
            var simpleTypes = from c in readedElements
                              where c.objectType == ObjectTypes.enumType
                              select c;

            Wr("Declare SimpleTypes:");
            foreach (Collective simpleType in simpleTypes)
            {
                EnumType et = new EnumType()
                {
                    Name = (string)simpleType.definition.Attribute("name")
                };
                simpleType.objRef = et;
                Wr("   " + et.Name);
            }

            // arrayType létrehozása
            // TODO: !!! mi van, ha több element van ???
            var arrayTypes = from c in readedElements
                             where c.objectType == ObjectTypes.complexType &&
                                   c.definition.Element(xmlNS + "sequence") != null &&
                                   c.definition.Element(xmlNS + "sequence").Element(xmlNS + "element") != null &&
                                   c.definition.Element(xmlNS + "sequence").Element(xmlNS + "element").Attribute("maxOccurs") != null &&
                                   (string)c.definition.Element(xmlNS + "sequence").Element(xmlNS + "element").Attribute("maxOccurs") != "1"
                             select c;

            Wr("Declare ArrayTypes:");
            foreach (Collective arrayType in arrayTypes)
            {
                ArrayType at = new ArrayType()
                {
                    Name = (string)arrayType.definition.Attribute("name")
                };
                arrayType.objRef = at;
                arrayType.objectType = ObjectTypes.arrayType;
                Wr("   " + at.Name);
            }

            // ExceptionType
            // kiválasztjuk a portType-okat
            Wr("Declare ExceptionTypes:");
            foreach (Collective portType in portTypes)
            {
                XElement node = portType.definition;

                // kiszűrjük a fault-tagek message attribútumát
                var faultsFromPorttype = from c in node.Descendants(wsdlNS + "fault")
                                         select c.Attribute("message");

                foreach (string faultMsgName in faultsFromPorttype)
                {
                    Collective message = SearchObject(ObjectTypes.message, faultMsgName, node);
                    var exceptionTypeNames = from c in message.definition.Descendants(wsdlNS + "part")
                                             select c.Attribute("element"); /// ?!?!?!

                    foreach (string excTypeName in exceptionTypeNames)
                    {
                        Collective exceptionType = SearchObject(ObjectTypes.complexType, excTypeName, node);
                        ExceptionType et = new ExceptionType()
                        {
                            Name = exceptionType.name
                        };

                        exceptionType.objRef = et;
                        exceptionType.objectType = ObjectTypes.exceptionType;
                        Wr("   " + et.Name);
                    }
                }
            }

            // structType létrehozása (minden olyan complexType, ami nem arrayType, vagy ExceptionType)
            var structTypes = from c in readedElements
                              where c.objectType == ObjectTypes.complexType
                              select c;

            Wr("Declare StructTypes:");
            foreach (Collective structType in structTypes)
            {
                StructType at = new StructType()
                {
                    Name = (string)structType.definition.Attribute("name")
                };
                structType.objRef = at;
                structType.objectType = ObjectTypes.structType;
                Wr("   " + at.Name);
            }
        }

        // Beállítja a megfelelő referenciákat
        private void Linking()
        {
            foreach (Collective coll in readedElements)
            {
                XElement def = coll.definition;

                switch (coll.objectType)
                {
                    case ObjectTypes.service:
                        {
                            Wr("Processing: (" + coll.objectType + ")" + coll.name);
                            Endpoint ep = (Endpoint)coll.objRef;

                            // binding beállítása:
                            string bindingName = (string)def.Element(wsdlNS + "port").Attribute("binding");
                            Collective bindingObject = SearchObject(ObjectTypes.binding, bindingName, def);
                            ep.Binding = (Binding)bindingObject.objRef;
                            Wr("   -add binding: " + ep.Binding.Name);

                            // interface beállítása
                            string interfaceName = (string)bindingObject.definition.Attribute("type");
                            Collective interfaceObject = SearchObject(ObjectTypes.porttype, interfaceName, bindingObject.definition);
                            ep.Interface = (Interface)interfaceObject.objRef;
                            Wr("   -add interface: " + ep.Interface.Name);

                            // Address beállítása
                            string location = "";
                            try
                            {
                                location = (string)def.Element(wsdlNS + "port").Element(soapNS + "address").Attribute("location");
                            }
                            catch (Exception) { }

                            try
                            {
                                location = (string)def.Element(wsdlNS + "port").Element(soap12NS + "address").Attribute("location");
                            }
                            catch (Exception e) { }
                            EndpointAddress epa = new EndpointAddress(location);
                            ep.Address = epa;
                            Wr("   -add address: " + ep.Address.Uri);
                        }
                        break;
                    case ObjectTypes.binding:
                        {
                            Wr("Processing: (" + coll.objectType + ")" + coll.name);
                            Binding bi = (Binding)coll.objRef;

                            
                            //// interface beállítása
                            //string interfaceName = (string)def.Attribute("type");
                            //Collective interfaceObject = SearchObject(ObjectTypes.porttype, interfaceName, def);
                            //bi.Interface = (Interface)interfaceObject.objRef;
                            //Wr("   -add interface: " + bi.Interface.Name);

                            //string transport = "";
                            //EncodingBindingElement ebe = new EncodingBindingElement();
                            //try
                            //{
                            //    transport = (string)def.Element(soapNS + "binding").Attribute("transport");
                            //    // ebe beállítása
                            //}
                            //catch (Exception e) { }

                            //try
                            //{
                            //    transport = (string)def.Element(soap12NS + "binding").Attribute("transport");
                            //    // ebe beállítása
                            //}
                            //catch (Exception e) { }
                            //TransportBindingElement tbe = new TransportBindingElement();
                            //// tbe beállítása
                            //bi.Encoding = ebe;
                            //bi.Transport = tbe;

                        }
                        break;
                    case ObjectTypes.porttype:
                        {
                            Wr("Processing: (" + coll.objectType + ")" + coll.name);
                            Interface ifa = (Interface)coll.objRef;

                            var operations = from c in def.Descendants(wsdlNS + "operation")
                                             select c;

                            foreach (XElement operation in operations)
                            {

                                string name = (string)operation.Attribute("name");
                                Operation op = new Operation()
                                {
                                    Name = name
                                };
                                Wr("   -add operation: " + op.Name);
                                try
                                {
                                    XElement input = operation.Element(wsdlNS + "input");
                                    string inputName = (string)input.Attribute("name"); // !!! EZ ITT MINEK LEGYEN A NEVE ???
                                    string messageName = (string)input.Attribute("message");
                                    Collective messageObject = SearchObject(ObjectTypes.message, messageName, def);

                                    var operationInputs = from c in messageObject.definition.Descendants(wsdlNS + "part")
                                                          select c;
                                    Wr("     -inputs:");
                                    foreach (XElement operationInput in operationInputs)
                                    {
                                        string inputMsgPartName = (string)operationInput.Attribute("name");
                                        string typeName = (string)operationInput.Attribute("element");
                                        SoaMetaModel.Type type = SearchType(typeName, def);
                                        OperationParameter opParam = new OperationParameter()
                                        {
                                            Name = inputName,
                                            Type = type,
                                            Operation = op
                                        };
                                        op.Parameters.Add(opParam);
                                        Wr("       -add parameter: (" + typeName + ")" + opParam.Name);
                                    }
                                }
                                catch (Exception e) { Wr("     -no inputs"); }
                                try
                                {
                                    XElement output = operation.Element(wsdlNS + "output");
                                    string outputName = (string)output.Attribute("name"); // !!! EZ ITT MINEK LEGYEN A NEVE ???
                                    string messageName = (string)output.Attribute("message");
                                    Collective messageObject = SearchObject(ObjectTypes.message, messageName, def);

                                    XElement operationOutput = messageObject.definition.Element(wsdlNS + "part");

                                    string typeName = (string)operationOutput.Attribute("element");
                                    SoaMetaModel.Type type = SearchType(typeName, def);

                                    op.ReturnType = type;

                                    Wr("     -return type: " + type.Name);
                                }
                                catch (Exception e) { Wr("     -no output"); }

                                try
                                {
                                    XElement fault = operation.Element(wsdlNS + "fault");
                                    string faultName = (string)fault.Attribute("name"); // !!! EZ ITT MINEK LEGYEN A NEVE ???
                                    string messageName = (string)fault.Attribute("message");
                                    Collective messageObject = SearchObject(ObjectTypes.message, messageName, def);

                                    var operationFaults = from c in messageObject.definition.Descendants(wsdlNS + "part")
                                                          select c;
                                    Wr("     -faults:");
                                    foreach (XElement operationFault in operationFaults)
                                    {
                                        string FaultMsgPartName = (string)operationFault.Attribute("name");
                                        string typeName = (string)operationFault.Attribute("element");
                                        SoaMetaModel.Type type = SearchType(typeName, def);

                                        op.Exceptions.Add((ExceptionType)type);

                                        Wr("       -add exception: " + type.Name);
                                    }
                                }
                                catch (Exception e) { Wr("     -no faults"); }
                                
                                // ??? op.InputType
                                // ??? op.OutputType
                            }

                        }
                        break;
                    case ObjectTypes.enumType:
                        {
                            Wr("Processing: (" + coll.objectType + ")" + coll.name);
                            EnumType et = (EnumType)coll.objRef;

                            // ??? et.UnderlyingType
                            var enumValues = from c in def.Descendants(xmlNS + "enumeration")
                                             select c;

                            foreach (XElement enumValue in enumValues)
                            {
                                string value = (string)enumValue.Attribute("value");
                                EnumValue ev = new EnumValue()
                                {
                                    Enum = et,
                                    Name = value
                                };
                                Wr("   -add enumValue: " + ev.Name);
                            }
                        }
                        break;
                    case ObjectTypes.exceptionType:
                        {
                            Wr("Processing: (" + coll.objectType + ")" + coll.name);
                            ExceptionType ex = (ExceptionType)coll.objRef;

                            try
                            {
                                string extensionBase = (string)def.Element(xmlNS + "complexContent").Element(xmlNS + "extension").Attribute("base");
                                SoaMetaModel.Type extType = SearchType(extensionBase, def);
                                ex.SuperType = (ExceptionType)extType;
                                Wr("   -set extension: " + ex.SuperType.Name);
                            }
                            catch (Exception e) { Wr("   -no extension"); }

                            var elements = from c in def.Descendants(xmlNS + "element")
                                           select c;

                            foreach (XElement element in elements)
                            {
                                string elementName = (string)element.Attribute("name");
                                string elementType = (string)element.Attribute("type");
                                SoaMetaModel.Type type = SearchType(elementType, def);

                                ExceptionField sf = new ExceptionField()
                                {
                                    Name = elementName,
                                    Exception = ex,
                                    Type = type,
                                };
                                ex.Fields.Add(sf);
                            }
                        }
                        break;
                    case ObjectTypes.structType:
                        {
                            Wr("Processing: (" + coll.objectType + ")" + coll.name);
                            StructType st = (StructType)coll.objRef;

                            try
                            {
                                string extensionBase = (string)def.Element(xmlNS + "complexContent").Element(xmlNS + "extension").Attribute("base");
                                SoaMetaModel.Type extType = SearchType(extensionBase, def);
                                st.SuperType = (StructType)extType;
                                Wr("   -set extension: " + st.SuperType.Name);
                            }
                            catch (Exception e) { Wr("   -no extension"); }

                            var elements = from c in def.Descendants(xmlNS + "element")
                                           select c;

                            foreach (XElement element in elements)
                            {
                                string elementName = (string)element.Attribute("name");
                                string elementType = (string)element.Attribute("type");
                                SoaMetaModel.Type type = SearchType(elementType, def);

                                StructField sf = new StructField()
                                {
                                    Name = elementName,
                                    Struct = st,
                                    Type = type
                                };
                                st.Fields.Add(sf);
                            }

                        }
                        break;
                    case ObjectTypes.arrayType:
                        {
                            Wr("Processing: (" + coll.objectType + ")" + coll.name);
                            ArrayType at = (ArrayType)coll.objRef;

                            try
                            {
                                string typeName = (string)def.Element(xmlNS + "sequence").Element(xmlNS + "element").Attribute("type");
                                SoaMetaModel.Type type = SearchType(typeName, def);
                                at.ItemType = type;
                                Wr("   -set array type: " + at.ItemType.Name);
                            }
                            catch (Exception e) { Wr("   -no array type"); }
                        }
                        break;
                    default: //throw new Exception();
                        break;
                }
                coll.isReady = true;
            }
        }

        private Collective SearchObject(ObjectTypes objType, string nameWithPrefix, XElement def)
        {
            // TODO: !!! MI VAN, HA NINCS PREFIX ???
            string prefix = (string)nameWithPrefix.Split(char.Parse(":")).GetValue(0);
            XNamespace objNS = def.GetNamespaceOfPrefix(prefix);

            string name = (string)nameWithPrefix.Split(char.Parse(":")).GetValue(1);

            var objs = from c in readedElements
                       where c.objectType == objType &&
                             c.name == name &&
                             c.ownNS == objNS
                       select c;

            if (objs.Count<Collective>() != 1)
            {
                Wr("Searched Object not found: " + nameWithPrefix);
                throw new Exception();
            }
            else
            {
                return objs.First<Collective>();
            }
        }

        private SoaMetaModel.Type SearchType(string nameWithPrefix, XElement def)
        {
            string prefix = (string)nameWithPrefix.Split(char.Parse(":")).GetValue(0);
            XNamespace objNS = def.GetNamespaceOfPrefix(prefix);

            string name = (string)nameWithPrefix.Split(char.Parse(":")).GetValue(1);

            if (objNS == xmlNS)
            {
                BuiltInType bit = BuiltInType.GetBuiltInType(name);
                return bit;
            }
            else
            {
                var objs = from c in readedElements
                           where c.name == name &&
                                 c.ownNS == objNS
                           select c;

                if (objs.Count<Collective>() != 1)
                {
                    Wr("Searched Type not found: " + nameWithPrefix);
                    throw new Exception();
                }
                else
                {
                    return (SoaMetaModel.Type)objs.First<Collective>().objRef;
                }
            }
        }

        private void Wr(string s)
        {
            if (loging)
            {
                if (s == null)
                {
                    Console.WriteLine("-NULL-");
                }
                else if (s.Length == 0)
                {
                    Console.WriteLine("-?-");
                }
                else
                {
                    Console.WriteLine(s);
                }
            }
        }
    }
}
