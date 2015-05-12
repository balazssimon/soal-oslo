using OsloExtensions;
using OsloExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoaMetaModel
{
    // The main file of the generator.
    public partial class JavaGenerator : Generator<IEnumerable<SoaObject>, GeneratorContext>
    {
        
        public JavaGenerator(IEnumerable<SoaObject> instances, GeneratorContext context)
            : base(instances, context)
        {
            this.Properties = new PropertyGroup_Properties();
        }
        
            #region functions from "C:\Users\Balazs\Documents\Visual Studio 2013\Projects\SoaMM\SoaGeneratorLib\JavaGenerator.mcg"
            public PropertyGroup_Properties Properties { get; private set; }
            
            public class PropertyGroup_Properties
            {
                public PropertyGroup_Properties()
                {
                    this.WsdlDirectory = "WEB-INF/wsdl/";
                    this.WsdlSuffix = "Endpoint";
                    this.SeparateWsdlsForEndpoints = false;
                    this.NoImplementationDelegates = true;
                    this.ThrowNotImplementedException = true;
                    this.GenerateProxyFeatureConstructors = false;
                    this.GenerateImplementationBase = false;
                    this.GenerateOracleAnnotations = false;
                    this.GenerateServerStubs = true;
                    this.GenerateClientProxies = true;
                }
                
                public string WsdlDirectory { get; set; }
                public string WsdlSuffix { get; set; }
                public bool SeparateWsdlsForEndpoints { get; set; }
                public bool NoImplementationDelegates { get; set; }
                public bool ThrowNotImplementedException { get; set; }
                public bool GenerateProxyFeatureConstructors { get; set; }
                public bool GenerateImplementationBase { get; set; }
                public bool GenerateOracleAnnotations { get; set; }
                public bool GenerateServerStubs { get; set; }
                public bool GenerateClientProxies { get; set; }
            }
            
            public override void Generated_Main()
            {
            }
            
            public string Generated_NamespaceToPath(Namespace ns)
            {
                return Generated_GetPackage(ns).Replace('.', '/').ToLower();
            }
            
            public void Generated_GenerateJavaCode(string rootDirectory)
            {
                rootDirectory = rootDirectory + "/";
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
                        Context.CreateFolder(rootDirectory + Generated_NamespaceToPath(ns));
                    }
                }
                int __loop3_iteration = 0;
                var __loop3_result =
                    (from __loop3_tmp_item___noname3 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                    from __loop3_tmp_item_ns in EnumerableExtensions.Enumerate((__loop3_tmp_item___noname3).GetEnumerator()).OfType<Namespace>()
                    select
                        new
                        {
                            __loop3_item___noname3 = __loop3_tmp_item___noname3,
                            __loop3_item_ns = __loop3_tmp_item_ns,
                        }).ToArray();
                foreach (var __loop3_item in __loop3_result)
                {
                    var __noname3 = __loop3_item.__loop3_item___noname3;
                    var ns = __loop3_item.__loop3_item_ns;
                    ++__loop3_iteration;
                    int i = 0;
                    int __loop4_iteration = 0;
                    var __loop4_result =
                        (from __loop4_tmp_item___noname4 in EnumerableExtensions.Enumerate((ns.Declarations).GetEnumerator())
                        from __loop4_tmp_item_d in EnumerableExtensions.Enumerate((__loop4_tmp_item___noname4).GetEnumerator()).OfType<Namespace>()
                        select
                            new
                            {
                                __loop4_item___noname4 = __loop4_tmp_item___noname4,
                                __loop4_item_d = __loop4_tmp_item_d,
                            }).ToArray();
                    foreach (var __loop4_item in __loop4_result)
                    {
                        var __noname4 = __loop4_item.__loop4_item___noname4;
                        var d = __loop4_item.__loop4_item_d;
                        ++__loop4_iteration;
                        i = i + 1;
                    }
                    if (ns.Declarations.Count > i)
                    {
                        Context.SetOutput(rootDirectory + Generated_NamespaceToPath(ns) + "/package-info.java");
                        Context.Output(Generated_GeneratePackageInfo(ns));
                        Context.SetOutput(rootDirectory + Generated_NamespaceToPath(ns) + "/ObjectFactory.java");
                        Context.Output(Generated_GenerateObjectFactory(ns));
                        if (!Properties.NoImplementationDelegates)
                        {
                            Context.SetOutput(rootDirectory + Generated_NamespaceToPath(ns) + "/ExpressionHelper.java");
                            Context.Output(Generated_GenerateExpressionHelper(ns));
                        }
                        int __loop5_iteration = 0;
                        var __loop5_result =
                            (from __loop5_tmp_item___noname5 in EnumerableExtensions.Enumerate((ns.Declarations).GetEnumerator())
                            from __loop5_tmp_item_en in EnumerableExtensions.Enumerate((__loop5_tmp_item___noname5).GetEnumerator()).OfType<EnumType>()
                            select
                                new
                                {
                                    __loop5_item___noname5 = __loop5_tmp_item___noname5,
                                    __loop5_item_en = __loop5_tmp_item_en,
                                }).ToArray();
                        foreach (var __loop5_item in __loop5_result)
                        {
                            var __noname5 = __loop5_item.__loop5_item___noname5;
                            var en = __loop5_item.__loop5_item_en;
                            ++__loop5_iteration;
                            Context.SetOutput(rootDirectory + Generated_NamespaceToPath(ns) + "/" + en.Name + ".java");
                            Context.Output(Generated_GenerateEnum(en));
                        }
                        int __loop6_iteration = 0;
                        var __loop6_result =
                            (from __loop6_tmp_item___noname6 in EnumerableExtensions.Enumerate((ns.Declarations).GetEnumerator())
                            from __loop6_tmp_item_st in EnumerableExtensions.Enumerate((__loop6_tmp_item___noname6).GetEnumerator()).OfType<StructType>()
                            select
                                new
                                {
                                    __loop6_item___noname6 = __loop6_tmp_item___noname6,
                                    __loop6_item_st = __loop6_tmp_item_st,
                                }).ToArray();
                        foreach (var __loop6_item in __loop6_result)
                        {
                            var __noname6 = __loop6_item.__loop6_item___noname6;
                            var st = __loop6_item.__loop6_item_st;
                            ++__loop6_iteration;
                            Context.SetOutput(rootDirectory + Generated_NamespaceToPath(ns) + "/" + st.Name + ".java");
                            Context.Output(Generated_GenerateStruct(st));
                        }
                        int __loop7_iteration = 0;
                        var __loop7_result =
                            (from __loop7_tmp_item___noname7 in EnumerableExtensions.Enumerate((ns.Declarations).GetEnumerator())
                            from __loop7_tmp_item_cs in EnumerableExtensions.Enumerate((__loop7_tmp_item___noname7).GetEnumerator()).OfType<ClaimsetType>()
                            select
                                new
                                {
                                    __loop7_item___noname7 = __loop7_tmp_item___noname7,
                                    __loop7_item_cs = __loop7_tmp_item_cs,
                                }).ToArray();
                        foreach (var __loop7_item in __loop7_result)
                        {
                            var __noname7 = __loop7_item.__loop7_item___noname7;
                            var cs = __loop7_item.__loop7_item_cs;
                            ++__loop7_iteration;
                            Context.SetOutput(rootDirectory + Generated_NamespaceToPath(ns) + "/" + cs.Name + ".java");
                            Context.Output(Generated_GenerateClaimset(cs));
                        }
                        if (!Properties.NoImplementationDelegates)
                        {
                            Context.SetOutput(rootDirectory + Generated_NamespaceToPath(ns) + "/SoaMMFault.java");
                            Context.Output(Generated_GenerateSoaMMFault(ns));
                            Context.SetOutput(rootDirectory + Generated_NamespaceToPath(ns) + "/SoaMMException.java");
                            Context.Output(Generated_GenerateSoaMMException(ns));
                        }
                        int __loop8_iteration = 0;
                        var __loop8_result =
                            (from __loop8_tmp_item___noname8 in EnumerableExtensions.Enumerate((ns.Declarations).GetEnumerator())
                            from __loop8_tmp_item_ex in EnumerableExtensions.Enumerate((__loop8_tmp_item___noname8).GetEnumerator()).OfType<ExceptionType>()
                            select
                                new
                                {
                                    __loop8_item___noname8 = __loop8_tmp_item___noname8,
                                    __loop8_item_ex = __loop8_tmp_item_ex,
                                }).ToArray();
                        foreach (var __loop8_item in __loop8_result)
                        {
                            var __noname8 = __loop8_item.__loop8_item___noname8;
                            var ex = __loop8_item.__loop8_item_ex;
                            ++__loop8_iteration;
                            Context.SetOutput(rootDirectory + Generated_NamespaceToPath(ns) + "/" + ex.Name + "Fault.java");
                            Context.Output(Generated_GenerateException(ex));
                            Context.SetOutput(rootDirectory + Generated_NamespaceToPath(ns) + "/" + ex.Name + ".java");
                            Context.Output(Generated_GenerateOperationException(ex));
                        }
                        int __loop9_iteration = 0;
                        var __loop9_result =
                            (from __loop9_tmp_item___noname9 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                            from __loop9_tmp_item_ar in EnumerableExtensions.Enumerate((__loop9_tmp_item___noname9).GetEnumerator()).OfType<ArrayType>()
                            select
                                new
                                {
                                    __loop9_item___noname9 = __loop9_tmp_item___noname9,
                                    __loop9_item_ar = __loop9_tmp_item_ar,
                                }).ToArray();
                        foreach (var __loop9_item in __loop9_result)
                        {
                            var __noname9 = __loop9_item.__loop9_item___noname9;
                            var ar = __loop9_item.__loop9_item_ar;
                            ++__loop9_iteration;
                            if (ar.ItemType is NullableType)
                            {
                                Context.SetOutput(rootDirectory + Generated_NamespaceToPath(ns) + "/ArrayOfNullable" + Generated_FirstLetterUp(ar.ItemType.Name) + ".java");
                                Context.Output(Generated_GenerateNullableArray(ar, ns));
                            }
                            else
                            {
                                Context.SetOutput(rootDirectory + Generated_NamespaceToPath(ns) + "/ArrayOf" + Generated_FirstLetterUp(ar.ItemType.Name) + ".java");
                                Context.Output(Generated_GenerateArray(ar, ns));
                            }
                        }
                        int __loop10_iteration = 0;
                        var __loop10_result =
                            (from __loop10_tmp_item___noname10 in EnumerableExtensions.Enumerate((ns.Declarations).GetEnumerator())
                            from __loop10_tmp_item_intf in EnumerableExtensions.Enumerate((__loop10_tmp_item___noname10).GetEnumerator()).OfType<Interface>()
                            select
                                new
                                {
                                    __loop10_item___noname10 = __loop10_tmp_item___noname10,
                                    __loop10_item_intf = __loop10_tmp_item_intf,
                                }).ToArray();
                        foreach (var __loop10_item in __loop10_result)
                        {
                            var __noname10 = __loop10_item.__loop10_item___noname10;
                            var intf = __loop10_item.__loop10_item_intf;
                            ++__loop10_iteration;
                            int __loop11_iteration = 0;
                            var __loop11_result =
                                (from __loop11_tmp_item___noname11 in EnumerableExtensions.Enumerate((intf.Operations).GetEnumerator())
                                from __loop11_tmp_item_op in EnumerableExtensions.Enumerate((__loop11_tmp_item___noname11).GetEnumerator()).OfType<Operation>()
                                select
                                    new
                                    {
                                        __loop11_item___noname11 = __loop11_tmp_item___noname11,
                                        __loop11_item_op = __loop11_tmp_item_op,
                                    }).ToArray();
                            foreach (var __loop11_item in __loop11_result)
                            {
                                var __noname11 = __loop11_item.__loop11_item___noname11;
                                var op = __loop11_item.__loop11_item_op;
                                ++__loop11_iteration;
                                Context.SetOutput(rootDirectory + Generated_NamespaceToPath(ns) + "/" + Generated_FirstLetterUp(op.Name) + ".java");
                                Context.Output(Generated_GenerateOperationType(op));
                                if (op.ReturnType != PseudoType.Async)
                                {
                                    Context.SetOutput(rootDirectory + Generated_NamespaceToPath(ns) + "/" + Generated_FirstLetterUp(op.Name) + "Response.java");
                                    Context.Output(Generated_GenerateOperationResponseType(op));
                                }
                            }
                            Context.SetOutput(rootDirectory + Generated_NamespaceToPath(ns) + "/" + intf.Name + ".java");
                            Context.Output(Generated_GenerateInterface(intf));
                            if (Properties.GenerateImplementationBase)
                            {
                                Context.SetOutput(rootDirectory + Generated_NamespaceToPath(ns) + "/" + intf.Name.Substring(1) + "Base.java");
                                Context.Output(Generated_GenerateImplementationBase(intf));
                            }
                        }
                        int __loop12_iteration = 0;
                        var __loop12_result =
                            (from __loop12_tmp_item___noname12 in EnumerableExtensions.Enumerate((ns.Declarations).GetEnumerator())
                            from __loop12_tmp_item_endp in EnumerableExtensions.Enumerate((__loop12_tmp_item___noname12).GetEnumerator()).OfType<Endpoint>()
                            select
                                new
                                {
                                    __loop12_item___noname12 = __loop12_tmp_item___noname12,
                                    __loop12_item_endp = __loop12_tmp_item_endp,
                                }).ToArray();
                        foreach (var __loop12_item in __loop12_result)
                        {
                            var __noname12 = __loop12_item.__loop12_item___noname12;
                            var endp = __loop12_item.__loop12_item_endp;
                            ++__loop12_iteration;
                            if (Properties.GenerateServerStubs)
                            {
                                if (!Properties.NoImplementationDelegates)
                                {
                                    Context.SetOutput(rootDirectory + Generated_NamespaceToPath(ns) + "/" + endp.Name + ".java");
                                    Context.Output(Generated_GenerateService(endp));
                                    Context.SetOutput(rootDirectory + Generated_NamespaceToPath(ns) + "/" + endp.Name + "Impl.java");
                                    Context.Output(Generated_GenerateImpl(endp));
                                }
                                else
                                {
                                    Context.SetOutput(rootDirectory + Generated_NamespaceToPath(ns) + "/" + endp.Name + ".java");
                                    Context.Output(Generated_GenerateImpl(endp));
                                }
                            }
                            if (Properties.GenerateClientProxies)
                            {
                                Context.SetOutput(rootDirectory + Generated_NamespaceToPath(ns) + "/" + endp.Name + "Service.java");
                                Context.Output(Generated_GenerateProxy(endp));
                            }
                        }
                        if (!Properties.NoImplementationDelegates)
                        {
                            int __loop13_iteration = 0;
                            var __loop13_result =
                                (from __loop13_tmp_item___noname13 in EnumerableExtensions.Enumerate((ns.Declarations).GetEnumerator())
                                from __loop13_tmp_item_auth in EnumerableExtensions.Enumerate((__loop13_tmp_item___noname13).GetEnumerator()).OfType<Authorization>()
                                select
                                    new
                                    {
                                        __loop13_item___noname13 = __loop13_tmp_item___noname13,
                                        __loop13_item_auth = __loop13_tmp_item_auth,
                                    }).ToArray();
                            foreach (var __loop13_item in __loop13_result)
                            {
                                var __noname13 = __loop13_item.__loop13_item___noname13;
                                var auth = __loop13_item.__loop13_item_auth;
                                ++__loop13_iteration;
                                Context.SetOutput(rootDirectory + Generated_NamespaceToPath(ns) + "/" + auth.Name + ".java");
                                Context.Output(Generated_GenerateAuth(auth));
                            }
                            int __loop14_iteration = 0;
                            var __loop14_result =
                                (from __loop14_tmp_item___noname14 in EnumerableExtensions.Enumerate((ns.Declarations).GetEnumerator())
                                from __loop14_tmp_item_con in EnumerableExtensions.Enumerate((__loop14_tmp_item___noname14).GetEnumerator()).OfType<Contract>()
                                select
                                    new
                                    {
                                        __loop14_item___noname14 = __loop14_tmp_item___noname14,
                                        __loop14_item_con = __loop14_tmp_item_con,
                                    }).ToArray();
                            foreach (var __loop14_item in __loop14_result)
                            {
                                var __noname14 = __loop14_item.__loop14_item___noname14;
                                var con = __loop14_item.__loop14_item_con;
                                ++__loop14_iteration;
                                Context.SetOutput(rootDirectory + Generated_NamespaceToPath(ns) + "/" + con.Name + ".java");
                                Context.Output(Generated_GenerateContract(con));
                            }
                        }
                    }
                }
            }
            
            public String Generated_PrintType(Type type)
            {
                if (type == PseudoType.Void || type == PseudoType.Async)
                {
                    return "void";
                }
                else if (type is BuiltInType)
                {
                    if (type == BuiltInType.Bool)
                    {
                        return "boolean";
                    }
                    else if (type == BuiltInType.String || type == BuiltInType.Guid)
                    {
                        return "String";
                    }
                    else if (type == BuiltInType.Date || type == BuiltInType.DateTime || type == BuiltInType.DateTime)
                    {
                        return "javax.xml.datatype.XMLGregorianCalendar";
                    }
                    else if (type == BuiltInType.TimeSpan)
                    {
                        return "javax.xml.datatype.Duration";
                    }
                    else if (type == BuiltInType.Byte || type == BuiltInType.Int || type == BuiltInType.Long || type == BuiltInType.Float || type == BuiltInType.Double)
                    {
                        return type.Name;
                    }
                }
                else if (type is StructType || type is EnumType || type is ExceptionType)
                {
                    return Generated_GetPackage(type.Namespace).ToLower() + "." + Generated_FirstLetterUp(type.Name);
                }
                else if (type is ArrayType)
                {
                    if (((ArrayType)type).ItemType is NullableType)
                    {
                        return "ArrayOfNullable" + Generated_FirstLetterUp(((ArrayType)type).ItemType.Name);
                    }
                    else if (((ArrayType)type).ItemType == BuiltInType.Byte)
                    {
                        return "byte[]";
                    }
                    else
                    {
                        return "ArrayOf" + Generated_FirstLetterUp(((ArrayType)type).ItemType.Name);
                    }
                }
                else if (type is NullableType)
                {
                    return Generated_PrintClassType(((NullableType)type).InnerType);
                }
                return "";
            }
            
            public String Generated_PrintClassType(Type type)
            {
                if (type is BuiltInType)
                {
                    if (type == BuiltInType.Bool)
                    {
                        return "Boolean";
                    }
                    else if (type == BuiltInType.String || type == BuiltInType.Guid)
                    {
                        return "String";
                    }
                    else if (type == BuiltInType.Date || type == BuiltInType.DateTime || type == BuiltInType.DateTime)
                    {
                        return "javax.xml.datatype.XMLGregorianCalendar";
                    }
                    else if (type == BuiltInType.TimeSpan)
                    {
                        return "javax.xml.datatype.Duration";
                    }
                    else if (type == BuiltInType.Int)
                    {
                        return "Integer";
                    }
                    else if (type == BuiltInType.Byte || type == BuiltInType.Long || type == BuiltInType.Float || type == BuiltInType.Double)
                    {
                        return Generated_FirstLetterUp(type.Name);
                    }
                }
                else
                {
                    return Generated_PrintType(type);
                }
                return "";
            }
            
            public List<string> Generated_GeneratePackageInfo(Namespace ns)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("@javax.xml.bind.annotation.XmlSchema(namespace = \"");
                    __printer.Write(Generated_GetUri(ns));
                    __printer.WriteTemplateOutput("\", elementFormDefault = javax.xml.bind.annotation.XmlNsForm.QUALIFIED)");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("package ");
                    __printer.Write(Generated_GetPackage(ns).ToLower());
                    __printer.WriteTemplateOutput(";");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateObjectFactory(Namespace ns)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("package ");
                    __printer.Write(Generated_GetPackage(ns).ToLower());
                    __printer.WriteTemplateOutput(";");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.bind.JAXBElement;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.bind.annotation.XmlElementDecl;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.bind.annotation.XmlRegistry;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.namespace.QName;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("@XmlRegistry");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("public class ObjectFactory {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    public ObjectFactory() {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                    int __loop15_iteration = 0;
                    var __loop15_result =
                        (from __loop15_tmp_item___noname15 in EnumerableExtensions.Enumerate((ns.Declarations).GetEnumerator())
                        from __loop15_tmp_item_type in EnumerableExtensions.Enumerate((__loop15_tmp_item___noname15).GetEnumerator()).OfType<StructType>()
                        select
                            new
                            {
                                __loop15_item___noname15 = __loop15_tmp_item___noname15,
                                __loop15_item_type = __loop15_tmp_item_type,
                            }).ToArray();
                    foreach (var __loop15_item in __loop15_result)
                    {
                        var __noname15 = __loop15_item.__loop15_item___noname15;
                        var type = __loop15_item.__loop15_item_type;
                        ++__loop15_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    private final static QName _");
                        __printer.Write(Generated_FirstLetterUp(type.Name));
                        __printer.WriteTemplateOutput("_QNAME = new QName(\"");
                        __printer.Write(Generated_GetUri(ns));
                        __printer.WriteTemplateOutput("\",\"");
                        __printer.Write(type.Name);
                        __printer.WriteTemplateOutput("\");");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    public ");
                        __printer.Write(Generated_GetPackage(ns).ToLower());
                        __printer.WriteTemplateOutput(".");
                        __printer.Write(Generated_FirstLetterUp(type.Name));
                        __printer.WriteTemplateOutput(" create");
                        __printer.Write(Generated_FirstLetterUp(type.Name));
                        __printer.WriteTemplateOutput("() {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        return new ");
                        __printer.Write(Generated_GetPackage(ns).ToLower());
                        __printer.WriteTemplateOutput(".");
                        __printer.Write(type.Name);
                        __printer.WriteTemplateOutput("();");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    }");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    @XmlElementDecl(namespace = \"");
                        __printer.Write(Generated_GetUri(ns));
                        __printer.WriteTemplateOutput("\", name = \"");
                        __printer.Write(type.Name);
                        __printer.WriteTemplateOutput("\")");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    public JAXBElement<");
                        __printer.Write(Generated_GetPackage(ns).ToLower());
                        __printer.WriteTemplateOutput(".");
                        __printer.Write(type.Name);
                        __printer.WriteTemplateOutput("> create");
                        __printer.Write(Generated_FirstLetterUp(type.Name));
                        __printer.WriteTemplateOutput("(");
                        __printer.Write(Generated_GetPackage(ns).ToLower());
                        __printer.WriteTemplateOutput(".");
                        __printer.Write(type.Name);
                        __printer.WriteTemplateOutput(" value) {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        return new JAXBElement<");
                        __printer.Write(Generated_GetPackage(ns).ToLower());
                        __printer.WriteTemplateOutput(".");
                        __printer.Write(type.Name);
                        __printer.WriteTemplateOutput(">(_");
                        __printer.Write(Generated_FirstLetterUp(type.Name));
                        __printer.WriteTemplateOutput("_QNAME, ");
                        __printer.Write(Generated_GetPackage(ns).ToLower());
                        __printer.WriteTemplateOutput(".");
                        __printer.Write(type.Name);
                        __printer.WriteTemplateOutput(".class, null, value);");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    }");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    int __loop16_iteration = 0;
                    var __loop16_result =
                        (from __loop16_tmp_item___noname16 in EnumerableExtensions.Enumerate((ns.Declarations).GetEnumerator())
                        from __loop16_tmp_item_type in EnumerableExtensions.Enumerate((__loop16_tmp_item___noname16).GetEnumerator()).OfType<EnumType>()
                        select
                            new
                            {
                                __loop16_item___noname16 = __loop16_tmp_item___noname16,
                                __loop16_item_type = __loop16_tmp_item_type,
                            }).ToArray();
                    foreach (var __loop16_item in __loop16_result)
                    {
                        var __noname16 = __loop16_item.__loop16_item___noname16;
                        var type = __loop16_item.__loop16_item_type;
                        ++__loop16_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    private final static QName _");
                        __printer.Write(Generated_FirstLetterUp(type.Name));
                        __printer.WriteTemplateOutput("_QNAME = new QName(\"");
                        __printer.Write(Generated_GetUri(ns));
                        __printer.WriteTemplateOutput("\",\"");
                        __printer.Write(type.Name);
                        __printer.WriteTemplateOutput("\");");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    @XmlElementDecl(namespace = \"");
                        __printer.Write(Generated_GetUri(ns));
                        __printer.WriteTemplateOutput("\", name = \"");
                        __printer.Write(type.Name);
                        __printer.WriteTemplateOutput("\")");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    public JAXBElement<");
                        __printer.Write(Generated_GetPackage(ns).ToLower());
                        __printer.WriteTemplateOutput(".");
                        __printer.Write(type.Name);
                        __printer.WriteTemplateOutput("> create");
                        __printer.Write(Generated_FirstLetterUp(type.Name));
                        __printer.WriteTemplateOutput("(");
                        __printer.Write(Generated_GetPackage(ns).ToLower());
                        __printer.WriteTemplateOutput(".");
                        __printer.Write(type.Name);
                        __printer.WriteTemplateOutput(" value) {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        return new JAXBElement<");
                        __printer.Write(Generated_GetPackage(ns).ToLower());
                        __printer.WriteTemplateOutput(".");
                        __printer.Write(type.Name);
                        __printer.WriteTemplateOutput(">(_");
                        __printer.Write(Generated_FirstLetterUp(type.Name));
                        __printer.WriteTemplateOutput("_QNAME, ");
                        __printer.Write(Generated_GetPackage(ns).ToLower());
                        __printer.WriteTemplateOutput(".");
                        __printer.Write(type.Name);
                        __printer.WriteTemplateOutput(".class, null, value);");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    }");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    int __loop17_iteration = 0;
                    var __loop17_result =
                        (from __loop17_tmp_item___noname17 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                        from __loop17_tmp_item_type in EnumerableExtensions.Enumerate((__loop17_tmp_item___noname17).GetEnumerator()).OfType<ArrayType>()
                        select
                            new
                            {
                                __loop17_item___noname17 = __loop17_tmp_item___noname17,
                                __loop17_item_type = __loop17_tmp_item_type,
                            }).ToArray();
                    foreach (var __loop17_item in __loop17_result)
                    {
                        var __noname17 = __loop17_item.__loop17_item___noname17;
                        var type = __loop17_item.__loop17_item_type;
                        ++__loop17_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    ");
                        if (type.ItemType is NullableType)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("^");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    private final static QName _ArrayOfNullable");
                            __printer.Write(Generated_FirstLetterUp(type.Name));
                            __printer.WriteTemplateOutput("_QNAME = new QName(\"");
                            __printer.Write(Generated_GetUri(ns));
                            __printer.WriteTemplateOutput("\",\"ArrayOfNullable");
                            __printer.Write(Generated_FirstLetterUp(type.Name));
                            __printer.WriteTemplateOutput("\");");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("^");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    public ");
                            __printer.Write(Generated_GetPackage(ns).ToLower());
                            __printer.WriteTemplateOutput(".ArrayOfNullable");
                            __printer.Write(Generated_FirstLetterUp(type.ItemType.Name));
                            __printer.WriteTemplateOutput(" create_ArrayOfNullable");
                            __printer.Write(Generated_FirstLetterUp(type.ItemType.Name));
                            __printer.WriteTemplateOutput("() {");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        return new ");
                            __printer.Write(Generated_GetPackage(ns).ToLower());
                            __printer.WriteTemplateOutput(".ArrayOfNullable");
                            __printer.Write(Generated_FirstLetterUp(type.ItemType.Name));
                            __printer.WriteTemplateOutput("();");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    }");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("^");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    @XmlElementDecl(namespace = \"");
                            __printer.Write(Generated_GetUri(ns));
                            __printer.WriteTemplateOutput("\", name = \"_ArrayOfNullable");
                            __printer.Write(Generated_FirstLetterUp(type.ItemType.Name));
                            __printer.WriteTemplateOutput("\")");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    public JAXBElement<");
                            __printer.Write(Generated_GetPackage(ns).ToLower());
                            __printer.WriteTemplateOutput(".ArrayOfNullable");
                            __printer.Write(Generated_FirstLetterUp(type.ItemType.Name));
                            __printer.WriteTemplateOutput("> create_ArrayOfNullable");
                            __printer.Write(Generated_FirstLetterUp(type.ItemType.Name));
                            __printer.WriteTemplateOutput("(");
                            __printer.Write(Generated_GetPackage(ns).ToLower());
                            __printer.WriteTemplateOutput(".ArrayOfNullable");
                            __printer.Write(Generated_FirstLetterUp(type.ItemType.Name));
                            __printer.WriteTemplateOutput(" value) {");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        return new JAXBElement<");
                            __printer.Write(Generated_GetPackage(ns).ToLower());
                            __printer.WriteTemplateOutput(".ArrayOfNullable");
                            __printer.Write(Generated_FirstLetterUp(type.ItemType.Name));
                            __printer.WriteTemplateOutput(">(_ArrayOfNullable");
                            __printer.Write(Generated_FirstLetterUp(type.ItemType.Name));
                            __printer.WriteTemplateOutput("_QNAME, ");
                            __printer.Write(Generated_GetPackage(ns).ToLower());
                            __printer.WriteTemplateOutput(".ArrayOfNullable");
                            __printer.Write(Generated_FirstLetterUp(type.ItemType.Name));
                            __printer.WriteTemplateOutput(".class, null, value);");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    }");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                        }
                        else if (type.ItemType != BuiltInType.Byte)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("^");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    private final static QName _ArrayOf");
                            __printer.Write(Generated_FirstLetterUp(type.ItemType.Name));
                            __printer.WriteTemplateOutput("_QNAME = new QName(\"");
                            __printer.Write(Generated_GetUri(ns));
                            __printer.WriteTemplateOutput("\",\"ArrayOf");
                            __printer.Write(Generated_FirstLetterUp(type.ItemType.Name));
                            __printer.WriteTemplateOutput("\");");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("^");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    public ");
                            __printer.Write(Generated_GetPackage(ns).ToLower());
                            __printer.WriteTemplateOutput(".ArrayOf");
                            __printer.Write(Generated_FirstLetterUp(type.ItemType.Name));
                            __printer.WriteTemplateOutput(" createArrayOf");
                            __printer.Write(Generated_FirstLetterUp(type.ItemType.Name));
                            __printer.WriteTemplateOutput("() {");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        return new ");
                            __printer.Write(Generated_GetPackage(ns).ToLower());
                            __printer.WriteTemplateOutput(".ArrayOf");
                            __printer.Write(Generated_FirstLetterUp(type.ItemType.Name));
                            __printer.WriteTemplateOutput("();");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    }");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("^");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    @XmlElementDecl(namespace = \"");
                            __printer.Write(Generated_GetUri(ns));
                            __printer.WriteTemplateOutput("\", name = \"ArrayOf");
                            __printer.Write(Generated_FirstLetterUp(type.ItemType.Name));
                            __printer.WriteTemplateOutput("\")");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    public JAXBElement<");
                            __printer.Write(Generated_GetPackage(ns).ToLower());
                            __printer.WriteTemplateOutput(".ArrayOf");
                            __printer.Write(Generated_FirstLetterUp(type.ItemType.Name));
                            __printer.WriteTemplateOutput("> createArrayOf");
                            __printer.Write(Generated_FirstLetterUp(type.ItemType.Name));
                            __printer.WriteTemplateOutput("(");
                            __printer.Write(Generated_GetPackage(ns).ToLower());
                            __printer.WriteTemplateOutput(".ArrayOf");
                            __printer.Write(Generated_FirstLetterUp(type.ItemType.Name));
                            __printer.WriteTemplateOutput(" value) {");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        return new JAXBElement<");
                            __printer.Write(Generated_GetPackage(ns).ToLower());
                            __printer.WriteTemplateOutput(".ArrayOf");
                            __printer.Write(Generated_FirstLetterUp(type.ItemType.Name));
                            __printer.WriteTemplateOutput(">(_ArrayOf");
                            __printer.Write(Generated_FirstLetterUp(type.ItemType.Name));
                            __printer.WriteTemplateOutput("_QNAME, ");
                            __printer.Write(Generated_GetPackage(ns).ToLower());
                            __printer.WriteTemplateOutput(".ArrayOf");
                            __printer.Write(Generated_FirstLetterUp(type.ItemType.Name));
                            __printer.WriteTemplateOutput(".class, null, value);");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    }");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    if (!Properties.NoImplementationDelegates)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    private final static QName _SoaMMFault_QNAME = new QName(\"");
                        __printer.Write(Generated_GetUri(ns));
                        __printer.WriteTemplateOutput("\",\"SoaMMException\");");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    public ");
                        __printer.Write(Generated_GetPackage(ns).ToLower());
                        __printer.WriteTemplateOutput(".SoaMMFault createSoaMMFault() {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        return new ");
                        __printer.Write(Generated_GetPackage(ns).ToLower());
                        __printer.WriteTemplateOutput(".SoaMMFault();");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    }");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    @XmlElementDecl(namespace = \"");
                        __printer.Write(Generated_GetUri(ns));
                        __printer.WriteTemplateOutput("\", name = \"SoaMMException\")");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    public JAXBElement<");
                        __printer.Write(Generated_GetPackage(ns).ToLower());
                        __printer.WriteTemplateOutput(".SoaMMFault> createSoaMMFault(");
                        __printer.Write(Generated_GetPackage(ns).ToLower());
                        __printer.WriteTemplateOutput(".SoaMMFault value) {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        return new JAXBElement<");
                        __printer.Write(Generated_GetPackage(ns).ToLower());
                        __printer.WriteTemplateOutput(".SoaMMFault>(_SoaMMFault_QNAME, ");
                        __printer.Write(Generated_GetPackage(ns).ToLower());
                        __printer.WriteTemplateOutput(".SoaMMFault.class, null, value);");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    }");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    int __loop18_iteration = 0;
                    var __loop18_result =
                        (from __loop18_tmp_item___noname18 in EnumerableExtensions.Enumerate((ns.Declarations).GetEnumerator())
                        from __loop18_tmp_item_type in EnumerableExtensions.Enumerate((__loop18_tmp_item___noname18).GetEnumerator()).OfType<ExceptionType>()
                        select
                            new
                            {
                                __loop18_item___noname18 = __loop18_tmp_item___noname18,
                                __loop18_item_type = __loop18_tmp_item_type,
                            }).ToArray();
                    foreach (var __loop18_item in __loop18_result)
                    {
                        var __noname18 = __loop18_item.__loop18_item___noname18;
                        var type = __loop18_item.__loop18_item_type;
                        ++__loop18_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    private final static QName _");
                        __printer.Write(Generated_FirstLetterUp(type.Name));
                        __printer.WriteTemplateOutput("Fault_QNAME = new QName(\"");
                        __printer.Write(Generated_GetUri(ns));
                        __printer.WriteTemplateOutput("\",\"");
                        __printer.Write(type.Name);
                        __printer.WriteTemplateOutput("\");");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    public ");
                        __printer.Write(Generated_GetPackage(ns).ToLower());
                        __printer.WriteTemplateOutput(".");
                        __printer.Write(type.Name);
                        __printer.WriteTemplateOutput("Fault create");
                        __printer.Write(Generated_FirstLetterUp(type.Name));
                        __printer.WriteTemplateOutput("Fault() {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        return new ");
                        __printer.Write(Generated_GetPackage(ns).ToLower());
                        __printer.WriteTemplateOutput(".");
                        __printer.Write(type.Name);
                        __printer.WriteTemplateOutput("Fault();");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    }");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    @XmlElementDecl(namespace = \"");
                        __printer.Write(Generated_GetUri(ns));
                        __printer.WriteTemplateOutput("\", name = \"");
                        __printer.Write(type.Name);
                        __printer.WriteTemplateOutput("\")");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    public JAXBElement<");
                        __printer.Write(Generated_GetPackage(ns).ToLower());
                        __printer.WriteTemplateOutput(".");
                        __printer.Write(type.Name);
                        __printer.WriteTemplateOutput("Fault> create");
                        __printer.Write(Generated_FirstLetterUp(type.Name));
                        __printer.WriteTemplateOutput("Fault(");
                        __printer.Write(Generated_GetPackage(ns).ToLower());
                        __printer.WriteTemplateOutput(".");
                        __printer.Write(type.Name);
                        __printer.WriteTemplateOutput("Fault value) {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        return new JAXBElement<");
                        __printer.Write(Generated_GetPackage(ns).ToLower());
                        __printer.WriteTemplateOutput(".");
                        __printer.Write(type.Name);
                        __printer.WriteTemplateOutput("Fault>(_");
                        __printer.Write(Generated_FirstLetterUp(type.Name));
                        __printer.WriteTemplateOutput("Fault_QNAME, ");
                        __printer.Write(Generated_GetPackage(ns).ToLower());
                        __printer.WriteTemplateOutput(".");
                        __printer.Write(type.Name);
                        __printer.WriteTemplateOutput("Fault.class, null, value);");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    }");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    int __loop19_iteration = 0;
                    var __loop19_result =
                        (from __loop19_tmp_item___noname19 in EnumerableExtensions.Enumerate((ns.Declarations).GetEnumerator())
                        from __loop19_tmp_item_intf in EnumerableExtensions.Enumerate((__loop19_tmp_item___noname19).GetEnumerator()).OfType<Interface>()
                        select
                            new
                            {
                                __loop19_item___noname19 = __loop19_tmp_item___noname19,
                                __loop19_item_intf = __loop19_tmp_item_intf,
                            }).ToArray();
                    foreach (var __loop19_item in __loop19_result)
                    {
                        var __noname19 = __loop19_item.__loop19_item___noname19;
                        var intf = __loop19_item.__loop19_item_intf;
                        ++__loop19_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        int __loop20_iteration = 0;
                        var __loop20_result =
                            (from __loop20_tmp_item___noname20 in EnumerableExtensions.Enumerate((intf.Operations).GetEnumerator())
                            from __loop20_tmp_item_type in EnumerableExtensions.Enumerate((__loop20_tmp_item___noname20).GetEnumerator()).OfType<Operation>()
                            select
                                new
                                {
                                    __loop20_item___noname20 = __loop20_tmp_item___noname20,
                                    __loop20_item_type = __loop20_tmp_item_type,
                                }).ToArray();
                        foreach (var __loop20_item in __loop20_result)
                        {
                            var __noname20 = __loop20_item.__loop20_item___noname20;
                            var type = __loop20_item.__loop20_item_type;
                            ++__loop20_iteration;
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("^");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    private final static QName _");
                            __printer.Write(Generated_FirstLetterUp(type.Name));
                            __printer.WriteTemplateOutput("_QNAME = new QName(\"");
                            __printer.Write(Generated_GetUri(ns));
                            __printer.WriteTemplateOutput("\",\"");
                            __printer.Write(type.Name);
                            __printer.WriteTemplateOutput("\");");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("^");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    public ");
                            __printer.Write(Generated_GetPackage(ns).ToLower());
                            __printer.WriteTemplateOutput(".");
                            __printer.Write(Generated_FirstLetterUp(type.Name));
                            __printer.WriteTemplateOutput(" create");
                            __printer.Write(Generated_FirstLetterUp(type.Name));
                            __printer.WriteTemplateOutput("() {");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        return new ");
                            __printer.Write(Generated_GetPackage(ns).ToLower());
                            __printer.WriteTemplateOutput(".");
                            __printer.Write(Generated_FirstLetterUp(type.Name));
                            __printer.WriteTemplateOutput("();");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    }");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("^");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    @XmlElementDecl(namespace = \"");
                            __printer.Write(Generated_GetUri(ns));
                            __printer.WriteTemplateOutput("\", name = \"");
                            __printer.Write(type.Name);
                            __printer.WriteTemplateOutput("\")");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    public JAXBElement<");
                            __printer.Write(Generated_GetPackage(ns).ToLower());
                            __printer.WriteTemplateOutput(".");
                            __printer.Write(Generated_FirstLetterUp(type.Name));
                            __printer.WriteTemplateOutput("> create");
                            __printer.Write(Generated_FirstLetterUp(type.Name));
                            __printer.WriteTemplateOutput("(");
                            __printer.Write(Generated_GetPackage(ns).ToLower());
                            __printer.WriteTemplateOutput(".");
                            __printer.Write(Generated_FirstLetterUp(type.Name));
                            __printer.WriteTemplateOutput(" value) {");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        return new JAXBElement<");
                            __printer.Write(Generated_GetPackage(ns).ToLower());
                            __printer.WriteTemplateOutput(".");
                            __printer.Write(Generated_FirstLetterUp(type.Name));
                            __printer.WriteTemplateOutput(">(_");
                            __printer.Write(Generated_FirstLetterUp(type.Name));
                            __printer.WriteTemplateOutput("_QNAME, ");
                            __printer.Write(Generated_GetPackage(ns).ToLower());
                            __printer.WriteTemplateOutput(".");
                            __printer.Write(Generated_FirstLetterUp(type.Name));
                            __printer.WriteTemplateOutput(".class, null, value);");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    }");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                            if (type.ReturnType != PseudoType.Async)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("^");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("    private final static QName _");
                                __printer.Write(Generated_FirstLetterUp(type.Name));
                                __printer.WriteTemplateOutput("Response_QNAME = new QName(\"");
                                __printer.Write(Generated_GetUri(ns));
                                __printer.WriteTemplateOutput("\",\"");
                                __printer.Write(type.Name);
                                __printer.WriteTemplateOutput("Response\");");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("^");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("    public ");
                                __printer.Write(Generated_GetPackage(ns).ToLower());
                                __printer.WriteTemplateOutput(".");
                                __printer.Write(Generated_FirstLetterUp(type.Name));
                                __printer.WriteTemplateOutput("Response create");
                                __printer.Write(Generated_FirstLetterUp(type.Name));
                                __printer.WriteTemplateOutput("Response() {");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        return new ");
                                __printer.Write(Generated_GetPackage(ns).ToLower());
                                __printer.WriteTemplateOutput(".");
                                __printer.Write(Generated_FirstLetterUp(type.Name));
                                __printer.WriteTemplateOutput("Response();");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("    }");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("^");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("    @XmlElementDecl(namespace = \"");
                                __printer.Write(Generated_GetUri(ns));
                                __printer.WriteTemplateOutput("\", name = \"");
                                __printer.Write(type.Name);
                                __printer.WriteTemplateOutput("Response\")");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("    public JAXBElement<");
                                __printer.Write(Generated_GetPackage(ns).ToLower());
                                __printer.WriteTemplateOutput(".");
                                __printer.Write(Generated_FirstLetterUp(type.Name));
                                __printer.WriteTemplateOutput("Response> create");
                                __printer.Write(Generated_FirstLetterUp(type.Name));
                                __printer.WriteTemplateOutput("Response(");
                                __printer.Write(Generated_GetPackage(ns).ToLower());
                                __printer.WriteTemplateOutput(".");
                                __printer.Write(Generated_FirstLetterUp(type.Name));
                                __printer.WriteTemplateOutput("Response value) {");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        return new JAXBElement<");
                                __printer.Write(Generated_GetPackage(ns).ToLower());
                                __printer.WriteTemplateOutput(".");
                                __printer.Write(Generated_FirstLetterUp(type.Name));
                                __printer.WriteTemplateOutput("Response>(_");
                                __printer.Write(Generated_FirstLetterUp(type.Name));
                                __printer.WriteTemplateOutput("Response_QNAME, ");
                                __printer.Write(Generated_GetPackage(ns).ToLower());
                                __printer.WriteTemplateOutput(".");
                                __printer.Write(Generated_FirstLetterUp(type.Name));
                                __printer.WriteTemplateOutput("Response.class, null, value);");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("    }");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("    ");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("}");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateEnum(EnumType en)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("package ");
                    __printer.Write(Generated_GetPackage(en.Namespace).ToLower());
                    __printer.WriteTemplateOutput(";");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.bind.annotation.XmlEnum;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.bind.annotation.XmlEnumValue;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.bind.annotation.XmlType;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("@XmlType(name = \"");
                    __printer.Write(en.Name);
                    __printer.WriteTemplateOutput("\")");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("@XmlEnum");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("public enum ");
                    __printer.Write(en.Name);
                    __printer.WriteTemplateOutput(" {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    int __loop21_iteration = 0;
                    string comma = "";
                    var __loop21_result =
                        (from __loop21_tmp_item___noname21 in EnumerableExtensions.Enumerate((en).GetEnumerator())
                        from __loop21_tmp_item_value in EnumerableExtensions.Enumerate((__loop21_tmp_item___noname21.Values).GetEnumerator())
                        select
                            new
                            {
                                __loop21_item___noname21 = __loop21_tmp_item___noname21,
                                __loop21_item_value = __loop21_tmp_item_value,
                            }).ToArray();
                    foreach (var __loop21_item in __loop21_result)
                    {
                        var __noname21 = __loop21_item.__loop21_item___noname21;
                        var value = __loop21_item.__loop21_item_value;
                        ++__loop21_iteration;
                        if (__loop21_iteration >= 2)
                        {
                            comma = ",";
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.Write(comma);
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    @XmlEnumValue(\"");
                        __printer.Write(value.Name);
                        __printer.WriteTemplateOutput("\")");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    ");
                        __printer.Write(value.Name.ToUpper());
                        __printer.WriteTemplateOutput("(\"");
                        __printer.Write(value.Name);
                        __printer.WriteTemplateOutput("\")\\");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput(";");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    private final String value;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    ");
                    __printer.Write(en.Name);
                    __printer.WriteTemplateOutput("(String v) {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        value = v;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    public String value() {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        return value;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    public static ");
                    __printer.Write(en.Name);
                    __printer.WriteTemplateOutput(" fromValue(String v) {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        for (");
                    __printer.Write(en.Name);
                    __printer.WriteTemplateOutput(" c: ");
                    __printer.Write(en.Name);
                    __printer.WriteTemplateOutput(".values()) {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            if (c.value.equals(v)) {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                return c;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        throw new IllegalArgumentException(v);");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("}");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public string Generated_GenSeeAlso(StructType str)
            {
                string result = "@javax.xml.bind.annotation.XmlSeeAlso({";
                int __loop22_iteration = 0;
                string delim = "";
                var __loop22_result =
                    (from __loop22_tmp_item_strde in EnumerableExtensions.Enumerate((str.GetAllDescendants()).GetEnumerator())
                    select
                        new
                        {
                            __loop22_item_strde = __loop22_tmp_item_strde,
                        }).ToArray();
                foreach (var __loop22_item in __loop22_result)
                {
                    var strde = __loop22_item.__loop22_item_strde;
                    ++__loop22_iteration;
                    if (__loop22_iteration >= 2)
                    {
                        delim = ", ";
                    }
                    result = result + delim + Generated_GetPackage(strde.Namespace).ToLower() + "." + strde.Name + ".class";
                }
                if (__loop22_iteration == 0)
                {
                    result = "";
                }
                if (result != "")
                {
                    result = result + "})";
                }
                return result;
            }
            
            public string Generated_GenSeeAlso(ExceptionType ex)
            {
                string result = "@javax.xml.bind.annotation.XmlSeeAlso({";
                int __loop23_iteration = 0;
                string delim = "";
                var __loop23_result =
                    (from __loop23_tmp_item_exde in EnumerableExtensions.Enumerate((ex.GetAllDescendants()).GetEnumerator())
                    select
                        new
                        {
                            __loop23_item_exde = __loop23_tmp_item_exde,
                        }).ToArray();
                foreach (var __loop23_item in __loop23_result)
                {
                    var exde = __loop23_item.__loop23_item_exde;
                    ++__loop23_iteration;
                    if (__loop23_iteration >= 2)
                    {
                        delim = ", ";
                    }
                    result = result + delim + Generated_GetPackage(exde.Namespace).ToLower() + "." + exde.Name + ".class";
                }
                if (__loop23_iteration == 0)
                {
                    result = "";
                }
                if (result != "")
                {
                    result = result + "})";
                }
                return result;
            }
            
            public List<string> Generated_GenerateStruct(StructType st)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("package ");
                    __printer.Write(Generated_GetPackage(st.Namespace).ToLower());
                    __printer.WriteTemplateOutput(";");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.bind.annotation.XmlAccessType;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.bind.annotation.XmlAccessorType;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.bind.annotation.XmlType;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("@XmlAccessorType(XmlAccessType.FIELD)");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("@XmlType(name = \"");
                    __printer.Write(st.Name);
                    __printer.WriteTemplateOutput("\", propOrder = {");
                    __printer.WriteLine();
                    int __loop24_iteration = 0;
                    string comma = "";
                    var __loop24_result =
                        (from __loop24_tmp_item___noname22 in EnumerableExtensions.Enumerate((st.Fields).GetEnumerator())
                        from __loop24_tmp_item_fi in EnumerableExtensions.Enumerate((__loop24_tmp_item___noname22).GetEnumerator()).OfType<StructField>()
                        select
                            new
                            {
                                __loop24_item___noname22 = __loop24_tmp_item___noname22,
                                __loop24_item_fi = __loop24_tmp_item_fi,
                            }).ToArray();
                    foreach (var __loop24_item in __loop24_result)
                    {
                        var __noname22 = __loop24_item.__loop24_item___noname22;
                        var fi = __loop24_item.__loop24_item_fi;
                        ++__loop24_iteration;
                        if (__loop24_iteration >= 2)
                        {
                            comma = ",";
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.Write(comma);
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    \"");
                        __printer.Write(Generated_FirstLetterLow(fi.Name));
                        __printer.WriteTemplateOutput("\"\\");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("})");
                    __printer.WriteLine();
                    __printer.Write(Generated_GenSeeAlso(st));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("public class ");
                    __printer.Write(st.Name);
                    __printer.WriteTemplateOutput(" \\");
                    __printer.WriteLine();
                    if (st.SuperType != null)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("extends ");
                        __printer.Write(Generated_GetPackage(st.SuperType.Namespace).ToLower());
                        __printer.WriteTemplateOutput(".");
                        __printer.Write(st.SuperType.Name);
                        __printer.WriteTemplateOutput(" {");
                        __printer.WriteLine();
                    }
                    else
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("{");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    int __loop25_iteration = 0;
                    var __loop25_result =
                        (from __loop25_tmp_item___noname23 in EnumerableExtensions.Enumerate((st.Fields).GetEnumerator())
                        from __loop25_tmp_item_fi in EnumerableExtensions.Enumerate((__loop25_tmp_item___noname23).GetEnumerator()).OfType<StructField>()
                        select
                            new
                            {
                                __loop25_item___noname23 = __loop25_tmp_item___noname23,
                                __loop25_item_fi = __loop25_tmp_item_fi,
                            }).ToArray();
                    foreach (var __loop25_item in __loop25_result)
                    {
                        var __noname23 = __loop25_item.__loop25_item___noname23;
                        var fi = __loop25_item.__loop25_item_fi;
                        ++__loop25_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        if ((fi.Type is ArrayType) && !(((ArrayType)fi.Type).ItemType == BuiltInType.Byte))
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    @javax.xml.bind.annotation.XmlElementWrapper(name = \"");
                            __printer.Write(fi.Name);
                            __printer.WriteTemplateOutput("\", required = true, nillable = true)");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    @javax.xml.bind.annotation.XmlElement(name = \"");
                            __printer.Write(((ArrayType)fi.Type).ItemType.Name);
                            __printer.WriteTemplateOutput("\", type = ");
                            __printer.Write(Generated_PrintType(((ArrayType)fi.Type).ItemType));
                            __printer.WriteTemplateOutput(".class, nillable = ");
                            __printer.Write(Generated_IsNillableType(((ArrayType)fi.Type).ItemType));
                            __printer.WriteTemplateOutput(")");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    protected java.util.List<");
                            __printer.Write(Generated_PrintType(((ArrayType)fi.Type).ItemType));
                            __printer.WriteTemplateOutput("> ");
                            __printer.Write(Generated_FirstLetterLow(fi.Name));
                            __printer.WriteTemplateOutput(";");
                            __printer.WriteLine();
                        }
                        else
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    @javax.xml.bind.annotation.XmlElement(name = \"");
                            __printer.Write(fi.Name);
                            __printer.WriteTemplateOutput("\", required = true, nillable = ");
                            __printer.Write(Generated_IsNillableType(fi.Type));
                            __printer.WriteTemplateOutput(")");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    protected ");
                            __printer.Write(Generated_PrintType(fi.Type));
                            __printer.WriteTemplateOutput(" ");
                            __printer.Write(Generated_FirstLetterLow(fi.Name));
                            __printer.WriteTemplateOutput(";");
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    public ");
                    __printer.Write(st.Name);
                    __printer.WriteTemplateOutput("() {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                    if (st.HasNonArrayFields())
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    public ");
                        __printer.Write(st.Name);
                        __printer.WriteTemplateOutput("(\\");
                        __printer.WriteLine();
                        int __loop26_iteration = 0;
                        string delim = "";
                        var __loop26_result =
                            (from __loop26_tmp_item___noname24 in EnumerableExtensions.Enumerate((st.Fields).GetEnumerator())
                            from __loop26_tmp_item_fi in EnumerableExtensions.Enumerate((__loop26_tmp_item___noname24).GetEnumerator()).OfType<StructField>()
                            where !(__loop26_tmp_item_fi.Type is ArrayType)
                            select
                                new
                                {
                                    __loop26_item___noname24 = __loop26_tmp_item___noname24,
                                    __loop26_item_fi = __loop26_tmp_item_fi,
                                }).ToArray();
                        foreach (var __loop26_item in __loop26_result)
                        {
                            var __noname24 = __loop26_item.__loop26_item___noname24;
                            var fi = __loop26_item.__loop26_item_fi;
                            ++__loop26_iteration;
                            if (__loop26_iteration >= 2)
                            {
                                delim = ", ";
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.Write(delim);
                            __printer.Write(Generated_PrintType(fi.Type));
                            __printer.WriteTemplateOutput(" ");
                            __printer.Write(Generated_FirstLetterLow(fi.Name));
                            __printer.WriteTemplateOutput("\\");
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput(") {");
                        __printer.WriteLine();
                        int __loop27_iteration = 0;
                        var __loop27_result =
                            (from __loop27_tmp_item___noname25 in EnumerableExtensions.Enumerate((st.Fields).GetEnumerator())
                            from __loop27_tmp_item_fi in EnumerableExtensions.Enumerate((__loop27_tmp_item___noname25).GetEnumerator()).OfType<StructField>()
                            where !(__loop27_tmp_item_fi.Type is ArrayType)
                            select
                                new
                                {
                                    __loop27_item___noname25 = __loop27_tmp_item___noname25,
                                    __loop27_item_fi = __loop27_tmp_item_fi,
                                }).ToArray();
                        foreach (var __loop27_item in __loop27_result)
                        {
                            var __noname25 = __loop27_item.__loop27_item___noname25;
                            var fi = __loop27_item.__loop27_item_fi;
                            ++__loop27_iteration;
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        this.");
                            __printer.Write(Generated_FirstLetterLow(fi.Name));
                            __printer.WriteTemplateOutput(" = ");
                            __printer.Write(Generated_FirstLetterLow(fi.Name));
                            __printer.WriteTemplateOutput(";");
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    }");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    int __loop28_iteration = 0;
                    var __loop28_result =
                        (from __loop28_tmp_item___noname26 in EnumerableExtensions.Enumerate((st.Fields).GetEnumerator())
                        from __loop28_tmp_item_fi in EnumerableExtensions.Enumerate((__loop28_tmp_item___noname26).GetEnumerator()).OfType<StructField>()
                        select
                            new
                            {
                                __loop28_item___noname26 = __loop28_tmp_item___noname26,
                                __loop28_item_fi = __loop28_tmp_item_fi,
                            }).ToArray();
                    foreach (var __loop28_item in __loop28_result)
                    {
                        var __noname26 = __loop28_item.__loop28_item___noname26;
                        var fi = __loop28_item.__loop28_item_fi;
                        ++__loop28_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        if ((fi.Type is ArrayType) && !(((ArrayType)fi.Type).ItemType == BuiltInType.Byte))
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    public java.util.List<");
                            __printer.Write(Generated_PrintType(((ArrayType)fi.Type).ItemType));
                            __printer.WriteTemplateOutput("> get");
                            __printer.Write(Generated_FirstLetterUp(fi.Name));
                            __printer.WriteTemplateOutput("() {");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        if (this.");
                            __printer.Write(Generated_FirstLetterLow(fi.Name));
                            __printer.WriteTemplateOutput(" == null) {");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("            this.");
                            __printer.Write(Generated_FirstLetterLow(fi.Name));
                            __printer.WriteTemplateOutput(" = new java.util.ArrayList<");
                            __printer.Write(Generated_PrintType(((ArrayType)fi.Type).ItemType));
                            __printer.WriteTemplateOutput(">();");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        }");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        return this.");
                            __printer.Write(Generated_FirstLetterLow(fi.Name));
                            __printer.WriteTemplateOutput(";");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    }");
                            __printer.WriteLine();
                        }
                        else
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    public ");
                            __printer.Write(Generated_PrintType(fi.Type));
                            __printer.WriteTemplateOutput(" get");
                            __printer.Write(Generated_FirstLetterUp(fi.Name));
                            __printer.WriteTemplateOutput("() {");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        return this.");
                            __printer.Write(Generated_FirstLetterLow(fi.Name));
                            __printer.WriteTemplateOutput(";");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    }");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("^");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    public void set");
                            __printer.Write(Generated_FirstLetterUp(fi.Name));
                            __printer.WriteTemplateOutput("(");
                            __printer.Write(Generated_PrintType(fi.Type));
                            __printer.WriteTemplateOutput(" value) {");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        this.");
                            __printer.Write(Generated_FirstLetterLow(fi.Name));
                            __printer.WriteTemplateOutput(" = value;");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    }");
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("}");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateClaimset(ClaimsetType cs)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("package ");
                    __printer.Write(Generated_GetPackage(cs.Namespace).ToLower());
                    __printer.WriteTemplateOutput(";");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.bind.annotation.XmlAccessType;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.bind.annotation.XmlAccessorType;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.bind.annotation.XmlType;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("@XmlAccessorType(XmlAccessType.FIELD)");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("@XmlType(name = \"");
                    __printer.Write(cs.Name);
                    __printer.WriteTemplateOutput("\", propOrder = {");
                    __printer.WriteLine();
                    int __loop29_iteration = 0;
                    string comma = "";
                    var __loop29_result =
                        (from __loop29_tmp_item___noname27 in EnumerableExtensions.Enumerate((cs.Fields).GetEnumerator())
                        from __loop29_tmp_item_fi in EnumerableExtensions.Enumerate((__loop29_tmp_item___noname27).GetEnumerator()).OfType<ClaimField>()
                        select
                            new
                            {
                                __loop29_item___noname27 = __loop29_tmp_item___noname27,
                                __loop29_item_fi = __loop29_tmp_item_fi,
                            }).ToArray();
                    foreach (var __loop29_item in __loop29_result)
                    {
                        var __noname27 = __loop29_item.__loop29_item___noname27;
                        var fi = __loop29_item.__loop29_item_fi;
                        ++__loop29_iteration;
                        if (__loop29_iteration >= 2)
                        {
                            comma = ",";
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.Write(comma);
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    \"");
                        __printer.Write(Generated_FirstLetterLow(fi.Name));
                        __printer.WriteTemplateOutput("\"\\");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("})");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("public class ");
                    __printer.Write(cs.Name);
                    __printer.WriteTemplateOutput(" {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    int __loop30_iteration = 0;
                    var __loop30_result =
                        (from __loop30_tmp_item___noname28 in EnumerableExtensions.Enumerate((cs.Fields).GetEnumerator())
                        from __loop30_tmp_item_fi in EnumerableExtensions.Enumerate((__loop30_tmp_item___noname28).GetEnumerator()).OfType<ClaimField>()
                        select
                            new
                            {
                                __loop30_item___noname28 = __loop30_tmp_item___noname28,
                                __loop30_item_fi = __loop30_tmp_item_fi,
                            }).ToArray();
                    foreach (var __loop30_item in __loop30_result)
                    {
                        var __noname28 = __loop30_item.__loop30_item___noname28;
                        var fi = __loop30_item.__loop30_item_fi;
                        ++__loop30_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        if ((fi.Type is ArrayType) && !(((ArrayType)fi.Type).ItemType == BuiltInType.Byte))
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    @javax.xml.bind.annotation.XmlElementWrapper(name = \"");
                            __printer.Write(fi.Name);
                            __printer.WriteTemplateOutput("\", required = true, nillable = true)");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    @javax.xml.bind.annotation.XmlElement(name = \"");
                            __printer.Write(((ArrayType)fi.Type).ItemType.Name);
                            __printer.WriteTemplateOutput("\", type = ");
                            __printer.Write(Generated_PrintType(((ArrayType)fi.Type).ItemType));
                            __printer.WriteTemplateOutput(".class, nillable = ");
                            __printer.Write(Generated_IsNillableType(((ArrayType)fi.Type).ItemType));
                            __printer.WriteTemplateOutput(")");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    protected java.util.List<");
                            __printer.Write(Generated_PrintType(((ArrayType)fi.Type).ItemType));
                            __printer.WriteTemplateOutput("> ");
                            __printer.Write(Generated_FirstLetterLow(fi.Name));
                            __printer.WriteTemplateOutput(";");
                            __printer.WriteLine();
                        }
                        else
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    @javax.xml.bind.annotation.XmlElement(name = \"");
                            __printer.Write(fi.Name);
                            __printer.WriteTemplateOutput("\", required = true, nillable = ");
                            __printer.Write(Generated_IsNillableType(fi.Type));
                            __printer.WriteTemplateOutput(")");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    protected ");
                            __printer.Write(Generated_PrintType(fi.Type));
                            __printer.WriteTemplateOutput(" ");
                            __printer.Write(Generated_FirstLetterLow(fi.Name));
                            __printer.WriteTemplateOutput(";");
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    public ");
                    __printer.Write(cs.Name);
                    __printer.WriteTemplateOutput("() {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                    if (cs.HasNonArrayFields())
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    public ");
                        __printer.Write(cs.Name);
                        __printer.WriteTemplateOutput("(\\");
                        __printer.WriteLine();
                        int __loop31_iteration = 0;
                        string delim = "";
                        var __loop31_result =
                            (from __loop31_tmp_item___noname29 in EnumerableExtensions.Enumerate((cs.Fields).GetEnumerator())
                            from __loop31_tmp_item_fi in EnumerableExtensions.Enumerate((__loop31_tmp_item___noname29).GetEnumerator()).OfType<ClaimField>()
                            where !(__loop31_tmp_item_fi.Type is ArrayType)
                            select
                                new
                                {
                                    __loop31_item___noname29 = __loop31_tmp_item___noname29,
                                    __loop31_item_fi = __loop31_tmp_item_fi,
                                }).ToArray();
                        foreach (var __loop31_item in __loop31_result)
                        {
                            var __noname29 = __loop31_item.__loop31_item___noname29;
                            var fi = __loop31_item.__loop31_item_fi;
                            ++__loop31_iteration;
                            if (__loop31_iteration >= 2)
                            {
                                delim = ", ";
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.Write(delim);
                            __printer.Write(Generated_PrintType(fi.Type));
                            __printer.WriteTemplateOutput(" ");
                            __printer.Write(Generated_FirstLetterLow(fi.Name));
                            __printer.WriteTemplateOutput("\\");
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput(") {");
                        __printer.WriteLine();
                        int __loop32_iteration = 0;
                        var __loop32_result =
                            (from __loop32_tmp_item___noname30 in EnumerableExtensions.Enumerate((cs.Fields).GetEnumerator())
                            from __loop32_tmp_item_fi in EnumerableExtensions.Enumerate((__loop32_tmp_item___noname30).GetEnumerator()).OfType<ClaimField>()
                            where !(__loop32_tmp_item_fi.Type is ArrayType)
                            select
                                new
                                {
                                    __loop32_item___noname30 = __loop32_tmp_item___noname30,
                                    __loop32_item_fi = __loop32_tmp_item_fi,
                                }).ToArray();
                        foreach (var __loop32_item in __loop32_result)
                        {
                            var __noname30 = __loop32_item.__loop32_item___noname30;
                            var fi = __loop32_item.__loop32_item_fi;
                            ++__loop32_iteration;
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        this.");
                            __printer.Write(Generated_FirstLetterLow(fi.Name));
                            __printer.WriteTemplateOutput(" = ");
                            __printer.Write(Generated_FirstLetterLow(fi.Name));
                            __printer.WriteTemplateOutput(";");
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    }");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    int __loop33_iteration = 0;
                    var __loop33_result =
                        (from __loop33_tmp_item___noname31 in EnumerableExtensions.Enumerate((cs.Fields).GetEnumerator())
                        from __loop33_tmp_item_fi in EnumerableExtensions.Enumerate((__loop33_tmp_item___noname31).GetEnumerator()).OfType<ClaimField>()
                        select
                            new
                            {
                                __loop33_item___noname31 = __loop33_tmp_item___noname31,
                                __loop33_item_fi = __loop33_tmp_item_fi,
                            }).ToArray();
                    foreach (var __loop33_item in __loop33_result)
                    {
                        var __noname31 = __loop33_item.__loop33_item___noname31;
                        var fi = __loop33_item.__loop33_item_fi;
                        ++__loop33_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        if ((fi.Type is ArrayType) && !(((ArrayType)fi.Type).ItemType == BuiltInType.Byte))
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    public java.util.List<");
                            __printer.Write(Generated_PrintType(((ArrayType)fi.Type).ItemType));
                            __printer.WriteTemplateOutput("> get");
                            __printer.Write(Generated_FirstLetterUp(fi.Name));
                            __printer.WriteTemplateOutput("() {");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        if (this.");
                            __printer.Write(Generated_FirstLetterLow(fi.Name));
                            __printer.WriteTemplateOutput(" == null) {");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("            this.");
                            __printer.Write(Generated_FirstLetterLow(fi.Name));
                            __printer.WriteTemplateOutput(" = new java.util.ArrayList<");
                            __printer.Write(Generated_PrintType(((ArrayType)fi.Type).ItemType));
                            __printer.WriteTemplateOutput(">();");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        }");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        return this.");
                            __printer.Write(Generated_FirstLetterLow(fi.Name));
                            __printer.WriteTemplateOutput(";");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    }");
                            __printer.WriteLine();
                        }
                        else
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    public ");
                            __printer.Write(Generated_PrintType(fi.Type));
                            __printer.WriteTemplateOutput(" get");
                            __printer.Write(Generated_FirstLetterUp(fi.Name));
                            __printer.WriteTemplateOutput("() {");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        return this.");
                            __printer.Write(Generated_FirstLetterLow(fi.Name));
                            __printer.WriteTemplateOutput(";");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    }");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("^");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    public void set");
                            __printer.Write(Generated_FirstLetterUp(fi.Name));
                            __printer.WriteTemplateOutput("(");
                            __printer.Write(Generated_PrintType(fi.Type));
                            __printer.WriteTemplateOutput(" value) {");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        this.");
                            __printer.Write(Generated_FirstLetterLow(fi.Name));
                            __printer.WriteTemplateOutput(" = value;");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    }");
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("}");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateException(ExceptionType ex)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("package ");
                    __printer.Write(Generated_GetPackage(ex.Namespace).ToLower());
                    __printer.WriteTemplateOutput(";");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.bind.annotation.XmlAccessType;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.bind.annotation.XmlAccessorType;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.bind.annotation.XmlType;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("@XmlAccessorType(XmlAccessType.FIELD)");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("@XmlType(name = \"");
                    __printer.Write(ex.Name);
                    __printer.WriteTemplateOutput("\", propOrder = {");
                    __printer.WriteLine();
                    int __loop34_iteration = 0;
                    string comma = "";
                    var __loop34_result =
                        (from __loop34_tmp_item___noname32 in EnumerableExtensions.Enumerate((ex.Fields).GetEnumerator())
                        from __loop34_tmp_item_fi in EnumerableExtensions.Enumerate((__loop34_tmp_item___noname32).GetEnumerator()).OfType<ExceptionField>()
                        select
                            new
                            {
                                __loop34_item___noname32 = __loop34_tmp_item___noname32,
                                __loop34_item_fi = __loop34_tmp_item_fi,
                            }).ToArray();
                    foreach (var __loop34_item in __loop34_result)
                    {
                        var __noname32 = __loop34_item.__loop34_item___noname32;
                        var fi = __loop34_item.__loop34_item_fi;
                        ++__loop34_iteration;
                        if (__loop34_iteration >= 2)
                        {
                            comma = ",";
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.Write(comma);
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    \"");
                        __printer.Write(Generated_FirstLetterLow(fi.Name));
                        __printer.WriteTemplateOutput("\"\\");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("})");
                    __printer.WriteLine();
                    __printer.Write(Generated_GenSeeAlso(ex));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("public class ");
                    __printer.Write(ex.Name);
                    __printer.WriteTemplateOutput("Fault \\");
                    __printer.WriteLine();
                    if (ex.SuperType != null)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("extends ");
                        __printer.Write(Generated_GetPackage(ex.SuperType.Namespace).ToLower());
                        __printer.WriteTemplateOutput(".");
                        __printer.Write(ex.SuperType.Name);
                        __printer.WriteTemplateOutput(" {");
                        __printer.WriteLine();
                    }
                    else
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("{");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    int __loop35_iteration = 0;
                    var __loop35_result =
                        (from __loop35_tmp_item___noname33 in EnumerableExtensions.Enumerate((ex.Fields).GetEnumerator())
                        from __loop35_tmp_item_fi in EnumerableExtensions.Enumerate((__loop35_tmp_item___noname33).GetEnumerator()).OfType<ExceptionField>()
                        select
                            new
                            {
                                __loop35_item___noname33 = __loop35_tmp_item___noname33,
                                __loop35_item_fi = __loop35_tmp_item_fi,
                            }).ToArray();
                    foreach (var __loop35_item in __loop35_result)
                    {
                        var __noname33 = __loop35_item.__loop35_item___noname33;
                        var fi = __loop35_item.__loop35_item_fi;
                        ++__loop35_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        if ((fi.Type is ArrayType) && !(((ArrayType)fi.Type).ItemType == BuiltInType.Byte))
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    @javax.xml.bind.annotation.XmlElementWrapper(name = \"");
                            __printer.Write(fi.Name);
                            __printer.WriteTemplateOutput("\", required = true, nillable = true)");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    @javax.xml.bind.annotation.XmlElement(name = \"");
                            __printer.Write(((ArrayType)fi.Type).ItemType.Name);
                            __printer.WriteTemplateOutput("\", type = ");
                            __printer.Write(Generated_PrintType(((ArrayType)fi.Type).ItemType));
                            __printer.WriteTemplateOutput(".class, nillable = ");
                            __printer.Write(Generated_IsNillableType(((ArrayType)fi.Type).ItemType));
                            __printer.WriteTemplateOutput(")");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    protected java.util.List<");
                            __printer.Write(Generated_PrintType(((ArrayType)fi.Type).ItemType));
                            __printer.WriteTemplateOutput("> ");
                            __printer.Write(Generated_FirstLetterLow(fi.Name));
                            __printer.WriteTemplateOutput(";");
                            __printer.WriteLine();
                        }
                        else
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    @javax.xml.bind.annotation.XmlElement(name = \"");
                            __printer.Write(fi.Name);
                            __printer.WriteTemplateOutput("\", required = true, nillable = ");
                            __printer.Write(Generated_IsNillableType(fi.Type));
                            __printer.WriteTemplateOutput(")");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    protected ");
                            __printer.Write(Generated_PrintType(fi.Type));
                            __printer.WriteTemplateOutput(" ");
                            __printer.Write(Generated_FirstLetterLow(fi.Name));
                            __printer.WriteTemplateOutput(";");
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    public ");
                    __printer.Write(ex.Name);
                    __printer.WriteTemplateOutput("Fault() {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                    if (ex.HasNonArrayFields())
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    public ");
                        __printer.Write(ex.Name);
                        __printer.WriteTemplateOutput("Fault(\\");
                        __printer.WriteLine();
                        int __loop36_iteration = 0;
                        string delim = "";
                        var __loop36_result =
                            (from __loop36_tmp_item___noname34 in EnumerableExtensions.Enumerate((ex.Fields).GetEnumerator())
                            from __loop36_tmp_item_fi in EnumerableExtensions.Enumerate((__loop36_tmp_item___noname34).GetEnumerator()).OfType<ExceptionField>()
                            where !(__loop36_tmp_item_fi.Type is ArrayType)
                            select
                                new
                                {
                                    __loop36_item___noname34 = __loop36_tmp_item___noname34,
                                    __loop36_item_fi = __loop36_tmp_item_fi,
                                }).ToArray();
                        foreach (var __loop36_item in __loop36_result)
                        {
                            var __noname34 = __loop36_item.__loop36_item___noname34;
                            var fi = __loop36_item.__loop36_item_fi;
                            ++__loop36_iteration;
                            if (__loop36_iteration >= 2)
                            {
                                delim = ", ";
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.Write(delim);
                            __printer.Write(Generated_PrintType(fi.Type));
                            __printer.WriteTemplateOutput(" ");
                            __printer.Write(Generated_FirstLetterLow(fi.Name));
                            __printer.WriteTemplateOutput("\\");
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput(") {");
                        __printer.WriteLine();
                        int __loop37_iteration = 0;
                        var __loop37_result =
                            (from __loop37_tmp_item___noname35 in EnumerableExtensions.Enumerate((ex.Fields).GetEnumerator())
                            from __loop37_tmp_item_fi in EnumerableExtensions.Enumerate((__loop37_tmp_item___noname35).GetEnumerator()).OfType<ExceptionField>()
                            where !(__loop37_tmp_item_fi.Type is ArrayType)
                            select
                                new
                                {
                                    __loop37_item___noname35 = __loop37_tmp_item___noname35,
                                    __loop37_item_fi = __loop37_tmp_item_fi,
                                }).ToArray();
                        foreach (var __loop37_item in __loop37_result)
                        {
                            var __noname35 = __loop37_item.__loop37_item___noname35;
                            var fi = __loop37_item.__loop37_item_fi;
                            ++__loop37_iteration;
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        this.");
                            __printer.Write(Generated_FirstLetterLow(fi.Name));
                            __printer.WriteTemplateOutput(" = ");
                            __printer.Write(Generated_FirstLetterLow(fi.Name));
                            __printer.WriteTemplateOutput(";");
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    }");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    int __loop38_iteration = 0;
                    var __loop38_result =
                        (from __loop38_tmp_item___noname36 in EnumerableExtensions.Enumerate((ex.Fields).GetEnumerator())
                        from __loop38_tmp_item_fi in EnumerableExtensions.Enumerate((__loop38_tmp_item___noname36).GetEnumerator()).OfType<ExceptionField>()
                        select
                            new
                            {
                                __loop38_item___noname36 = __loop38_tmp_item___noname36,
                                __loop38_item_fi = __loop38_tmp_item_fi,
                            }).ToArray();
                    foreach (var __loop38_item in __loop38_result)
                    {
                        var __noname36 = __loop38_item.__loop38_item___noname36;
                        var fi = __loop38_item.__loop38_item_fi;
                        ++__loop38_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        if ((fi.Type is ArrayType) && !(((ArrayType)fi.Type).ItemType == BuiltInType.Byte))
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    public java.util.List<");
                            __printer.Write(Generated_PrintType(((ArrayType)fi.Type).ItemType));
                            __printer.WriteTemplateOutput("> get");
                            __printer.Write(Generated_FirstLetterUp(fi.Name));
                            __printer.WriteTemplateOutput("() {");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        if (this.");
                            __printer.Write(Generated_FirstLetterLow(fi.Name));
                            __printer.WriteTemplateOutput(" == null) {");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("            this.");
                            __printer.Write(Generated_FirstLetterLow(fi.Name));
                            __printer.WriteTemplateOutput(" = new java.util.ArrayList<");
                            __printer.Write(Generated_PrintType(((ArrayType)fi.Type).ItemType));
                            __printer.WriteTemplateOutput(">();");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        }");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        return this.");
                            __printer.Write(Generated_FirstLetterLow(fi.Name));
                            __printer.WriteTemplateOutput(";");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    }");
                            __printer.WriteLine();
                        }
                        else
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    public ");
                            __printer.Write(Generated_PrintType(fi.Type));
                            __printer.WriteTemplateOutput(" get");
                            __printer.Write(Generated_FirstLetterUp(fi.Name));
                            __printer.WriteTemplateOutput("() {");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        return this.");
                            __printer.Write(Generated_FirstLetterLow(fi.Name));
                            __printer.WriteTemplateOutput(";");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    }");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("^");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    public void set");
                            __printer.Write(Generated_FirstLetterUp(fi.Name));
                            __printer.WriteTemplateOutput("(");
                            __printer.Write(Generated_PrintType(fi.Type));
                            __printer.WriteTemplateOutput(" value) {");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        this.");
                            __printer.Write(Generated_FirstLetterLow(fi.Name));
                            __printer.WriteTemplateOutput(" = value;");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    }");
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("}");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateNullableArray(ArrayType ar, Namespace ns)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("package ");
                    __printer.Write(Generated_GetPackage(ns).ToLower());
                    __printer.WriteTemplateOutput(";");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import java.util.ArrayList;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import java.util.List;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.bind.annotation.XmlAccessType;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.bind.annotation.XmlAccessorType;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.bind.annotation.XmlElement;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.bind.annotation.XmlType;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("@XmlAccessorType(XmlAccessType.FIELD)");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("@XmlType(name = \"ArrayOfNullable");
                    __printer.Write(Generated_FirstLetterUp(ar.ItemType.Name));
                    __printer.WriteTemplateOutput("\", propOrder = {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    \"_");
                    __printer.Write(ar.ItemType.Name);
                    __printer.WriteTemplateOutput("\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("})");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("public class ArrayOfNullable");
                    __printer.Write(Generated_FirstLetterUp(ar.ItemType.Name));
                    __printer.WriteTemplateOutput(" {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    @XmlElement(name = \"");
                    __printer.Write(ar.ItemType.Name);
                    __printer.WriteTemplateOutput("\", type = ");
                    __printer.Write(Generated_PrintClassType(ar.ItemType));
                    __printer.WriteTemplateOutput(".class, nillable = true)");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    protected List<");
                    __printer.Write(Generated_PrintClassType(ar.ItemType));
                    __printer.WriteTemplateOutput("> _");
                    __printer.Write(ar.ItemType.Name);
                    __printer.WriteTemplateOutput(";");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    public List<");
                    __printer.Write(Generated_PrintClassType(ar.ItemType));
                    __printer.WriteTemplateOutput("> get");
                    __printer.Write(Generated_FirstLetterUp(ar.ItemType.Name));
                    __printer.WriteTemplateOutput("() {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        if (_");
                    __printer.Write(ar.ItemType.Name);
                    __printer.WriteTemplateOutput(" == null) {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            _");
                    __printer.Write(ar.ItemType.Name);
                    __printer.WriteTemplateOutput(" = new ArrayList<");
                    __printer.Write(Generated_PrintClassType(ar.ItemType));
                    __printer.WriteTemplateOutput(">();");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        return this._");
                    __printer.Write(ar.ItemType.Name);
                    __printer.WriteTemplateOutput(";");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("}");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateArray(ArrayType ar, Namespace ns)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("package ");
                    __printer.Write(Generated_GetPackage(ns).ToLower());
                    __printer.WriteTemplateOutput(";");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import java.util.ArrayList;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import java.util.List;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.bind.annotation.XmlAccessType;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.bind.annotation.XmlAccessorType;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.bind.annotation.XmlElement;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.bind.annotation.XmlType;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("@XmlAccessorType(XmlAccessType.FIELD)");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("@XmlType(name = \"ArrayOf");
                    __printer.Write(Generated_FirstLetterUp(ar.ItemType.Name));
                    __printer.WriteTemplateOutput("\", propOrder = {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    \"_");
                    __printer.Write(ar.ItemType.Name);
                    __printer.WriteTemplateOutput("\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("})");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("public class ArrayOf");
                    __printer.Write(Generated_FirstLetterUp(ar.ItemType.Name));
                    __printer.WriteTemplateOutput(" {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    @XmlElement(name = \"");
                    __printer.Write(ar.ItemType.Name);
                    __printer.WriteTemplateOutput("\", type = ");
                    __printer.Write(Generated_PrintClassType(ar.ItemType));
                    __printer.WriteTemplateOutput(".class, nillable = true)");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    protected List<");
                    __printer.Write(Generated_PrintClassType(ar.ItemType));
                    __printer.WriteTemplateOutput("> _");
                    __printer.Write(ar.ItemType.Name);
                    __printer.WriteTemplateOutput(";");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    public List<");
                    __printer.Write(Generated_PrintClassType(ar.ItemType));
                    __printer.WriteTemplateOutput("> get");
                    __printer.Write(Generated_FirstLetterUp(ar.ItemType.Name));
                    __printer.WriteTemplateOutput("() {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        if (_");
                    __printer.Write(ar.ItemType.Name);
                    __printer.WriteTemplateOutput(" == null) {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            _");
                    __printer.Write(ar.ItemType.Name);
                    __printer.WriteTemplateOutput(" = new ArrayList<");
                    __printer.Write(Generated_PrintClassType(ar.ItemType));
                    __printer.WriteTemplateOutput(">();");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        return this._");
                    __printer.Write(ar.ItemType.Name);
                    __printer.WriteTemplateOutput(";");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("}");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateOperationType(Operation op)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("package ");
                    __printer.Write(Generated_GetPackage(op.Interface.Namespace).ToLower());
                    __printer.WriteTemplateOutput(";");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.bind.annotation.XmlAccessType;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.bind.annotation.XmlAccessorType;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.bind.annotation.XmlElement;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.bind.annotation.XmlType;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("@XmlAccessorType(XmlAccessType.FIELD)");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("@XmlType(name = \"");
                    __printer.Write(op.Name);
                    __printer.WriteTemplateOutput("\", propOrder = {");
                    __printer.WriteLine();
                    int __loop39_iteration = 0;
                    string comma = "";
                    var __loop39_result =
                        (from __loop39_tmp_item___noname37 in EnumerableExtensions.Enumerate((op.Parameters).GetEnumerator())
                        from __loop39_tmp_item_fi in EnumerableExtensions.Enumerate((__loop39_tmp_item___noname37).GetEnumerator()).OfType<OperationParameter>()
                        select
                            new
                            {
                                __loop39_item___noname37 = __loop39_tmp_item___noname37,
                                __loop39_item_fi = __loop39_tmp_item_fi,
                            }).ToArray();
                    foreach (var __loop39_item in __loop39_result)
                    {
                        var __noname37 = __loop39_item.__loop39_item___noname37;
                        var fi = __loop39_item.__loop39_item_fi;
                        ++__loop39_iteration;
                        if (__loop39_iteration >= 2)
                        {
                            comma = ",";
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.Write(comma);
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    \"");
                        __printer.Write(Generated_FirstLetterLow(fi.Name));
                        __printer.WriteTemplateOutput("\"\\");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("})");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("public class ");
                    __printer.Write(Generated_FirstLetterUp(op.Name));
                    __printer.WriteTemplateOutput(" {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    int __loop40_iteration = 0;
                    var __loop40_result =
                        (from __loop40_tmp_item___noname38 in EnumerableExtensions.Enumerate((op.Parameters).GetEnumerator())
                        from __loop40_tmp_item_pa in EnumerableExtensions.Enumerate((__loop40_tmp_item___noname38).GetEnumerator()).OfType<OperationParameter>()
                        select
                            new
                            {
                                __loop40_item___noname38 = __loop40_tmp_item___noname38,
                                __loop40_item_pa = __loop40_tmp_item_pa,
                            }).ToArray();
                    foreach (var __loop40_item in __loop40_result)
                    {
                        var __noname38 = __loop40_item.__loop40_item___noname38;
                        var pa = __loop40_item.__loop40_item_pa;
                        ++__loop40_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    @XmlElement(name = \"");
                        __printer.Write(pa.Name);
                        __printer.WriteTemplateOutput("\", required = true, nillable = ");
                        __printer.Write(Generated_IsNillableType(pa.Type));
                        __printer.WriteTemplateOutput(")");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    protected ");
                        __printer.Write(Generated_PrintType(pa.Type));
                        __printer.WriteTemplateOutput(" ");
                        __printer.Write(pa.Name);
                        __printer.WriteTemplateOutput(";");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    int __loop41_iteration = 0;
                    var __loop41_result =
                        (from __loop41_tmp_item___noname39 in EnumerableExtensions.Enumerate((op.Parameters).GetEnumerator())
                        from __loop41_tmp_item_pa in EnumerableExtensions.Enumerate((__loop41_tmp_item___noname39).GetEnumerator()).OfType<OperationParameter>()
                        select
                            new
                            {
                                __loop41_item___noname39 = __loop41_tmp_item___noname39,
                                __loop41_item_pa = __loop41_tmp_item_pa,
                            }).ToArray();
                    foreach (var __loop41_item in __loop41_result)
                    {
                        var __noname39 = __loop41_item.__loop41_item___noname39;
                        var pa = __loop41_item.__loop41_item_pa;
                        ++__loop41_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    public ");
                        __printer.Write(Generated_PrintType(pa.Type));
                        __printer.WriteTemplateOutput(" get");
                        __printer.Write(Generated_FirstLetterUp(pa.Name));
                        __printer.WriteTemplateOutput("() {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        return ");
                        __printer.Write(Generated_FirstLetterLow(pa.Name));
                        __printer.WriteTemplateOutput(";");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    }");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    public void set");
                        __printer.Write(Generated_FirstLetterUp(pa.Name));
                        __printer.WriteTemplateOutput("(");
                        __printer.Write(Generated_PrintType(pa.Type));
                        __printer.WriteTemplateOutput(" value) {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        this.");
                        __printer.Write(Generated_FirstLetterLow(pa.Name));
                        __printer.WriteTemplateOutput(" = value;");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    }");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("}");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateOperationResponseType(Operation op)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("package ");
                    __printer.Write(Generated_GetPackage(op.Interface.Namespace).ToLower());
                    __printer.WriteTemplateOutput(";");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.bind.annotation.XmlAccessType;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.bind.annotation.XmlAccessorType;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.bind.annotation.XmlElement;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.bind.annotation.XmlType;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("@XmlAccessorType(XmlAccessType.FIELD)");
                    __printer.WriteLine();
                    if (op.ReturnType == PseudoType.Void)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("@XmlType(name = \"");
                        __printer.Write(op.Name);
                        __printer.WriteTemplateOutput("Response\")");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("public class ");
                        __printer.Write(Generated_FirstLetterUp(op.Name));
                        __printer.WriteTemplateOutput("Response {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("}");
                        __printer.WriteLine();
                    }
                    else
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("@XmlType(name = \"");
                        __printer.Write(op.Name);
                        __printer.WriteTemplateOutput("Response\", propOrder = {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    \"");
                        __printer.Write(Generated_FirstLetterLow(op.Name));
                        __printer.WriteTemplateOutput("Result\"");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("})");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("public class ");
                        __printer.Write(Generated_FirstLetterUp(op.Name));
                        __printer.WriteTemplateOutput("Response {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    @XmlElement(name = \"");
                        __printer.Write(op.Name);
                        __printer.WriteTemplateOutput("Result\", required = true, nillable = ");
                        __printer.Write(Generated_IsNillableType(op.ReturnType));
                        __printer.WriteTemplateOutput(")");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    protected ");
                        __printer.Write(Generated_PrintType(op.ReturnType));
                        __printer.WriteTemplateOutput(" ");
                        __printer.Write(Generated_FirstLetterLow(op.Name));
                        __printer.WriteTemplateOutput("Result;");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    public ");
                        __printer.Write(Generated_PrintType(op.ReturnType));
                        __printer.WriteTemplateOutput(" get");
                        __printer.Write(Generated_FirstLetterUp(op.Name));
                        __printer.WriteTemplateOutput("Result() {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        return ");
                        __printer.Write(Generated_FirstLetterLow(op.Name));
                        __printer.WriteTemplateOutput("Result;");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    }");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    public void set");
                        __printer.Write(Generated_FirstLetterUp(op.Name));
                        __printer.WriteTemplateOutput("Result(");
                        __printer.Write(Generated_PrintType(op.ReturnType));
                        __printer.WriteTemplateOutput(" value) {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        this.");
                        __printer.Write(Generated_FirstLetterLow(op.Name));
                        __printer.WriteTemplateOutput("Result = value;");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    }");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("}");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateOperationException(ExceptionType ex)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("package ");
                    __printer.Write(Generated_GetPackage(ex.Namespace).ToLower());
                    __printer.WriteTemplateOutput(";");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.ws.WebFault;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("@WebFault(name = \"");
                    __printer.Write(ex.Name);
                    __printer.WriteTemplateOutput("\", targetNamespace = \"");
                    __printer.Write(Generated_GetUri(ex.Namespace));
                    __printer.WriteTemplateOutput("\")");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("public class ");
                    __printer.Write(ex.Name);
                    __printer.WriteTemplateOutput(" extends Exception {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    private ");
                    __printer.Write(Generated_PrintType(ex));
                    __printer.WriteTemplateOutput("Fault faultInfo;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    public ");
                    __printer.Write(ex.Name);
                    __printer.WriteTemplateOutput("(String message, ");
                    __printer.Write(Generated_PrintType(ex));
                    __printer.WriteTemplateOutput("Fault faultInfo) {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        super(message);");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        this.faultInfo = faultInfo;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    public ");
                    __printer.Write(ex.Name);
                    __printer.WriteTemplateOutput("(String message, ");
                    __printer.Write(Generated_PrintType(ex));
                    __printer.WriteTemplateOutput("Fault faultInfo, Throwable cause) {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        super(message, cause);");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        this.faultInfo = faultInfo;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    public ");
                    __printer.Write(Generated_PrintType(ex));
                    __printer.WriteTemplateOutput("Fault getFaultInfo() {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        return faultInfo;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("}");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateOperationHead(Operation op, bool webparam)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    if (webparam)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("public ");
                        __printer.Write(Generated_PrintType(op.ReturnType));
                        __printer.WriteTemplateOutput(" ");
                        __printer.Write(Generated_FirstLetterLow(op.Name));
                        __printer.WriteTemplateOutput("(");
                        __printer.WriteLine();
                        int __loop42_iteration = 0;
                        string comma = "";
                        var __loop42_result =
                            (from __loop42_tmp_item___noname40 in EnumerableExtensions.Enumerate((op.Parameters).GetEnumerator())
                            from __loop42_tmp_item_pa in EnumerableExtensions.Enumerate((__loop42_tmp_item___noname40).GetEnumerator()).OfType<OperationParameter>()
                            select
                                new
                                {
                                    __loop42_item___noname40 = __loop42_tmp_item___noname40,
                                    __loop42_item_pa = __loop42_tmp_item_pa,
                                }).ToArray();
                        foreach (var __loop42_item in __loop42_result)
                        {
                            var __noname40 = __loop42_item.__loop42_item___noname40;
                            var pa = __loop42_item.__loop42_item_pa;
                            ++__loop42_iteration;
                            if (__loop42_iteration >= 2)
                            {
                                comma = ",";
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.Write(comma);
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    @WebParam(name = \"");
                            __printer.Write(pa.Name);
                            __printer.WriteTemplateOutput("\", targetNamespace = \"");
                            __printer.Write(Generated_GetUri(op.Interface.Namespace));
                            __printer.WriteTemplateOutput("\")");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                            __printer.Write(Generated_PrintType(pa.Type));
                            __printer.WriteTemplateOutput(" ");
                            __printer.Write(pa.Name);
                            __printer.WriteTemplateOutput("\\");
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput(")");
                        __printer.WriteLine();
                        int __loop43_iteration = 0;
                        string del = Generated_ThrowsSoaMMException();
                        var __loop43_result =
                            (from __loop43_tmp_item___noname41 in EnumerableExtensions.Enumerate((op.Exceptions).GetEnumerator())
                            from __loop43_tmp_item_ex in EnumerableExtensions.Enumerate((__loop43_tmp_item___noname41).GetEnumerator()).OfType<ExceptionType>()
                            select
                                new
                                {
                                    __loop43_item___noname41 = __loop43_tmp_item___noname41,
                                    __loop43_item_ex = __loop43_tmp_item_ex,
                                }).ToArray();
                        foreach (var __loop43_item in __loop43_result)
                        {
                            var __noname41 = __loop43_item.__loop43_item___noname41;
                            var ex = __loop43_item.__loop43_item_ex;
                            ++__loop43_iteration;
                            if (__loop43_iteration >= 2)
                            {
                                del = ", ";
                            }
                            __printer.Write(del);
                            __printer.Write(ex.Name);
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                    }
                    else
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.Write(Generated_GenerateOperationHead(op));
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateOperationHead(Operation op)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("public ");
                    __printer.Write(Generated_PrintType(op.ReturnType));
                    __printer.WriteTemplateOutput(" ");
                    __printer.Write(Generated_FirstLetterLow(op.Name));
                    __printer.WriteTemplateOutput("(\\");
                    __printer.WriteLine();
                    int __loop44_iteration = 0;
                    string comma = "";
                    var __loop44_result =
                        (from __loop44_tmp_item___noname42 in EnumerableExtensions.Enumerate((op.Parameters).GetEnumerator())
                        from __loop44_tmp_item_pa in EnumerableExtensions.Enumerate((__loop44_tmp_item___noname42).GetEnumerator()).OfType<OperationParameter>()
                        select
                            new
                            {
                                __loop44_item___noname42 = __loop44_tmp_item___noname42,
                                __loop44_item_pa = __loop44_tmp_item_pa,
                            }).ToArray();
                    foreach (var __loop44_item in __loop44_result)
                    {
                        var __noname42 = __loop44_item.__loop44_item___noname42;
                        var pa = __loop44_item.__loop44_item_pa;
                        ++__loop44_iteration;
                        if (__loop44_iteration >= 2)
                        {
                            comma = ", ";
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.Write(comma);
                        __printer.Write(Generated_PrintType(pa.Type));
                        __printer.WriteTemplateOutput(" ");
                        __printer.Write(pa.Name);
                        __printer.WriteTemplateOutput("\\");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput(")");
                    int __loop45_iteration = 0;
                    string del = Generated_ThrowsSoaMMException();
                    var __loop45_result =
                        (from __loop45_tmp_item___noname43 in EnumerableExtensions.Enumerate((op.Exceptions).GetEnumerator())
                        from __loop45_tmp_item_ex in EnumerableExtensions.Enumerate((__loop45_tmp_item___noname43).GetEnumerator()).OfType<ExceptionType>()
                        select
                            new
                            {
                                __loop45_item___noname43 = __loop45_tmp_item___noname43,
                                __loop45_item_ex = __loop45_tmp_item_ex,
                            }).ToArray();
                    foreach (var __loop45_item in __loop45_result)
                    {
                        var __noname43 = __loop45_item.__loop45_item___noname43;
                        var ex = __loop45_item.__loop45_item_ex;
                        ++__loop45_iteration;
                        if (__loop45_iteration >= 2)
                        {
                            del = ", ";
                        }
                        __printer.Write(del);
                        __printer.Write(ex.Name);
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateOperationCall(Operation op)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.Write(Generated_FirstLetterLow(op.Name));
                    __printer.WriteTemplateOutput("(\\");
                    __printer.WriteLine();
                    int __loop46_iteration = 0;
                    string comma = "";
                    var __loop46_result =
                        (from __loop46_tmp_item___noname44 in EnumerableExtensions.Enumerate((op.Parameters).GetEnumerator())
                        from __loop46_tmp_item_pa in EnumerableExtensions.Enumerate((__loop46_tmp_item___noname44).GetEnumerator()).OfType<OperationParameter>()
                        select
                            new
                            {
                                __loop46_item___noname44 = __loop46_tmp_item___noname44,
                                __loop46_item_pa = __loop46_tmp_item_pa,
                            }).ToArray();
                    foreach (var __loop46_item in __loop46_result)
                    {
                        var __noname44 = __loop46_item.__loop46_item___noname44;
                        var pa = __loop46_item.__loop46_item_pa;
                        ++__loop46_iteration;
                        if (__loop46_iteration >= 2)
                        {
                            comma = ", ";
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.Write(comma);
                        __printer.Write(pa.Name);
                        __printer.WriteTemplateOutput("\\");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput(")");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateOperationRefHead(OperationImplementation oi)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("public ");
                    __printer.Write(Generated_PrintType(oi.Operation.ReturnType));
                    __printer.WriteTemplateOutput(" ");
                    __printer.Write(Generated_FirstLetterLow(oi.Operation.Name));
                    __printer.WriteTemplateOutput(" ");
                    __printer.Write(Generated_GenerateOperationRefHeadParams(oi));
                    __printer.Write(Generated_ThrowsSoaMMException());
                    int __loop47_iteration = 0;
                    string del = ", ";
                    var __loop47_result =
                        (from __loop47_tmp_item___noname45 in EnumerableExtensions.Enumerate((oi.Operation.Exceptions).GetEnumerator())
                        from __loop47_tmp_item_ex in EnumerableExtensions.Enumerate((__loop47_tmp_item___noname45).GetEnumerator()).OfType<ExceptionType>()
                        select
                            new
                            {
                                __loop47_item___noname45 = __loop47_tmp_item___noname45,
                                __loop47_item_ex = __loop47_tmp_item_ex,
                            }).ToArray();
                    foreach (var __loop47_item in __loop47_result)
                    {
                        var __noname45 = __loop47_item.__loop47_item___noname45;
                        var ex = __loop47_item.__loop47_item_ex;
                        ++__loop47_iteration;
                        __printer.Write(del);
                        __printer.Write(ex.Name);
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public string Generated_ThrowsSoaMMException()
            {
                if (!Properties.NoImplementationDelegates)
                {
                    return " throws SoaMMException";
                }
                else
                {
                    return " throws ";
                }
            }
            
            public List<string> Generated_GenerateOperationRefHeadParams(OperationImplementation oi)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("(\\");
                    __printer.WriteLine();
                    int __loop48_iteration = 0;
                    string comma = "";
                    var __loop48_result =
                        (from __loop48_tmp_item___noname46 in EnumerableExtensions.Enumerate((oi.References).GetEnumerator())
                        from __loop48_tmp_item_re in EnumerableExtensions.Enumerate((__loop48_tmp_item___noname46).GetEnumerator()).OfType<Reference>()
                        where __loop48_tmp_item_re.Object is OperationParameter
                        select
                            new
                            {
                                __loop48_item___noname46 = __loop48_tmp_item___noname46,
                                __loop48_item_re = __loop48_tmp_item_re,
                            }).ToArray();
                    foreach (var __loop48_item in __loop48_result)
                    {
                        var __noname46 = __loop48_item.__loop48_item___noname46;
                        var re = __loop48_item.__loop48_item_re;
                        ++__loop48_iteration;
                        if (__loop48_iteration >= 2)
                        {
                            comma = ", ";
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.Write(comma);
                        __printer.Write(Generated_PrintType(((OperationParameter)re.Object).Type));
                        __printer.WriteTemplateOutput(" ");
                        __printer.Write(re.Name);
                        __printer.WriteTemplateOutput("\\");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput(")");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateOperationRefCall(OperationImplementation oi)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.Write(Generated_FirstLetterLow(oi.Operation.Name));
                    __printer.Write(Generated_GenerateOperationRefCallParams(oi));
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateOperationRefCallParams(OperationImplementation oi)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("(\\");
                    __printer.WriteLine();
                    int __loop49_iteration = 0;
                    string comma = "";
                    var __loop49_result =
                        (from __loop49_tmp_item___noname47 in EnumerableExtensions.Enumerate((oi.References).GetEnumerator())
                        from __loop49_tmp_item_re in EnumerableExtensions.Enumerate((__loop49_tmp_item___noname47).GetEnumerator()).OfType<Reference>()
                        where __loop49_tmp_item_re.Object is OperationParameter
                        select
                            new
                            {
                                __loop49_item___noname47 = __loop49_tmp_item___noname47,
                                __loop49_item_re = __loop49_tmp_item_re,
                            }).ToArray();
                    foreach (var __loop49_item in __loop49_result)
                    {
                        var __noname47 = __loop49_item.__loop49_item___noname47;
                        var re = __loop49_item.__loop49_item_re;
                        ++__loop49_iteration;
                        if (__loop49_iteration >= 2)
                        {
                            comma = ", ";
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.Write(comma);
                        __printer.Write(re.Name);
                        __printer.WriteTemplateOutput("\\");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput(")");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateInterface(Interface intf)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("package ");
                    __printer.Write(Generated_GetPackage(intf.Namespace).ToLower());
                    __printer.WriteTemplateOutput(";");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.jws.*;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.bind.annotation.XmlSeeAlso;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.ws.*;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("@WebService(targetNamespace = \"");
                    __printer.Write(Generated_GetUri(intf.Namespace));
                    __printer.WriteTemplateOutput("\")");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("@XmlSeeAlso({");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    ObjectFactory.class");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("})");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("public interface ");
                    __printer.Write(intf.Name);
                    __printer.WriteTemplateOutput(" \\");
                    __printer.WriteLine();
                    if (intf.SuperInterfaces.Count != 0)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("extends \\");
                        __printer.WriteLine();
                        int __loop50_iteration = 0;
                        string comma = "";
                        var __loop50_result =
                            (from __loop50_tmp_item___noname48 in EnumerableExtensions.Enumerate((intf.SuperInterfaces).GetEnumerator())
                            from __loop50_tmp_item_sup in EnumerableExtensions.Enumerate((__loop50_tmp_item___noname48).GetEnumerator()).OfType<Interface>()
                            select
                                new
                                {
                                    __loop50_item___noname48 = __loop50_tmp_item___noname48,
                                    __loop50_item_sup = __loop50_tmp_item_sup,
                                }).ToArray();
                        foreach (var __loop50_item in __loop50_result)
                        {
                            var __noname48 = __loop50_item.__loop50_item___noname48;
                            var sup = __loop50_item.__loop50_item_sup;
                            ++__loop50_iteration;
                            if (__loop50_iteration >= 2)
                            {
                                comma = ", ";
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.Write(comma);
                            __printer.Write(Generated_GetPackage(sup.Namespace).ToLower());
                            __printer.WriteTemplateOutput(".");
                            __printer.Write(sup.Name);
                            __printer.WriteTemplateOutput("\\");
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput(" {");
                        __printer.WriteLine();
                    }
                    else
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("{");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    int __loop51_iteration = 0;
                    var __loop51_result =
                        (from __loop51_tmp_item___noname49 in EnumerableExtensions.Enumerate((intf.Operations).GetEnumerator())
                        from __loop51_tmp_item_op in EnumerableExtensions.Enumerate((__loop51_tmp_item___noname49).GetEnumerator()).OfType<Operation>()
                        select
                            new
                            {
                                __loop51_item___noname49 = __loop51_tmp_item___noname49,
                                __loop51_item_op = __loop51_tmp_item_op,
                            }).ToArray();
                    foreach (var __loop51_item in __loop51_result)
                    {
                        var __noname49 = __loop51_item.__loop51_item___noname49;
                        var op = __loop51_item.__loop51_item_op;
                        ++__loop51_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    @WebMethod(operationName = \"");
                        __printer.Write(op.Name);
                        __printer.WriteTemplateOutput("\", action = \"");
                        __printer.Write(Generated_GetUriWithSlash(op.Interface.Namespace) + op.Interface.Name + "/" + op.Name);
                        __printer.WriteTemplateOutput("\")");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    ");
                        if (op.ReturnType == PseudoType.Async)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    @Oneway");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    @RequestWrapper(localName = \"");
                            __printer.Write(op.Name);
                            __printer.WriteTemplateOutput("\", targetNamespace = \"");
                            __printer.Write(Generated_GetUri(op.Interface.Namespace));
                            __printer.WriteTemplateOutput("\", className = \"");
                            __printer.Write(Generated_GetPackage(op.Interface.Namespace).ToLower());
                            __printer.WriteTemplateOutput(".");
                            __printer.Write(op.Name);
                            __printer.WriteTemplateOutput("\")");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    @Action(");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        input=\"");
                            __printer.Write(Generated_GetUriWithSlash(op.Interface.Namespace) + op.Interface.Name + "/" + op.Name);
                            __printer.WriteTemplateOutput("\"");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        ");
                            if (op.Exceptions.Count > 0)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        , fault = {");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        ");
                                int __loop52_iteration = 0;
                                string delim = "";
                                var __loop52_result =
                                    (from __loop52_tmp_item___noname50 in EnumerableExtensions.Enumerate((op.Exceptions).GetEnumerator())
                                    from __loop52_tmp_item_ex in EnumerableExtensions.Enumerate((__loop52_tmp_item___noname50).GetEnumerator()).OfType<ExceptionType>()
                                    select
                                        new
                                        {
                                            __loop52_item___noname50 = __loop52_tmp_item___noname50,
                                            __loop52_item_ex = __loop52_tmp_item_ex,
                                        }).ToArray();
                                foreach (var __loop52_item in __loop52_result)
                                {
                                    var __noname50 = __loop52_item.__loop52_item___noname50;
                                    var ex = __loop52_item.__loop52_item_ex;
                                    ++__loop52_iteration;
                                    if (__loop52_iteration >= 2)
                                    {
                                        delim = ",";
                                    }
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("            ");
                                    __printer.Write(delim);
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("            @FaultAction(className = ");
                                    __printer.Write(ex.Name);
                                    __printer.WriteTemplateOutput(".class, value=\"");
                                    __printer.Write(Generated_GetUriWithSlash(op.Interface.Namespace) + op.Interface.Name + "/" + op.Name + "Fault/" + ex.Name);
                                    __printer.WriteTemplateOutput("\")");
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("        ");
                                }
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        }");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        ");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        )");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                        }
                        else
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    @WebResult(name = \"");
                            __printer.Write(op.Name);
                            __printer.WriteTemplateOutput("Result\", targetNamespace = \"");
                            __printer.Write(Generated_GetUri(op.Interface.Namespace));
                            __printer.WriteTemplateOutput("\")");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    @RequestWrapper(localName = \"");
                            __printer.Write(op.Name);
                            __printer.WriteTemplateOutput("\", targetNamespace = \"");
                            __printer.Write(Generated_GetUri(op.Interface.Namespace));
                            __printer.WriteTemplateOutput("\", className = \"");
                            __printer.Write(Generated_GetPackage(op.Interface.Namespace).ToLower());
                            __printer.WriteTemplateOutput(".");
                            __printer.Write(op.Name);
                            __printer.WriteTemplateOutput("\")");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    @ResponseWrapper(localName = \"");
                            __printer.Write(op.Name);
                            __printer.WriteTemplateOutput("Response\", targetNamespace = \"");
                            __printer.Write(Generated_GetUri(op.Interface.Namespace));
                            __printer.WriteTemplateOutput("\", className = \"");
                            __printer.Write(Generated_GetPackage(op.Interface.Namespace).ToLower());
                            __printer.WriteTemplateOutput(".");
                            __printer.Write(op.Name);
                            __printer.WriteTemplateOutput("Response\")");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    @Action(");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        input=\"");
                            __printer.Write(Generated_GetUriWithSlash(op.Interface.Namespace) + op.Interface.Name + "/" + op.Name);
                            __printer.WriteTemplateOutput("\",");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        output=\"");
                            __printer.Write(Generated_GetUriWithSlash(op.Interface.Namespace) + op.Interface.Name + "/" + op.Name + "Response");
                            __printer.WriteTemplateOutput("\"");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        ");
                            if (op.Exceptions.Count > 0)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        , fault = {");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        ");
                                int __loop53_iteration = 0;
                                string delim = "";
                                var __loop53_result =
                                    (from __loop53_tmp_item___noname51 in EnumerableExtensions.Enumerate((op.Exceptions).GetEnumerator())
                                    from __loop53_tmp_item_ex in EnumerableExtensions.Enumerate((__loop53_tmp_item___noname51).GetEnumerator()).OfType<ExceptionType>()
                                    select
                                        new
                                        {
                                            __loop53_item___noname51 = __loop53_tmp_item___noname51,
                                            __loop53_item_ex = __loop53_tmp_item_ex,
                                        }).ToArray();
                                foreach (var __loop53_item in __loop53_result)
                                {
                                    var __noname51 = __loop53_item.__loop53_item___noname51;
                                    var ex = __loop53_item.__loop53_item_ex;
                                    ++__loop53_iteration;
                                    if (__loop53_iteration >= 2)
                                    {
                                        delim = ",";
                                    }
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("            ");
                                    __printer.Write(delim);
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("            @FaultAction(className = ");
                                    __printer.Write(ex.Name);
                                    __printer.WriteTemplateOutput(".class, value=\"");
                                    __printer.Write(Generated_GetUriWithSlash(op.Interface.Namespace) + op.Interface.Name + "/" + op.Name + "Fault/" + ex.Name);
                                    __printer.WriteTemplateOutput("\")");
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("        ");
                                }
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        }");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        ");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        )");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    ");
                        __printer.Write(Generated_GenerateOperationHead(op, true));
                        __printer.WriteTemplateOutput(";");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("}");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateImplementationBase(Interface intf)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("package ");
                    __printer.Write(Generated_GetPackage(intf.Namespace).ToLower());
                    __printer.WriteTemplateOutput(";");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("public class ");
                    __printer.Write(intf.Name.Substring(1));
                    __printer.WriteTemplateOutput("Base implements ");
                    __printer.Write(intf.Name);
                    __printer.WriteTemplateOutput(" {");
                    __printer.WriteLine();
                    int __loop54_iteration = 0;
                    var __loop54_result =
                        (from __loop54_tmp_item___noname52 in EnumerableExtensions.Enumerate((intf.Operations).GetEnumerator())
                        from __loop54_tmp_item_op in EnumerableExtensions.Enumerate((__loop54_tmp_item___noname52).GetEnumerator()).OfType<Operation>()
                        select
                            new
                            {
                                __loop54_item___noname52 = __loop54_tmp_item___noname52,
                                __loop54_item_op = __loop54_tmp_item_op,
                            }).ToArray();
                    foreach (var __loop54_item in __loop54_result)
                    {
                        var __noname52 = __loop54_item.__loop54_item___noname52;
                        var op = __loop54_item.__loop54_item_op;
                        ++__loop54_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    ");
                        __printer.Write(Generated_GenerateOperationHead(op));
                        __printer.WriteTemplateOutput(" {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    }");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("}");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateService(Endpoint endp)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("package ");
                    __printer.Write(Generated_GetPackage(endp.Namespace).ToLower());
                    __printer.WriteTemplateOutput(";");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.jws.WebService;");
                    __printer.WriteLine();
                    if (endp.Authorization != null)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("import javax.annotation.Resource;");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("import javax.xml.ws.WebServiceContext;");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    if (endp.Binding.Encoding is SoapEncodingBindingElement && ((SoapEncodingBindingElement)endp.Binding.Encoding).Version == SoapVersion.Soap12)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("@javax.xml.ws.BindingType(value=javax.xml.ws.soap.SOAPBinding.SOAP12HTTP_BINDING)");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    if (endp.Binding.Encoding is SoapEncodingBindingElement && ((SoapEncodingBindingElement)endp.Binding.Encoding).MtomEnabled)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("@javax.xml.ws.soap.MTOM");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    if (Properties.GenerateOracleAnnotations)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                        if (endp.HasAddressing())
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("@javax.xml.ws.soap.Addressing(required = true)");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                        if (endp.Binding.HasPolicy())
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("@weblogic.jws.Policy(uri = \"policy:");
                            __printer.Write(endp.Binding.Name);
                            __printer.WriteTemplateOutput("_Policy.xml\")");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                        if (endp.HasSecurity())
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("@weblogic.jws.security.WssConfiguration(\"");
                            __printer.Write(endp.Namespace.FullName);
                            __printer.WriteTemplateOutput("Security\")");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    if (Properties.SeparateWsdlsForEndpoints)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("@WebService(serviceName = \"");
                        __printer.Write(endp.Name);
                        __printer.WriteTemplateOutput("\", portName = \"");
                        __printer.Write(endp.Interface.Name);
                        __printer.WriteTemplateOutput("_");
                        __printer.Write(endp.Binding.Name);
                        __printer.WriteTemplateOutput("_Port\", ");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("endpointInterface = \"");
                        __printer.Write(Generated_GetPackage(endp.Namespace).ToLower());
                        __printer.WriteTemplateOutput(".");
                        __printer.Write(endp.Interface.Name);
                        __printer.WriteTemplateOutput("\", targetNamespace = \"");
                        __printer.Write(Generated_GetUri(endp.Namespace));
                        __printer.WriteTemplateOutput("\", wsdlLocation = \"");
                        __printer.Write(Properties.WsdlDirectory);
                        __printer.Write(endp.Name);
                        __printer.Write(Properties.WsdlSuffix);
                        __printer.WriteTemplateOutput(".wsdl\")");
                        __printer.WriteLine();
                    }
                    else
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("@WebService(serviceName = \"");
                        __printer.Write(endp.Name);
                        __printer.WriteTemplateOutput("\", portName = \"");
                        __printer.Write(endp.Interface.Name);
                        __printer.WriteTemplateOutput("_");
                        __printer.Write(endp.Binding.Name);
                        __printer.WriteTemplateOutput("_Port\", ");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("endpointInterface = \"");
                        __printer.Write(Generated_GetPackage(endp.Namespace).ToLower());
                        __printer.WriteTemplateOutput(".");
                        __printer.Write(endp.Interface.Name);
                        __printer.WriteTemplateOutput("\", targetNamespace = \"");
                        __printer.Write(Generated_GetUri(endp.Namespace));
                        __printer.WriteTemplateOutput("\", wsdlLocation = \"");
                        __printer.Write(Properties.WsdlDirectory);
                        __printer.Write(endp.Namespace.FullName);
                        __printer.Write(Properties.WsdlSuffix);
                        __printer.WriteTemplateOutput(".wsdl\")");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("public class ");
                    __printer.Write(endp.Name);
                    __printer.WriteTemplateOutput(" implements ");
                    __printer.Write(endp.Interface.Name);
                    __printer.WriteTemplateOutput(" {");
                    __printer.WriteLine();
                    if (endp.Authorization != null)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    @Resource");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    private WebServiceContext context;");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    private ");
                    __printer.Write(endp.Interface.Name);
                    __printer.WriteTemplateOutput(" inner;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    public ");
                    __printer.Write(endp.Name);
                    __printer.WriteTemplateOutput("() {");
                    __printer.WriteLine();
                    if (endp.Authorization == null && endp.Contract == null)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        inner = new ");
                        __printer.Write(endp.Name);
                        __printer.WriteTemplateOutput("Impl();");
                        __printer.WriteLine();
                    }
                    else if (endp.Authorization == null && endp.Contract != null)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        inner = new ");
                        __printer.Write(endp.Contract.Name);
                        __printer.WriteTemplateOutput("(new ");
                        __printer.Write(endp.Name);
                        __printer.WriteTemplateOutput("Impl());");
                        __printer.WriteLine();
                    }
                    else if (endp.Authorization != null && endp.Contract == null)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        inner = new ");
                        __printer.Write(endp.Authorization.Name);
                        __printer.WriteTemplateOutput("(context, new ");
                        __printer.Write(endp.Name);
                        __printer.WriteTemplateOutput("Impl());");
                        __printer.WriteLine();
                    }
                    else
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        inner = new ");
                        __printer.Write(endp.Authorization.Name);
                        __printer.WriteTemplateOutput("(context, new ");
                        __printer.Write(endp.Contract.Name);
                        __printer.WriteTemplateOutput("(new ");
                        __printer.Write(endp.Name);
                        __printer.WriteTemplateOutput("Impl()));");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                    int __loop55_iteration = 0;
                    var __loop55_result =
                        (from __loop55_tmp_item___noname53 in EnumerableExtensions.Enumerate((endp.Interface.Operations).GetEnumerator())
                        from __loop55_tmp_item_op in EnumerableExtensions.Enumerate((__loop55_tmp_item___noname53).GetEnumerator()).OfType<Operation>()
                        select
                            new
                            {
                                __loop55_item___noname53 = __loop55_tmp_item___noname53,
                                __loop55_item_op = __loop55_tmp_item_op,
                            }).ToArray();
                    foreach (var __loop55_item in __loop55_result)
                    {
                        var __noname53 = __loop55_item.__loop55_item___noname53;
                        var op = __loop55_item.__loop55_item_op;
                        ++__loop55_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    ");
                        __printer.Write(Generated_GenerateOperationHead(op, false));
                        __printer.WriteTemplateOutput(" {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    ");
                        if (op.ReturnType != PseudoType.Void && op.ReturnType != PseudoType.Async)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        return this.inner.");
                            __printer.Write(Generated_GenerateOperationCall(op));
                            __printer.WriteTemplateOutput(";");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                        }
                        else
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        this.inner.");
                            __printer.Write(Generated_GenerateOperationCall(op));
                            __printer.WriteTemplateOutput(";");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    }");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("}");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateImpl(Endpoint endp)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("package ");
                    __printer.Write(Generated_GetPackage(endp.Namespace).ToLower());
                    __printer.WriteTemplateOutput(";");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    if (Properties.NoImplementationDelegates)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("import javax.jws.WebService;");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    if (!Properties.NoImplementationDelegates)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        if (Properties.GenerateImplementationBase)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("public class ");
                            __printer.Write(endp.Name);
                            __printer.WriteTemplateOutput("Impl extends ");
                            __printer.Write(endp.Interface.Name.Substring(1));
                            __printer.WriteTemplateOutput("Base implements ");
                            __printer.Write(endp.Interface.Name);
                            __printer.WriteTemplateOutput(" {");
                            __printer.WriteLine();
                        }
                        else
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("public class ");
                            __printer.Write(endp.Name);
                            __printer.WriteTemplateOutput("Impl implements ");
                            __printer.Write(endp.Interface.Name);
                            __printer.WriteTemplateOutput(" {");
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                    }
                    else
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        if (endp.Binding.Encoding is SoapEncodingBindingElement && ((SoapEncodingBindingElement)endp.Binding.Encoding).Version == SoapVersion.Soap12)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("@javax.xml.ws.BindingType(value=javax.xml.ws.soap.SOAPBinding.SOAP12HTTP_BINDING)");
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        if (endp.Binding.Encoding is SoapEncodingBindingElement && ((SoapEncodingBindingElement)endp.Binding.Encoding).MtomEnabled)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("@javax.xml.ws.soap.MTOM");
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        if (Properties.GenerateOracleAnnotations)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                            if (endp.HasAddressing())
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("@javax.xml.ws.soap.Addressing(required = true)");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("	");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                            if (endp.Binding.HasPolicy())
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("@weblogic.jws.Policy(uri = \"policy:");
                                __printer.Write(endp.Binding.Name);
                                __printer.WriteTemplateOutput("_Policy.xml\")");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("	");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                            if (endp.HasSecurity())
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("@weblogic.jws.security.WssConfiguration(\"");
                                __printer.Write(endp.Namespace.FullName);
                                __printer.WriteTemplateOutput("Security\")");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("	");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        if (Properties.SeparateWsdlsForEndpoints)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("@WebService(serviceName = \"");
                            __printer.Write(endp.Name);
                            __printer.WriteTemplateOutput("\", portName = \"");
                            __printer.Write(endp.Interface.Name);
                            __printer.WriteTemplateOutput("_");
                            __printer.Write(endp.Binding.Name);
                            __printer.WriteTemplateOutput("_Port\", ");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("endpointInterface = \"");
                            __printer.Write(Generated_GetPackage(endp.Namespace).ToLower());
                            __printer.WriteTemplateOutput(".");
                            __printer.Write(endp.Interface.Name);
                            __printer.WriteTemplateOutput("\", targetNamespace = \"");
                            __printer.Write(Generated_GetUri(endp.Namespace));
                            __printer.WriteTemplateOutput("\", wsdlLocation = \"");
                            __printer.Write(Properties.WsdlDirectory);
                            __printer.Write(endp.Name);
                            __printer.Write(Properties.WsdlSuffix);
                            __printer.WriteTemplateOutput(".wsdl\")");
                            __printer.WriteLine();
                        }
                        else
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("@WebService(serviceName = \"");
                            __printer.Write(endp.Name);
                            __printer.WriteTemplateOutput("\", portName = \"");
                            __printer.Write(endp.Interface.Name);
                            __printer.WriteTemplateOutput("_");
                            __printer.Write(endp.Binding.Name);
                            __printer.WriteTemplateOutput("_Port\", ");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("endpointInterface = \"");
                            __printer.Write(Generated_GetPackage(endp.Namespace).ToLower());
                            __printer.WriteTemplateOutput(".");
                            __printer.Write(endp.Interface.Name);
                            __printer.WriteTemplateOutput("\", targetNamespace = \"");
                            __printer.Write(Generated_GetUri(endp.Namespace));
                            __printer.WriteTemplateOutput("\", wsdlLocation = \"");
                            __printer.Write(Properties.WsdlDirectory);
                            __printer.Write(endp.Namespace.FullName);
                            __printer.Write(Properties.WsdlSuffix);
                            __printer.WriteTemplateOutput(".wsdl\")");
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        if (Properties.GenerateImplementationBase)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("public class ");
                            __printer.Write(endp.Name);
                            __printer.WriteTemplateOutput(" extends ");
                            __printer.Write(endp.Interface.Name.Substring(1));
                            __printer.WriteTemplateOutput("Base implements ");
                            __printer.Write(endp.Interface.Name);
                            __printer.WriteTemplateOutput(" {");
                            __printer.WriteLine();
                        }
                        else
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("public class ");
                            __printer.Write(endp.Name);
                            __printer.WriteTemplateOutput(" implements ");
                            __printer.Write(endp.Interface.Name);
                            __printer.WriteTemplateOutput(" {");
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    int __loop56_iteration = 0;
                    var __loop56_result =
                        (from __loop56_tmp_item___noname54 in EnumerableExtensions.Enumerate((endp.Interface.Operations).GetEnumerator())
                        from __loop56_tmp_item_op in EnumerableExtensions.Enumerate((__loop56_tmp_item___noname54).GetEnumerator()).OfType<Operation>()
                        select
                            new
                            {
                                __loop56_item___noname54 = __loop56_tmp_item___noname54,
                                __loop56_item_op = __loop56_tmp_item_op,
                            }).ToArray();
                    foreach (var __loop56_item in __loop56_result)
                    {
                        var __noname54 = __loop56_item.__loop56_item___noname54;
                        var op = __loop56_item.__loop56_item_op;
                        ++__loop56_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    ");
                        __printer.Write(Generated_GenerateOperationHead(op, false));
                        __printer.WriteTemplateOutput(" {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        ");
                        if (Properties.GenerateImplementationBase)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("            ");
                            if (op.ReturnType != PseudoType.Void && op.ReturnType != PseudoType.Async)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        return super.");
                                __printer.Write(Generated_GenerateOperationCall(op));
                                __printer.WriteTemplateOutput(";                ");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("            ");
                            }
                            else
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        super.");
                                __printer.Write(Generated_GenerateOperationCall(op));
                                __printer.WriteTemplateOutput(";                ");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("            ");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        ");
                        }
                        else
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("            ");
                            if (Properties.ThrowNotImplementedException)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        // TODO implement this method");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        throw new UnsupportedOperationException(\"Not implemented yet.\");");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("            ");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        ");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    }");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("}");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateAuth(Authorization auth)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("package ");
                    __printer.Write(Generated_GetPackage(auth.Namespace).ToLower());
                    __printer.WriteTemplateOutput(";");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.ws.WebServiceContext;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import saml.SAMLSubject;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import saml.validator.BaseValidator;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("public class ");
                    __printer.Write(auth.Name);
                    __printer.WriteTemplateOutput(" extends BaseValidator implements ");
                    __printer.Write(auth.Interface.Name);
                    __printer.WriteTemplateOutput(" {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    private ");
                    __printer.Write(auth.Interface.Name);
                    __printer.WriteTemplateOutput(" inner;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    public ");
                    __printer.Write(auth.Name);
                    __printer.WriteTemplateOutput("(WebServiceContext context, ");
                    __printer.Write(auth.Interface.Name);
                    __printer.WriteTemplateOutput(" inner) {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        super(context);");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        this.inner = inner;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                    int __loop57_iteration = 0;
                    var __loop57_result =
                        (from __loop57_tmp_item___noname55 in EnumerableExtensions.Enumerate((auth.OperationAuthorizations).GetEnumerator())
                        from __loop57_tmp_item_op in EnumerableExtensions.Enumerate((__loop57_tmp_item___noname55).GetEnumerator()).OfType<OperationAuthorization>()
                        select
                            new
                            {
                                __loop57_item___noname55 = __loop57_tmp_item___noname55,
                                __loop57_item_op = __loop57_tmp_item_op,
                            }).ToArray();
                    foreach (var __loop57_item in __loop57_result)
                    {
                        var __noname55 = __loop57_item.__loop57_item___noname55;
                        var op = __loop57_item.__loop57_item_op;
                        ++__loop57_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    ");
                        __printer.Write(Generated_GenerateOperationRefHead(op));
                        __printer.WriteTemplateOutput(" {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        if(validate");
                        __printer.Write(Generated_FirstLetterUp(op.Operation.Name));
                        __printer.Write(Generated_GenerateOperationRefCallParams(op));
                        __printer.WriteTemplateOutput(") {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    ");
                        if (op.Operation.ReturnType != PseudoType.Void && op.Operation.ReturnType != PseudoType.Async)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("            return this.inner.");
                            __printer.Write(Generated_GenerateOperationRefCall(op));
                            __printer.WriteTemplateOutput(";");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                        }
                        else
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("            this.inner.");
                            __printer.Write(Generated_GenerateOperationRefCall(op));
                            __printer.WriteTemplateOutput(";");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        } else {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("            SoaMMFault faultInfo = new SoaMMFault();");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("            faultInfo.setMessage(\"Authorization Exception\");");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("            throw new SoaMMException(\"Authorization Exception\", faultInfo);");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        }");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    }");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    public boolean validate");
                        __printer.Write(Generated_FirstLetterUp(op.Operation.Name));
                        __printer.Write(Generated_GenerateOperationRefHeadParams(op));
                        __printer.WriteTemplateOutput(" {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        try {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("            SAMLSubject subject = getsAMLSubject();");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("            ");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    ");
                        int __loop58_iteration = 0;
                        var __loop58_result =
                            (from __loop58_tmp_item___noname56 in EnumerableExtensions.Enumerate((op.References).GetEnumerator())
                            from __loop58_tmp_item_re in EnumerableExtensions.Enumerate((__loop58_tmp_item___noname56).GetEnumerator()).OfType<Reference>()
                            select
                                new
                                {
                                    __loop58_item___noname56 = __loop58_tmp_item___noname56,
                                    __loop58_item_re = __loop58_tmp_item_re,
                                }).ToArray();
                        foreach (var __loop58_item in __loop58_result)
                        {
                            var __noname56 = __loop58_item.__loop58_item___noname56;
                            var re = __loop58_item.__loop58_item_re;
                            ++__loop58_iteration;
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        ");
                            if (re.Object is ClaimField)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("^");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("            ");
                                __printer.Write(Generated_PrintType(((ClaimField)re.Object).Type));
                                __printer.WriteTemplateOutput(" ");
                                __printer.Write(re.Name);
                                __printer.WriteTemplateOutput(" =  (");
                                __printer.Write(Generated_PrintType(((ClaimField)re.Object).Type));
                                __printer.WriteTemplateOutput(")subject.getAttribute(");
                                __printer.Write(Generated_PrintClassType(((ClaimField)re.Object).Type));
                                __printer.WriteTemplateOutput(".class,\"");
                                __printer.Write(((ClaimField)re.Object).Name);
                                __printer.WriteTemplateOutput("\");");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        ");
                            }
                            else if (re.Object is ClaimsetType)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("^");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("            ");
                                __printer.Write(Generated_GetPackage(((ClaimsetType)re.Object).Namespace).ToLower());
                                __printer.WriteTemplateOutput(".");
                                __printer.Write(((ClaimsetType)re.Object).Name);
                                __printer.WriteTemplateOutput(" ");
                                __printer.Write(re.Name);
                                __printer.WriteTemplateOutput(" = new ");
                                __printer.Write(Generated_GetPackage(((ClaimsetType)re.Object).Namespace).ToLower());
                                __printer.WriteTemplateOutput(".");
                                __printer.Write(((ClaimsetType)re.Object).Name);
                                __printer.WriteTemplateOutput("();");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("            ");
                                int __loop59_iteration = 0;
                                var __loop59_result =
                                    (from __loop59_tmp_item___noname57 in EnumerableExtensions.Enumerate((((ClaimsetType)re.Object).Fields).GetEnumerator())
                                    from __loop59_tmp_item_f in EnumerableExtensions.Enumerate((__loop59_tmp_item___noname57).GetEnumerator()).OfType<ClaimField>()
                                    select
                                        new
                                        {
                                            __loop59_item___noname57 = __loop59_tmp_item___noname57,
                                            __loop59_item_f = __loop59_tmp_item_f,
                                        }).ToArray();
                                foreach (var __loop59_item in __loop59_result)
                                {
                                    var __noname57 = __loop59_item.__loop59_item___noname57;
                                    var f = __loop59_item.__loop59_item_f;
                                    ++__loop59_iteration;
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("            ");
                                    __printer.Write(re.Name);
                                    __printer.WriteTemplateOutput(".set");
                                    __printer.Write(Generated_FirstLetterUp(f.Name));
                                    __printer.WriteTemplateOutput("((");
                                    __printer.Write(Generated_PrintType(f.Type));
                                    __printer.WriteTemplateOutput(")subject.getAttribute(");
                                    __printer.Write(Generated_PrintClassType(f.Type));
                                    __printer.WriteTemplateOutput(".class, \"");
                                    __printer.Write(f.Name);
                                    __printer.WriteTemplateOutput("\"));");
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("            ");
                                }
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        ");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    ");
                        int __loop60_iteration = 0;
                        var __loop60_result =
                            (from __loop60_tmp_item___noname58 in EnumerableExtensions.Enumerate((op.OperationAuthorizationStatements).GetEnumerator())
                            from __loop60_tmp_item_oas in EnumerableExtensions.Enumerate((__loop60_tmp_item___noname58).GetEnumerator()).OfType<OperationAuthorizationStatement>()
                            select
                                new
                                {
                                    __loop60_item___noname58 = __loop60_tmp_item___noname58,
                                    __loop60_item_oas = __loop60_tmp_item_oas,
                                }).ToArray();
                        foreach (var __loop60_item in __loop60_result)
                        {
                            var __noname58 = __loop60_item.__loop60_item___noname58;
                            var oas = __loop60_item.__loop60_item_oas;
                            ++__loop60_iteration;
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("            // Demand: ");
                            __printer.Write(oas.Text);
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("            if (");
                            __printer.Write(Generated_GenerateExpression(oas.Rule));
                            __printer.WriteTemplateOutput(") {");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("                return true;");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("            }");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        } catch (Exception ex) {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        }");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        return false;");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    }");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("}");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateContract(Contract con)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("package ");
                    __printer.Write(Generated_GetPackage(con.Namespace).ToLower());
                    __printer.WriteTemplateOutput(";");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("public class ");
                    __printer.Write(con.Name);
                    __printer.WriteTemplateOutput(" implements ");
                    __printer.Write(con.Interface.Name);
                    __printer.WriteTemplateOutput(" {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    private ");
                    __printer.Write(con.Interface.Name);
                    __printer.WriteTemplateOutput(" inner;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    public ");
                    __printer.Write(con.Name);
                    __printer.WriteTemplateOutput("(");
                    __printer.Write(con.Interface.Name);
                    __printer.WriteTemplateOutput(" inner) {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        this.inner = inner;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                    int __loop61_iteration = 0;
                    var __loop61_result =
                        (from __loop61_tmp_item___noname59 in EnumerableExtensions.Enumerate((con.OperationContracts).GetEnumerator())
                        from __loop61_tmp_item_op in EnumerableExtensions.Enumerate((__loop61_tmp_item___noname59).GetEnumerator()).OfType<OperationContract>()
                        select
                            new
                            {
                                __loop61_item___noname59 = __loop61_tmp_item___noname59,
                                __loop61_item_op = __loop61_tmp_item_op,
                            }).ToArray();
                    foreach (var __loop61_item in __loop61_result)
                    {
                        var __noname59 = __loop61_item.__loop61_item___noname59;
                        var op = __loop61_item.__loop61_item_op;
                        ++__loop61_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    ");
                        __printer.Write(Generated_GenerateOperationRefHead(op));
                        __printer.WriteTemplateOutput(" {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    ");
                        int __loop62_iteration = 0;
                        var __loop62_result =
                            (from __loop62_tmp_item___noname60 in EnumerableExtensions.Enumerate((op.OperationContractStatements).GetEnumerator())
                            from __loop62_tmp_item_ocs in EnumerableExtensions.Enumerate((__loop62_tmp_item___noname60).GetEnumerator()).OfType<Requires>()
                            select
                                new
                                {
                                    __loop62_item___noname60 = __loop62_tmp_item___noname60,
                                    __loop62_item_ocs = __loop62_tmp_item_ocs,
                                }).ToArray();
                        foreach (var __loop62_item in __loop62_result)
                        {
                            var __noname60 = __loop62_item.__loop62_item___noname60;
                            var ocs = __loop62_item.__loop62_item_ocs;
                            ++__loop62_iteration;
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("^");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        // Requires: ");
                            __printer.Write(ocs.Text);
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        if (!(");
                            __printer.Write(Generated_GenerateExpression(ocs.Rule));
                            __printer.WriteTemplateOutput(")) {");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("            ");
                            if (ocs.Otherwise == null)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("            SoaMMFault faultInfo = new SoaMMFault();");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("            faultInfo.setMessage(\"Contract requirement error\");");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("            throw new SoaMMException(\"Contract requirement error\", faultInfo);");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("            ");
                            }
                            else
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("            throw ");
                                __printer.Write(Generated_GenerateExpression(ocs.Otherwise));
                                __printer.WriteTemplateOutput(";");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("            ");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        }");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    ");
                        if (op.Operation.ReturnType != PseudoType.Void && op.Operation.ReturnType != PseudoType.Async)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        ");
                            __printer.Write(Generated_PrintType(op.Operation.ReturnType));
                            __printer.WriteTemplateOutput(" result = this.inner.");
                            __printer.Write(Generated_GenerateOperationRefCall(op));
                            __printer.WriteTemplateOutput(";");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                        }
                        else
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        this.inner.");
                            __printer.Write(Generated_GenerateOperationRefCall(op));
                            __printer.WriteTemplateOutput(";");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    ");
                        int __loop63_iteration = 0;
                        var __loop63_result =
                            (from __loop63_tmp_item___noname61 in EnumerableExtensions.Enumerate((op.OperationContractStatements).GetEnumerator())
                            from __loop63_tmp_item_ocs in EnumerableExtensions.Enumerate((__loop63_tmp_item___noname61).GetEnumerator()).OfType<Ensures>()
                            select
                                new
                                {
                                    __loop63_item___noname61 = __loop63_tmp_item___noname61,
                                    __loop63_item_ocs = __loop63_tmp_item_ocs,
                                }).ToArray();
                        foreach (var __loop63_item in __loop63_result)
                        {
                            var __noname61 = __loop63_item.__loop63_item___noname61;
                            var ocs = __loop63_item.__loop63_item_ocs;
                            ++__loop63_iteration;
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("^");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        // Ensures: ");
                            __printer.Write(ocs.Text);
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        if (!(");
                            __printer.Write(Generated_GenerateExpression(ocs.Rule));
                            __printer.WriteTemplateOutput(")) {");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("            SoaMMFault faultInfo = new SoaMMFault();");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("            faultInfo.setMessage(\"Contract ensurement error\");");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("            throw new SoaMMException(\"Contract ensurement error\", faultInfo);");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        }");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    ");
                        if (op.Operation.ReturnType != PseudoType.Void && op.Operation.ReturnType != PseudoType.Async)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        return result;");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    }");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("}");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateProxy(Endpoint endp)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("package ");
                    __printer.Write(Generated_GetPackage(endp.Namespace).ToLower());
                    __printer.WriteTemplateOutput(";");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import java.net.MalformedURLException;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import java.net.URL;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.namespace.QName;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.ws.Service;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.ws.WebEndpoint;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.ws.WebServiceClient;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.ws.WebServiceException;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.ws.WebServiceFeature;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("@WebServiceClient(name = \"");
                    __printer.Write(endp.Name);
                    __printer.WriteTemplateOutput("\", targetNamespace = \"");
                    __printer.Write(Generated_GetUri(endp.Namespace));
                    __printer.WriteTemplateOutput("\", wsdlLocation = \"");
                    __printer.Write(Properties.WsdlDirectory);
                    __printer.Write(endp.Namespace.FullName);
                    __printer.Write(Properties.WsdlSuffix);
                    __printer.WriteTemplateOutput(".wsdl\")");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("public class ");
                    __printer.Write(endp.Name);
                    __printer.WriteTemplateOutput("Service extends Service {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    private final static URL ");
                    __printer.Write((endp.Name + "Service").ToUpper());
                    __printer.WriteTemplateOutput("_WSDL_LOCATION;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    private final static WebServiceException ");
                    __printer.Write((endp.Name + "Service").ToUpper());
                    __printer.WriteTemplateOutput("_EXCEPTION;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    private final static QName ");
                    __printer.Write((endp.Name + "Service").ToUpper());
                    __printer.WriteTemplateOutput("_QNAME = new QName(\"");
                    __printer.Write(Generated_GetUri(endp.Namespace));
                    __printer.WriteTemplateOutput("\", \"");
                    __printer.Write(endp.Name);
                    __printer.WriteTemplateOutput("\");");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    static {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        URL url = null;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        WebServiceException e = null;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        try {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            url = new URL(\"file:");
                    __printer.Write(Properties.WsdlDirectory);
                    __printer.Write(endp.Namespace.FullName);
                    __printer.Write(Properties.WsdlSuffix);
                    __printer.WriteTemplateOutput(".wsdl\");");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        } catch (MalformedURLException ex) {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            e = new WebServiceException(ex);");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        ");
                    __printer.Write((endp.Name + "Service").ToUpper());
                    __printer.WriteTemplateOutput("_WSDL_LOCATION = url;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        ");
                    __printer.Write((endp.Name + "Service").ToUpper());
                    __printer.WriteTemplateOutput("_EXCEPTION = e;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    public ");
                    __printer.Write(endp.Name);
                    __printer.WriteTemplateOutput("Service() {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        super(__getWsdlLocation(), ");
                    __printer.Write((endp.Name + "Service").ToUpper());
                    __printer.WriteTemplateOutput("_QNAME);");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                    if (Properties.GenerateProxyFeatureConstructors)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    public ");
                        __printer.Write(endp.Name);
                        __printer.WriteTemplateOutput("Service(WebServiceFeature... features) {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        super(__getWsdlLocation(), ");
                        __printer.Write((endp.Name + "Service").ToUpper());
                        __printer.WriteTemplateOutput("_QNAME, features);");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    }");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    public ");
                    __printer.Write(endp.Name);
                    __printer.WriteTemplateOutput("Service(URL wsdlLocation) {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        super(wsdlLocation, ");
                    __printer.Write((endp.Name + "Service").ToUpper());
                    __printer.WriteTemplateOutput("_QNAME);");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                    if (Properties.GenerateProxyFeatureConstructors)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    public ");
                        __printer.Write(endp.Name);
                        __printer.WriteTemplateOutput("Service(URL wsdlLocation, WebServiceFeature... features) {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        super(wsdlLocation, ");
                        __printer.Write((endp.Name + "Service").ToUpper());
                        __printer.WriteTemplateOutput("_QNAME, features);");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    }");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    public ");
                    __printer.Write(endp.Name);
                    __printer.WriteTemplateOutput("Service(URL wsdlLocation, QName serviceName) {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        super(wsdlLocation, serviceName);");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                    if (Properties.GenerateProxyFeatureConstructors)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    public ");
                        __printer.Write(endp.Name);
                        __printer.WriteTemplateOutput("Service(URL wsdlLocation, QName serviceName, WebServiceFeature... features) {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        super(wsdlLocation, serviceName, features);");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    }");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    @WebEndpoint(name = \"");
                    __printer.Write(endp.Interface.Name);
                    __printer.WriteTemplateOutput("_");
                    __printer.Write(endp.Binding.Name);
                    __printer.WriteTemplateOutput("_Port\")");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    public ");
                    __printer.Write(endp.Interface.Name);
                    __printer.WriteTemplateOutput(" get");
                    __printer.Write(endp.Interface.Name + endp.Binding.Name + "Port");
                    __printer.WriteTemplateOutput("() {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        return super.getPort(new QName(\"");
                    __printer.Write(endp.Namespace.Uri);
                    __printer.WriteTemplateOutput("\", \"");
                    __printer.Write(endp.Interface.Name);
                    __printer.WriteTemplateOutput("_");
                    __printer.Write(endp.Binding.Name);
                    __printer.WriteTemplateOutput("_Port\"), ");
                    __printer.Write(endp.Interface.Name);
                    __printer.WriteTemplateOutput(".class);");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    @WebEndpoint(name = \"");
                    __printer.Write(endp.Interface.Name);
                    __printer.WriteTemplateOutput("_");
                    __printer.Write(endp.Binding.Name);
                    __printer.WriteTemplateOutput("_Port\")");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    public ");
                    __printer.Write(endp.Interface.Name);
                    __printer.WriteTemplateOutput(" get");
                    __printer.Write(endp.Interface.Name + endp.Binding.Name + "Port");
                    __printer.WriteTemplateOutput("(WebServiceFeature... features) {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        return super.getPort(new QName(\"");
                    __printer.Write(endp.Namespace.Uri);
                    __printer.WriteTemplateOutput("\", \"");
                    __printer.Write(endp.Interface.Name);
                    __printer.WriteTemplateOutput("_");
                    __printer.Write(endp.Binding.Name);
                    __printer.WriteTemplateOutput("_Port\"), ");
                    __printer.Write(endp.Interface.Name);
                    __printer.WriteTemplateOutput(".class, features);");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    private static URL __getWsdlLocation() {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        if (");
                    __printer.Write((endp.Name + "Service").ToUpper());
                    __printer.WriteTemplateOutput("_EXCEPTION!= null) {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            throw ");
                    __printer.Write((endp.Name + "Service").ToUpper());
                    __printer.WriteTemplateOutput("_EXCEPTION;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        return ");
                    __printer.Write((endp.Name + "Service").ToUpper());
                    __printer.WriteTemplateOutput("_WSDL_LOCATION;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("}");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateSoaMMFault(Namespace ns)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("package ");
                    __printer.Write(Generated_GetPackage(ns).ToLower());
                    __printer.WriteTemplateOutput(";");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.bind.annotation.XmlAccessType;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.bind.annotation.XmlAccessorType;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.bind.annotation.XmlElement;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.bind.annotation.XmlType;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("@XmlAccessorType(XmlAccessType.FIELD)");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("@XmlType(name = \"SoaMMException\", propOrder = {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    \"message\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("})");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("public class SoaMMFault {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    @XmlElement(name = \"message\")");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    protected String message;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    public String getMessage() {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        return message;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    public void setMessage(String value) {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        this.message = value;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("}");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateSoaMMException(Namespace ns)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("package ");
                    __printer.Write(Generated_GetPackage(ns).ToLower());
                    __printer.WriteTemplateOutput(";");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.ws.WebFault;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("@WebFault(name = \"SoaMMException\", targetNamespace = \"");
                    __printer.Write(Generated_GetUri(ns));
                    __printer.WriteTemplateOutput("\")");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("public class SoaMMException extends Exception {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    private ");
                    __printer.Write(Generated_GetPackage(ns).ToLower());
                    __printer.WriteTemplateOutput(".SoaMMFault faultInfo;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    public SoaMMException(String message, ");
                    __printer.Write(Generated_GetPackage(ns).ToLower());
                    __printer.WriteTemplateOutput(".SoaMMFault faultInfo) {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        super(message);");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        this.faultInfo = faultInfo;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    public SoaMMException(String message, ");
                    __printer.Write(Generated_GetPackage(ns).ToLower());
                    __printer.WriteTemplateOutput(".SoaMMFault faultInfo, Throwable cause) {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        super(message, cause);");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        this.faultInfo = faultInfo;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    public ");
                    __printer.Write(Generated_GetPackage(ns).ToLower());
                    __printer.WriteTemplateOutput(".SoaMMFault getFaultInfo() {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        return faultInfo;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("}");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            #endregion
                #region functions from "C:\Users\Balazs\Documents\Visual Studio 2013\Projects\SoaMM\SoaGeneratorLib\GeneratorLib.mcg"
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
        
