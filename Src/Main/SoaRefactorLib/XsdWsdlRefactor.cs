using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using SoaMetaModel;
using Sb.Meta;

namespace SoaMM
{
    public class XsdWsdlRefactor
    {
        private XNamespace xsdNS = "http://www.w3.org/2001/XMLSchema";
        private XNamespace wsdlNS = "http://schemas.xmlsoap.org/wsdl/";
        private XNamespace soap12NS = "http://schemas.xmlsoap.org/wsdl/soap12/";
        private XNamespace soapNS = "http://schemas.xmlsoap.org/wsdl/soap/";

        public string Path { get; set; }
        public string FileName { get; set; }
        public bool Logging { get; set; }
        public SoaModel Model { get; set; }

        private List<StoreItem> store = new List<StoreItem>();
        private Dictionary<XNamespace, Namespace> nsDictionary = new Dictionary<XNamespace, Namespace>();
        private int fileCount = 0;
        
        private class StoreItem
        {
            public string name { get; set; }
            public StoreItemType objectType { get; set; }
            public XNamespace ownNS { get; set; }
            public XElement definition { get; set; }
            public bool isReady { get; set; }
            public SoaObject objRef { get; set; }

            public StoreItem()
            {
                this.name = "";
                this.ownNS = null;
                this.definition = null;
                this.isReady = false;
                this.objRef = null;
            }
        }

        private enum StoreItemType
        {
            service,
            binding,
            portType,
            message,

            enumType,
            complexType,
            unNamedElementType,

            exceptionType,
            structType,
            arrayType
        }

        public XsdWsdlRefactor(SoaModel model, string wsdlFile){
            this.Path = System.IO.Path.GetDirectoryName(System.IO.Path.GetFullPath(wsdlFile));
            //this.FileName = "HelloNamespace.wsdl";
            //this.FileName = @"\test50\hello_world_schema_import.wsdl";
            this.FileName = System.IO.Path.GetFileName(wsdlFile);
            this.Logging = true;
            this.Model = model;
            //this.Model = new SoaModel();
        }

        public void Run()
        {
            using (new ModelContextScope<SoaModel>(Model))
            {
                Read();
                Declare();
                Linking();
            }
            Wr("Succeful");
            Console.ReadKey();
        }

