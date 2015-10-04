using OsloExtensions;
using OsloExtensions.Extensions;
using SoaMetaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoaMetaModel
{
    // The main file of the generator.
    public partial class XsdWsdlGenerator : Generator<IEnumerable<SoaObject>, GeneratorContext>
    {
        
        public XsdWsdlGenerator(IEnumerable<SoaObject> instances, GeneratorContext context)
            : base(instances, context)
        {
            this.Properties = new PropertyGroup_Properties();
        }
        
            #region functions from "k:\VersionControl\soal-oslo\Src\Main\SoaGeneratorLib\XsdWsdlGenerator.mcg"
            public PropertyGroup_Properties Properties { get; private set; }
            
            public class PropertyGroup_Properties
            {
                public PropertyGroup_Properties()
                {
                    this.OutputDir = "../../Output/common";
                    this.GenerateSingleWsdl = false;
                    this.GenerateSeparateXsdWsdlFolder = true;
                    this.GeneratePolicies = true;
                    this.GenerateMetroJksService = false;
                    this.GenerateMetroJksClient = false;
                    this.GenerateServiceUrl = false;
                    this.ServiceUrlPattern = "http://localhost/{0}";
                    this.Ibm = false;
                    this.XPathSignEncrypt = false;
                    this.WsPolicyNamespace = "http://www.w3.org/ns/ws-policy";
                    this.WsSecurityPolicyNamespace = "http://docs.oasis-open.org/ws-sx/ws-securitypolicy/200702";
                }
                
                public string OutputDir { get; set; }
                public bool GenerateSingleWsdl { get; set; }
                public bool GenerateSeparateXsdWsdlFolder { get; set; }
                public bool GeneratePolicies { get; set; }
                public bool GenerateMetroJksService { get; set; }
                public bool GenerateMetroJksClient { get; set; }
                public bool GenerateServiceUrl { get; set; }
                public string ServiceUrlPattern { get; set; }
                public bool Ibm { get; set; }
                public bool XPathSignEncrypt { get; set; }
                public string WsPolicyNamespace { get; set; }
                public string WsSecurityPolicyNamespace { get; set; }
            }
            
            public override void Generated_Main()
            {
                Context.CreateFolder(Properties.OutputDir);
                Context.SetOutputFolder(Properties.OutputDir);
                int __loop1_iteration = 0;
                var __loop1_result =
                    (from __loop1_tmp_item___noname1 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                    from __loop1_tmp_item_ns in EnumerableExtensions.Enumerate((__loop1_tmp_item___noname1).GetEnumerator()).OfType<Namespace>()
                    select
                        new
                        {
                            __loop1_item___noname1 = __loop1_tmp_item___noname1,
                            __loop1_item_ns = __loop1_tmp_item_ns,
                        }).ToArray();
                foreach (var __loop1_item in __loop1_result)
                {
                    var __noname1 = __loop1_item.__loop1_item___noname1;
                    var ns = __loop1_item.__loop1_item_ns;
                    ++__loop1_iteration;
                    Generated_GenerateXsdWsdl(ns);
                }
            }
            
            public void Generated_GenerateXsdWsdl(Namespace ns)
            {
                if (Properties.GenerateSeparateXsdWsdlFolder)
                {
                    Context.CreateFolder("schema");
                    Context.CreateFolder("wsdl");
                }
                int i = 0;
                int __loop2_iteration = 0;
                var __loop2_result =
                    (from __loop2_tmp_item___noname2 in EnumerableExtensions.Enumerate((ns.Declarations).GetEnumerator())
                    from __loop2_tmp_item_d in EnumerableExtensions.Enumerate((__loop2_tmp_item___noname2).GetEnumerator()).OfType<Namespace>()
                    select
                        new
                        {
                            __loop2_item___noname2 = __loop2_tmp_item___noname2,
                            __loop2_item_d = __loop2_tmp_item_d,
                        }).ToArray();
                foreach (var __loop2_item in __loop2_result)
                {
                    var __noname2 = __loop2_item.__loop2_item___noname2;
                    var d = __loop2_item.__loop2_item_d;
                    ++__loop2_iteration;
                    i = i + 1;
                }
                if (ns.Declarations.Count > i)
                {
                    if (Properties.GenerateSeparateXsdWsdlFolder)
                    {
                        Context.SetOutput("schema/" + ns.FullName + ".xsd");
                    }
                    else
                    {
                        Context.SetOutput(ns.FullName + ".xsd");
                    }
                    Context.Output(Generated_GenerateXsd(ns));
                    if (Properties.GenerateSingleWsdl)
                    {
                        if (Properties.GenerateSeparateXsdWsdlFolder)
                        {
                            Context.SetOutput("wsdl/" + ns.FullName + ".wsdl");
                        }
                        else
                        {
                            Context.SetOutput(ns.FullName + ".wsdl");
                        }
                        Context.Output(Generated_GenerateSingleWsdl(ns));
                    }
                    else
                    {
                        if (Properties.GenerateSeparateXsdWsdlFolder)
                        {
                            Context.SetOutput("wsdl/" + ns.FullName + ".wsdl");
                        }
                        else
                        {
                            Context.SetOutput(ns.FullName + ".wsdl");
                        }
                        Context.Output(Generated_GenerateWsdlAbstract(ns));
                        if (Properties.GenerateSeparateXsdWsdlFolder)
                        {
                            Context.SetOutput("wsdl/" + ns.FullName + "Binding.wsdl");
                        }
                        else
                        {
                            Context.SetOutput(ns.FullName + "Binding.wsdl");
                        }
                        Context.Output(Generated_GenerateWsdlBinding(ns));
                        if (Properties.GenerateSeparateXsdWsdlFolder)
                        {
                            Context.SetOutput("wsdl/" + ns.FullName + "Endpoint.wsdl");
                        }
                        else
                        {
                            Context.SetOutput(ns.FullName + "Endpoint.wsdl");
                        }
                        Context.Output(Generated_GenerateWsdlEndpoint(ns));
                    }
                }
            }
            
            public List<string> Generated_GenerateXsd(Namespace ns)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<xs:schema targetNamespace=\"");
                    __printer.Write(Generated_GetUri(ns));
                    __printer.WriteTemplateOutput("\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		xmlns:xs=\"http://www.w3.org/2001/XMLSchema\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		xmlns:tns=\"");
                    __printer.Write(Generated_GetUri(ns));
                    __printer.WriteTemplateOutput("\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		xmlns:");
                    __printer.Write(ns.Prefix);
                    __printer.WriteTemplateOutput("=\"");
                    __printer.Write(Generated_GetUri(ns));
                    __printer.WriteTemplateOutput("\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		elementFormDefault=\"qualified\">");
                    __printer.WriteLine();
                    int __loop3_iteration = 0;
                    var __loop3_result =
                        (from __loop3_tmp_item___noname3 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                        from __loop3_tmp_item_type in EnumerableExtensions.Enumerate((__loop3_tmp_item___noname3).GetEnumerator()).OfType<ArrayType>()
                        select
                            new
                            {
                                __loop3_item___noname3 = __loop3_tmp_item___noname3,
                                __loop3_item_type = __loop3_tmp_item_type,
                            }).ToArray();
                    foreach (var __loop3_item in __loop3_result)
                    {
                        var __noname3 = __loop3_item.__loop3_item___noname3;
                        var type = __loop3_item.__loop3_item_type;
                        ++__loop3_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        if (type.ItemType is NullableType)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	<xs:element name=\"ArrayOfNullable");
                            __printer.Write(type.ItemType.Name);
                            __printer.WriteTemplateOutput("\" nillable=\"true\" type=\"");
                            __printer.Write(ns.Prefix);
                            __printer.WriteTemplateOutput(":ArrayOfNullable");
                            __printer.Write(type.ItemType.Name);
                            __printer.WriteTemplateOutput("\"/>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	<xs:complexType name=\"ArrayOfNullable");
                            __printer.Write(type.ItemType.Name);
                            __printer.WriteTemplateOutput("\">");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		<xs:sequence>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("			");
                            __printer.Write(Generated_GenerateElement(type.ItemType, type.ItemType.Name, ns, true, true));
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		</xs:sequence>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	</xs:complexType>");
                            __printer.WriteLine();
                        }
                        else if (type.ItemType != BuiltInType.Byte)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	<xs:element name=\"ArrayOf");
                            __printer.Write(type.ItemType.Name);
                            __printer.WriteTemplateOutput("\" nillable=\"true\" type=\"");
                            __printer.Write(ns.Prefix);
                            __printer.WriteTemplateOutput(":ArrayOf");
                            __printer.Write(type.ItemType.Name);
                            __printer.WriteTemplateOutput("\"/>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	<xs:complexType name=\"ArrayOf");
                            __printer.Write(type.ItemType.Name);
                            __printer.WriteTemplateOutput("\">");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		<xs:sequence>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("			");
                            __printer.Write(Generated_GenerateElement(type.ItemType, type.ItemType.Name, ns, true, false));
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		</xs:sequence>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	</xs:complexType>");
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    int __loop4_iteration = 0;
                    var __loop4_result =
                        (from __loop4_tmp_item___noname4 in EnumerableExtensions.Enumerate((ns.Declarations).GetEnumerator())
                        from __loop4_tmp_item_type in EnumerableExtensions.Enumerate((__loop4_tmp_item___noname4).GetEnumerator()).OfType<StructType>()
                        select
                            new
                            {
                                __loop4_item___noname4 = __loop4_tmp_item___noname4,
                                __loop4_item_type = __loop4_tmp_item_type,
                            }).ToArray();
                    foreach (var __loop4_item in __loop4_result)
                    {
                        var __noname4 = __loop4_item.__loop4_item___noname4;
                        var type = __loop4_item.__loop4_item_type;
                        ++__loop4_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	<xs:element name=\"");
                        __printer.Write(type.Name);
                        __printer.WriteTemplateOutput("\" nillable=\"true\" type=\"");
                        __printer.Write(ns.Prefix);
                        __printer.WriteTemplateOutput(":");
                        __printer.Write(type.Name);
                        __printer.WriteTemplateOutput("\"/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	<xs:complexType name=\"");
                        __printer.Write(type.Name);
                        __printer.WriteTemplateOutput("\">");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		");
                        if (type.SuperType != null)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		<xs:complexContent>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("			<xs:extension base=\"");
                            __printer.Write(type.SuperType.Namespace.Prefix);
                            __printer.WriteTemplateOutput(":");
                            __printer.Write(type.SuperType.Name);
                            __printer.WriteTemplateOutput("\">");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("				");
                            if (type.Fields.Count == 0)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("				<xs:sequence />");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("				");
                            }
                            else
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("				<xs:sequence>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("					");
                                int __loop5_iteration = 0;
                                var __loop5_result =
                                    (from __loop5_tmp_item___noname5 in EnumerableExtensions.Enumerate((type.Fields).GetEnumerator())
                                    from __loop5_tmp_item_param in EnumerableExtensions.Enumerate((__loop5_tmp_item___noname5).GetEnumerator()).OfType<StructField>()
                                    select
                                        new
                                        {
                                            __loop5_item___noname5 = __loop5_tmp_item___noname5,
                                            __loop5_item_param = __loop5_tmp_item_param,
                                        }).ToArray();
                                foreach (var __loop5_item in __loop5_result)
                                {
                                    var __noname5 = __loop5_item.__loop5_item___noname5;
                                    var param = __loop5_item.__loop5_item_param;
                                    ++__loop5_iteration;
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("					");
                                    __printer.Write(Generated_GenerateElement(param.Type, param.Name, ns, false, false));
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("					");
                                }
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("				</xs:sequence>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("				");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("			</xs:extension>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		</xs:complexContent>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		");
                        }
                        else
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		");
                            if (type.Fields.Count == 0)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		<xs:sequence />");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		");
                            }
                            else
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		<xs:sequence>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("			");
                                int __loop6_iteration = 0;
                                var __loop6_result =
                                    (from __loop6_tmp_item___noname6 in EnumerableExtensions.Enumerate((type.Fields).GetEnumerator())
                                    from __loop6_tmp_item_param in EnumerableExtensions.Enumerate((__loop6_tmp_item___noname6).GetEnumerator()).OfType<StructField>()
                                    select
                                        new
                                        {
                                            __loop6_item___noname6 = __loop6_tmp_item___noname6,
                                            __loop6_item_param = __loop6_tmp_item_param,
                                        }).ToArray();
                                foreach (var __loop6_item in __loop6_result)
                                {
                                    var __noname6 = __loop6_item.__loop6_item___noname6;
                                    var param = __loop6_item.__loop6_item_param;
                                    ++__loop6_iteration;
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("			");
                                    __printer.Write(Generated_GenerateElement(param.Type, param.Name, ns, false, false));
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("			");
                                }
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		</xs:sequence>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	</xs:complexType>");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    int __loop7_iteration = 0;
                    var __loop7_result =
                        (from __loop7_tmp_item___noname7 in EnumerableExtensions.Enumerate((ns.Declarations).GetEnumerator())
                        from __loop7_tmp_item_type in EnumerableExtensions.Enumerate((__loop7_tmp_item___noname7).GetEnumerator()).OfType<ExceptionType>()
                        select
                            new
                            {
                                __loop7_item___noname7 = __loop7_tmp_item___noname7,
                                __loop7_item_type = __loop7_tmp_item_type,
                            }).ToArray();
                    foreach (var __loop7_item in __loop7_result)
                    {
                        var __noname7 = __loop7_item.__loop7_item___noname7;
                        var type = __loop7_item.__loop7_item_type;
                        ++__loop7_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	<xs:element name=\"");
                        __printer.Write(type.Name);
                        __printer.WriteTemplateOutput("\" nillable=\"true\" type=\"");
                        __printer.Write(ns.Prefix);
                        __printer.WriteTemplateOutput(":");
                        __printer.Write(type.Name);
                        __printer.WriteTemplateOutput("\"/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	<xs:complexType name=\"");
                        __printer.Write(type.Name);
                        __printer.WriteTemplateOutput("\">");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		");
                        if (type.SuperType != null)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		<xs:extension base=\"");
                            __printer.Write(type.SuperType.Name);
                            __printer.WriteTemplateOutput("\">");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("			");
                            if (type.Fields.Count == 0)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("			<xs:sequence />");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("			");
                            }
                            else
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("			<xs:sequence>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("				");
                                int __loop8_iteration = 0;
                                var __loop8_result =
                                    (from __loop8_tmp_item___noname8 in EnumerableExtensions.Enumerate((type.Fields).GetEnumerator())
                                    from __loop8_tmp_item_param in EnumerableExtensions.Enumerate((__loop8_tmp_item___noname8).GetEnumerator()).OfType<ExceptionField>()
                                    select
                                        new
                                        {
                                            __loop8_item___noname8 = __loop8_tmp_item___noname8,
                                            __loop8_item_param = __loop8_tmp_item_param,
                                        }).ToArray();
                                foreach (var __loop8_item in __loop8_result)
                                {
                                    var __noname8 = __loop8_item.__loop8_item___noname8;
                                    var param = __loop8_item.__loop8_item_param;
                                    ++__loop8_iteration;
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("				");
                                    __printer.Write(Generated_GenerateElement(param.Type, param.Name, ns, false, false));
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("				");
                                }
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("			</xs:sequence>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("			");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		</xs:extension>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		");
                        }
                        else
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		");
                            if (type.Fields.Count == 0)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		<xs:sequence />");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		");
                            }
                            else
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		<xs:sequence>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("			");
                                int __loop9_iteration = 0;
                                var __loop9_result =
                                    (from __loop9_tmp_item___noname9 in EnumerableExtensions.Enumerate((type.Fields).GetEnumerator())
                                    from __loop9_tmp_item_param in EnumerableExtensions.Enumerate((__loop9_tmp_item___noname9).GetEnumerator()).OfType<ExceptionField>()
                                    select
                                        new
                                        {
                                            __loop9_item___noname9 = __loop9_tmp_item___noname9,
                                            __loop9_item_param = __loop9_tmp_item_param,
                                        }).ToArray();
                                foreach (var __loop9_item in __loop9_result)
                                {
                                    var __noname9 = __loop9_item.__loop9_item___noname9;
                                    var param = __loop9_item.__loop9_item_param;
                                    ++__loop9_iteration;
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("			");
                                    __printer.Write(Generated_GenerateElement(param.Type, param.Name, ns, false, false));
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("			");
                                }
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		</xs:sequence>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	</xs:complexType>");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    int __loop10_iteration = 0;
                    var __loop10_result =
                        (from __loop10_tmp_item___noname10 in EnumerableExtensions.Enumerate((ns.Declarations).GetEnumerator())
                        from __loop10_tmp_item_type in EnumerableExtensions.Enumerate((__loop10_tmp_item___noname10).GetEnumerator()).OfType<EnumType>()
                        select
                            new
                            {
                                __loop10_item___noname10 = __loop10_tmp_item___noname10,
                                __loop10_item_type = __loop10_tmp_item_type,
                            }).ToArray();
                    foreach (var __loop10_item in __loop10_result)
                    {
                        var __noname10 = __loop10_item.__loop10_item___noname10;
                        var type = __loop10_item.__loop10_item_type;
                        ++__loop10_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	<xs:element name=\"");
                        __printer.Write(type.Name);
                        __printer.WriteTemplateOutput("\" nillable=\"true\" type=\"");
                        __printer.Write(ns.Prefix);
                        __printer.WriteTemplateOutput(":");
                        __printer.Write(type.Name);
                        __printer.WriteTemplateOutput("\"/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	<xs:simpleType name=\"");
                        __printer.Write(type.Name);
                        __printer.WriteTemplateOutput("\">");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		<xs:restriction base=\"xs:string\">");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("			");
                        int __loop11_iteration = 0;
                        var __loop11_result =
                            (from __loop11_tmp_item___noname11 in EnumerableExtensions.Enumerate((type.Values).GetEnumerator())
                            from __loop11_tmp_item_value in EnumerableExtensions.Enumerate((__loop11_tmp_item___noname11).GetEnumerator()).OfType<EnumValue>()
                            select
                                new
                                {
                                    __loop11_item___noname11 = __loop11_tmp_item___noname11,
                                    __loop11_item_value = __loop11_tmp_item_value,
                                }).ToArray();
                        foreach (var __loop11_item in __loop11_result)
                        {
                            var __noname11 = __loop11_item.__loop11_item___noname11;
                            var value = __loop11_item.__loop11_item_value;
                            ++__loop11_iteration;
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("			<xs:enumeration value=\"");
                            __printer.Write(value.Name);
                            __printer.WriteTemplateOutput("\"/>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("			");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		</xs:restriction>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	</xs:simpleType>");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    int __loop12_iteration = 0;
                    var __loop12_result =
                        (from __loop12_tmp_item___noname12 in EnumerableExtensions.Enumerate((ns.Declarations).GetEnumerator())
                        from __loop12_tmp_item_intf in EnumerableExtensions.Enumerate((__loop12_tmp_item___noname12).GetEnumerator()).OfType<Interface>()
                        select
                            new
                            {
                                __loop12_item___noname12 = __loop12_tmp_item___noname12,
                                __loop12_item_intf = __loop12_tmp_item_intf,
                            }).ToArray();
                    foreach (var __loop12_item in __loop12_result)
                    {
                        var __noname12 = __loop12_item.__loop12_item___noname12;
                        var intf = __loop12_item.__loop12_item_intf;
                        ++__loop12_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                        int __loop13_iteration = 0;
                        var __loop13_result =
                            (from __loop13_tmp_item___noname13 in EnumerableExtensions.Enumerate((intf.Operations).GetEnumerator())
                            from __loop13_tmp_item_op in EnumerableExtensions.Enumerate((__loop13_tmp_item___noname13).GetEnumerator()).OfType<Operation>()
                            select
                                new
                                {
                                    __loop13_item___noname13 = __loop13_tmp_item___noname13,
                                    __loop13_item_op = __loop13_tmp_item_op,
                                }).ToArray();
                        foreach (var __loop13_item in __loop13_result)
                        {
                            var __noname13 = __loop13_item.__loop13_item___noname13;
                            var op = __loop13_item.__loop13_item_op;
                            ++__loop13_iteration;
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	^");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	<xs:element name=\"");
                            __printer.Write(op.Name);
                            __printer.WriteTemplateOutput("\" nillable=\"true\" type=\"");
                            __printer.Write(ns.Prefix);
                            __printer.WriteTemplateOutput(":");
                            __printer.Write(op.Name);
                            __printer.WriteTemplateOutput("\"/>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	<xs:complexType name=\"");
                            __printer.Write(op.Name);
                            __printer.WriteTemplateOutput("\">");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                            if (op.Parameters.Count == 0)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		<xs:sequence />");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("	");
                            }
                            else
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		<xs:sequence>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		");
                                int __loop14_iteration = 0;
                                var __loop14_result =
                                    (from __loop14_tmp_item___noname14 in EnumerableExtensions.Enumerate((op.Parameters).GetEnumerator())
                                    from __loop14_tmp_item_param in EnumerableExtensions.Enumerate((__loop14_tmp_item___noname14).GetEnumerator()).OfType<OperationParameter>()
                                    select
                                        new
                                        {
                                            __loop14_item___noname14 = __loop14_tmp_item___noname14,
                                            __loop14_item_param = __loop14_tmp_item_param,
                                        }).ToArray();
                                foreach (var __loop14_item in __loop14_result)
                                {
                                    var __noname14 = __loop14_item.__loop14_item___noname14;
                                    var param = __loop14_item.__loop14_item_param;
                                    ++__loop14_iteration;
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("			");
                                    __printer.Write(Generated_GenerateElement(param.Type, param.Name, ns, false, false));
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("		");
                                }
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		</xs:sequence>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("	");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	</xs:complexType>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                            if (op.ReturnType != PseudoType.Async)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("	^");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("	<xs:element name=\"");
                                __printer.Write(op.Name);
                                __printer.WriteTemplateOutput("Response\" nillable=\"true\" type=\"");
                                __printer.Write(ns.Prefix);
                                __printer.WriteTemplateOutput(":");
                                __printer.Write(op.Name);
                                __printer.WriteTemplateOutput("Response\"/>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("	<xs:complexType name=\"");
                                __printer.Write(op.Name);
                                __printer.WriteTemplateOutput("Response\">");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("	");
                                if (op.ReturnType == PseudoType.Void)
                                {
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("		<xs:sequence />");
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("	");
                                }
                                else
                                {
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("		<xs:sequence>");
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("			");
                                    __printer.Write(Generated_GenerateElement(op.ReturnType, op.Name + "Result", ns, false, false));
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("		</xs:sequence>");
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("	");
                                }
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("	</xs:complexType>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("	");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</xs:schema>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateElementPostfix(bool arrayElement, bool isNullable)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    if (isNullable && !arrayElement)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("nillable=\"true\"");
                        __printer.WriteLine();
                    }
                    else if (arrayElement && !isNullable)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("minOccurs=\"0\" maxOccurs=\"unbounded\"");
                        __printer.WriteLine();
                    }
                    else if (arrayElement && isNullable)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("nillable=\"true\" minOccurs=\"0\" maxOccurs=\"unbounded\"");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateElement(Type type, string name, Namespace ns, bool arrayElement, bool isNullable)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    if (type is BuiltInType)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                        if (type == BuiltInType.Guid)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("<xs:element name=\"");
                            __printer.Write(name);
                            __printer.WriteTemplateOutput("\" type=\"xs:string\" ");
                            __printer.Write(Generated_GenerateElementPostfix(arrayElement, isNullable));
                            __printer.WriteTemplateOutput("/>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        else if (type == BuiltInType.String)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("<xs:element name=\"");
                            __printer.Write(name);
                            __printer.WriteTemplateOutput("\" type=\"xs:string\" ");
                            __printer.Write(Generated_GenerateElementPostfix(arrayElement, true));
                            __printer.WriteTemplateOutput("/>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        else if (type == BuiltInType.Bool)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("<xs:element name=\"");
                            __printer.Write(name);
                            __printer.WriteTemplateOutput("\" type=\"xs:boolean\" ");
                            __printer.Write(Generated_GenerateElementPostfix(arrayElement, isNullable));
                            __printer.WriteTemplateOutput("/>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        else if (type == BuiltInType.Date)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("<xs:element name=\"");
                            __printer.Write(name);
                            __printer.WriteTemplateOutput("\" type=\"xs:date\" ");
                            __printer.Write(Generated_GenerateElementPostfix(arrayElement, isNullable));
                            __printer.WriteTemplateOutput("/>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        else if (type == BuiltInType.Time)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("<xs:element name=\"");
                            __printer.Write(name);
                            __printer.WriteTemplateOutput("\" type=\"xs:time\" ");
                            __printer.Write(Generated_GenerateElementPostfix(arrayElement, isNullable));
                            __printer.WriteTemplateOutput("/>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        else if (type == BuiltInType.DateTime)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("<xs:element name=\"");
                            __printer.Write(name);
                            __printer.WriteTemplateOutput("\" type=\"xs:dateTime\" ");
                            __printer.Write(Generated_GenerateElementPostfix(arrayElement, isNullable));
                            __printer.WriteTemplateOutput("/>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        else if (type == BuiltInType.TimeSpan)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("<xs:element name=\"");
                            __printer.Write(name);
                            __printer.WriteTemplateOutput("\" type=\"xs:duration\" ");
                            __printer.Write(Generated_GenerateElementPostfix(arrayElement, isNullable));
                            __printer.WriteTemplateOutput("/>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        else
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("<xs:element name=\"");
                            __printer.Write(name);
                            __printer.WriteTemplateOutput("\" type=\"xs:");
                            __printer.Write(type.Name);
                            __printer.WriteTemplateOutput("\" ");
                            __printer.Write(Generated_GenerateElementPostfix(arrayElement, isNullable));
                            __printer.WriteTemplateOutput("/>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                    }
                    else if (type is StructType)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("<xs:element name=\"");
                        __printer.Write(name);
                        __printer.WriteTemplateOutput("\" nillable=\"true\" type=\"");
                        __printer.Write(type.Namespace.Prefix);
                        __printer.WriteTemplateOutput(":");
                        __printer.Write(type.Name);
                        __printer.WriteTemplateOutput("\" ");
                        __printer.Write(Generated_GenerateElementPostfix(arrayElement, isNullable));
                        __printer.WriteTemplateOutput("/>");
                        __printer.WriteLine();
                    }
                    else if (type is EnumType)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("<xs:element name=\"");
                        __printer.Write(name);
                        __printer.WriteTemplateOutput("\" type=\"");
                        __printer.Write(type.Namespace.Prefix);
                        __printer.WriteTemplateOutput(":");
                        __printer.Write(type.Name);
                        __printer.WriteTemplateOutput("\" ");
                        __printer.Write(Generated_GenerateElementPostfix(arrayElement, isNullable));
                        __printer.WriteTemplateOutput("/>");
                        __printer.WriteLine();
                    }
                    else if (type is NullableType)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                        if (((NullableType)type).InnerType is BuiltInType)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		");
                            if (((NullableType)type).InnerType == BuiltInType.Guid)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("<xs:element name=\"");
                                __printer.Write(name);
                                __printer.WriteTemplateOutput("\" type=\"xs:string\" ");
                                __printer.Write(Generated_GenerateElementPostfix(arrayElement, true));
                                __printer.WriteTemplateOutput("/>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		");
                            }
                            else if (((NullableType)type).InnerType == BuiltInType.String)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("<xs:element name=\"");
                                __printer.Write(name);
                                __printer.WriteTemplateOutput("\" type=\"xs:string\" ");
                                __printer.Write(Generated_GenerateElementPostfix(arrayElement, true));
                                __printer.WriteTemplateOutput("/>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		");
                            }
                            else if (((NullableType)type).InnerType == BuiltInType.Bool)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("<xs:element name=\"");
                                __printer.Write(name);
                                __printer.WriteTemplateOutput("\" type=\"xs:boolean\" ");
                                __printer.Write(Generated_GenerateElementPostfix(arrayElement, true));
                                __printer.WriteTemplateOutput("/>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		");
                            }
                            else if (((NullableType)type).InnerType == BuiltInType.Date)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("<xs:element name=\"");
                                __printer.Write(name);
                                __printer.WriteTemplateOutput("\" type=\"xs:date\" ");
                                __printer.Write(Generated_GenerateElementPostfix(arrayElement, true));
                                __printer.WriteTemplateOutput("/>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		");
                            }
                            else if (((NullableType)type).InnerType == BuiltInType.Time)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("<xs:element name=\"");
                                __printer.Write(name);
                                __printer.WriteTemplateOutput("\" type=\"xs:time\" ");
                                __printer.Write(Generated_GenerateElementPostfix(arrayElement, true));
                                __printer.WriteTemplateOutput("/>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		");
                            }
                            else if (((NullableType)type).InnerType == BuiltInType.DateTime)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("<xs:element name=\"");
                                __printer.Write(name);
                                __printer.WriteTemplateOutput("\" type=\"xs:dateTime\" ");
                                __printer.Write(Generated_GenerateElementPostfix(arrayElement, true));
                                __printer.WriteTemplateOutput("/>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		");
                            }
                            else if (((NullableType)type).InnerType == BuiltInType.TimeSpan)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("<xs:element name=\"");
                                __printer.Write(name);
                                __printer.WriteTemplateOutput("\" type=\"xs:duration\" ");
                                __printer.Write(Generated_GenerateElementPostfix(arrayElement, true));
                                __printer.WriteTemplateOutput("/>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		");
                            }
                            else
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("<xs:element name=\"");
                                __printer.Write(name);
                                __printer.WriteTemplateOutput("\" type=\"xs:");
                                __printer.Write(((NullableType)type).InnerType.Name);
                                __printer.WriteTemplateOutput("\" ");
                                __printer.Write(Generated_GenerateElementPostfix(arrayElement, true));
                                __printer.WriteTemplateOutput("/>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        else if (((NullableType)type).InnerType is StructType || ((NullableType)type).InnerType is EnumType)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("<xs:element name=\"");
                            __printer.Write(name);
                            __printer.WriteTemplateOutput("\" type=\"");
                            __printer.Write(type.Namespace.Prefix);
                            __printer.WriteTemplateOutput(":");
                            __printer.Write(((NullableType)type).InnerType.Name);
                            __printer.WriteTemplateOutput("\" ");
                            __printer.Write(Generated_GenerateElementPostfix(arrayElement, true));
                            __printer.WriteTemplateOutput("/>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                    }
                    else if (type is ArrayType)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                        if (((ArrayType)type).ItemType is NullableType)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("<xs:element name=\"");
                            __printer.Write(name);
                            __printer.WriteTemplateOutput("\" nillable=\"true\" type=\"");
                            __printer.Write(ns.Prefix);
                            __printer.WriteTemplateOutput(":ArrayOfNullable");
                            __printer.Write(((ArrayType)type).ItemType.Name);
                            __printer.WriteTemplateOutput("\" ");
                            __printer.Write(Generated_GenerateElementPostfix(arrayElement, isNullable));
                            __printer.WriteTemplateOutput("/>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        else if (((ArrayType)type).ItemType == BuiltInType.Byte)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("<xs:element name=\"");
                            __printer.Write(name);
                            __printer.WriteTemplateOutput("\" nillable=\"true\" type=\"xs:base64Binary\" ");
                            __printer.Write(Generated_GenerateElementPostfix(arrayElement, isNullable));
                            __printer.WriteTemplateOutput("/>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        else
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("<xs:element name=\"");
                            __printer.Write(name);
                            __printer.WriteTemplateOutput("\" nillable=\"true\" type=\"");
                            __printer.Write(ns.Prefix);
                            __printer.WriteTemplateOutput(":ArrayOf");
                            __printer.Write(((ArrayType)type).ItemType.Name);
                            __printer.WriteTemplateOutput("\" ");
                            __printer.Write(Generated_GenerateElementPostfix(arrayElement, isNullable));
                            __printer.WriteTemplateOutput("/>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateSingleWsdl(Namespace ns)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<wsdl:definitions targetNamespace=\"");
                    __printer.Write(Generated_GetUri(ns));
                    __printer.WriteTemplateOutput("\"  ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:tns=\"");
                    __printer.Write(Generated_GetUri(ns));
                    __printer.WriteTemplateOutput("\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:");
                    __printer.Write(ns.Prefix);
                    __printer.WriteTemplateOutput("=\"");
                    __printer.Write(Generated_GetUri(ns));
                    __printer.WriteTemplateOutput("\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:wsdl=\"http://schemas.xmlsoap.org/wsdl/\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:soap=\"http://schemas.xmlsoap.org/wsdl/soap/\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:soap12=\"http://schemas.xmlsoap.org/wsdl/soap12/\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    __printer.Write(Generated_GeneratePolicyNamespaces());
                    __printer.WriteTemplateOutput("	");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput(">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    __printer.Write(Generated_GenerateWsdlTypesPart(ns));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    __printer.Write(Generated_GeneratePolicy(ns, false));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    __printer.Write(Generated_GenerateWsdlAbstractPart(ns));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    __printer.Write(Generated_GenerateWsdlBindingPart(ns));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    __printer.Write(Generated_GenerateWsdlEndpointPart(ns));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</wsdl:definitions>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateSingleWsdl(Endpoint endp)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<wsdl:definitions targetNamespace=\"");
                    __printer.Write(Generated_GetUri(endp.Namespace));
                    __printer.WriteTemplateOutput("\"  ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:tns=\"");
                    __printer.Write(Generated_GetUri(endp.Namespace));
                    __printer.WriteTemplateOutput("\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:");
                    __printer.Write(endp.Namespace.Prefix);
                    __printer.WriteTemplateOutput("=\"");
                    __printer.Write(Generated_GetUri(endp.Namespace));
                    __printer.WriteTemplateOutput("\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:wsdl=\"http://schemas.xmlsoap.org/wsdl/\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:soap=\"http://schemas.xmlsoap.org/wsdl/soap/\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:soap12=\"http://schemas.xmlsoap.org/wsdl/soap12/\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    __printer.Write(Generated_GeneratePolicyNamespaces());
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput(">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    __printer.Write(Generated_GenerateWsdlTypesPart(endp.Namespace));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    __printer.Write(Generated_GeneratePolicy(endp.Binding, false));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    __printer.Write(Generated_GenerateWsdlAbstractPart(endp.Interface));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    __printer.Write(Generated_GenerateWsdlBinding(endp));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    __printer.Write(Generated_GenerateWsdlEndpoint(endp));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</wsdl:definitions>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateWsdlAbstract(Namespace ns)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<wsdl:definitions targetNamespace=\"");
                    __printer.Write(Generated_GetUri(ns));
                    __printer.WriteTemplateOutput("\"  ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:wsdl=\"http://schemas.xmlsoap.org/wsdl/\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:wsaw=\"http://www.w3.org/2006/05/addressing/wsdl\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:tns=\"");
                    __printer.Write(Generated_GetUri(ns));
                    __printer.WriteTemplateOutput("\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:");
                    __printer.Write(ns.Prefix);
                    __printer.WriteTemplateOutput("=\"");
                    __printer.Write(Generated_GetUri(ns));
                    __printer.WriteTemplateOutput("\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    __printer.Write(Generated_GenerateWsdlTypesPart(ns));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    __printer.Write(Generated_GenerateWsdlAbstractPart(ns));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</wsdl:definitions>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateWsdlTypesPart(Namespace ns)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<wsdl:types>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<xs:schema>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    if (Properties.GenerateSeparateXsdWsdlFolder)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		<xs:import schemaLocation=\"");
                        __printer.Write("../schema/" + ns.FullName + ".xsd");
                        __printer.WriteTemplateOutput("\" namespace=\"");
                        __printer.Write(Generated_GetUri(ns));
                        __printer.WriteTemplateOutput("\"/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                    }
                    else
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		<xs:import schemaLocation=\"");
                        __printer.Write(ns.FullName + ".xsd");
                        __printer.WriteTemplateOutput("\" namespace=\"");
                        __printer.Write(Generated_GetUri(ns));
                        __printer.WriteTemplateOutput("\"/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	</xs:schema>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</wsdl:types>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateWsdlAbstractPart(Namespace ns)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    int __loop15_iteration = 0;
                    var __loop15_result =
                        (from __loop15_tmp_item___noname15 in EnumerableExtensions.Enumerate((ns.Declarations).GetEnumerator())
                        from __loop15_tmp_item_intf in EnumerableExtensions.Enumerate((__loop15_tmp_item___noname15).GetEnumerator()).OfType<Interface>()
                        select
                            new
                            {
                                __loop15_item___noname15 = __loop15_tmp_item___noname15,
                                __loop15_item_intf = __loop15_tmp_item_intf,
                            }).ToArray();
                    foreach (var __loop15_item in __loop15_result)
                    {
                        var __noname15 = __loop15_item.__loop15_item___noname15;
                        var intf = __loop15_item.__loop15_item_intf;
                        ++__loop15_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.Write(Generated_GenerateMessages(intf));
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    int __loop16_iteration = 0;
                    var __loop16_result =
                        (from __loop16_tmp_item___noname16 in EnumerableExtensions.Enumerate((ns.Declarations).GetEnumerator())
                        from __loop16_tmp_item_intf in EnumerableExtensions.Enumerate((__loop16_tmp_item___noname16).GetEnumerator()).OfType<Interface>()
                        select
                            new
                            {
                                __loop16_item___noname16 = __loop16_tmp_item___noname16,
                                __loop16_item_intf = __loop16_tmp_item_intf,
                            }).ToArray();
                    foreach (var __loop16_item in __loop16_result)
                    {
                        var __noname16 = __loop16_item.__loop16_item___noname16;
                        var intf = __loop16_item.__loop16_item_intf;
                        ++__loop16_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.Write(Generated_GeneratePortType(intf));
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateWsdlAbstractPart(Interface intf)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.Write(Generated_GenerateMessages(intf));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.Write(Generated_GeneratePortType(intf));
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateMessages(Interface intf)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    int __loop17_iteration = 0;
                    var __loop17_result =
                        (from __loop17_tmp_item___noname17 in EnumerableExtensions.Enumerate((intf.Operations).GetEnumerator())
                        from __loop17_tmp_item_op in EnumerableExtensions.Enumerate((__loop17_tmp_item___noname17).GetEnumerator()).OfType<Operation>()
                        select
                            new
                            {
                                __loop17_item___noname17 = __loop17_tmp_item___noname17,
                                __loop17_item_op = __loop17_tmp_item_op,
                            }).ToArray();
                    foreach (var __loop17_item in __loop17_result)
                    {
                        var __noname17 = __loop17_item.__loop17_item___noname17;
                        var op = __loop17_item.__loop17_item_op;
                        ++__loop17_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("<wsdl:message name=\"");
                        __printer.Write(intf.Name);
                        __printer.WriteTemplateOutput("_");
                        __printer.Write(op.Name);
                        __printer.WriteTemplateOutput("_InputMessage\">");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	<wsdl:part name=\"parameters\" element=\"");
                        __printer.Write(intf.Namespace.Prefix);
                        __printer.WriteTemplateOutput(":");
                        __printer.Write(op.Name);
                        __printer.WriteTemplateOutput("\"/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("</wsdl:message>");
                        __printer.WriteLine();
                        if (op.ReturnType != PseudoType.Async)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("^");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("<wsdl:message name=\"");
                            __printer.Write(intf.Name);
                            __printer.WriteTemplateOutput("_");
                            __printer.Write(op.Name);
                            __printer.WriteTemplateOutput("_OutputMessage\">");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	<wsdl:part name=\"parameters\" element=\"");
                            __printer.Write(intf.Namespace.Prefix);
                            __printer.WriteTemplateOutput(":");
                            __printer.Write(op.Name);
                            __printer.WriteTemplateOutput("Response\"/>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("</wsdl:message>");
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        int __loop18_iteration = 0;
                        var __loop18_result =
                            (from __loop18_tmp_item___noname18 in EnumerableExtensions.Enumerate((op.Exceptions).GetEnumerator())
                            from __loop18_tmp_item_ex in EnumerableExtensions.Enumerate((__loop18_tmp_item___noname18).GetEnumerator()).OfType<ExceptionType>()
                            select
                                new
                                {
                                    __loop18_item___noname18 = __loop18_tmp_item___noname18,
                                    __loop18_item_ex = __loop18_tmp_item_ex,
                                }).ToArray();
                        foreach (var __loop18_item in __loop18_result)
                        {
                            var __noname18 = __loop18_item.__loop18_item___noname18;
                            var ex = __loop18_item.__loop18_item_ex;
                            ++__loop18_iteration;
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("^");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("<wsdl:message name=\"");
                            __printer.Write(intf.Name);
                            __printer.WriteTemplateOutput("_");
                            __printer.Write(op.Name);
                            __printer.WriteTemplateOutput("_");
                            __printer.Write(ex.Name);
                            __printer.WriteTemplateOutput("\">");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	<wsdl:part name=\"fault\" element=\"");
                            __printer.Write(ex.Namespace.Prefix);
                            __printer.WriteTemplateOutput(":");
                            __printer.Write(ex.Name);
                            __printer.WriteTemplateOutput("\"/>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("</wsdl:message>");
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GeneratePortType(Interface intf)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<wsdl:portType name=\"");
                    __printer.Write(intf.Name);
                    __printer.WriteTemplateOutput("\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    __printer.Write(Generated_GenerateOperations(intf));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</wsdl:portType>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateOperations(Interface intf)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    int __loop19_iteration = 0;
                    var __loop19_result =
                        (from __loop19_tmp_item___noname19 in EnumerableExtensions.Enumerate((intf.SuperInterfaces).GetEnumerator())
                        from __loop19_tmp_item_sup in EnumerableExtensions.Enumerate((__loop19_tmp_item___noname19).GetEnumerator()).OfType<Interface>()
                        select
                            new
                            {
                                __loop19_item___noname19 = __loop19_tmp_item___noname19,
                                __loop19_item_sup = __loop19_tmp_item_sup,
                            }).ToArray();
                    foreach (var __loop19_item in __loop19_result)
                    {
                        var __noname19 = __loop19_item.__loop19_item___noname19;
                        var sup = __loop19_item.__loop19_item_sup;
                        ++__loop19_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.Write(Generated_GenerateOperations(sup));
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    int __loop20_iteration = 0;
                    var __loop20_result =
                        (from __loop20_tmp_item___noname20 in EnumerableExtensions.Enumerate((intf.Operations).GetEnumerator())
                        from __loop20_tmp_item_op in EnumerableExtensions.Enumerate((__loop20_tmp_item___noname20).GetEnumerator()).OfType<Operation>()
                        select
                            new
                            {
                                __loop20_item___noname20 = __loop20_tmp_item___noname20,
                                __loop20_item_op = __loop20_tmp_item_op,
                            }).ToArray();
                    foreach (var __loop20_item in __loop20_result)
                    {
                        var __noname20 = __loop20_item.__loop20_item___noname20;
                        var op = __loop20_item.__loop20_item_op;
                        ++__loop20_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("<wsdl:operation name=\"");
                        __printer.Write(op.Name);
                        __printer.WriteTemplateOutput("\">");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	<wsdl:input wsaw:action=\"");
                        __printer.Write(Generated_GetUriWithSlash(op.Interface.Namespace) + op.Interface.Name + "/" + op.Name);
                        __printer.WriteTemplateOutput("\" message=\"");
                        __printer.Write(op.Interface.Namespace.Prefix);
                        __printer.WriteTemplateOutput(":");
                        __printer.Write(op.Interface.Name);
                        __printer.WriteTemplateOutput("_");
                        __printer.Write(op.Name);
                        __printer.WriteTemplateOutput("_InputMessage\"/>");
                        __printer.WriteLine();
                        if (op.ReturnType != PseudoType.Async)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	<wsdl:output wsaw:action=\"");
                            __printer.Write(Generated_GetUriWithSlash(op.Interface.Namespace) + op.Interface.Name + "/" + op.Name + "Response");
                            __printer.WriteTemplateOutput("\" message=\"");
                            __printer.Write(op.Interface.Namespace.Prefix);
                            __printer.WriteTemplateOutput(":");
                            __printer.Write(op.Interface.Name);
                            __printer.WriteTemplateOutput("_");
                            __printer.Write(op.Name);
                            __printer.WriteTemplateOutput("_OutputMessage\"/>");
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                        int __loop21_iteration = 0;
                        var __loop21_result =
                            (from __loop21_tmp_item___noname21 in EnumerableExtensions.Enumerate((op.Exceptions).GetEnumerator())
                            from __loop21_tmp_item_ex in EnumerableExtensions.Enumerate((__loop21_tmp_item___noname21).GetEnumerator()).OfType<ExceptionType>()
                            select
                                new
                                {
                                    __loop21_item___noname21 = __loop21_tmp_item___noname21,
                                    __loop21_item_ex = __loop21_tmp_item_ex,
                                }).ToArray();
                        foreach (var __loop21_item in __loop21_result)
                        {
                            var __noname21 = __loop21_item.__loop21_item___noname21;
                            var ex = __loop21_item.__loop21_item_ex;
                            ++__loop21_iteration;
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	<wsdl:fault wsaw:action=\"");
                            __printer.Write(Generated_GetUriWithSlash(op.Interface.Namespace) + op.Interface.Name + "/" + op.Name + "Fault/" + ex.Name);
                            __printer.WriteTemplateOutput("\" message=\"");
                            __printer.Write(ex.Namespace.Prefix);
                            __printer.WriteTemplateOutput(":");
                            __printer.Write(op.Interface.Name);
                            __printer.WriteTemplateOutput("_");
                            __printer.Write(op.Name);
                            __printer.WriteTemplateOutput("_");
                            __printer.Write(ex.Name);
                            __printer.WriteTemplateOutput("\" name=\"");
                            __printer.Write(ex.Name);
                            __printer.WriteTemplateOutput("\"/>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("</wsdl:operation>");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateWsdlBinding(Namespace ns)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<wsdl:definitions targetNamespace=\"");
                    __printer.Write(Generated_GetUri(ns));
                    __printer.WriteTemplateOutput("\"  ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:tns=\"");
                    __printer.Write(Generated_GetUri(ns));
                    __printer.WriteTemplateOutput("\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:");
                    __printer.Write(ns.Prefix);
                    __printer.WriteTemplateOutput("=\"");
                    __printer.Write(Generated_GetUri(ns));
                    __printer.WriteTemplateOutput("\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:wsdl=\"http://schemas.xmlsoap.org/wsdl/\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:soap=\"http://schemas.xmlsoap.org/wsdl/soap/\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:soap12=\"http://schemas.xmlsoap.org/wsdl/soap12/\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    __printer.Write(Generated_GeneratePolicyNamespaces());
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput(">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<wsdl:import location=\"");
                    __printer.Write(ns.FullName);
                    __printer.WriteTemplateOutput(".wsdl\" namespace=\"");
                    __printer.Write(Generated_GetUri(ns));
                    __printer.WriteTemplateOutput("\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    __printer.Write(Generated_GeneratePolicy(ns, false));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    __printer.Write(Generated_GenerateWsdlBindingPart(ns));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</wsdl:definitions>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateWsdlBindingPart(Namespace ns)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    int __loop22_iteration = 0;
                    var __loop22_result =
                        (from __loop22_tmp_item___noname22 in EnumerableExtensions.Enumerate((ns.Declarations).GetEnumerator())
                        from __loop22_tmp_item_endpoint in EnumerableExtensions.Enumerate((__loop22_tmp_item___noname22).GetEnumerator()).OfType<Endpoint>()
                        select
                            new
                            {
                                __loop22_item___noname22 = __loop22_tmp_item___noname22,
                                __loop22_item_endpoint = __loop22_tmp_item_endpoint,
                            }).ToArray();
                    foreach (var __loop22_item in __loop22_result)
                    {
                        var __noname22 = __loop22_item.__loop22_item___noname22;
                        var endpoint = __loop22_item.__loop22_item_endpoint;
                        ++__loop22_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.Write(Generated_GenerateWsdlBinding(endpoint));
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateWsdlBinding(Endpoint endpoint)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    int __loop23_iteration = 0;
                    var __loop23_result =
                        (from __loop23_tmp_item___noname23 in EnumerableExtensions.Enumerate((endpoint).GetEnumerator())
                        from __loop23_tmp_item_ns in EnumerableExtensions.Enumerate((__loop23_tmp_item___noname23.Namespace).GetEnumerator())
                        select
                            new
                            {
                                __loop23_item___noname23 = __loop23_tmp_item___noname23,
                                __loop23_item_ns = __loop23_tmp_item_ns,
                            }).ToArray();
                    foreach (var __loop23_item in __loop23_result)
                    {
                        var __noname23 = __loop23_item.__loop23_item___noname23;
                        var ns = __loop23_item.__loop23_item_ns;
                        ++__loop23_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("<wsdl:binding name=\"");
                        __printer.Write(endpoint.Interface.Name);
                        __printer.WriteTemplateOutput("_");
                        __printer.Write(endpoint.Binding.Name);
                        __printer.WriteTemplateOutput("_Binding\" type=\"");
                        __printer.Write(ns.Prefix);
                        __printer.WriteTemplateOutput(":");
                        __printer.Write(endpoint.Interface.Name);
                        __printer.WriteTemplateOutput("\">");
                        __printer.WriteLine();
                        if (endpoint.Binding.Protocols.Count > 0 || endpoint.Binding.Transport.GetType() == typeof(HttpsTransportBindingElement))
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                            if (Properties.GeneratePolicies)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("	<wsp:PolicyReference URI=\"#");
                                __printer.Write(endpoint.Binding.Name);
                                __printer.WriteTemplateOutput("_Policy\"/>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("	");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                            int __loop24_iteration = 0;
                            var __loop24_result =
                                (from __loop24_tmp_item___noname24 in EnumerableExtensions.Enumerate((endpoint.Binding.Protocols).GetEnumerator())
                                from __loop24_tmp_item_addressing in EnumerableExtensions.Enumerate((__loop24_tmp_item___noname24).GetEnumerator()).OfType<AddressingProtocolBindingElement>()
                                select
                                    new
                                    {
                                        __loop24_item___noname24 = __loop24_tmp_item___noname24,
                                        __loop24_item_addressing = __loop24_tmp_item_addressing,
                                    }).ToArray();
                            foreach (var __loop24_item in __loop24_result)
                            {
                                var __noname24 = __loop24_item.__loop24_item___noname24;
                                var addressing = __loop24_item.__loop24_item_addressing;
                                ++__loop24_iteration;
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("	");
                                if (addressing.Version == AddressingVersion.AddressingAugust2004)
                                {
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("	<wsaw:UsingAddressing/>");
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("	");
                                }
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("	");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        if (endpoint.Binding.Encoding.GetType() == typeof(SoapEncodingBindingElement) && ((SoapEncodingBindingElement)endpoint.Binding.Encoding).Version == SoapVersion.Soap11)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                            if (endpoint.Binding.Transport.GetType() == typeof(HttpTransportBindingElement) || endpoint.Binding.Transport.GetType() == typeof(HttpsTransportBindingElement))
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("	<soap:binding style=\"document\" transport=\"http://schemas.xmlsoap.org/soap/http\"/>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("	");
                            }
                            else
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("	<soap:binding style=\"document\"/>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("	");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        if (endpoint.Binding.Encoding.GetType() == typeof(SoapEncodingBindingElement) && ((SoapEncodingBindingElement)endpoint.Binding.Encoding).Version == SoapVersion.Soap12)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                            if (endpoint.Binding.Transport.GetType() == typeof(HttpTransportBindingElement) || endpoint.Binding.Transport.GetType() == typeof(HttpsTransportBindingElement))
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("	<soap12:binding style=\"document\" transport=\"http://schemas.xmlsoap.org/soap/http\"/>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("	");
                            }
                            else
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("	<soap12:binding style=\"document\"/>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("	");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        int __loop25_iteration = 0;
                        var __loop25_result =
                            (from __loop25_tmp_item___noname25 in EnumerableExtensions.Enumerate((endpoint.Interface.Operations).GetEnumerator())
                            from __loop25_tmp_item_op in EnumerableExtensions.Enumerate((__loop25_tmp_item___noname25).GetEnumerator()).OfType<Operation>()
                            select
                                new
                                {
                                    __loop25_item___noname25 = __loop25_tmp_item___noname25,
                                    __loop25_item_op = __loop25_tmp_item_op,
                                }).ToArray();
                        foreach (var __loop25_item in __loop25_result)
                        {
                            var __noname25 = __loop25_item.__loop25_item___noname25;
                            var op = __loop25_item.__loop25_item_op;
                            ++__loop25_iteration;
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	<wsdl:operation name=\"");
                            __printer.Write(op.Name);
                            __printer.WriteTemplateOutput("\">");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                            if (endpoint.Binding.Encoding.GetType() == typeof(SoapEncodingBindingElement) && ((SoapEncodingBindingElement)endpoint.Binding.Encoding).Version == SoapVersion.Soap11)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		<soap:operation style=\"document\" soapAction=\"");
                                __printer.Write(Generated_GetUriWithSlash(endpoint.Interface.Namespace) + endpoint.Interface.Name + "/" + op.Name);
                                __printer.WriteTemplateOutput("\"/>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("	");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                            if (endpoint.Binding.Encoding.GetType() == typeof(SoapEncodingBindingElement) && ((SoapEncodingBindingElement)endpoint.Binding.Encoding).Version == SoapVersion.Soap12)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		<soap12:operation style=\"document\" soapAction=\"");
                                __printer.Write(Generated_GetUriWithSlash(endpoint.Interface.Namespace) + endpoint.Interface.Name + "/" + op.Name);
                                __printer.WriteTemplateOutput("\"/>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("	");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		<wsdl:input>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		");
                            if (Properties.GeneratePolicies)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("			");
                                int __loop26_iteration = 0;
                                var __loop26_result =
                                    (from __loop26_tmp_item___noname26 in EnumerableExtensions.Enumerate((endpoint.Binding.Protocols).GetEnumerator())
                                    from __loop26_tmp_item_security in EnumerableExtensions.Enumerate((__loop26_tmp_item___noname26).GetEnumerator()).OfType<SecurityProtocolBindingElement>()
                                    select
                                        new
                                        {
                                            __loop26_item___noname26 = __loop26_tmp_item___noname26,
                                            __loop26_item_security = __loop26_tmp_item_security,
                                        }).ToArray();
                                foreach (var __loop26_item in __loop26_result)
                                {
                                    var __noname26 = __loop26_item.__loop26_item___noname26;
                                    var security = __loop26_item.__loop26_item_security;
                                    ++__loop26_iteration;
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("			<wsp:PolicyReference URI=\"#");
                                    __printer.Write(endpoint.Binding.Name);
                                    __printer.WriteTemplateOutput("_Input_Policy\"/>");
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("			");
                                }
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		");
                            if (endpoint.Binding.Encoding.GetType() == typeof(SoapEncodingBindingElement) && ((SoapEncodingBindingElement)endpoint.Binding.Encoding).Version == SoapVersion.Soap11)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("			<soap:body use=\"literal\"/>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		");
                            if (endpoint.Binding.Encoding.GetType() == typeof(SoapEncodingBindingElement) && ((SoapEncodingBindingElement)endpoint.Binding.Encoding).Version == SoapVersion.Soap12)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("			<soap12:body use=\"literal\"/>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		</wsdl:input>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		<wsdl:output>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		");
                            if (Properties.GeneratePolicies)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("			");
                                int __loop27_iteration = 0;
                                var __loop27_result =
                                    (from __loop27_tmp_item___noname27 in EnumerableExtensions.Enumerate((endpoint.Binding.Protocols).GetEnumerator())
                                    from __loop27_tmp_item_security in EnumerableExtensions.Enumerate((__loop27_tmp_item___noname27).GetEnumerator()).OfType<SecurityProtocolBindingElement>()
                                    select
                                        new
                                        {
                                            __loop27_item___noname27 = __loop27_tmp_item___noname27,
                                            __loop27_item_security = __loop27_tmp_item_security,
                                        }).ToArray();
                                foreach (var __loop27_item in __loop27_result)
                                {
                                    var __noname27 = __loop27_item.__loop27_item___noname27;
                                    var security = __loop27_item.__loop27_item_security;
                                    ++__loop27_iteration;
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("			<wsp:PolicyReference URI=\"#");
                                    __printer.Write(endpoint.Binding.Name);
                                    __printer.WriteTemplateOutput("_Output_Policy\"/>");
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("			");
                                }
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		");
                            if (endpoint.Binding.Encoding.GetType() == typeof(SoapEncodingBindingElement) && ((SoapEncodingBindingElement)endpoint.Binding.Encoding).Version == SoapVersion.Soap11)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("			<soap:body use=\"literal\"/>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		");
                            if (endpoint.Binding.Encoding.GetType() == typeof(SoapEncodingBindingElement) && ((SoapEncodingBindingElement)endpoint.Binding.Encoding).Version == SoapVersion.Soap12)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("			<soap12:body use=\"literal\"/>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		</wsdl:output>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                            int __loop28_iteration = 0;
                            var __loop28_result =
                                (from __loop28_tmp_item___noname28 in EnumerableExtensions.Enumerate((op.Exceptions).GetEnumerator())
                                from __loop28_tmp_item_ex in EnumerableExtensions.Enumerate((__loop28_tmp_item___noname28).GetEnumerator()).OfType<ExceptionType>()
                                select
                                    new
                                    {
                                        __loop28_item___noname28 = __loop28_tmp_item___noname28,
                                        __loop28_item_ex = __loop28_tmp_item_ex,
                                    }).ToArray();
                            foreach (var __loop28_item in __loop28_result)
                            {
                                var __noname28 = __loop28_item.__loop28_item___noname28;
                                var ex = __loop28_item.__loop28_item_ex;
                                ++__loop28_iteration;
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		<wsdl:fault name=\"");
                                __printer.Write(ex.Name);
                                __printer.WriteTemplateOutput("\">");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		");
                                if (endpoint.Binding.Encoding.GetType() == typeof(SoapEncodingBindingElement) && ((SoapEncodingBindingElement)endpoint.Binding.Encoding).Version == SoapVersion.Soap11)
                                {
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("			<soap:fault name=\"");
                                    __printer.Write(ex.Name);
                                    __printer.WriteTemplateOutput("\" use=\"literal\"/>");
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("		");
                                }
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		");
                                if (endpoint.Binding.Encoding.GetType() == typeof(SoapEncodingBindingElement) && ((SoapEncodingBindingElement)endpoint.Binding.Encoding).Version == SoapVersion.Soap12)
                                {
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("			<soap12:fault name=\"");
                                    __printer.Write(ex.Name);
                                    __printer.WriteTemplateOutput("\" use=\"literal\"/>");
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("		");
                                }
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		</wsdl:fault>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("	");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	</wsdl:operation>");
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("</wsdl:binding>");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateMetroJks()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("	");
                    if (Properties.GenerateMetroJksService)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	<sc:KeyStore xmlns:sc=\"http://schemas.sun.com/2006/03/wss/server\" xmlns:wspp=\"http://java.sun.com/xml/ns/wsit/policy\"");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("			        wspp:visibility=\"private\" location=\"server_keystore.jks\" type=\"JKS\" storepass=\"changeit\" alias=\"wspservicepriv\" keypass=\"changeit\"/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	<sc:TrustStore xmlns:sc=\"http://schemas.sun.com/2006/03/wss/server\" xmlns:wspp=\"http://java.sun.com/xml/ns/wsit/policy\"");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("			        wspp:visibility=\"private\" storepass=\"changeit\" type=\"JKS\" location=\"server_truststore.jks\"/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    if (Properties.GenerateMetroJksClient)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	<sc:KeyStore xmlns:sc=\"http://schemas.sun.com/2006/03/wss/client\" xmlns:wspp=\"http://java.sun.com/xml/ns/wsit/policy\"");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		            wspp:visibility=\"private\" location=\"client_keystore.jks\" type=\"JKS\" storepass=\"changeit\" alias=\"wspclientpriv\" keypass=\"changeit\"/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	<sc:TrustStore xmlns:sc=\"http://schemas.sun.com/2006/03/wss/client\" xmlns:wspp=\"http://java.sun.com/xml/ns/wsit/policy\"");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		            wspp:visibility=\"private\" storepass=\"changeit\" type=\"JKS\" location=\"client_truststore.jks\" peeralias=\"wspservicepub\"/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GeneratePolicy(Namespace ns, bool combineSecurityPolicies)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    int __loop29_iteration = 0;
                    var __loop29_result =
                        (from __loop29_tmp_item___noname29 in EnumerableExtensions.Enumerate((ns.Declarations).GetEnumerator())
                        from __loop29_tmp_item_binding in EnumerableExtensions.Enumerate((__loop29_tmp_item___noname29).GetEnumerator()).OfType<Binding>()
                        select
                            new
                            {
                                __loop29_item___noname29 = __loop29_tmp_item___noname29,
                                __loop29_item_binding = __loop29_tmp_item_binding,
                            }).ToArray();
                    foreach (var __loop29_item in __loop29_result)
                    {
                        var __noname29 = __loop29_item.__loop29_item___noname29;
                        var binding = __loop29_item.__loop29_item_binding;
                        ++__loop29_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.Write(Generated_GeneratePolicy(binding, combineSecurityPolicies));
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GeneratePolicy(Binding binding, bool combineSecurityPolicies)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    if (Properties.GeneratePolicies)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        if (binding.HasPolicy())
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("^");
                            __printer.WriteLine();
                            __printer.Write(Generated_GeneratePolicy(binding.Namespace, binding, false, combineSecurityPolicies));
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GeneratePolicyNamespaces()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("xmlns:wsp=\"");
                    __printer.Write(Properties.WsPolicyNamespace);
                    __printer.WriteTemplateOutput("\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("xmlns:wsu=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("xmlns:wsoma=\"http://schemas.xmlsoap.org/ws/2004/09/policy/optimizedmimeserialization\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("xmlns:wsa=\"http://www.w3.org/2005/08/addressing\" ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("xmlns:wsam=\"http://www.w3.org/2007/05/addressing/metadata\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("xmlns:wsaw=\"http://www.w3.org/2006/05/addressing/wsdl\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("xmlns:wsrmp=\"http://docs.oasis-open.org/ws-rx/wsrmp/200702\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("xmlns:wsat=\"http://schemas.xmlsoap.org/ws/2004/10/wsat\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("xmlns:sp=\"");
                    __printer.Write(Properties.WsSecurityPolicyNamespace);
                    __printer.WriteTemplateOutput("\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("xmlns:wst=\"http://docs.oasis-open.org/ws-sx/ws-trust/200512\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("xmlns:wsx=\"http://schemas.xmlsoap.org/ws/2004/09/mex\"");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GeneratePolicy(Namespace ns, Binding binding, bool GenNsPrefixes, bool combineSecurityPolicies)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    if (GenNsPrefixes)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("<wsp:Policy wsu:Id=\"");
                        __printer.Write(binding.Name);
                        __printer.WriteTemplateOutput("_Policy\"");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                        __printer.Write(Generated_GeneratePolicyNamespaces());
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput(">");
                        __printer.WriteLine();
                    }
                    else
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("<wsp:Policy wsu:Id=\"");
                        __printer.Write(binding.Name);
                        __printer.WriteTemplateOutput("_Policy\">");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    __printer.Write(Generated_GenerateHttpsPolicy(binding));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    __printer.Write(Generated_GenerateMtomPolicy(binding));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    __printer.Write(Generated_GenerateAddressingPolicy(binding));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    __printer.Write(Generated_GenerateReliableMessagingPolicy(binding));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    __printer.Write(Generated_GenerateAtomicTransactionPolicy(binding));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    __printer.Write(Generated_GenerateSecurityPolicy(binding, combineSecurityPolicies));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</wsp:Policy>");
                    __printer.WriteLine();
                    if (!combineSecurityPolicies)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        int __loop30_iteration = 0;
                        var __loop30_result =
                            (from __loop30_tmp_item___noname30 in EnumerableExtensions.Enumerate((binding.Protocols).GetEnumerator())
                            from __loop30_tmp_item_security in EnumerableExtensions.Enumerate((__loop30_tmp_item___noname30).GetEnumerator()).OfType<SecurityProtocolBindingElement>()
                            select
                                new
                                {
                                    __loop30_item___noname30 = __loop30_tmp_item___noname30,
                                    __loop30_item_security = __loop30_tmp_item_security,
                                }).ToArray();
                        foreach (var __loop30_item in __loop30_result)
                        {
                            var __noname30 = __loop30_item.__loop30_item___noname30;
                            var security = __loop30_item.__loop30_item_security;
                            ++__loop30_iteration;
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("^");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("<wsp:Policy wsu:Id=\"");
                            __printer.Write(binding.Name);
                            __printer.WriteTemplateOutput("_Input_Policy\">");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                            __printer.Write(Generated_GenerateAppMessagePolicy(security));
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("</wsp:Policy>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("^");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("<wsp:Policy wsu:Id=\"");
                            __printer.Write(binding.Name);
                            __printer.WriteTemplateOutput("_Output_Policy\">");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                            __printer.Write(Generated_GenerateAppMessagePolicy(security));
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("</wsp:Policy>");
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateAddressingPolicy(Binding binding)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    int __loop31_iteration = 0;
                    var __loop31_result =
                        (from __loop31_tmp_item___noname31 in EnumerableExtensions.Enumerate((binding.Protocols).GetEnumerator())
                        from __loop31_tmp_item_addressing in EnumerableExtensions.Enumerate((__loop31_tmp_item___noname31).GetEnumerator()).OfType<AddressingProtocolBindingElement>()
                        select
                            new
                            {
                                __loop31_item___noname31 = __loop31_tmp_item___noname31,
                                __loop31_item_addressing = __loop31_tmp_item_addressing,
                            }).ToArray();
                    foreach (var __loop31_item in __loop31_result)
                    {
                        var __noname31 = __loop31_item.__loop31_item___noname31;
                        var addressing = __loop31_item.__loop31_item_addressing;
                        ++__loop31_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                        if (addressing.Version == AddressingVersion.Addressing10)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("<wsam:Addressing/>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                        if (addressing.Version == AddressingVersion.AddressingAugust2004)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateMtomPolicy(Binding binding)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    if (binding.Encoding.GetType() == typeof(SoapEncodingBindingElement))
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                        if (((SoapEncodingBindingElement)binding.Encoding).MtomEnabled == true)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("<wsoma:OptimizedMimeSerialization/>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateHttpsPolicy(Binding binding)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    if (binding.Transport.GetType() == typeof(HttpsTransportBindingElement))
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("<sp:TransportBinding>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	<wsp:Policy>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		<sp:TransportToken>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("			<wsp:Policy>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("			");
                        if (((HttpsTransportBindingElement)binding.Transport).ClientAuthentication == HttpsClientAuthentication.Certificate)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("				<sp:HttpsToken RequireClientCertificate=\"true\"/>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("			");
                        }
                        else
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("				<sp:HttpsToken RequireClientCertificate=\"false\"/>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("			");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("			</wsp:Policy>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		</sp:TransportToken>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		<sp:AlgorithmSuite>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("			<wsp:Policy>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("				<sp:Basic256/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("			</wsp:Policy>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		</sp:AlgorithmSuite>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		<sp:Layout>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("			<wsp:Policy>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("				<sp:Strict/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("			</wsp:Policy>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		</sp:Layout> ");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	</wsp:Policy>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("</sp:TransportBinding>");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateReliableMessagingPolicy(Binding binding)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    int __loop32_iteration = 0;
                    var __loop32_result =
                        (from __loop32_tmp_item___noname32 in EnumerableExtensions.Enumerate((binding.Protocols).GetEnumerator())
                        from __loop32_tmp_item_rm in EnumerableExtensions.Enumerate((__loop32_tmp_item___noname32).GetEnumerator()).OfType<ReliableMessagingProtocolBindingElement>()
                        select
                            new
                            {
                                __loop32_item___noname32 = __loop32_tmp_item___noname32,
                                __loop32_item_rm = __loop32_tmp_item_rm,
                            }).ToArray();
                    foreach (var __loop32_item in __loop32_result)
                    {
                        var __noname32 = __loop32_item.__loop32_item___noname32;
                        var rm = __loop32_item.__loop32_item_rm;
                        ++__loop32_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                        if (rm.Version == ReliableMessagingVersion.ReliableMessaging11)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("<wsrmp:RMAssertion>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	<wsp:Policy>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		<wsrmp:DeliveryAssurance>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("			<wsp:Policy>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("			");
                            if (rm.Delivery == ReliableMessagingDelivery.AtLeastOnce)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("				<wsrmp:AtLeastOnce/>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("			");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("			");
                            if (rm.Delivery == ReliableMessagingDelivery.AtMostOnce)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("				<wsrmp:AtMostOnce/>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("			");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("			");
                            if (rm.Delivery == ReliableMessagingDelivery.ExactlyOnce)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("				<wsrmp:ExactlyOnce/>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("			");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("			");
                            if (rm.InOrder == true)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("				<wsrmp:InOrder/>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("			");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("			</wsp:Policy>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		</wsrmp:DeliveryAssurance>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	</wsp:Policy>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("</wsrmp:RMAssertion>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateAtomicTransactionPolicy(Binding binding)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    int __loop33_iteration = 0;
                    var __loop33_result =
                        (from __loop33_tmp_item___noname33 in EnumerableExtensions.Enumerate((binding.Protocols).GetEnumerator())
                        from __loop33_tmp_item_at in EnumerableExtensions.Enumerate((__loop33_tmp_item___noname33).GetEnumerator()).OfType<AtomicTransactionProtocolBindingElement>()
                        select
                            new
                            {
                                __loop33_item___noname33 = __loop33_tmp_item___noname33,
                                __loop33_item_at = __loop33_tmp_item_at,
                            }).ToArray();
                    foreach (var __loop33_item in __loop33_result)
                    {
                        var __noname33 = __loop33_item.__loop33_item___noname33;
                        var at = __loop33_item.__loop33_item_at;
                        ++__loop33_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                        if (at.Version == AtomicTransactionVersion.AtomicTransaction10)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("<wsat:ATAssertion/>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateSecurityPolicy(Binding binding, bool generateMessagePolicy)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    int __loop34_iteration = 0;
                    var __loop34_result =
                        (from __loop34_tmp_item___noname34 in EnumerableExtensions.Enumerate((binding.Protocols).GetEnumerator())
                        from __loop34_tmp_item_mc in EnumerableExtensions.Enumerate((__loop34_tmp_item___noname34).GetEnumerator()).OfType<MutualCertificateSecurityProtocolBindingElement>()
                        select
                            new
                            {
                                __loop34_item___noname34 = __loop34_tmp_item___noname34,
                                __loop34_item_mc = __loop34_tmp_item_mc,
                            }).ToArray();
                    foreach (var __loop34_item in __loop34_result)
                    {
                        var __noname34 = __loop34_item.__loop34_item___noname34;
                        var mc = __loop34_item.__loop34_item_mc;
                        ++__loop34_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.Write(Generated_GenerateMutualCertificatePolicy(mc.AlgorithmSuite, mc.HeaderLayout, mc.ProtectionOrder, mc.RequireSignatureConfirmation));
                        __printer.WriteLine();
                        __printer.Write(Generated_GenerateMetroJks());
                        __printer.WriteLine();
                        if (generateMessagePolicy)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.Write(Generated_GenerateAppMessagePolicy(mc));
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    int __loop35_iteration = 0;
                    var __loop35_result =
                        (from __loop35_tmp_item___noname35 in EnumerableExtensions.Enumerate((binding.Protocols).GetEnumerator())
                        from __loop35_tmp_item_sts in EnumerableExtensions.Enumerate((__loop35_tmp_item___noname35).GetEnumerator()).OfType<StsSecurityProtocolBindingElement>()
                        select
                            new
                            {
                                __loop35_item___noname35 = __loop35_tmp_item___noname35,
                                __loop35_item_sts = __loop35_tmp_item_sts,
                            }).ToArray();
                    foreach (var __loop35_item in __loop35_result)
                    {
                        var __noname35 = __loop35_item.__loop35_item___noname35;
                        var sts = __loop35_item.__loop35_item_sts;
                        ++__loop35_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.Write(Generated_GenerateStsPolicy(sts.AlgorithmSuite, sts.HeaderLayout, sts.ProtectionOrder, sts.TokenVersion, sts.TokenType, sts.TokenIssuer, sts.RequireSignatureConfirmation, sts.DerivedKeys));
                        __printer.WriteLine();
                        if (generateMessagePolicy)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.Write(Generated_GenerateAppMessagePolicy(sts));
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    int __loop36_iteration = 0;
                    var __loop36_result =
                        (from __loop36_tmp_item___noname36 in EnumerableExtensions.Enumerate((binding.Protocols).GetEnumerator())
                        from __loop36_tmp_item_saml in EnumerableExtensions.Enumerate((__loop36_tmp_item___noname36).GetEnumerator()).OfType<SamlSecurityProtocolBindingElement>()
                        select
                            new
                            {
                                __loop36_item___noname36 = __loop36_tmp_item___noname36,
                                __loop36_item_saml = __loop36_tmp_item_saml,
                            }).ToArray();
                    foreach (var __loop36_item in __loop36_result)
                    {
                        var __noname36 = __loop36_item.__loop36_item___noname36;
                        var saml = __loop36_item.__loop36_item_saml;
                        ++__loop36_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.Write(Generated_GenerateSamlPolicy(saml.AlgorithmSuite, saml.HeaderLayout, saml.ProtectionOrder, saml.TokenVersion, saml.TokenType, saml.TokenIssuer, saml.RequireSignatureConfirmation, saml.Claims));
                        __printer.WriteLine();
                        if (generateMessagePolicy)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.Write(Generated_GenerateAppMessagePolicy(saml));
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    int __loop37_iteration = 0;
                    var __loop37_result =
                        (from __loop37_tmp_item___noname37 in EnumerableExtensions.Enumerate((binding.Protocols).GetEnumerator())
                        from __loop37_tmp_item_sc in EnumerableExtensions.Enumerate((__loop37_tmp_item___noname37).GetEnumerator()).OfType<SecureConversationSecurityProtocolBindingElement>()
                        select
                            new
                            {
                                __loop37_item___noname37 = __loop37_tmp_item___noname37,
                                __loop37_item_sc = __loop37_tmp_item_sc,
                            }).ToArray();
                    foreach (var __loop37_item in __loop37_result)
                    {
                        var __noname37 = __loop37_item.__loop37_item___noname37;
                        var sc = __loop37_item.__loop37_item_sc;
                        ++__loop37_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("<sp:SymmetricBinding>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	<wsp:Policy>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		<sp:ProtectionToken>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("			<wsp:Policy>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("				<sp:SecureConversationToken sp:IncludeToken=\"");
                        __printer.Write(Properties.WsSecurityPolicyNamespace);
                        __printer.WriteTemplateOutput("/IncludeToken/AlwaysToRecipient\">");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("					<wsp:Policy>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("						<sp:BootstrapPolicy>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("							<wsp:Policy>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("							");
                        if (sc.Bootstrap.GetType() == typeof(MutualCertificateBootstrapProtocolBindingElement))
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("								");
                            __printer.Write(Generated_GenerateMutualCertificatePolicy(sc.AlgorithmSuite, sc.HeaderLayout, sc.ProtectionOrder, sc.RequireSignatureConfirmation));
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("							");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("							");
                        if (sc.Bootstrap.GetType() == typeof(StsBootstrapProtocolBindingElement))
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("								");
                            __printer.Write(Generated_GenerateStsPolicy(sc.AlgorithmSuite, sc.HeaderLayout, sc.ProtectionOrder, ((StsBootstrapProtocolBindingElement)sc.Bootstrap).TokenVersion, ((StsBootstrapProtocolBindingElement)sc.Bootstrap).TokenType, ((StsBootstrapProtocolBindingElement)sc.Bootstrap).TokenIssuer, ((StsBootstrapProtocolBindingElement)sc.Bootstrap).RequireSignatureConfirmation, ((StsBootstrapProtocolBindingElement)sc.Bootstrap).DerivedKeys));
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("							");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("							");
                        if (sc.Bootstrap.GetType() == typeof(SamlBootstrapProtocolBindingElement))
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("								");
                            __printer.Write(Generated_GenerateSamlPolicy(sc.AlgorithmSuite, sc.HeaderLayout, sc.ProtectionOrder, ((SamlBootstrapProtocolBindingElement)sc.Bootstrap).TokenVersion, ((SamlBootstrapProtocolBindingElement)sc.Bootstrap).TokenType, ((SamlBootstrapProtocolBindingElement)sc.Bootstrap).TokenIssuer, ((SamlBootstrapProtocolBindingElement)sc.Bootstrap).RequireSignatureConfirmation, ((SamlBootstrapProtocolBindingElement)sc.Bootstrap).Claims));
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("							");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("								");
                        __printer.Write(Generated_GenerateBootstrapMessagePolicy(sc.Bootstrap));
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("							</wsp:Policy>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("						</sp:BootstrapPolicy>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("						");
                        if (sc.DerivedKeys == true)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("						<sp:RequireDerivedKeys/>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("						");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("					</wsp:Policy>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("				</sp:SecureConversationToken>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("			</wsp:Policy>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		</sp:ProtectionToken>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		");
                        __printer.Write(Generated_GenerateGeneralPolicy(sc.AlgorithmSuite, sc.HeaderLayout, sc.ProtectionOrder, false));
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	</wsp:Policy>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("</sp:SymmetricBinding>");
                        __printer.WriteLine();
                        __printer.Write(Generated_GenerateTrustPolicy(false));
                        __printer.WriteLine();
                        if (generateMessagePolicy)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.Write(Generated_GenerateAppMessagePolicy(sc));
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.Write(Generated_GenerateMetroJks());
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateMutualCertificatePolicy(SecurityAlgorithmSuite algorithm, SecurityHeaderLayout layout, SecurityProtectionOrder order, bool signatureConfirmation)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<sp:AsymmetricBinding>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		<sp:InitiatorToken>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			<wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("				<sp:X509Token sp:IncludeToken=\"");
                    __printer.Write(Properties.WsSecurityPolicyNamespace);
                    __printer.WriteTemplateOutput("/IncludeToken/AlwaysToRecipient\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("					<wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("						<sp:WssX509V3Token10/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("					</wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("				</sp:X509Token>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			</wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		</sp:InitiatorToken>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		<sp:RecipientToken>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			<wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("				<sp:X509Token sp:IncludeToken=\"");
                    __printer.Write(Properties.WsSecurityPolicyNamespace);
                    __printer.WriteTemplateOutput("/IncludeToken/Never\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("					<wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("						<sp:WssX509V3Token10/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("					</wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("				</sp:X509Token>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			</wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		</sp:RecipientToken>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		");
                    __printer.Write(Generated_GenerateGeneralPolicy(algorithm, layout, order, true));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	</wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</sp:AsymmetricBinding>");
                    __printer.WriteLine();
                    __printer.Write(Generated_GenerateTrustPolicy(signatureConfirmation));
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateStsPolicy(SecurityAlgorithmSuite algorithm, SecurityHeaderLayout layout, SecurityProtectionOrder order, IssuedTokenVersion version, IssuedTokenType type, IssuedTokenIssuer issuer, bool signatureConfirmation, bool derived)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<sp:SymmetricBinding>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		<sp:ProtectionToken>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			<wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("				");
                    __printer.Write(Generated_GenerateIssuedTokenPolicy(version, type, issuer, derived, null));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			</wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		</sp:ProtectionToken>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		");
                    __printer.Write(Generated_GenerateGeneralPolicy(algorithm, layout, order, false));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	</wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</sp:SymmetricBinding>");
                    __printer.WriteLine();
                    __printer.Write(Generated_GenerateTrustPolicy(signatureConfirmation));
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateSamlPolicy(SecurityAlgorithmSuite algorithm, SecurityHeaderLayout layout, SecurityProtectionOrder order, IssuedTokenVersion version, IssuedTokenType type, IssuedTokenIssuer issuer, bool signatureConfirmation, IList<ClaimsetType> claims)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<sp:AsymmetricBinding>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		<sp:InitiatorToken>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			<wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("				");
                    __printer.Write(Generated_GenerateIssuedTokenPolicy(version, type, issuer, false, claims));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			</wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	    </sp:InitiatorToken>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	    <sp:RecipientToken>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			<wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("				<sp:X509Token sp:IncludeToken=\"");
                    __printer.Write(Properties.WsSecurityPolicyNamespace);
                    __printer.WriteTemplateOutput("/IncludeToken/AlwaysToInitiator\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("					<wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("						<sp:WssX509V3Token10/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("					</wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("				</sp:X509Token>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			</wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		</sp:RecipientToken>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		");
                    __printer.Write(Generated_GenerateGeneralPolicy(algorithm, layout, order, true));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	</wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</sp:AsymmetricBinding>");
                    __printer.WriteLine();
                    __printer.Write(Generated_GenerateTrustPolicy(signatureConfirmation));
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateIssuedTokenPolicy(IssuedTokenVersion version, IssuedTokenType type, IssuedTokenIssuer issuer, bool derived, IEnumerable<ClaimsetType> claims)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<sp:IssuedToken sp:IncludeToken=\"");
                    __printer.Write(Properties.WsSecurityPolicyNamespace);
                    __printer.WriteTemplateOutput("/IncludeToken/AlwaysToRecipient\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<sp:RequestSecurityTokenTemplate>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    if (version == IssuedTokenVersion.Token10)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		<wst:TokenType>urn:oasis:names:tc:SAML:1.0:assertion</wst:TokenType>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    if (version == IssuedTokenVersion.Token11)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		<wst:TokenType>http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV1.1</wst:TokenType>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    if (version == IssuedTokenVersion.Token20)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		<wst:TokenType>urn:oasis:names:tc:SAML:2.0:assertion</wst:TokenType>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    if (type == IssuedTokenType.Symmetric128)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		<wst:KeyType>http://docs.oasis-open.org/ws-sx/ws-trust/200512/SymmetricKey</wst:KeyType>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		<wst:KeySize>128</wst:KeySize>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    if (type == IssuedTokenType.Symmetric192)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		<wst:KeyType>http://docs.oasis-open.org/ws-sx/ws-trust/200512/SymmetricKey</wst:KeyType>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		<wst:KeySize>192</wst:KeySize>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    if (type == IssuedTokenType.Symmetric256)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		<wst:KeyType>http://docs.oasis-open.org/ws-sx/ws-trust/200512/SymmetricKey</wst:KeyType>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		<wst:KeySize>256</wst:KeySize>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    if (type == IssuedTokenType.Asymmetric1024)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		<wst:KeyType>http://docs.oasis-open.org/ws-sx/ws-trust/200512/PublicKey</wst:KeyType>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		<wst:KeySize>1024</wst:KeySize>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    if (type == IssuedTokenType.Asymmetric2048)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		<wst:KeyType>http://docs.oasis-open.org/ws-sx/ws-trust/200512/PublicKey</wst:KeyType>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		<wst:KeySize>2048</wst:KeySize>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    if (type == IssuedTokenType.Asymmetric3072)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		<wst:KeyType>http://docs.oasis-open.org/ws-sx/ws-trust/200512/PublicKey</wst:KeyType>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		<wst:KeySize>3072</wst:KeySize>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    if (claims != null)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		<wst:Claims Dialect=\"http://schemas.xmlsoap.org/ws/2005/05/identity\" xmlns:ic=\"http://schemas.xmlsoap.org/ws/2005/05/identity\">");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("			");
                        int __loop38_iteration = 0;
                        var __loop38_result =
                            (from __loop38_tmp_item___noname38 in EnumerableExtensions.Enumerate((claims).GetEnumerator())
                            from __loop38_tmp_item_claim in EnumerableExtensions.Enumerate((__loop38_tmp_item___noname38).GetEnumerator()).OfType<ClaimsetType>()
                            select
                                new
                                {
                                    __loop38_item___noname38 = __loop38_tmp_item___noname38,
                                    __loop38_item_claim = __loop38_tmp_item_claim,
                                }).ToArray();
                        foreach (var __loop38_item in __loop38_result)
                        {
                            var __noname38 = __loop38_item.__loop38_item___noname38;
                            var claim = __loop38_item.__loop38_item_claim;
                            ++__loop38_iteration;
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("			<ic:ClaimType Uri=\"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/");
                            __printer.Write(claim.Name);
                            __printer.WriteTemplateOutput("\"/>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("			");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		</wst:Claims>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	</sp:RequestSecurityTokenTemplate>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		<sp:RequireInternalReference/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    if (derived == true)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		<sp:RequireDerivedKeys/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	</wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    if (issuer != null)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	<sp:Issuer>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		<wsa:Address>");
                        __printer.Write(issuer.Address);
                        __printer.WriteTemplateOutput("</wsa:Address>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		");
                        if (issuer.MetadataAddress != null && issuer.MetadataAddress.Length > 0)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		<wsa:Metadata>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("			<wsx:Metadata>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("				<wsx:MetadataSection>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("					<wsx:MetadataReference>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("						<wsa:Address>");
                            __printer.Write(issuer.MetadataAddress);
                            __printer.WriteTemplateOutput("</wsa:Address>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("					</wsx:MetadataReference>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("				</wsx:MetadataSection>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("			</wsx:Metadata>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		</wsa:Metadata>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	</sp:Issuer>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</sp:IssuedToken>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateTrustPolicy(bool signatureConfirmation)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<sp:Wss11>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		<sp:MustSupportRefIssuerSerial/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		<sp:MustSupportRefThumbprint/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		<sp:MustSupportRefEncryptedKey/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    if (signatureConfirmation == true)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		<sp:RequireSignatureConfirmation/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	</wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</sp:Wss11>");
                    __printer.WriteLine();
                    if (!Properties.Ibm)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("<sp:Trust13>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	<wsp:Policy>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		<sp:MustSupportIssuedTokens/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		<sp:RequireClientEntropy/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		<sp:RequireServerEntropy/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	</wsp:Policy>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("</sp:Trust13>");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateGeneralPolicy(SecurityAlgorithmSuite algorithm, SecurityHeaderLayout layout, SecurityProtectionOrder order, bool timestamp)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<sp:AlgorithmSuite>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		<sp:");
                    __printer.Write(algorithm.ToString());
                    __printer.WriteTemplateOutput("/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	</wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</sp:AlgorithmSuite>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<sp:Layout>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		<sp:");
                    __printer.Write(layout.ToString());
                    __printer.WriteTemplateOutput("/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	</wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</sp:Layout>");
                    __printer.WriteLine();
                    if (timestamp)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("<sp:IncludeTimestamp/>");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    if (!Properties.XPathSignEncrypt)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("<sp:OnlySignEntireHeadersAndBody/>");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    if (order == SecurityProtectionOrder.EncryptBeforeSign)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("<sp:EncryptBeforeSign/>");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    if (order == SecurityProtectionOrder.SignBeforeEncryptAndEncryptSignature)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("<sp:EncryptSignature/>");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateAppMessagePolicy(SecurityProtocolBindingElement sp)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    if (Properties.XPathSignEncrypt)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.Write(Generated_GenerateAppSignEncryptParts(sp));
                        __printer.WriteLine();
                    }
                    else
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("<sp:EncryptedParts>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	<sp:Body/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("</sp:EncryptedParts>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("<sp:SignedParts>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	<sp:Body/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	<sp:Header Namespace=\"http://www.w3.org/2005/08/addressing\"/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	<sp:Header Namespace=\"http://docs.oasis-open.org/ws-rx/wsrm/200702\"/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("</sp:SignedParts>");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateBootstrapMessagePolicy(BootstrapProtocolBindingElement sp)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    if (Properties.XPathSignEncrypt)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.Write(Generated_GenerateBootstrapSignEncryptParts(sp));
                        __printer.WriteLine();
                    }
                    else
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("<sp:EncryptedParts>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	<sp:Body/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("</sp:EncryptedParts>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("<sp:SignedParts>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	<sp:Body/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	<sp:Header Namespace=\"http://www.w3.org/2005/08/addressing\"/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	<sp:Header Namespace=\"http://docs.oasis-open.org/ws-rx/wsrm/200702\"/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("</sp:SignedParts>");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateWsdlEndpoint(Namespace ns)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<wsdl:definitions targetNamespace=\"");
                    __printer.Write(Generated_GetUri(ns));
                    __printer.WriteTemplateOutput("\"  ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:wsdl=\"http://schemas.xmlsoap.org/wsdl/\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:wsaw=\"http://www.w3.org/2006/05/addressing/wsdl\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:soap=\"http://schemas.xmlsoap.org/wsdl/soap/\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:soap12=\"http://schemas.xmlsoap.org/wsdl/soap12/\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:tns=\"");
                    __printer.Write(Generated_GetUri(ns));
                    __printer.WriteTemplateOutput("\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:");
                    __printer.Write(ns.Prefix);
                    __printer.WriteTemplateOutput("=\"");
                    __printer.Write(Generated_GetUri(ns));
                    __printer.WriteTemplateOutput("\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<wsdl:import location=\"");
                    __printer.Write(ns.FullName);
                    __printer.WriteTemplateOutput("Binding.wsdl\" namespace=\"");
                    __printer.Write(Generated_GetUri(ns));
                    __printer.WriteTemplateOutput("\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    __printer.Write(Generated_GenerateWsdlEndpointPart(ns));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</wsdl:definitions>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateWsdlEndpointPart(Namespace ns)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    int __loop39_iteration = 0;
                    var __loop39_result =
                        (from __loop39_tmp_item___noname39 in EnumerableExtensions.Enumerate((ns.Declarations).GetEnumerator())
                        from __loop39_tmp_item_endpoint in EnumerableExtensions.Enumerate((__loop39_tmp_item___noname39).GetEnumerator()).OfType<Endpoint>()
                        select
                            new
                            {
                                __loop39_item___noname39 = __loop39_tmp_item___noname39,
                                __loop39_item_endpoint = __loop39_tmp_item_endpoint,
                            }).ToArray();
                    foreach (var __loop39_item in __loop39_result)
                    {
                        var __noname39 = __loop39_item.__loop39_item___noname39;
                        var endpoint = __loop39_item.__loop39_item_endpoint;
                        ++__loop39_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.Write(Generated_GenerateWsdlEndpoint(endpoint));
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateWsdlEndpoint(Endpoint endpoint)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<wsdl:service name=\"");
                    __printer.Write(endpoint.Name);
                    __printer.WriteTemplateOutput("\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<wsdl:port name=\"");
                    __printer.Write(endpoint.Interface.Name);
                    __printer.WriteTemplateOutput("_");
                    __printer.Write(endpoint.Binding.Name);
                    __printer.WriteTemplateOutput("_Port\" binding=\"");
                    __printer.Write(endpoint.Binding.Namespace.Prefix);
                    __printer.WriteTemplateOutput(":");
                    __printer.Write(endpoint.Interface.Name);
                    __printer.WriteTemplateOutput("_");
                    __printer.Write(endpoint.Binding.Name);
                    __printer.WriteTemplateOutput("_Binding\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    if (Properties.GenerateServiceUrl)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                        if (endpoint.Binding.Encoding.GetType() == typeof(SoapEncodingBindingElement) && ((SoapEncodingBindingElement)endpoint.Binding.Encoding).Version == SoapVersion.Soap11)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		<soap:address location=\"");
                            __printer.Write(string.Format(Properties.ServiceUrlPattern, endpoint.Name));
                            __printer.WriteTemplateOutput("\"/>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                        if (endpoint.Binding.Encoding.GetType() == typeof(SoapEncodingBindingElement) && ((SoapEncodingBindingElement)endpoint.Binding.Encoding).Version == SoapVersion.Soap12)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		<soap12:address location=\"");
                            __printer.Write(string.Format(Properties.ServiceUrlPattern, endpoint.Name));
                            __printer.WriteTemplateOutput("\"/>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                    }
                    else
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                        if (endpoint.Binding.Encoding.GetType() == typeof(SoapEncodingBindingElement) && ((SoapEncodingBindingElement)endpoint.Binding.Encoding).Version == SoapVersion.Soap11)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		<soap:address location=\"");
                            __printer.Write(endpoint.Address.Uri);
                            __printer.WriteTemplateOutput("\"/>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                        if (endpoint.Binding.Encoding.GetType() == typeof(SoapEncodingBindingElement) && ((SoapEncodingBindingElement)endpoint.Binding.Encoding).Version == SoapVersion.Soap12)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		<soap12:address location=\"");
                            __printer.Write(endpoint.Address.Uri);
                            __printer.WriteTemplateOutput("\"/>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	</wsdl:port>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</wsdl:service>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateAppSignEncryptParts(SecurityProtocolBindingElement sp)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<wsp:Policy wsu:Id=\"request:app_signparts\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<sp:SignedParts>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sp:Body/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sp:Header Namespace=\"http://schemas.xmlsoap.org/ws/2004/08/addressing\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sp:Header Namespace=\"http://www.w3.org/2005/08/addressing\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</sp:SignedParts>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<sp:SignedElements>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sp:XPath>");
                    __printer.Write("/*[namespace-uri()='http://schemas.xmlsoap.org/soap/envelope/' and local-name()='Envelope']/*[namespace-uri()='http://schemas.xmlsoap.org/soap/envelope/' and local-name()='Header']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='Security']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd' and local-name()='Timestamp']");
                    __printer.WriteTemplateOutput("<sp:XPath>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sp:XPath>");
                    __printer.Write("/*[namespace-uri()='http://schemas.xmlsoap.org/soap/envelope/' and local-name()='Envelope']/*[namespace-uri()='http://schemas.xmlsoap.org/soap/envelope/' and local-name()='Header']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='Security']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='UsernameToken']");
                    __printer.WriteTemplateOutput("<sp:XPath>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sp:XPath>");
                    __printer.Write("/*[namespace-uri()='http://www.w3.org/2003/05/soap-envelope' and local-name()='Envelope']/*[namespace-uri()='http://www.w3.org/2003/05/soap-envelope' and local-name()='Header']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='Security']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd' and local-name()='Timestamp']");
                    __printer.WriteTemplateOutput("<sp:XPath>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sp:XPath>");
                    __printer.Write("/*[namespace-uri()='http://www.w3.org/2003/05/soap-envelope' and local-name()='Envelope']/*[namespace-uri()='http://www.w3.org/2003/05/soap-envelope' and local-name()='Header']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='Security']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='UsernameToken']");
                    __printer.WriteTemplateOutput("<sp:XPath>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</sp:SignedElements>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<wsp:Policy wsu:Id=\"request:app_encparts\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<sp:EncryptedParts>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sp:Body/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</sp:EncryptedParts>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<sp:EncryptedElements>");
                    __printer.WriteLine();
                    if (sp.ProtectionOrder == SecurityProtectionOrder.SignBeforeEncryptAndEncryptSignature)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    <sp:XPath>");
                        __printer.Write("/*[namespace-uri()='http://schemas.xmlsoap.org/soap/envelope/' and local-name()='Envelope']/*[namespace-uri()='http://schemas.xmlsoap.org/soap/envelope/' and local-name()='Header']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='Security']/*[namespace-uri()='http://www.w3.org/2000/09/xmldsig#' and local-name()='Signature']");
                        __printer.WriteTemplateOutput("<sp:XPath>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    <sp:XPath>");
                        __printer.Write("/*[namespace-uri()='http://www.w3.org/2003/05/soap-envelope' and local-name()='Envelope']/*[namespace-uri()='http://www.w3.org/2003/05/soap-envelope' and local-name()='Header']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='Security']/*[namespace-uri()='http://www.w3.org/2000/09/xmldsig#' and local-name()='Signature']");
                        __printer.WriteTemplateOutput("<sp:XPath>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    <sp:XPath>");
                        __printer.Write("/*[namespace-uri()='http://schemas.xmlsoap.org/soap/envelope/' and local-name()='Envelope']/*[namespace-uri()='http://schemas.xmlsoap.org/soap/envelope/' and local-name()='Header']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='Security']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='UsernameToken']");
                        __printer.WriteTemplateOutput("<sp:XPath>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    <sp:XPath>");
                        __printer.Write("/*[namespace-uri()='http://www.w3.org/2003/05/soap-envelope' and local-name()='Envelope']/*[namespace-uri()='http://www.w3.org/2003/05/soap-envelope' and local-name()='Header']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='Security']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='UsernameToken']");
                        __printer.WriteTemplateOutput("<sp:XPath>");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</sp:EncryptedElements>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<wsp:Policy wsu:Id=\"response:app_signparts\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<sp:SignedParts>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sp:Body/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sp:Header Namespace=\"http://schemas.xmlsoap.org/ws/2004/08/addressing\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sp:Header Namespace=\"http://www.w3.org/2005/08/addressing\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</sp:SignedParts>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<sp:SignedElements>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sp:XPath>");
                    __printer.Write("/*[namespace-uri()='http://schemas.xmlsoap.org/soap/envelope/' and local-name()='Envelope']/*[namespace-uri()='http://schemas.xmlsoap.org/soap/envelope/' and local-name()='Header']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='Security']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd' and local-name()='Timestamp']");
                    __printer.WriteTemplateOutput("<sp:XPath>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sp:XPath>");
                    __printer.Write("/*[namespace-uri()='http://www.w3.org/2003/05/soap-envelope' and local-name()='Envelope']/*[namespace-uri()='http://www.w3.org/2003/05/soap-envelope' and local-name()='Header']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='Security']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd' and local-name()='Timestamp']");
                    __printer.WriteTemplateOutput("<sp:XPath>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</sp:SignedElements>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<wsp:Policy wsu:Id=\"response:app_encparts\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<sp:EncryptedParts>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sp:Body/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</sp:EncryptedParts>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<sp:EncryptedElements>");
                    __printer.WriteLine();
                    if (sp.ProtectionOrder == SecurityProtectionOrder.SignBeforeEncryptAndEncryptSignature)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    <sp:XPath>");
                        __printer.Write("/*[namespace-uri()='http://schemas.xmlsoap.org/soap/envelope/' and local-name()='Envelope']/*[namespace-uri()='http://schemas.xmlsoap.org/soap/envelope/' and local-name()='Header']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='Security']/*[namespace-uri()='http://www.w3.org/2000/09/xmldsig#' and local-name()='Signature']");
                        __printer.WriteTemplateOutput("<sp:XPath>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    <sp:XPath>");
                        __printer.Write("/*[namespace-uri()='http://www.w3.org/2003/05/soap-envelope' and local-name()='Envelope']/*[namespace-uri()='http://www.w3.org/2003/05/soap-envelope' and local-name()='Header']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='Security']/*[namespace-uri()='http://www.w3.org/2000/09/xmldsig#' and local-name()='Signature']");
                        __printer.WriteTemplateOutput("<sp:XPath>");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</sp:EncryptedElements>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</wsp:Policy>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateBootstrapSignEncryptParts(BootstrapProtocolBindingElement sp)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<wsp:Policy wsu:Id=\"request:bootstrap_signparts\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<sp:SignedParts>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sp:Body/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sp:Header Namespace=\"http://schemas.xmlsoap.org/ws/2004/08/addressing\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sp:Header Namespace=\"http://www.w3.org/2005/08/addressing\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</sp:SignedParts>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<sp:SignedElements>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sp:XPath>");
                    __printer.Write("/*[namespace-uri()='http://schemas.xmlsoap.org/soap/envelope/' and local-name()='Envelope']/*[namespace-uri()='http://schemas.xmlsoap.org/soap/envelope/' and local-name()='Header']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='Security']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd' and local-name()='Timestamp']");
                    __printer.WriteTemplateOutput("</sp:XPath>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sp:XPath>");
                    __printer.Write("/*[namespace-uri()='http://www.w3.org/2003/05/soap-envelope' and local-name()='Envelope']/*[namespace-uri()='http://www.w3.org/2003/05/soap-envelope' and local-name()='Header']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='Security']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd' and local-name()='Timestamp']");
                    __printer.WriteTemplateOutput("</sp:XPath>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</sp:SignedElements>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<wsp:Policy wsu:Id=\"request:bootstrap_encparts\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<sp:EncryptedParts>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sp:Body/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</sp:EncryptedParts>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<sp:EncryptedElements>");
                    __printer.WriteLine();
                    if (sp.ProtectionOrder == SecurityProtectionOrder.SignBeforeEncryptAndEncryptSignature)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    <sp:XPath>");
                        __printer.Write("/*[namespace-uri()='http://schemas.xmlsoap.org/soap/envelope/' and local-name()='Envelope']/*[namespace-uri()='http://schemas.xmlsoap.org/soap/envelope/' and local-name()='Header']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='Security']/*[namespace-uri()='http://www.w3.org/2000/09/xmldsig#' and local-name()='Signature']");
                        __printer.WriteTemplateOutput("</sp:XPath>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    <sp:XPath>");
                        __printer.Write("/*[namespace-uri()='http://www.w3.org/2003/05/soap-envelope' and local-name()='Envelope']/*[namespace-uri()='http://www.w3.org/2003/05/soap-envelope' and local-name()='Header']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='Security']/*[namespace-uri()='http://www.w3.org/2000/09/xmldsig#' and local-name()='Signature']");
                        __printer.WriteTemplateOutput("</sp:XPath>");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</sp:EncryptedElements>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<wsp:Policy wsu:Id=\"response:bootstrap_signparts\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<sp:SignedParts>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sp:Body/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sp:Header Namespace=\"http://schemas.xmlsoap.org/ws/2004/08/addressing\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sp:Header Namespace=\"http://www.w3.org/2005/08/addressing\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</sp:SignedParts>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<sp:SignedElements>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sp:XPath>");
                    __printer.Write("/*[namespace-uri()='http://schemas.xmlsoap.org/soap/envelope/' and local-name()='Envelope']/*[namespace-uri()='http://schemas.xmlsoap.org/soap/envelope/' and local-name()='Header']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='Security']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd' and local-name()='Timestamp']");
                    __printer.WriteTemplateOutput("</sp:XPath>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sp:XPath>");
                    __printer.Write("/*[namespace-uri()='http://www.w3.org/2003/05/soap-envelope' and local-name()='Envelope']/*[namespace-uri()='http://www.w3.org/2003/05/soap-envelope' and local-name()='Header']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='Security']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd' and local-name()='Timestamp']");
                    __printer.WriteTemplateOutput("</sp:XPath>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</sp:SignedElements>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<wsp:Policy wsu:Id=\"response:bootstrap_encparts\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<sp:EncryptedParts>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sp:Body/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</sp:EncryptedParts>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<sp:EncryptedElements>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sp:XPath>");
                    __printer.Write("/*[namespace-uri()='http://schemas.xmlsoap.org/soap/envelope/' and local-name()='Envelope']/*[namespace-uri()='http://schemas.xmlsoap.org/soap/envelope/' and local-name()='Header']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='Security']/*[namespace-uri()='http://docs.oasis-open.org/wss/oasis-wss-wssecurity-secext-1.1.xsd' and local-name()='SignatureConfirmation']");
                    __printer.WriteTemplateOutput("</sp:XPath>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sp:XPath>");
                    __printer.Write("/*[namespace-uri()='http://www.w3.org/2003/05/soap-envelope' and local-name()='Envelope']/*[namespace-uri()='http://www.w3.org/2003/05/soap-envelope' and local-name()='Header']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='Security']/*[namespace-uri()='http://docs.oasis-open.org/wss/oasis-wss-wssecurity-secext-1.1.xsd' and local-name()='SignatureConfirmation']");
                    __printer.WriteTemplateOutput("</sp:XPath>");
                    __printer.WriteLine();
                    if (sp.ProtectionOrder == SecurityProtectionOrder.SignBeforeEncryptAndEncryptSignature)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    <sp:XPath>");
                        __printer.Write("/*[namespace-uri()='http://schemas.xmlsoap.org/soap/envelope/' and local-name()='Envelope']/*[namespace-uri()='http://schemas.xmlsoap.org/soap/envelope/' and local-name()='Header']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='Security']/*[namespace-uri()='http://www.w3.org/2000/09/xmldsig#' and local-name()='Signature']");
                        __printer.WriteTemplateOutput("</sp:XPath>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    <sp:XPath>");
                        __printer.Write("/*[namespace-uri()='http://www.w3.org/2003/05/soap-envelope' and local-name()='Envelope']/*[namespace-uri()='http://www.w3.org/2003/05/soap-envelope' and local-name()='Header']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='Security']/*[namespace-uri()='http://www.w3.org/2000/09/xmldsig#' and local-name()='Signature']");
                        __printer.WriteTemplateOutput("</sp:XPath>");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</sp:EncryptedElements>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</wsp:Policy>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            #endregion
                #region functions from "k:\VersionControl\soal-oslo\Src\Main\SoaGeneratorLib\GeneratorLib.mcg"
                public string Generated_FirstLetterLow(string s)
                {
                    return s.Substring(0, 1).ToLower() + s.Substring(1);
                }
                
                public string Generated_FirstLetterUp(string s)
                {
                    return s.Substring(0, 1).ToUpper() + s.Substring(1);
                }
                
                public string Generated_GetUri(Namespace ns)
                {
                    return GeneratorLibExtensions.GetUri(ns);
                }
                
                public string Generated_GetUriWithSlash(Namespace ns)
                {
                    return GeneratorLibExtensions.GetUriWithSlash(ns);
                }
                
                public string Generated_GetPackage(Namespace ns)
                {
                    return GeneratorLibExtensions.GetPackage(ns);
                }
                
                public string Generated_IsNillableType(Type t)
                {
                    if (t is BuiltInType)
                    {
                        if (t == BuiltInType.String)
                        {
                            return "true";
                        }
                        else
                        {
                            return "false";
                        }
                    }
                    else if (t is EnumType)
                    {
                        return "false";
                    }
                    else
                    {
                        return "true";
                    }
                }
                
                #endregion
            }
        }
        