        private void Read()
        {
            try
            {

                // Beolvasandó dokumentumok listája
                LinkedList<XDocument> documentList = new LinkedList<XDocument>();

                // Imprortálások bekérése
                XDocument doc = XDocument.Load(System.IO.Path.Combine(this.Path, this.FileName));

                // Rekurzív függvény az importálások kifejtésére
                CheckImports(documentList, doc, Path);

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
                        targetNS = (string)xd.Element(xsdNS + "schema").Attribute("targetNamespace");
                        defNS = xsdNS;
                    }
                    catch { }
                    try
                    {
                        targetNS = (string)xd.Element(wsdlNS + "definitions").Attribute("targetNamespace");
                        defNS = wsdlNS;
                    }
                    catch { }

                    if (!nsDictionary.ContainsKey(targetNS))
                    {
                        Namespace myNs = new Namespace()
                        {
                            Name = "namespace" + fileCount.ToString(),
                            Uri = targetNS.NamespaceName
                        };

                        nsDictionary.Add(targetNS, myNs);
                        fileCount++;
                    }

                    // services
                    var services = from c in xd.Descendants(wsdlNS + "service")
                                   select c;

                    foreach (XElement xe in services)
                    {
                        StoreItem coll = new StoreItem()
                        {
                            name = (string)xe.Attribute("name"),
                            definition = xe,
                            objectType = StoreItemType.service,
                            ownNS = targetNS
                        };

                        store.Add(coll);
                    }

                    // binding
                    var bindings = from c in xd.Descendants(wsdlNS + "binding")
                                   select c;

                    foreach (XElement xe in bindings)
                    {
                        StoreItem coll = new StoreItem()
                        {
                            name = (string)xe.Attribute("name"),
                            definition = xe,
                            objectType = StoreItemType.binding,
                            ownNS = targetNS
                        };

                        store.Add(coll);
                    }

                    // porttype
                    var portTypes = from c in xd.Descendants(wsdlNS + "portType")
                                    select c;

                    foreach (XElement xe in portTypes)
                    {
                        StoreItem coll = new StoreItem()
                        {
                            name = (string)xe.Attribute("name"),
                            definition = xe,
                            objectType = StoreItemType.portType,
                            ownNS = targetNS
                        };

                        store.Add(coll);
                    }

                    // message
                    var messages = from c in xd.Descendants(wsdlNS + "message")
                                   select c;

                    foreach (XElement xe in messages)
                    {
                        StoreItem coll = new StoreItem()
                        {
                            name = (string)xe.Attribute("name"),
                            definition = xe,
                            objectType = StoreItemType.message,
                            ownNS = targetNS
                        };

                        store.Add(coll);
                    }

                    //TODO: <type> és <schema> ellenőrzése a targetNamespace miatt!!!
                    var schemas = from c in xd.Descendants(xsdNS + "schema")
                                  select c;

                    foreach (XElement schema in schemas)
                    {
                        targetNS = (string)schema.Attribute("targetNamespace");

                        if (!nsDictionary.ContainsKey(targetNS))
                        {
                            Namespace myxsdNs = new Namespace()
                            {
                                Name = "namespace" + fileCount.ToString(),
                                Uri = targetNS.NamespaceName
                            };

                            nsDictionary.Add(targetNS, myxsdNs);
                            fileCount++;
                        }

                        // simpleTypes
                        var simpleTypes = from c in schema.Elements(xsdNS + "simpleType")
                                          select c;
                        //var simpleTypes = from c in xd.Descendants(xsdNS + "simpleType")
                        //                  select c;

                        foreach (XElement xe in simpleTypes)
                        {
                            StoreItem coll = new StoreItem()
                            {
                                name = (string)xe.Attribute("name"),
                                definition = xe,
                                objectType = StoreItemType.enumType,
                                ownNS = targetNS
                            };

                            store.Add(coll);
                        }

                        // complexTypes
                        //var complexTypes = from c in xd.Descendants(xsdNS + "complexType")
                        //                   select c;
                        var complexTypes = from c in schema.Elements(xsdNS + "complexType")
                                           select c;

                        foreach (XElement xe in complexTypes)
                        {
                            
                            StoreItem coll = new StoreItem()
                            {
                                name = (string)xe.Attribute("name"),
                                definition = xe,
                                objectType = StoreItemType.complexType,
                                ownNS = targetNS

                            };

                            store.Add(coll);
                        }

                        var elements = from c in schema.Elements(xsdNS + "element")
                                       select c;

                        foreach (XElement xe in elements)
                        {
                            if (xe.Attribute("type") != null)
                            {
                                StoreItem coll = new StoreItem()
                                {
                                    name = (string)xe.Attribute("name"),
                                    definition = xe,
                                    objectType = StoreItemType.unNamedElementType,
                                    ownNS = targetNS
                                };
                                //Wr(coll.name);
                                store.Add(coll);
                            }
                            else
                            {
                                XAttribute nameAttribute = xe.Attribute("name");
                                XElement cType = xe.Element(xsdNS + "complexType");
                                XElement modifiendXe = cType;
                                modifiendXe.Add(nameAttribute);
                                StoreItem coll = new StoreItem()
                                {
                                    name = (string)modifiendXe.Attribute("name"),
                                    definition = modifiendXe,
                                    objectType = StoreItemType.complexType,
                                    ownNS = targetNS
                                };
                                //Wr(coll.name);
                                store.Add(coll);
                            }
                        }
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
            var xsdFiles = from c in doc.Descendants(xsdNS + "import")
                        select c.Attribute("schemaLocation");

            foreach (string file in xsdFiles)
            {
                XDocument import = XDocument.Load(System.IO.Path.Combine(path, file));
                Wr("Import file: " + file);
                CheckImports(documentList, import, path);
            }

            var wsdlFiles = from c in doc.Descendants(wsdlNS + "import")
                            select c.Attribute("location");

            foreach (string file in wsdlFiles)
            {
                XDocument import = XDocument.Load(System.IO.Path.Combine(path, file));
                Wr("Import file: " + file);
                CheckImports(documentList, import, path);
            }
        }

        // Deklarálja az objektumokat
        private void Declare()
        {
            // service
            var services = from c in store
                           where c.objectType == StoreItemType.service
                           select c;

            Wr("Declare services:");
            foreach (StoreItem service in services)
            {
                Endpoint ep = new Endpoint()
                {
                    Name = (string)service.definition.Attribute("name")
                };
                service.objRef = ep;
                Wr("   " + ep.Name);
            }

            // binding
            var bindings = from c in store
                           where c.objectType == StoreItemType.binding
                           select c;

            Wr("Declare bindings:");
            foreach (StoreItem binding in bindings)
            {
                Binding bg = new Binding()
                {
                    Name = (string)binding.definition.Attribute("name")
                };
                binding.objRef = bg;
                Wr("   " + bg.Name);
            }
            // porttype
            var portTypes = from c in store
                            where c.objectType == StoreItemType.portType
                            select c;

            Wr("Declare porttypes:");
            foreach (StoreItem porttype in portTypes)
            {
                Interface ifa = new Interface()
                {
                    Name = (string)porttype.definition.Attribute("name")
                };
                porttype.objRef = ifa;
                Wr("   " + ifa.Name);
            }

            // simpleTypes
            var simpleTypes = from c in store
                              where c.objectType == StoreItemType.enumType
                              select c;

            Wr("Declare SimpleTypes:");
            foreach (StoreItem simpleType in simpleTypes)
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
            var arrayTypes = from c in store
                             where c.objectType == StoreItemType.complexType &&
                                   c.definition.Element(xsdNS + "sequence") != null &&
                                   c.definition.Element(xsdNS + "sequence").Element(xsdNS + "element") != null &&
                                   c.definition.Element(xsdNS + "sequence").Element(xsdNS + "element").Attribute("maxOccurs") != null &&
                                   (string)c.definition.Element(xsdNS + "sequence").Element(xsdNS + "element").Attribute("maxOccurs") != "1"
                             select c;

            Wr("Declare ArrayTypes:");
            foreach (StoreItem arrayType in arrayTypes)
            {
                ArrayType at = new ArrayType()
                {
                    Name = (string)arrayType.definition.Attribute("name")
                };
                arrayType.objRef = at;
                arrayType.objectType = StoreItemType.arrayType;
                Wr("   " + at.Name);
            }

            // ExceptionType
            // kiválasztjuk a portType-okat
            Wr("Declare ExceptionTypes:");
            foreach (StoreItem portType in portTypes)
            {
                XElement node = portType.definition;
                XElement def = node;
                // kiszűrjük a fault-tagek message attribútumát
                var faultsFromPorttype = from c in node.Descendants(wsdlNS + "fault")
                                         select c.Attribute("message");

                foreach (string faultMsgName in faultsFromPorttype)
                {
                    StoreItem message = SearchObject(StoreItemType.message, faultMsgName, def);
                    string referencedTypeName = null;
                    referencedTypeName = (string)message.definition.Element(wsdlNS + "part").Attribute("type");
                    def = message.definition;
                    if (referencedTypeName == null)
                    {
                        referencedTypeName = (string)message.definition.Element(wsdlNS + "part").Attribute("element");
                        StoreItem element = TrySearchObject(StoreItemType.unNamedElementType, referencedTypeName, def);

                        if (element != null)
                        {
                            referencedTypeName = (string)element.definition.Attribute("type");
                            def = element.definition;
                        }

                    }
                    StoreItem exceptionType = TrySearchObject(StoreItemType.complexType, referencedTypeName, def);
                    if (exceptionType != null)
                    {
                        ExceptionType et = new ExceptionType()
                            {
                                Name = exceptionType.name
                            };

                        exceptionType.objRef = et;
                        exceptionType.objectType = StoreItemType.exceptionType;
                        Wr("   " + et.Name);
                    }
                    else
                    {
                        Wr("Unable to make ExceptionType from " + referencedTypeName);
                        throw new  XmlException();
                    }
                }
            }

            // structType létrehozása (minden olyan complexType, ami nem arrayType, vagy ExceptionType)
            var structTypes = from c in store
                              where c.objectType == StoreItemType.complexType
                              select c;

            Wr("Declare StructTypes:");
            foreach (StoreItem structType in structTypes)
            {
                StructType at = new StructType()
                {
                    Name = (string)structType.definition.Attribute("name")
                };
                structType.objRef = at;
                structType.objectType = StoreItemType.structType;
                Wr("   " + at.Name);
            }
        }

        // Beállítja a megfelelő referenciákat
        private void Linking()
        {
            foreach (StoreItem coll in store)
            {
                XElement def = coll.definition;

                switch (coll.objectType)
                {
                    case StoreItemType.service:
                        {
                            Wr("Processing: (" + coll.objectType + ")" + coll.name);
                            Endpoint ep = (Endpoint)coll.objRef;

                            // binding beállítása:
                            string bindingName = (string)def.Element(wsdlNS + "port").Attribute("binding");
                            StoreItem bindingObject = SearchObject(StoreItemType.binding, bindingName, def);
                            ep.Binding = (Binding)bindingObject.objRef;
                            Wr("   -add binding: " + ep.Binding.Name);

                            // interface beállítása
                            string interfaceName = (string)bindingObject.definition.Attribute("type");
                            StoreItem interfaceObject = SearchObject(StoreItemType.portType, interfaceName, bindingObject.definition);
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
                            catch (Exception) { }
                            EndpointAddress epa = new EndpointAddress(location);
                            ep.Address = epa;
                            Wr("   -add address: " + ep.Address.Uri);
                            ep.Namespace = nsDictionary[coll.ownNS];
                        }
                        break;
                    case StoreItemType.binding:
                        {
                            Wr("Processing: (" + coll.objectType + ")" + coll.name);
                            Binding bi = (Binding)coll.objRef;
                            
                            // interface beállítása
                            //string interfaceName = (string)def.Attribute("type");
                            //StoreItem interfaceObject = SearchObject(StoreItemType.portType, interfaceName, def);
                            //bi.Interface = (Interface)interfaceObject.objRef;
                            //Wr("   -add interface: " + interfaceName);

                            string transport = "";
                            SoapEncodingBindingElement sebe = new SoapEncodingBindingElement();
                            try
                            {
                                transport = (string)def.Element(soapNS + "binding").Attribute("transport");
                                sebe.Version = SoapVersion.Soap11;
                            }
                            catch (Exception) { }

                            try
                            {
                                transport = (string)def.Element(soap12NS + "binding").Attribute("transport");
                                sebe.Version = SoapVersion.Soap12;
                            }
                            catch (Exception) { }
                            HttpTransportBindingElement htbe = new HttpTransportBindingElement();
                          
                            bi.Encoding = sebe;
                            
                            Wr("   -set SOAP version: " + sebe.Version.ToString());
                            bi.Transport = htbe;
                            bi.Namespace = nsDictionary[coll.ownNS];
                            
                        }
                        break;
                    case StoreItemType.portType:
                        {
                            Wr("Processing: (" + coll.objectType + ")" + coll.name);
                            Interface ifa = (Interface)coll.objRef;

                            var operations = from c in def.Descendants(wsdlNS + "operation")
                                             select c;

                            foreach (XElement operation in operations)
                            {

                                string operationName = (string)operation.Attribute("name");
                                Operation op = new Operation()
                                {
                                    Name = operationName
                                };
                                Wr("   -add operation: " + op.Name);

                                XElement input = operation.Element(wsdlNS + "input");
                                if (input == null)
                                {
                                    Wr("     -no inputs");
                                }
                                else
                                {


                                    string inputName = (string)input.Attribute("name");
                                    string messageName = (string)input.Attribute("message");
                                    StoreItem messageObject = SearchObject(StoreItemType.message, messageName, def);

                                    var operationInputs = from c in messageObject.definition.Descendants(wsdlNS + "part")
                                                          select c;
                                    Wr("     -inputs:");

                                    foreach (XElement operationInput in operationInputs)
                                    {
                                        string inputMsgPartName = (string)operationInput.Attribute("name");
                                        string typeName = (string)operationInput.Attribute("type");
                                        string elementName = (string)operationInput.Attribute("element");

                                        if (typeName != null)
                                        {
                                            SoaMetaModel.Type type = SearchType(typeName, def);
                                            OperationParameter opParam = new OperationParameter()
                                            {
                                                Name = inputMsgPartName,
                                                Type = type,
                                                Operation = op
                                            };
                                            op.Parameters.Add(opParam);
                                            Wr("       -add parameter: (" + type.Name + ")" + opParam.Name);
                                        }
                                        else if (elementName != null)
                                        {
                                            StoreItem element = TrySearchObject(StoreItemType.unNamedElementType, elementName, def);
                                            if (operationInputs.Count<XElement>() == 1 &&
                                                elementName.Split(char.Parse(":")).GetValue(1).Equals(operationName))
                                            {
                                                // Ekkor a formázási stílus Document/wrapped
                                                StoreItem concrateType;
                                                if (element == null)
                                                {
                                                    concrateType = TrySearchObject(StoreItemType.structType, elementName, def);
                                                    if (concrateType == null)
                                                    {
                                                        concrateType = SearchObject(StoreItemType.arrayType, elementName, def);
                                                    }
                                                    
                                                    var elementsInReferencedType = from c in concrateType.definition.Descendants(xsdNS + "element")
                                                                                   select c;
                                                    foreach (XElement elementInReferencedType in elementsInReferencedType)
                                                    {
                                                        string elementInReferencedTypeName = (string)elementInReferencedType.Attribute("name");
                                                        string elementInReferencedTypeType = (string)elementInReferencedType.Attribute("type");

                                                        SoaMetaModel.Type type = SearchType(elementInReferencedTypeType, elementInReferencedType);
                                                        OperationParameter opParam = new OperationParameter()
                                                        {
                                                            Name = elementInReferencedTypeName,
                                                            Type = type,
                                                            Operation = op
                                                        };
                                                        op.Parameters.Add(opParam);
                                                        Wr("       -add parameter: (" + type.Name + ")" + opParam.Name);

                                                    }
                                                }
                                                else
                                                {
                                                    string referencedTypeName = (string)element.definition.Attribute("type");
                                                    
                                                    SoaMetaModel.Type type = SearchType(referencedTypeName, element.definition);
                                                    OperationParameter opParam = new OperationParameter()
                                                    {
                                                        Name = inputMsgPartName,
                                                        Type = type,
                                                        Operation = op
                                                    };
                                                    op.Parameters.Add(opParam);
                                                    Wr("       -add parameter: (" + type.Name + ")" + opParam.Name);
                                                }

                                                
                                            }
                                            else
                                            {
                                                SoaMetaModel.Type type;
                                                if (element == null)
                                                {
                                                    type = SearchType(elementName, def);
                                                }
                                                else
                                                {
                                                    string referencedTypeName = (string)element.definition.Attribute("type");
                                                    type = SearchType(referencedTypeName, element.definition);
                                                }

                                                OperationParameter opParam = new OperationParameter()
                                                {
                                                    Name = inputMsgPartName,
                                                    Type = type,
                                                    Operation = op
                                                };
                                                op.Parameters.Add(opParam);
                                                Wr("       -add parameter: (" + type.Name + ")" + opParam.Name);
                                            }
                                        }
                                        else
                                        {
                                            // TODO
                                        }
                                    }
                                }


                                XElement output = operation.Element(wsdlNS + "output");
                                if (output == null)
                                {
                                    Wr("     -no output");
                                }
                                else
                                {
                                    string outputName = (string)output.Attribute("name");
                                    string messageName = (string)output.Attribute("message");
                                    StoreItem messageObject = SearchObject(StoreItemType.message, messageName, def);

                                    XElement operationOutput = messageObject.definition.Element(wsdlNS + "part");

                                    string typeName = (string)operationOutput.Attribute("type");
                                    string elementName = (string)operationOutput.Attribute("element");

                                    SoaMetaModel.Type type = null;

                                    if (typeName != null)
                                    {
                                        type = SearchType(typeName, def);
                                    }
                                    else if (elementName != null)
                                    {
                                        StoreItem element = TrySearchObject(StoreItemType.unNamedElementType, elementName, def);
                                        if (element == null)
                                        {
                                            type = SearchType(elementName, def);
                                        }
                                        else
                                        {
                                            string referencedTypeName = (string)element.definition.Attribute("type");
                                            type = SearchType(referencedTypeName, element.definition);
                                        }
                                    }
                                    else
                                    {
                                        //TODO
                                    }
                                    op.ReturnType = type;

                                    Wr("     -return type: " + type.Name);
                                }

                                var faults = from c in operation.Descendants(wsdlNS + "fault")
                                             select c;

                                if (faults.Count<XElement>() == 0)
                                {
                                    Wr("     -no faults");
                                }
                                else
                                {
                                    Wr("     -faults:");

                                    foreach (XElement fault in faults)
                                    {
                                        string faultName = (string)fault.Attribute("name");
                                        string messageName = (string)fault.Attribute("message");
                                        StoreItem messageObject = SearchObject(StoreItemType.message, messageName, def);

                                        var operationFaults = from c in messageObject.definition.Descendants(wsdlNS + "part")
                                                              select c;

                                        foreach (XElement operationFault in operationFaults)
                                        {

                                            string FaultMsgPartName = (string)operationFault.Attribute("name");
                                            string typeName = (string)operationFault.Attribute("type");
                                            string elementName = (string)operationFault.Attribute("element");

                                            SoaMetaModel.Type type = null;

                                            if (typeName != null)
                                            {
                                                type = SearchType(typeName, def);
                                            }
                                            else if (elementName != null)
                                            {
                                                StoreItem element = TrySearchObject(StoreItemType.unNamedElementType, elementName, def);
                                                if (element == null)
                                                {
                                                    type = SearchType(elementName, def);
                                                }
                                                else
                                                {
                                                    string referencedTypeName = (string)element.definition.Attribute("type");
                                                    type = SearchType(referencedTypeName, element.definition);
                                                }
                                            }
                                            else
                                            {
                                                //TODO
                                            }

                                            op.Exceptions.Add((ExceptionType)type);

                                            Wr("       -add exception: " + type.Name);
                                        }
                                    }
                                }
                                ifa.Operations.Add(op);
                            }
                            ifa.Namespace = nsDictionary[coll.ownNS];
                        }
                        break;
                    case StoreItemType.enumType:
                        {
                            Wr("Processing: (" + coll.objectType + ")" + coll.name);
                            EnumType et = (EnumType)coll.objRef;

                            var enumValues = from c in def.Descendants(xsdNS + "enumeration")
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
                            et.Namespace = nsDictionary[coll.ownNS];
                        }
                        break;
                    case StoreItemType.exceptionType:
                        {
                            Wr("Processing: (" + coll.objectType + ")" + coll.name);
                            ExceptionType ex = (ExceptionType)coll.objRef;

                            try
                            {
                                string extensionBase = (string)def.Element(xsdNS + "complexContent").Element(xsdNS + "extension").Attribute("base");
                                SoaMetaModel.Type extType = SearchType(extensionBase, def);
                                ex.SuperType = (ExceptionType)extType;
                                Wr("   -set extension: " + ex.SuperType.Name);
                            }
                            catch (Exception) { Wr("   -no extension"); }

                            var elements = from c in def.Descendants(xsdNS + "element")
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
                            ex.Namespace = nsDictionary[coll.ownNS];
                        }
                        break;
                    case StoreItemType.structType:
                        {
                            Wr("Processing: (" + coll.objectType + ")" + coll.name);
                            StructType st = (StructType)coll.objRef;

                            try
                            {
                                string extensionBase = (string)def.Element(xsdNS + "complexContent").Element(xsdNS + "extension").Attribute("base");
                                SoaMetaModel.Type extType = SearchType(extensionBase, def);
                                st.SuperType = (StructType)extType;
                                Wr("   -set extension: " + st.SuperType.Name);
                            }
                            catch (Exception) { Wr("   -no extension"); }

                            var elements = from c in def.Descendants(xsdNS + "element")
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
                            st.Namespace = nsDictionary[coll.ownNS];

                        }
                        break;
                    case StoreItemType.arrayType:
                        {
                            Wr("Processing: (" + coll.objectType + ")" + coll.name);
                            ArrayType at = (ArrayType)coll.objRef;

                            try
                            {
                                string typeName = (string)def.Element(xsdNS + "sequence").Element(xsdNS + "element").Attribute("type");
                                SoaMetaModel.Type type = SearchType(typeName, def);
                                at.ItemType = type;
                                Wr("   -set array type: " + at.ItemType.Name);
                            }
                            catch (Exception) { Wr("   -no array type"); }
                            at.Namespace = nsDictionary[coll.ownNS];
                        }
                        break;
                    default: //throw new Exception();
                        break;
                }
                coll.isReady = true;
            }
        }

        private StoreItem SearchObject(StoreItemType objType, string nameWithPrefix, XElement def)
        {
            StoreItem result = TrySearchObject(objType,nameWithPrefix,def);
            if (result == null)
            {
                Wr("Searched Object not found: " + nameWithPrefix);
                throw new Exception();
            }
            return result;
        }
        private StoreItem TrySearchObject(StoreItemType objType, string nameWithPrefix, XElement def)
        {
            if (nameWithPrefix == null) return null;
            if (!nameWithPrefix.Contains(":")) return null;
            string prefix = (string)nameWithPrefix.Split(char.Parse(":")).GetValue(0);
            XNamespace objNS = def.GetNamespaceOfPrefix(prefix);

            string name = (string)nameWithPrefix.Split(char.Parse(":")).GetValue(1);

            var objs = from c in store
                       where c.objectType == objType &&
                             c.name == name &&
                             c.ownNS == objNS
                       select c;

            if (objs.Count<StoreItem>() != 1)
            {
                return null;
            }
            else
            {
                return objs.First<StoreItem>();
            }
        }
        
        private SoaMetaModel.Type SearchType(string nameWithPrefix, XElement def)
        {
            SoaMetaModel.Type result = TrySearchType(nameWithPrefix, def);
            if (result == null)
            {
                Wr("Searched Type not found: " + nameWithPrefix);
                //throw new Exception();
            }
            return result;
        }
        
        private SoaMetaModel.Type TrySearchType(string nameWithPrefix, XElement def)
        {
            if (nameWithPrefix == null) return null;
            if (!nameWithPrefix.Contains(":")) return null;
            string prefix = (string)nameWithPrefix.Split(char.Parse(":")).GetValue(0);
            XNamespace objNS = def.GetNamespaceOfPrefix(prefix);

            string name = (string)nameWithPrefix.Split(char.Parse(":")).GetValue(1);
            if (name.Equals("dateTime")) name = "DateTime";
            if (name.Equals("boolean")) name = "bool";

            if (objNS == xsdNS)
            {
                BuiltInType bit = BuiltInType.GetBuiltInType(name);
                return bit;
            }
            else
            {
                var objs = from c in store
                           where c.name == name &&
                                 c.ownNS == objNS && (
                                 c.objectType == StoreItemType.arrayType ||
                                 c.objectType == StoreItemType.enumType ||
                                 c.objectType == StoreItemType.exceptionType ||
                                 c.objectType == StoreItemType.structType)
                           select c;

                if (objs.Count<StoreItem>() != 1)
                {
                    return null;
                }
                else
                {
                    return (SoaMetaModel.Type)objs.First<StoreItem>().objRef;
                }
            }
        }
        private void Wr(string s)
        {
            if (Logging)
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
