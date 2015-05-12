using OsloExtensions;
using OsloExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SoaMetaModel
{
    // The main file of the generator.
    public partial class VSGenerator : Generator<IEnumerable<SoaObject>, GeneratorContext>
    {
        
        public VSGenerator(IEnumerable<SoaObject> instances, GeneratorContext context)
            : base(instances, context)
        {
            this.Properties = new PropertyGroup_Properties();
        }
        
            #region functions from "C:\Users\Balazs\Documents\Visual Studio 2013\Projects\SoaMM\SoaGeneratorLib\VSGenerator.mcg"
            public PropertyGroup_Properties Properties { get; private set; }
            
            public class PropertyGroup_Properties
            {
                public PropertyGroup_Properties()
                {
                    this.ProjectName = "VisualStudioProject";
                    this.ResourcesDir = "../Resources";
                    this.OutputDir = "../../Output";
                    this.NoImplementationDelegates = true;
                    this.ThrowNotImplementedException = true;
                    this.NoWindowsIdentityFoundation = true;
                    this.GenerateImplementationBase = false;
                }
                
                public string ProjectName { get; set; }
                public string ResourcesDir { get; set; }
                public string OutputDir { get; set; }
                public bool NoImplementationDelegates { get; set; }
                public bool ThrowNotImplementedException { get; set; }
                public bool NoWindowsIdentityFoundation { get; set; }
                public bool GenerateImplementationBase { get; set; }
            }
            
            public override void Generated_Main()
            {
                Context.SetOutputFolder(Properties.OutputDir);
                Context.CreateFolder("VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName);
                File.Copy(Properties.ResourcesDir + "/VisualStudio/About.aspx", "VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/About.aspx", true);
                File.Copy(Properties.ResourcesDir + "/VisualStudio/About.aspx.cs", "VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/About.aspx.cs", true);
                File.Copy(Properties.ResourcesDir + "/VisualStudio/Default.aspx", "VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/Default.aspx", true);
                File.Copy(Properties.ResourcesDir + "/VisualStudio/Default.aspx.cs", "VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/Default.aspx.cs", true);
                File.Copy(Properties.ResourcesDir + "/VisualStudio/Global.asax", "VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/Global.asax", true);
                File.Copy(Properties.ResourcesDir + "/VisualStudio/Site.master", "VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/Site.master", true);
                File.Copy(Properties.ResourcesDir + "/VisualStudio/Site.master.cs", "VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/Site.master.cs", true);
                File.Copy(Properties.ResourcesDir + "/VisualStudio/web.config", "VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/web.config", true);
                Context.CreateFolder("VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/Account");
                File.Copy(Properties.ResourcesDir + "/VisualStudio/ChangePassword.aspx", "VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/Account/ChangePassword.aspx", true);
                File.Copy(Properties.ResourcesDir + "/VisualStudio/ChangePassword.aspx.cs", "VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/Account/ChangePassword.aspx.cs", true);
                File.Copy(Properties.ResourcesDir + "/VisualStudio/ChangePasswordSuccess.aspx", "VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/Account/ChangePasswordSuccess.aspx", true);
                File.Copy(Properties.ResourcesDir + "/VisualStudio/ChangePasswordSuccess.aspx.cs", "VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/Account/ChangePasswordSuccess.aspx.cs", true);
                File.Copy(Properties.ResourcesDir + "/VisualStudio/Login.aspx", "VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/Account/Login.aspx", true);
                File.Copy(Properties.ResourcesDir + "/VisualStudio/Login.aspx.cs", "VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/Account/Login.aspx.cs", true);
                File.Copy(Properties.ResourcesDir + "/VisualStudio/Register.aspx", "VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/Account/Register.aspx", true);
                File.Copy(Properties.ResourcesDir + "/VisualStudio/Register.aspx.cs", "VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/Account/Register.aspx.cs", true);
                File.Copy(Properties.ResourcesDir + "/VisualStudio/AccountWeb.config", "VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/Account/Web.config", true);
                Context.CreateFolder("VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/App_Code");
                if (!Properties.NoWindowsIdentityFoundation)
                {
                    File.Copy(Properties.ResourcesDir + "/VisualStudio/SampleRequestValidator.cs", "VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/App_Code/SampleRequestValidator.cs", true);
                }
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
                        Context.SetOutput("VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/App_Code/" + ns.FullName + ".cs");
                        Context.Output(Generated_GenerateDataTypes(ns));
                    }
                }
                int __loop3_iteration = 0;
                var __loop3_result =
                    (from __loop3_tmp_item___noname3 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                    from __loop3_tmp_item_intf in EnumerableExtensions.Enumerate((__loop3_tmp_item___noname3).GetEnumerator()).OfType<Interface>()
                    select
                        new
                        {
                            __loop3_item___noname3 = __loop3_tmp_item___noname3,
                            __loop3_item_intf = __loop3_tmp_item_intf,
                        }).ToArray();
                foreach (var __loop3_item in __loop3_result)
                {
                    var __noname3 = __loop3_item.__loop3_item___noname3;
                    var intf = __loop3_item.__loop3_item_intf;
                    ++__loop3_iteration;
                    Context.SetOutput("VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/App_Code/" + intf.Name + ".cs");
                    Context.Output(Generated_GenerateInterface(intf));
                    if (Properties.GenerateImplementationBase)
                    {
                        Context.SetOutput("VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/App_Code/" + intf.Name.Substring(1) + "Base.cs");
                        Context.Output(Generated_GenerateInterfaceImplBase(intf));
                    }
                }
                int __loop4_iteration = 0;
                var __loop4_result =
                    (from __loop4_tmp_item___noname4 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                    from __loop4_tmp_item_endp in EnumerableExtensions.Enumerate((__loop4_tmp_item___noname4).GetEnumerator()).OfType<Endpoint>()
                    select
                        new
                        {
                            __loop4_item___noname4 = __loop4_tmp_item___noname4,
                            __loop4_item_endp = __loop4_tmp_item_endp,
                        }).ToArray();
                foreach (var __loop4_item in __loop4_result)
                {
                    var __noname4 = __loop4_item.__loop4_item___noname4;
                    var endp = __loop4_item.__loop4_item_endp;
                    ++__loop4_iteration;
                    if (!Properties.NoImplementationDelegates)
                    {
                        Context.SetOutput("VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/App_Code/" + endp.Name + "Implementation.cs");
                        Context.Output(Generated_GenerateInterfaceImpl(endp));
                        Context.SetOutput("VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/App_Code/" + endp.Name + ".cs");
                        Context.Output(Generated_GenerateEndpoint(endp));
                    }
                    else
                    {
                        Context.SetOutput("VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/App_Code/" + endp.Name + ".cs");
                        Context.Output(Generated_GenerateInterfaceImpl(endp));
                    }
                    Context.SetOutput("VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/App_Code/" + endp.Name + "Client.cs");
                    Context.Output(Generated_GenerateClient(endp));
                }
                if (!Properties.NoImplementationDelegates)
                {
                    int __loop5_iteration = 0;
                    var __loop5_result =
                        (from __loop5_tmp_item___noname5 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                        from __loop5_tmp_item_con in EnumerableExtensions.Enumerate((__loop5_tmp_item___noname5).GetEnumerator()).OfType<Contract>()
                        select
                            new
                            {
                                __loop5_item___noname5 = __loop5_tmp_item___noname5,
                                __loop5_item_con = __loop5_tmp_item_con,
                            }).ToArray();
                    foreach (var __loop5_item in __loop5_result)
                    {
                        var __noname5 = __loop5_item.__loop5_item___noname5;
                        var con = __loop5_item.__loop5_item_con;
                        ++__loop5_iteration;
                        Context.SetOutput("VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/App_Code/" + con.Name + ".cs");
                        Context.Output(Generated_GenerateContract(con));
                    }
                    int __loop6_iteration = 0;
                    var __loop6_result =
                        (from __loop6_tmp_item___noname6 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                        from __loop6_tmp_item_auth in EnumerableExtensions.Enumerate((__loop6_tmp_item___noname6).GetEnumerator()).OfType<Authorization>()
                        select
                            new
                            {
                                __loop6_item___noname6 = __loop6_tmp_item___noname6,
                                __loop6_item_auth = __loop6_tmp_item_auth,
                            }).ToArray();
                    foreach (var __loop6_item in __loop6_result)
                    {
                        var __noname6 = __loop6_item.__loop6_item___noname6;
                        var auth = __loop6_item.__loop6_item_auth;
                        ++__loop6_iteration;
                        Context.SetOutput("VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/App_Code/" + auth.Name + ".cs");
                        Context.Output(Generated_GenerateAuthorization(auth));
                    }
                }
                Context.CreateFolder("VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/App_Data");
                Context.CreateFolder("VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/Scripts");
                File.Copy(Properties.ResourcesDir + "/VisualStudio/jquery-1.4.1.js", "VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/Scripts/jquery-1.4.1.js", true);
                File.Copy(Properties.ResourcesDir + "/VisualStudio/jquery-1.4.1.min.js", "VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/Scripts/jquery-1.4.1.min.js", true);
                File.Copy(Properties.ResourcesDir + "/VisualStudio/jquery-1.4.1-vsdoc.js", "VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/Scripts/jquery-1.4.1-vsdoc.js", true);
                Context.CreateFolder("VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/Services");
                int __loop7_iteration = 0;
                var __loop7_result =
                    (from __loop7_tmp_item___noname7 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                    from __loop7_tmp_item_endp in EnumerableExtensions.Enumerate((__loop7_tmp_item___noname7).GetEnumerator()).OfType<Endpoint>()
                    select
                        new
                        {
                            __loop7_item___noname7 = __loop7_tmp_item___noname7,
                            __loop7_item_endp = __loop7_tmp_item_endp,
                        }).ToArray();
                foreach (var __loop7_item in __loop7_result)
                {
                    var __noname7 = __loop7_item.__loop7_item___noname7;
                    var endp = __loop7_item.__loop7_item_endp;
                    ++__loop7_iteration;
                    Context.SetOutput("VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/Services/" + endp.Name + ".svc");
                    Context.Output(Generated_GenerateService(endp));
                }
                Context.SetOutput("VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/Services/Web.config");
                Context.Output(Generated_GenerateWebConfig());
                Context.CreateFolder("VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/Clients");
                Context.SetOutput("VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/Clients/App.config");
                Context.Output(Generated_GenerateClientAppConfig());
                Context.SetOutput("VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/Services/Default.aspx");
                Context.Output(Generated_GenerateServicesDefaultAspx());
                Context.SetOutput("VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/Services/Default.aspx.cs");
                Context.Output(Generated_GenerateServicesDefaultAspxCs());
                Context.CreateFolder("VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/Styles");
                File.Copy(Properties.ResourcesDir + "/VisualStudio/Site.css", "VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "/Styles/Site.css", true);
                Context.SetOutputFolder(Properties.OutputDir);
                Context.CreateFolder("VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "Client");
                Context.SetOutput("VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + ".sln");
                Context.Output(Generated_GenerateSolution());
                Context.SetOutput("VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "Client/" + Properties.ProjectName + "Client.csproj");
                Context.Output(Generated_GenerateClientProject());
                Context.SetOutput("VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "Client/Program.cs");
                Context.Output(Generated_GenerateProgramCs());
                Context.SetOutput("VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "Client/App.config");
                Context.Output(Generated_GenerateClientAppConfig());
                Context.CreateFolder("VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "Client/Properties");
                Context.SetOutput("VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "Client/Properties/AssemblyInfo.cs");
                Context.Output(Generated_GenerateAssemblyInfo());
                int __loop8_iteration = 0;
                var __loop8_result =
                    (from __loop8_tmp_item___noname8 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                    from __loop8_tmp_item_ns in EnumerableExtensions.Enumerate((__loop8_tmp_item___noname8).GetEnumerator()).OfType<Namespace>()
                    select
                        new
                        {
                            __loop8_item___noname8 = __loop8_tmp_item___noname8,
                            __loop8_item_ns = __loop8_tmp_item_ns,
                        }).ToArray();
                foreach (var __loop8_item in __loop8_result)
                {
                    var __noname8 = __loop8_item.__loop8_item___noname8;
                    var ns = __loop8_item.__loop8_item_ns;
                    ++__loop8_iteration;
                    if (ns.HasDeclarations())
                    {
                        Context.SetOutput("VisualStudio/" + Properties.ProjectName + "/" + Properties.ProjectName + "Client/" + ns.FullName + ".cs");
                        Context.Output(Generated_GenerateFullNamespace(ns));
                    }
                }
                Context.SetOutput("VisualStudio/" + Properties.ProjectName + "_windows_script.bat");
                Context.Output(Generated_GenerateInstallCertificates());
            }
            
            public List<string> Generated_PrintType(Type type)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    if (type == PseudoType.Void || type == PseudoType.Async)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("void");
                        __printer.WriteLine();
                    }
                    else if (type is BuiltInType)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    ");
                        if (type == BuiltInType.Guid)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("Guid");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                        }
                        else if (type == BuiltInType.Date || type == BuiltInType.Time)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("string");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                        }
                        else if (type == BuiltInType.DateTime)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("DateTime");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                        }
                        else if (type == BuiltInType.TimeSpan)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("TimeSpan");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                        }
                        else
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.Write(type.Name);
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                    }
                    else if (type is StructType || type is EnumType || type is ExceptionType)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.Write(Generated_FirstLetterUp(type.Name));
                        __printer.WriteLine();
                    }
                    else if (type is ArrayType)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        if (((ArrayType)type).ItemType == BuiltInType.Byte)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("byte");
                            __printer.Write("[]");
                            __printer.WriteLine();
                        }
                        else if (((ArrayType)type).ItemType is NullableType)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("ArrayOfNullable");
                            __printer.Write(Generated_PrintType(((NullableType)((ArrayType)type).ItemType).InnerType));
                            __printer.WriteLine();
                        }
                        else
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("ArrayOf");
                            __printer.Write(Generated_PrintType(((ArrayType)type).ItemType));
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                    }
                    else if (type is NullableType)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.Write(Generated_PrintType(((NullableType)type).InnerType));
                        __printer.WriteTemplateOutput("?");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateFullNamespace(Namespace ns)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("using System;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Collections.Generic;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Linq;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Runtime.Serialization;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.ServiceModel;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Text;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("namespace ");
                    __printer.Write(ns.FullName);
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("{");
                    __printer.WriteLine();
                    __printer.Write(Generated_GenerateDataTypesPart(ns));
                    __printer.WriteLine();
                    __printer.Write(Generated_GenerateInterfacePart(ns));
                    __printer.WriteLine();
                    __printer.Write(Generated_GenerateClientPart(ns));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("}");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateDataTypes(Namespace ns)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("using System;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Collections.Generic;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Linq;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Runtime.Serialization;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.ServiceModel;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Text;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("namespace ");
                    __printer.Write(ns.FullName);
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("{");
                    __printer.WriteLine();
                    __printer.Write(Generated_GenerateDataTypesPart(ns));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("}");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateDataTypesPart(Namespace ns)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    int __loop9_iteration = 0;
                    var __loop9_result =
                        (from __loop9_tmp_item___noname9 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                        from __loop9_tmp_item_type in EnumerableExtensions.Enumerate((__loop9_tmp_item___noname9).GetEnumerator()).OfType<ArrayType>()
                        select
                            new
                            {
                                __loop9_item___noname9 = __loop9_tmp_item___noname9,
                                __loop9_item_type = __loop9_tmp_item_type,
                            }).ToArray();
                    foreach (var __loop9_item in __loop9_result)
                    {
                        var __noname9 = __loop9_item.__loop9_item___noname9;
                        var type = __loop9_item.__loop9_item_type;
                        ++__loop9_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        if (((ArrayType)type).ItemType is NullableType)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("^");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                            __printer.Write("[");
                            __printer.WriteTemplateOutput("System.Runtime.Serialization.CollectionDataContractAttribute(Name = \"ArrayOfNullable");
                            __printer.Write(Generated_PrintType(((NullableType)((ArrayType)type).ItemType).InnerType));
                            __printer.WriteTemplateOutput("\", Namespace = \"");
                            __printer.Write(Generated_GetUri(ns));
                            __printer.WriteTemplateOutput("\", ItemName = \"");
                            __printer.Write(((NullableType)((ArrayType)type).ItemType).InnerType.Name);
                            __printer.WriteTemplateOutput("\")");
                            __printer.Write("]");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    public class ArrayOfNullable");
                            __printer.Write(Generated_PrintType(((NullableType)((ArrayType)type).ItemType).InnerType));
                            __printer.WriteTemplateOutput(" : List<");
                            __printer.Write(Generated_PrintType(((NullableType)((ArrayType)type).ItemType).InnerType));
                            __printer.WriteTemplateOutput(">");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    {");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    }");
                            __printer.WriteLine();
                        }
                        else if (((ArrayType)type).ItemType != BuiltInType.Byte)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("^");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                            __printer.Write("[");
                            __printer.WriteTemplateOutput("System.Runtime.Serialization.CollectionDataContractAttribute(Name = \"ArrayOf");
                            __printer.Write(Generated_PrintType(((ArrayType)type).ItemType));
                            __printer.WriteTemplateOutput("\", Namespace = \"");
                            __printer.Write(Generated_GetUri(ns));
                            __printer.WriteTemplateOutput("\", ItemName = \"");
                            __printer.Write(((ArrayType)type).ItemType.Name);
                            __printer.WriteTemplateOutput("\")");
                            __printer.Write("]");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    public class ArrayOf");
                            __printer.Write(Generated_PrintType(((ArrayType)type).ItemType));
                            __printer.WriteTemplateOutput(" : List<");
                            __printer.Write(Generated_PrintType(((ArrayType)type).ItemType));
                            __printer.WriteTemplateOutput(">");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    {");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    }");
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    int __loop10_iteration = 0;
                    var __loop10_result =
                        (from __loop10_tmp_item___noname10 in EnumerableExtensions.Enumerate((ns.Declarations).GetEnumerator())
                        from __loop10_tmp_item_str in EnumerableExtensions.Enumerate((__loop10_tmp_item___noname10).GetEnumerator()).OfType<StructType>()
                        select
                            new
                            {
                                __loop10_item___noname10 = __loop10_tmp_item___noname10,
                                __loop10_item_str = __loop10_tmp_item_str,
                            }).ToArray();
                    foreach (var __loop10_item in __loop10_result)
                    {
                        var __noname10 = __loop10_item.__loop10_item___noname10;
                        var str = __loop10_item.__loop10_item_str;
                        ++__loop10_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    ");
                        __printer.Write("[");
                        __printer.WriteTemplateOutput("System.Runtime.Serialization.DataContractAttribute(Name = \"");
                        __printer.Write(str.Name);
                        __printer.WriteTemplateOutput("\", Namespace = \"");
                        __printer.Write(Generated_GetUri(ns));
                        __printer.WriteTemplateOutput("\")");
                        __printer.Write("]");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                        int __loop11_iteration = 0;
                        var __loop11_result =
                            (from __loop11_tmp_item_strde in EnumerableExtensions.Enumerate((str.GetAllDescendants()).GetEnumerator())
                            select
                                new
                                {
                                    __loop11_item_strde = __loop11_tmp_item_strde,
                                }).ToArray();
                        foreach (var __loop11_item in __loop11_result)
                        {
                            var strde = __loop11_item.__loop11_item_strde;
                            ++__loop11_iteration;
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                            __printer.Write("[");
                            __printer.WriteTemplateOutput("System.Runtime.Serialization.KnownTypeAttribute(typeof(");
                            __printer.Write(strde.Namespace.FullName);
                            __printer.WriteTemplateOutput(".");
                            __printer.Write(strde.Name);
                            __printer.WriteTemplateOutput("))");
                            __printer.Write("]");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    ");
                        if (str.SuperType != null)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    public class ");
                            __printer.Write(str.Name);
                            __printer.WriteTemplateOutput(" : ");
                            __printer.Write(str.SuperType.Namespace.FullName);
                            __printer.WriteTemplateOutput(".");
                            __printer.Write(str.SuperType.Name);
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                        }
                        else
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    public class ");
                            __printer.Write(str.Name);
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    ");
                        int __loop12_iteration = 0;
                        var __loop12_result =
                            (from __loop12_tmp_item___noname11 in EnumerableExtensions.Enumerate((str.Fields).GetEnumerator())
                            from __loop12_tmp_item_field in EnumerableExtensions.Enumerate((__loop12_tmp_item___noname11).GetEnumerator()).OfType<StructField>()
                            select
                                new
                                {
                                    __loop12_item___noname11 = __loop12_tmp_item___noname11,
                                    __loop12_item_field = __loop12_tmp_item_field,
                                }).ToArray();
                        foreach (var __loop12_item in __loop12_result)
                        {
                            var __noname11 = __loop12_item.__loop12_item___noname11;
                            var field = __loop12_item.__loop12_item_field;
                            ++__loop12_iteration;
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("^");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        private ");
                            __printer.Write(Generated_PrintType(field.Type));
                            __printer.WriteTemplateOutput(" ");
                            __printer.Write(Generated_FirstLetterLow(field.Name));
                            __printer.WriteTemplateOutput("Field;");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    ");
                        int __loop13_iteration = 0;
                        int order = 0;
                        var __loop13_result =
                            (from __loop13_tmp_item___noname12 in EnumerableExtensions.Enumerate((str.Fields).GetEnumerator())
                            from __loop13_tmp_item_field in EnumerableExtensions.Enumerate((__loop13_tmp_item___noname12).GetEnumerator()).OfType<StructField>()
                            select
                                new
                                {
                                    __loop13_item___noname12 = __loop13_tmp_item___noname12,
                                    __loop13_item_field = __loop13_tmp_item_field,
                                }).ToArray();
                        foreach (var __loop13_item in __loop13_result)
                        {
                            var __noname12 = __loop13_item.__loop13_item___noname12;
                            var field = __loop13_item.__loop13_item_field;
                            ++__loop13_iteration;
                            if (__loop13_iteration >= 2)
                            {
                                order = order + 1;
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("^");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        ");
                            __printer.Write("[");
                            __printer.WriteTemplateOutput("System.Runtime.Serialization.DataMemberAttribute(IsRequired = true, Order = ");
                            __printer.Write(order);
                            __printer.WriteTemplateOutput(")");
                            __printer.Write("]");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        public ");
                            __printer.Write(Generated_PrintType(field.Type));
                            __printer.WriteTemplateOutput(" ");
                            __printer.Write(Generated_FirstLetterUp(field.Name));
                            __printer.WriteTemplateOutput(" ");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        { ");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("            ");
                            if (field.Type is ArrayType && ((ArrayType)field.Type).ItemType != BuiltInType.Byte)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("            get ");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("            { ");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("                if (this.");
                                __printer.Write(Generated_FirstLetterLow(field.Name));
                                __printer.WriteTemplateOutput("Field == null)");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("                {");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("                    this.");
                                __printer.Write(Generated_FirstLetterLow(field.Name));
                                __printer.WriteTemplateOutput("Field = new ");
                                __printer.Write(Generated_PrintType(field.Type));
                                __printer.WriteTemplateOutput("();");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("                }");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("                return this.");
                                __printer.Write(Generated_FirstLetterLow(field.Name));
                                __printer.WriteTemplateOutput("Field; ");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("            }");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("            ");
                            }
                            else
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("            get { return this.");
                                __printer.Write(Generated_FirstLetterLow(field.Name));
                                __printer.WriteTemplateOutput("Field; }");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("            set { this.");
                                __printer.Write(Generated_FirstLetterLow(field.Name));
                                __printer.WriteTemplateOutput("Field = value; }");
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
                        __printer.WriteTemplateOutput("    }");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    int __loop14_iteration = 0;
                    var __loop14_result =
                        (from __loop14_tmp_item___noname13 in EnumerableExtensions.Enumerate((ns.Declarations).GetEnumerator())
                        from __loop14_tmp_item_en in EnumerableExtensions.Enumerate((__loop14_tmp_item___noname13).GetEnumerator()).OfType<EnumType>()
                        select
                            new
                            {
                                __loop14_item___noname13 = __loop14_tmp_item___noname13,
                                __loop14_item_en = __loop14_tmp_item_en,
                            }).ToArray();
                    foreach (var __loop14_item in __loop14_result)
                    {
                        var __noname13 = __loop14_item.__loop14_item___noname13;
                        var en = __loop14_item.__loop14_item_en;
                        ++__loop14_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    ");
                        __printer.Write("[");
                        __printer.WriteTemplateOutput("System.Runtime.Serialization.DataContractAttribute(Name = \"");
                        __printer.Write(en.Name);
                        __printer.WriteTemplateOutput("\", Namespace = \"");
                        __printer.Write(Generated_GetUri(ns));
                        __printer.WriteTemplateOutput("\")");
                        __printer.Write("]");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    public enum ");
                        __printer.Write(en.Name);
                        __printer.WriteTemplateOutput(" : int");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    {");
                        __printer.WriteLine();
                        int __loop15_iteration = 0;
                        int counter = 0;
                        var __loop15_result =
                            (from __loop15_tmp_item___noname14 in EnumerableExtensions.Enumerate((en.Values).GetEnumerator())
                            from __loop15_tmp_item_val in EnumerableExtensions.Enumerate((__loop15_tmp_item___noname14).GetEnumerator()).OfType<EnumValue>()
                            select
                                new
                                {
                                    __loop15_item___noname14 = __loop15_tmp_item___noname14,
                                    __loop15_item_val = __loop15_tmp_item_val,
                                }).ToArray();
                        foreach (var __loop15_item in __loop15_result)
                        {
                            var __noname14 = __loop15_item.__loop15_item___noname14;
                            var val = __loop15_item.__loop15_item_val;
                            ++__loop15_iteration;
                            if (__loop15_iteration >= 2)
                            {
                                counter = counter + 1;
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        ");
                            __printer.Write("[");
                            __printer.WriteTemplateOutput("System.Runtime.Serialization.EnumMemberAttribute()");
                            __printer.Write("]");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        ");
                            __printer.Write(val.Name);
                            __printer.WriteTemplateOutput(" = ");
                            __printer.Write(counter);
                            __printer.WriteTemplateOutput(",");
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    }");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    int __loop16_iteration = 0;
                    var __loop16_result =
                        (from __loop16_tmp_item___noname15 in EnumerableExtensions.Enumerate((ns.Declarations).GetEnumerator())
                        from __loop16_tmp_item_ex in EnumerableExtensions.Enumerate((__loop16_tmp_item___noname15).GetEnumerator()).OfType<ExceptionType>()
                        select
                            new
                            {
                                __loop16_item___noname15 = __loop16_tmp_item___noname15,
                                __loop16_item_ex = __loop16_tmp_item_ex,
                            }).ToArray();
                    foreach (var __loop16_item in __loop16_result)
                    {
                        var __noname15 = __loop16_item.__loop16_item___noname15;
                        var ex = __loop16_item.__loop16_item_ex;
                        ++__loop16_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    ");
                        __printer.Write("[");
                        __printer.WriteTemplateOutput("System.Runtime.Serialization.DataContractAttribute(Name = \"");
                        __printer.Write(ex.Name);
                        __printer.WriteTemplateOutput("\", Namespace = \"");
                        __printer.Write(Generated_GetUri(ns));
                        __printer.WriteTemplateOutput("\")");
                        __printer.Write("]");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                        int __loop17_iteration = 0;
                        var __loop17_result =
                            (from __loop17_tmp_item_exde in EnumerableExtensions.Enumerate((ex.GetAllDescendants()).GetEnumerator())
                            select
                                new
                                {
                                    __loop17_item_exde = __loop17_tmp_item_exde,
                                }).ToArray();
                        foreach (var __loop17_item in __loop17_result)
                        {
                            var exde = __loop17_item.__loop17_item_exde;
                            ++__loop17_iteration;
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                            __printer.Write("[");
                            __printer.WriteTemplateOutput("System.Runtime.Serialization.KnownTypeAttribute(typeof(");
                            __printer.Write(exde.Namespace.FullName);
                            __printer.WriteTemplateOutput(".");
                            __printer.Write(exde.Name);
                            __printer.WriteTemplateOutput("))");
                            __printer.Write("]");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    ");
                        if (ex.SuperType != null)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    public class ");
                            __printer.Write(ex.Name);
                            __printer.WriteTemplateOutput(" : ");
                            __printer.Write(ex.SuperType.Namespace.FullName);
                            __printer.WriteTemplateOutput(".");
                            __printer.Write(ex.SuperType.Name);
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                        }
                        else
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    public class ");
                            __printer.Write(ex.Name);
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    ");
                        int __loop18_iteration = 0;
                        var __loop18_result =
                            (from __loop18_tmp_item___noname16 in EnumerableExtensions.Enumerate((ex.Fields).GetEnumerator())
                            from __loop18_tmp_item_field in EnumerableExtensions.Enumerate((__loop18_tmp_item___noname16).GetEnumerator()).OfType<ExceptionField>()
                            select
                                new
                                {
                                    __loop18_item___noname16 = __loop18_tmp_item___noname16,
                                    __loop18_item_field = __loop18_tmp_item_field,
                                }).ToArray();
                        foreach (var __loop18_item in __loop18_result)
                        {
                            var __noname16 = __loop18_item.__loop18_item___noname16;
                            var field = __loop18_item.__loop18_item_field;
                            ++__loop18_iteration;
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("^");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        private ");
                            __printer.Write(Generated_PrintType(field.Type));
                            __printer.WriteTemplateOutput(" ");
                            __printer.Write(Generated_FirstLetterLow(field.Name));
                            __printer.WriteTemplateOutput("Field;");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    ");
                        int __loop19_iteration = 0;
                        int order = 0;
                        var __loop19_result =
                            (from __loop19_tmp_item___noname17 in EnumerableExtensions.Enumerate((ex.Fields).GetEnumerator())
                            from __loop19_tmp_item_field in EnumerableExtensions.Enumerate((__loop19_tmp_item___noname17).GetEnumerator()).OfType<ExceptionField>()
                            select
                                new
                                {
                                    __loop19_item___noname17 = __loop19_tmp_item___noname17,
                                    __loop19_item_field = __loop19_tmp_item_field,
                                }).ToArray();
                        foreach (var __loop19_item in __loop19_result)
                        {
                            var __noname17 = __loop19_item.__loop19_item___noname17;
                            var field = __loop19_item.__loop19_item_field;
                            ++__loop19_iteration;
                            if (__loop19_iteration >= 2)
                            {
                                order = order + 1;
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("^");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        ");
                            __printer.Write("[");
                            __printer.WriteTemplateOutput("System.Runtime.Serialization.DataMemberAttribute(IsRequired = true, Order = ");
                            __printer.Write(order);
                            __printer.WriteTemplateOutput(")");
                            __printer.Write("]");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        public ");
                            __printer.Write(Generated_PrintType(field.Type));
                            __printer.WriteTemplateOutput(" ");
                            __printer.Write(Generated_FirstLetterUp(field.Name));
                            __printer.WriteTemplateOutput(" ");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        { ");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("            ");
                            if (field.Type is ArrayType && ((ArrayType)field.Type).ItemType != BuiltInType.Byte)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("            get ");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("            { ");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("                if (this.");
                                __printer.Write(Generated_FirstLetterLow(field.Name));
                                __printer.WriteTemplateOutput("Field == null)");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("                {");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("                    this.");
                                __printer.Write(Generated_FirstLetterLow(field.Name));
                                __printer.WriteTemplateOutput("Field = new ");
                                __printer.Write(Generated_PrintType(field.Type));
                                __printer.WriteTemplateOutput("();");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("                }");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("                return this.");
                                __printer.Write(Generated_FirstLetterLow(field.Name));
                                __printer.WriteTemplateOutput("Field; ");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("            }");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("            ");
                            }
                            else
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("            get { return this.");
                                __printer.Write(Generated_FirstLetterLow(field.Name));
                                __printer.WriteTemplateOutput("Field; }");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("            set { this.");
                                __printer.Write(Generated_FirstLetterLow(field.Name));
                                __printer.WriteTemplateOutput("Field = value; }");
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
                        __printer.WriteTemplateOutput("    }");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateInterfacePart(Namespace ns)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    int __loop20_iteration = 0;
                    var __loop20_result =
                        (from __loop20_tmp_item___noname18 in EnumerableExtensions.Enumerate((ns).GetEnumerator())
                        from __loop20_tmp_item_Declarations in EnumerableExtensions.Enumerate((__loop20_tmp_item___noname18.Declarations).GetEnumerator())
                        from __loop20_tmp_item_intf in EnumerableExtensions.Enumerate((__loop20_tmp_item_Declarations).GetEnumerator()).OfType<Interface>()
                        select
                            new
                            {
                                __loop20_item___noname18 = __loop20_tmp_item___noname18,
                                __loop20_item_Declarations = __loop20_tmp_item_Declarations,
                                __loop20_item_intf = __loop20_tmp_item_intf,
                            }).ToArray();
                    foreach (var __loop20_item in __loop20_result)
                    {
                        var __noname18 = __loop20_item.__loop20_item___noname18;
                        var Declarations = __loop20_item.__loop20_item_Declarations;
                        var intf = __loop20_item.__loop20_item_intf;
                        ++__loop20_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    ");
                        __printer.Write(Generated_GenerateInterfacePart(intf));
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateClientPart(Namespace ns)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    int __loop21_iteration = 0;
                    var __loop21_result =
                        (from __loop21_tmp_item___noname19 in EnumerableExtensions.Enumerate((ns).GetEnumerator())
                        from __loop21_tmp_item_Declarations in EnumerableExtensions.Enumerate((__loop21_tmp_item___noname19.Declarations).GetEnumerator())
                        from __loop21_tmp_item_endp in EnumerableExtensions.Enumerate((__loop21_tmp_item_Declarations).GetEnumerator()).OfType<Endpoint>()
                        select
                            new
                            {
                                __loop21_item___noname19 = __loop21_tmp_item___noname19,
                                __loop21_item_Declarations = __loop21_tmp_item_Declarations,
                                __loop21_item_endp = __loop21_tmp_item_endp,
                            }).ToArray();
                    foreach (var __loop21_item in __loop21_result)
                    {
                        var __noname19 = __loop21_item.__loop21_item___noname19;
                        var Declarations = __loop21_item.__loop21_item_Declarations;
                        var endp = __loop21_item.__loop21_item_endp;
                        ++__loop21_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    ");
                        __printer.Write(Generated_GenerateClientPart(endp));
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
                    __printer.Write(Generated_PrintType(op.ReturnType));
                    __printer.WriteTemplateOutput(" ");
                    __printer.Write(op.Name);
                    __printer.WriteTemplateOutput("(\\");
                    __printer.WriteLine();
                    int __loop22_iteration = 0;
                    string comma = "";
                    var __loop22_result =
                        (from __loop22_tmp_item_pa in EnumerableExtensions.Enumerate((op.Parameters).GetEnumerator())
                        select
                            new
                            {
                                __loop22_item_pa = __loop22_tmp_item_pa,
                            }).ToArray();
                    foreach (var __loop22_item in __loop22_result)
                    {
                        var pa = __loop22_item.__loop22_item_pa;
                        ++__loop22_iteration;
                        if (__loop22_iteration >= 2)
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
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateOperationCall(Operation op)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.Write(op.Name);
                    __printer.WriteTemplateOutput("(\\");
                    __printer.WriteLine();
                    int __loop23_iteration = 0;
                    string comma = "";
                    var __loop23_result =
                        (from __loop23_tmp_item_pa in EnumerableExtensions.Enumerate((op.Parameters).GetEnumerator())
                        select
                            new
                            {
                                __loop23_item_pa = __loop23_tmp_item_pa,
                            }).ToArray();
                    foreach (var __loop23_item in __loop23_result)
                    {
                        var pa = __loop23_item.__loop23_item_pa;
                        ++__loop23_iteration;
                        if (__loop23_iteration >= 2)
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
                    __printer.Write(Generated_PrintType(oi.Operation.ReturnType));
                    __printer.WriteTemplateOutput(" ");
                    __printer.Write(oi.Operation.Name);
                    __printer.WriteTemplateOutput("(\\");
                    __printer.WriteLine();
                    int __loop24_iteration = 0;
                    string comma = "";
                    var __loop24_result =
                        (from __loop24_tmp_item___noname20 in EnumerableExtensions.Enumerate((oi.References).GetEnumerator())
                        from __loop24_tmp_item_re in EnumerableExtensions.Enumerate((__loop24_tmp_item___noname20).GetEnumerator()).OfType<Reference>()
                        where __loop24_tmp_item_re.Object is OperationParameter
                        select
                            new
                            {
                                __loop24_item___noname20 = __loop24_tmp_item___noname20,
                                __loop24_item_re = __loop24_tmp_item_re,
                            }).ToArray();
                    foreach (var __loop24_item in __loop24_result)
                    {
                        var __noname20 = __loop24_item.__loop24_item___noname20;
                        var re = __loop24_item.__loop24_item_re;
                        ++__loop24_iteration;
                        if (__loop24_iteration >= 2)
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
                    __printer.Write(oi.Operation.Name);
                    __printer.WriteTemplateOutput("(\\");
                    __printer.WriteLine();
                    int __loop25_iteration = 0;
                    string comma = "";
                    var __loop25_result =
                        (from __loop25_tmp_item___noname21 in EnumerableExtensions.Enumerate((oi.References).GetEnumerator())
                        from __loop25_tmp_item_re in EnumerableExtensions.Enumerate((__loop25_tmp_item___noname21).GetEnumerator()).OfType<Reference>()
                        where __loop25_tmp_item_re.Object is OperationParameter
                        select
                            new
                            {
                                __loop25_item___noname21 = __loop25_tmp_item___noname21,
                                __loop25_item_re = __loop25_tmp_item_re,
                            }).ToArray();
                    foreach (var __loop25_item in __loop25_result)
                    {
                        var __noname21 = __loop25_item.__loop25_item___noname21;
                        var re = __loop25_item.__loop25_item_re;
                        ++__loop25_iteration;
                        if (__loop25_iteration >= 2)
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
                    __printer.WriteTemplateOutput("using System;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Collections.Generic;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Linq;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Runtime.Serialization;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.ServiceModel;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Text;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("namespace ");
                    __printer.Write(intf.Namespace.FullName);
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("{");
                    __printer.WriteLine();
                    __printer.Write(Generated_GenerateInterfacePart(intf));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("}");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateInterfacePart(Interface intf)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("    ");
                    __printer.Write("[");
                    __printer.WriteTemplateOutput("System.ServiceModel.ServiceContractAttribute(Namespace = \"");
                    __printer.Write(Generated_GetUri(intf.Namespace));
                    __printer.WriteTemplateOutput("\")");
                    __printer.Write("]");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    public interface ");
                    __printer.Write(intf.Name);
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    {");
                    __printer.WriteLine();
                    int __loop26_iteration = 0;
                    var __loop26_result =
                        (from __loop26_tmp_item___noname22 in EnumerableExtensions.Enumerate((intf.Operations).GetEnumerator())
                        from __loop26_tmp_item_op in EnumerableExtensions.Enumerate((__loop26_tmp_item___noname22).GetEnumerator()).OfType<Operation>()
                        select
                            new
                            {
                                __loop26_item___noname22 = __loop26_tmp_item___noname22,
                                __loop26_item_op = __loop26_tmp_item_op,
                            }).ToArray();
                    foreach (var __loop26_item in __loop26_result)
                    {
                        var __noname22 = __loop26_item.__loop26_item___noname22;
                        var op = __loop26_item.__loop26_item_op;
                        ++__loop26_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        ");
                        __printer.Write("[");
                        __printer.WriteTemplateOutput("System.ServiceModel.OperationContractAttribute(Action=\"");
                        __printer.Write(Generated_GetUriWithSlash(op.Interface.Namespace) + op.Interface.Name + "/" + op.Name);
                        __printer.WriteTemplateOutput("\", ReplyAction=\"");
                        __printer.Write(Generated_GetUriWithSlash(op.Interface.Namespace) + op.Interface.Name + "/" + op.Name + "Response");
                        __printer.WriteTemplateOutput("\")");
                        __printer.Write("]");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    ");
                        int __loop27_iteration = 0;
                        var __loop27_result =
                            (from __loop27_tmp_item___noname23 in EnumerableExtensions.Enumerate((op.Exceptions).GetEnumerator())
                            from __loop27_tmp_item_ex in EnumerableExtensions.Enumerate((__loop27_tmp_item___noname23).GetEnumerator()).OfType<ExceptionType>()
                            select
                                new
                                {
                                    __loop27_item___noname23 = __loop27_tmp_item___noname23,
                                    __loop27_item_ex = __loop27_tmp_item_ex,
                                }).ToArray();
                        foreach (var __loop27_item in __loop27_result)
                        {
                            var __noname23 = __loop27_item.__loop27_item___noname23;
                            var ex = __loop27_item.__loop27_item_ex;
                            ++__loop27_iteration;
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        ");
                            __printer.Write("[");
                            __printer.WriteTemplateOutput("System.ServiceModel.FaultContractAttribute(typeof(");
                            __printer.Write(Generated_PrintType(ex));
                            __printer.WriteTemplateOutput("), Action = \"");
                            __printer.Write(Generated_GetUriWithSlash(op.Interface.Namespace) + op.Interface.Name + "/" + op.Name + "Fault/" + ex.Name);
                            __printer.WriteTemplateOutput("\", Name = \"");
                            __printer.Write(ex.Name);
                            __printer.WriteTemplateOutput("\")");
                            __printer.Write("]");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        ");
                        __printer.Write(Generated_GenerateOperationHead(op));
                        __printer.WriteTemplateOutput(";");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateInterfaceImpl(Endpoint endp)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("using System;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Collections.Generic;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Linq;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Web;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.ServiceModel;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("namespace ");
                    __printer.Write(endp.Namespace.FullName);
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("{");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    ");
                    __printer.Write("[");
                    __printer.WriteTemplateOutput("ServiceBehavior(Namespace = \"");
                    __printer.Write(Generated_GetUri(endp.Namespace));
                    __printer.WriteTemplateOutput("\")");
                    __printer.Write("]");
                    __printer.WriteLine();
                    if (!Properties.NoImplementationDelegates)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    public class ");
                        __printer.Write(endp.Name);
                        __printer.WriteTemplateOutput("Implementation : ");
                        __printer.Write(endp.Interface.Name);
                        __printer.WriteLine();
                    }
                    else
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        if (Properties.GenerateImplementationBase)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    public class ");
                            __printer.Write(endp.Name);
                            __printer.WriteTemplateOutput(" : ");
                            __printer.Write(endp.Interface.Name.Substring(1));
                            __printer.WriteTemplateOutput("Base, ");
                            __printer.Write(endp.Interface.Name);
                            __printer.WriteLine();
                        }
                        else
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    public class ");
                            __printer.Write(endp.Name);
                            __printer.WriteTemplateOutput(" : ");
                            __printer.Write(endp.Interface.Name);
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    {");
                    __printer.WriteLine();
                    int __loop28_iteration = 0;
                    var __loop28_result =
                        (from __loop28_tmp_item___noname24 in EnumerableExtensions.Enumerate((endp.Interface.Operations).GetEnumerator())
                        from __loop28_tmp_item_op in EnumerableExtensions.Enumerate((__loop28_tmp_item___noname24).GetEnumerator()).OfType<Operation>()
                        select
                            new
                            {
                                __loop28_item___noname24 = __loop28_tmp_item___noname24,
                                __loop28_item_op = __loop28_tmp_item_op,
                            }).ToArray();
                    foreach (var __loop28_item in __loop28_result)
                    {
                        var __noname24 = __loop28_item.__loop28_item___noname24;
                        var op = __loop28_item.__loop28_item_op;
                        ++__loop28_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        public ");
                        __printer.Write(Generated_GenerateOperationHead(op));
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("            ");
                        if (Properties.GenerateImplementationBase)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("                ");
                            if (op.ReturnType != PseudoType.Void && op.ReturnType != PseudoType.Async)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("            return base.");
                                __printer.Write(Generated_GenerateOperationCall(op));
                                __printer.WriteTemplateOutput(";");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("                ");
                            }
                            else
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("            base.");
                                __printer.Write(Generated_GenerateOperationCall(op));
                                __printer.WriteTemplateOutput(";");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("                ");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("            ");
                        }
                        else
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("                ");
                            if (Properties.ThrowNotImplementedException)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("            throw new NotImplementedException();");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("                ");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("            ");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        }");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("}");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateInterfaceImplBase(Interface intf)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("using System;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Collections.Generic;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Linq;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Web;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.ServiceModel;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("namespace ");
                    __printer.Write(intf.Namespace.FullName);
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("{");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    public class ");
                    __printer.Write(intf.Name.Substring(1));
                    __printer.WriteTemplateOutput("Base : ");
                    __printer.Write(intf.Name);
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    {");
                    __printer.WriteLine();
                    int __loop29_iteration = 0;
                    var __loop29_result =
                        (from __loop29_tmp_item___noname25 in EnumerableExtensions.Enumerate((intf.Operations).GetEnumerator())
                        from __loop29_tmp_item_op in EnumerableExtensions.Enumerate((__loop29_tmp_item___noname25).GetEnumerator()).OfType<Operation>()
                        select
                            new
                            {
                                __loop29_item___noname25 = __loop29_tmp_item___noname25,
                                __loop29_item_op = __loop29_tmp_item_op,
                            }).ToArray();
                    foreach (var __loop29_item in __loop29_result)
                    {
                        var __noname25 = __loop29_item.__loop29_item___noname25;
                        var op = __loop29_item.__loop29_item_op;
                        ++__loop29_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        public ");
                        __printer.Write(Generated_GenerateOperationHead(op));
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        }");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
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
                    __printer.WriteTemplateOutput("using System;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Collections.Generic;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Linq;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Web;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("namespace ");
                    __printer.Write(con.Namespace.FullName);
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("{");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    public class ");
                    __printer.Write(con.Name);
                    __printer.WriteTemplateOutput(" : ");
                    __printer.Write(con.Interface.Name);
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        private ");
                    __printer.Write(con.Interface.Name);
                    __printer.WriteTemplateOutput(" inner;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        public ");
                    __printer.Write(con.Name);
                    __printer.WriteTemplateOutput("(");
                    __printer.Write(con.Interface.Name);
                    __printer.WriteTemplateOutput(" inner)");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            this.inner = inner;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        }");
                    __printer.WriteLine();
                    int __loop30_iteration = 0;
                    var __loop30_result =
                        (from __loop30_tmp_item___noname26 in EnumerableExtensions.Enumerate((con.OperationContracts).GetEnumerator())
                        from __loop30_tmp_item_op in EnumerableExtensions.Enumerate((__loop30_tmp_item___noname26).GetEnumerator()).OfType<OperationContract>()
                        select
                            new
                            {
                                __loop30_item___noname26 = __loop30_tmp_item___noname26,
                                __loop30_item_op = __loop30_tmp_item_op,
                            }).ToArray();
                    foreach (var __loop30_item in __loop30_result)
                    {
                        var __noname26 = __loop30_item.__loop30_item___noname26;
                        var op = __loop30_item.__loop30_item_op;
                        ++__loop30_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        public ");
                        __printer.Write(Generated_GenerateOperationRefHead(op));
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    ");
                        int __loop31_iteration = 0;
                        var __loop31_result =
                            (from __loop31_tmp_item___noname27 in EnumerableExtensions.Enumerate((op.OperationContractStatements).GetEnumerator())
                            from __loop31_tmp_item_ocs in EnumerableExtensions.Enumerate((__loop31_tmp_item___noname27).GetEnumerator()).OfType<Requires>()
                            select
                                new
                                {
                                    __loop31_item___noname27 = __loop31_tmp_item___noname27,
                                    __loop31_item_ocs = __loop31_tmp_item_ocs,
                                }).ToArray();
                        foreach (var __loop31_item in __loop31_result)
                        {
                            var __noname27 = __loop31_item.__loop31_item___noname27;
                            var ocs = __loop31_item.__loop31_item_ocs;
                            ++__loop31_iteration;
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("^");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("            // Requires: ");
                            __printer.Write(ocs.Text);
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("            if (!(");
                            __printer.Write(Generated_GenerateExpression(ocs.Rule));
                            __printer.WriteTemplateOutput("))");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("            {");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        ");
                            if (ocs.Otherwise == null)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("                throw new Exception(\"Contract requirement error\");");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        ");
                            }
                            else
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("                throw ");
                                __printer.Write(Generated_GenerateExpression(ocs.Otherwise));
                                __printer.WriteTemplateOutput(";");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        ");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("            }");
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
                            __printer.WriteTemplateOutput("            ");
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
                            __printer.WriteTemplateOutput("            this.inner.");
                            __printer.Write(Generated_GenerateOperationRefCall(op));
                            __printer.WriteTemplateOutput(";");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    ");
                        int __loop32_iteration = 0;
                        var __loop32_result =
                            (from __loop32_tmp_item___noname28 in EnumerableExtensions.Enumerate((op.OperationContractStatements).GetEnumerator())
                            from __loop32_tmp_item_ocs in EnumerableExtensions.Enumerate((__loop32_tmp_item___noname28).GetEnumerator()).OfType<Ensures>()
                            select
                                new
                                {
                                    __loop32_item___noname28 = __loop32_tmp_item___noname28,
                                    __loop32_item_ocs = __loop32_tmp_item_ocs,
                                }).ToArray();
                        foreach (var __loop32_item in __loop32_result)
                        {
                            var __noname28 = __loop32_item.__loop32_item___noname28;
                            var ocs = __loop32_item.__loop32_item_ocs;
                            ++__loop32_iteration;
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("^");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("            // Ensures: ");
                            __printer.Write(ocs.Text);
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("            if (!(");
                            __printer.Write(Generated_GenerateExpression(ocs.Rule));
                            __printer.WriteTemplateOutput("))");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("            {");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("                throw new Exception(\"Contract ensurement error\");");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("            }");
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
                            __printer.WriteTemplateOutput("            return result;");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        }");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("}");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateAuthorization(Authorization auth)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("using System;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Collections.Generic;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Linq;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Web;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using Microsoft.IdentityModel.Claims;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Threading;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("namespace ");
                    __printer.Write(auth.Namespace.FullName);
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("{");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    public class ");
                    __printer.Write(auth.Name);
                    __printer.WriteTemplateOutput(" : ");
                    __printer.Write(auth.Interface.Name);
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        private ");
                    __printer.Write(auth.Interface.Name);
                    __printer.WriteTemplateOutput(" inner;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        public ");
                    __printer.Write(auth.Name);
                    __printer.WriteTemplateOutput("(");
                    __printer.Write(auth.Interface.Name);
                    __printer.WriteTemplateOutput(" inner)");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            this.inner = inner;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        }");
                    __printer.WriteLine();
                    int __loop33_iteration = 0;
                    var __loop33_result =
                        (from __loop33_tmp_item___noname29 in EnumerableExtensions.Enumerate((auth.OperationAuthorizations).GetEnumerator())
                        from __loop33_tmp_item_op in EnumerableExtensions.Enumerate((__loop33_tmp_item___noname29).GetEnumerator()).OfType<OperationAuthorization>()
                        select
                            new
                            {
                                __loop33_item___noname29 = __loop33_tmp_item___noname29,
                                __loop33_item_op = __loop33_tmp_item_op,
                            }).ToArray();
                    foreach (var __loop33_item in __loop33_result)
                    {
                        var __noname29 = __loop33_item.__loop33_item___noname29;
                        var op = __loop33_item.__loop33_item_op;
                        ++__loop33_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        public ");
                        __printer.Write(Generated_GenerateOperationRefHead(op));
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("            IClaimsPrincipal principal = (IClaimsPrincipal)Thread.CurrentPrincipal;");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("            IClaimsIdentity identity = (IClaimsIdentity)principal.Identity;");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("            // Check claims here...");
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
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        }");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("}");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateEndpoint(Endpoint endp)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("using System;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Collections.Generic;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Linq;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Runtime.Serialization;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.ServiceModel;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Text;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("namespace ");
                    __printer.Write(endp.Namespace.FullName);
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("{");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    public class ");
                    __printer.Write(endp.Name);
                    __printer.WriteTemplateOutput(" : ");
                    __printer.Write(endp.Interface.Name);
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        private ");
                    __printer.Write(endp.Interface.Name);
                    __printer.WriteTemplateOutput(" inner;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        public ");
                    __printer.Write(endp.Name);
                    __printer.WriteTemplateOutput("()");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        {");
                    __printer.WriteLine();
                    if (endp.Contract == null && endp.Authorization == null)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("            this.inner = new ");
                        __printer.Write(endp.Name);
                        __printer.WriteTemplateOutput("Implementation();");
                        __printer.WriteLine();
                    }
                    else if (endp.Contract != null && endp.Authorization == null)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("            this.inner = new ");
                        __printer.Write(endp.Name);
                        __printer.WriteTemplateOutput("Contract(new ");
                        __printer.Write(endp.Name);
                        __printer.WriteTemplateOutput("Implementation());");
                        __printer.WriteLine();
                    }
                    else if (endp.Contract == null && endp.Authorization != null)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("            this.inner = new ");
                        __printer.Write(endp.Name);
                        __printer.WriteTemplateOutput("Authorization(new ");
                        __printer.Write(endp.Name);
                        __printer.WriteTemplateOutput("Implementation());");
                        __printer.WriteLine();
                    }
                    else
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("            this.inner = new ");
                        __printer.Write(endp.Name);
                        __printer.WriteTemplateOutput("Authorization(new ");
                        __printer.Write(endp.Name);
                        __printer.WriteTemplateOutput("Contract(new ");
                        __printer.Write(endp.Name);
                        __printer.WriteTemplateOutput("Implementation()));");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        }");
                    __printer.WriteLine();
                    int __loop34_iteration = 0;
                    var __loop34_result =
                        (from __loop34_tmp_item___noname30 in EnumerableExtensions.Enumerate((endp.Interface.Operations).GetEnumerator())
                        from __loop34_tmp_item_op in EnumerableExtensions.Enumerate((__loop34_tmp_item___noname30).GetEnumerator()).OfType<Operation>()
                        select
                            new
                            {
                                __loop34_item___noname30 = __loop34_tmp_item___noname30,
                                __loop34_item_op = __loop34_tmp_item_op,
                            }).ToArray();
                    foreach (var __loop34_item in __loop34_result)
                    {
                        var __noname30 = __loop34_item.__loop34_item___noname30;
                        var op = __loop34_item.__loop34_item_op;
                        ++__loop34_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        public ");
                        __printer.Write(Generated_GenerateOperationHead(op));
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    ");
                        if (op.ReturnType != PseudoType.Void && op.ReturnType != PseudoType.Async)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("            return this.inner.");
                            __printer.Write(Generated_GenerateOperationCall(op));
                            __printer.WriteTemplateOutput(";");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                        }
                        else
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("            this.inner.");
                            __printer.Write(Generated_GenerateOperationCall(op));
                            __printer.WriteTemplateOutput(";");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        }");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("}");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateClient(Endpoint endp)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("using System;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Collections.Generic;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Linq;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Runtime.Serialization;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.ServiceModel;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Text;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("namespace ");
                    __printer.Write(endp.Namespace.FullName);
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("{");
                    __printer.WriteLine();
                    __printer.Write(Generated_GenerateClientPart(endp));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("}");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateClientPart(Endpoint endp)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("    public partial class ");
                    __printer.Write(endp.Name);
                    __printer.WriteTemplateOutput("Client : System.ServiceModel.ClientBase<");
                    __printer.Write(endp.Namespace.FullName);
                    __printer.WriteTemplateOutput(".");
                    __printer.Write(endp.Interface.Name);
                    __printer.WriteTemplateOutput(">, ");
                    __printer.Write(endp.Namespace.FullName);
                    __printer.WriteTemplateOutput(".");
                    __printer.Write(endp.Interface.Name);
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        public ");
                    __printer.Write(endp.Name);
                    __printer.WriteTemplateOutput("Client()");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        public ");
                    __printer.Write(endp.Name);
                    __printer.WriteTemplateOutput("Client(string endpointConfigurationName) : ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                base(endpointConfigurationName)");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        public ");
                    __printer.Write(endp.Name);
                    __printer.WriteTemplateOutput("Client(string endpointConfigurationName, string remoteAddress) : ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                base(endpointConfigurationName, remoteAddress)");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        public ");
                    __printer.Write(endp.Name);
                    __printer.WriteTemplateOutput("Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                base(endpointConfigurationName, remoteAddress)");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        public ");
                    __printer.Write(endp.Name);
                    __printer.WriteTemplateOutput("Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                base(binding, remoteAddress)");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        }");
                    __printer.WriteLine();
                    int __loop35_iteration = 0;
                    var __loop35_result =
                        (from __loop35_tmp_item___noname31 in EnumerableExtensions.Enumerate((endp.Interface.Operations).GetEnumerator())
                        from __loop35_tmp_item_op in EnumerableExtensions.Enumerate((__loop35_tmp_item___noname31).GetEnumerator()).OfType<Operation>()
                        select
                            new
                            {
                                __loop35_item___noname31 = __loop35_tmp_item___noname31,
                                __loop35_item_op = __loop35_tmp_item_op,
                            }).ToArray();
                    foreach (var __loop35_item in __loop35_result)
                    {
                        var __noname31 = __loop35_item.__loop35_item___noname31;
                        var op = __loop35_item.__loop35_item_op;
                        ++__loop35_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        public ");
                        __printer.Write(Generated_GenerateOperationHead(op));
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        ");
                        if (op.ReturnType != PseudoType.Void && op.ReturnType != PseudoType.Async)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("            return base.Channel.");
                            __printer.Write(Generated_GenerateOperationCall(op));
                            __printer.WriteTemplateOutput(";");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        ");
                        }
                        else
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("            base.Channel.");
                            __printer.Write(Generated_GenerateOperationCall(op));
                            __printer.WriteTemplateOutput(";");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        ");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        }");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateSolution()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("Microsoft Visual Studio Solution File, Format Version 11.00");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("# Visual Studio 2010");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("Project(\"{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}\") = \"");
                    __printer.Write(Properties.ProjectName);
                    __printer.WriteTemplateOutput("Client\", \"");
                    __printer.Write(Properties.ProjectName);
                    __printer.WriteTemplateOutput("Client\\");
                    __printer.Write(Properties.ProjectName);
                    __printer.WriteTemplateOutput("Client.csproj\", \"{25817C9A-811D-4D02-B475-927904A404FD}\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("EndProject");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("Global");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	GlobalSection(SolutionConfigurationPlatforms) = preSolution");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		Debug|x86 = Debug|x86");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		Release|x86 = Release|x86");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	EndGlobalSection");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	GlobalSection(ProjectConfigurationPlatforms) = postSolution");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		{25817C9A-811D-4D02-B475-927904A404FD}.Debug|x86.ActiveCfg = Debug|x86");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		{25817C9A-811D-4D02-B475-927904A404FD}.Debug|x86.Build.0 = Debug|x86");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		{25817C9A-811D-4D02-B475-927904A404FD}.Release|x86.ActiveCfg = Release|x86");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		{25817C9A-811D-4D02-B475-927904A404FD}.Release|x86.Build.0 = Release|x86");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	EndGlobalSection");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	GlobalSection(SolutionProperties) = preSolution");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		HideSolutionNode = FALSE");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	EndGlobalSection");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("EndGlobal");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateClientProject()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<Project ToolsVersion=\"4.0\" DefaultTargets=\"Build\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <PropertyGroup>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <Configuration Condition=\" '$(Configuration)' == '' \">Debug</Configuration>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <Platform Condition=\" '$(Platform)' == '' \">x86</Platform>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <ProductVersion>8.0.30703</ProductVersion>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <SchemaVersion>2.0</SchemaVersion>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <ProjectGuid>{25817C9A-811D-4D02-B475-927904A404FD}</ProjectGuid>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <OutputType>Exe</OutputType>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <AppDesignerFolder>Properties</AppDesignerFolder>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <RootNamespace>");
                    __printer.Write(Properties.ProjectName);
                    __printer.WriteTemplateOutput("Client</RootNamespace>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <AssemblyName>");
                    __printer.Write(Properties.ProjectName);
                    __printer.WriteTemplateOutput("Client</AssemblyName>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <TargetFrameworkVersion>v4.0</TargetFrameworkVersion>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <TargetFrameworkProfile>Client</TargetFrameworkProfile>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <FileAlignment>512</FileAlignment>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  </PropertyGroup>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Debug|x86' \">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <PlatformTarget>x86</PlatformTarget>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <DebugSymbols>true</DebugSymbols>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <DebugType>full</DebugType>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <Optimize>false</Optimize>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <OutputPath>bin\\Debug\\</OutputPath>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <DefineConstants>DEBUG;TRACE</DefineConstants>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <ErrorReport>prompt</ErrorReport>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <WarningLevel>4</WarningLevel>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  </PropertyGroup>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Release|x86' \">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <PlatformTarget>x86</PlatformTarget>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <DebugType>pdbonly</DebugType>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <Optimize>true</Optimize>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <OutputPath>bin\\Release\\</OutputPath>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <DefineConstants>TRACE</DefineConstants>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <ErrorReport>prompt</ErrorReport>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <WarningLevel>4</WarningLevel>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  </PropertyGroup>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <ItemGroup>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <Reference Include=\"System\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <Reference Include=\"System.Core\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <Reference Include=\"System.Runtime.Serialization\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <Reference Include=\"System.ServiceModel\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <Reference Include=\"System.Xml.Linq\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <Reference Include=\"System.Data.DataSetExtensions\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <Reference Include=\"Microsoft.CSharp\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <Reference Include=\"System.Data\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <Reference Include=\"System.Xml\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  </ItemGroup>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <ItemGroup>");
                    __printer.WriteLine();
                    int __loop36_iteration = 0;
                    var __loop36_result =
                        (from __loop36_tmp_item___noname32 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                        from __loop36_tmp_item_ns in EnumerableExtensions.Enumerate((__loop36_tmp_item___noname32).GetEnumerator()).OfType<Namespace>()
                        select
                            new
                            {
                                __loop36_item___noname32 = __loop36_tmp_item___noname32,
                                __loop36_item_ns = __loop36_tmp_item_ns,
                            }).ToArray();
                    foreach (var __loop36_item in __loop36_result)
                    {
                        var __noname32 = __loop36_item.__loop36_item___noname32;
                        var ns = __loop36_item.__loop36_item_ns;
                        ++__loop36_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        if (ns.HasDeclarations())
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    <Compile Include=\"");
                            __printer.Write(ns.FullName);
                            __printer.WriteTemplateOutput(".cs\" />");
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <Compile Include=\"Program.cs\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <Compile Include=\"Properties\\AssemblyInfo.cs\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  </ItemGroup>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <ItemGroup>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <None Include=\"App.config\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <SubType>Designer</SubType>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </None>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  </ItemGroup>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <Import Project=\"$(MSBuildToolsPath)\\Microsoft.CSharp.targets\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <!-- To modify your build process, add your task inside one of the targets below and uncomment it. ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("       Other similar extension points exist, see Microsoft.Common.targets.");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <Target Name=\"BeforeBuild\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  </Target>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <Target Name=\"AfterBuild\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  </Target>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  -->");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</Project>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateAssemblyInfo()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("using System.Reflection;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Runtime.CompilerServices;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Runtime.InteropServices;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("// General Information about an assembly is controlled through the following ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("// set of attributes. Change these attribute values to modify the information");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("// associated with an assembly.");
                    __printer.WriteLine();
                    __printer.Write("[");
                    __printer.WriteTemplateOutput("assembly: AssemblyTitle(\"");
                    __printer.Write(Properties.ProjectName);
                    __printer.WriteTemplateOutput("Client\")");
                    __printer.Write("]");
                    __printer.WriteLine();
                    __printer.Write("[");
                    __printer.WriteTemplateOutput("assembly: AssemblyDescription(\"\")");
                    __printer.Write("]");
                    __printer.WriteLine();
                    __printer.Write("[");
                    __printer.WriteTemplateOutput("assembly: AssemblyConfiguration(\"\")");
                    __printer.Write("]");
                    __printer.WriteLine();
                    __printer.Write("[");
                    __printer.WriteTemplateOutput("assembly: AssemblyCompany(\"\")");
                    __printer.Write("]");
                    __printer.WriteLine();
                    __printer.Write("[");
                    __printer.WriteTemplateOutput("assembly: AssemblyProduct(\"");
                    __printer.Write(Properties.ProjectName);
                    __printer.WriteTemplateOutput("Client\")");
                    __printer.Write("]");
                    __printer.WriteLine();
                    __printer.Write("[");
                    __printer.WriteTemplateOutput("assembly: AssemblyCopyright(\"Copyright ©  2014\")");
                    __printer.Write("]");
                    __printer.WriteLine();
                    __printer.Write("[");
                    __printer.WriteTemplateOutput("assembly: AssemblyTrademark(\"\")");
                    __printer.Write("]");
                    __printer.WriteLine();
                    __printer.Write("[");
                    __printer.WriteTemplateOutput("assembly: AssemblyCulture(\"\")");
                    __printer.Write("]");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("// Setting ComVisible to false makes the types in this assembly not visible ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("// to COM components.  If you need to access a type in this assembly from ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("// COM, set the ComVisible attribute to true on that type.");
                    __printer.WriteLine();
                    __printer.Write("[");
                    __printer.WriteTemplateOutput("assembly: ComVisible(false)");
                    __printer.Write("]");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("// The following GUID is for the ID of the typelib if this project is exposed to COM");
                    __printer.WriteLine();
                    __printer.Write("[");
                    __printer.WriteTemplateOutput("assembly: Guid(\"ef038eee-e47d-4905-84cc-5e147df1ffec\")");
                    __printer.Write("]");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("// Version information for an assembly consists of the following four values:");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("//");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("//      Major Version");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("//      Minor Version ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("//      Build Number");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("//      Revision");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("//");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("// You can specify all the values or you can default the Build and Revision Numbers ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("// by using the '*' as shown below:");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("// ");
                    __printer.Write("[");
                    __printer.WriteTemplateOutput("assembly: AssemblyVersion(\"1.0.*\")");
                    __printer.Write("]");
                    __printer.WriteLine();
                    __printer.Write("[");
                    __printer.WriteTemplateOutput("assembly: AssemblyVersion(\"1.0.0.0\")");
                    __printer.Write("]");
                    __printer.WriteLine();
                    __printer.Write("[");
                    __printer.WriteTemplateOutput("assembly: AssemblyFileVersion(\"1.0.0.0\")");
                    __printer.Write("]");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateProgramCs()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("using System;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Collections.Generic;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Linq;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Text;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.ServiceModel;");
                    __printer.WriteLine();
                    int __loop37_iteration = 0;
                    var __loop37_result =
                        (from __loop37_tmp_item___noname33 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                        from __loop37_tmp_item_ns in EnumerableExtensions.Enumerate((__loop37_tmp_item___noname33).GetEnumerator()).OfType<Namespace>()
                        select
                            new
                            {
                                __loop37_item___noname33 = __loop37_tmp_item___noname33,
                                __loop37_item_ns = __loop37_tmp_item_ns,
                            }).ToArray();
                    foreach (var __loop37_item in __loop37_result)
                    {
                        var __noname33 = __loop37_item.__loop37_item___noname33;
                        var ns = __loop37_item.__loop37_item_ns;
                        ++__loop37_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        if (ns.HasDeclarations())
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("using ");
                            __printer.Write(ns.FullName);
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
                    __printer.WriteTemplateOutput("namespace ");
                    __printer.Write(Properties.ProjectName);
                    __printer.WriteTemplateOutput("Client");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("{");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    enum TargetFramework");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        Wcf,");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        Metro,");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        TomcatCxf,");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        Oracle,");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        Ibm");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    public class Program");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        private const bool PrintExceptions = false;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        private static readonly Dictionary<TargetFramework, string> Urls = new Dictionary<TargetFramework, string>();");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        private const TargetFramework Target = TargetFramework.Wcf;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        static void Main(string");
                    __printer.Write("[]");
                    __printer.WriteTemplateOutput(" args)");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            using (ConsoleCopy cc = new ConsoleCopy(@\"..\\..\\Wcf.txt\"))");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                Urls.Add(TargetFramework.Wcf, \"http://localhost/WsInteropTest/Services/{0}.svc\");");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                Urls.Add(TargetFramework.Metro, \"http://localhost:8080/WsInteropTest/services/{0}\");");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                Urls.Add(TargetFramework.TomcatCxf, \"http://localhost:9080/WsInteropTest/services/{0}\");");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                Urls.Add(TargetFramework.Oracle, \"http://192.168.136.128:7101/WsInteropTest/services/{0}\");");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                Urls.Add(TargetFramework.Ibm, \"http://192.168.136.128:9080/WsInteropTest/{0}\");");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                string url = Urls");
                    __printer.Write("[Target]");
                    __printer.WriteTemplateOutput(";");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                try");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("					");
                    int __loop38_iteration = 0;
                    var __loop38_result =
                        (from __loop38_tmp_item___noname34 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                        from __loop38_tmp_item_endp in EnumerableExtensions.Enumerate((__loop38_tmp_item___noname34).GetEnumerator()).OfType<Endpoint>()
                        select
                            new
                            {
                                __loop38_item___noname34 = __loop38_tmp_item___noname34,
                                __loop38_item_endp = __loop38_tmp_item_endp,
                            }).ToArray();
                    foreach (var __loop38_item in __loop38_result)
                    {
                        var __noname34 = __loop38_item.__loop38_item___noname34;
                        var endp = __loop38_item.__loop38_item_endp;
                        ++__loop38_iteration;
                        __printer.WriteTemplateOutput("                ");
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("					Test");
                        __printer.Write(endp.Interface.Name);
                        __printer.WriteTemplateOutput("(\"");
                        __printer.Write(endp.Name);
                        __printer.WriteTemplateOutput("\", url);");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("					");
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                catch (Exception ex)");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                    Console.WriteLine(ex);");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		");
                    int __loop39_iteration = 0;
                    var __loop39_result =
                        (from __loop39_tmp_item___noname35 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                        from __loop39_tmp_item_intf in EnumerableExtensions.Enumerate((__loop39_tmp_item___noname35).GetEnumerator()).OfType<Interface>()
                        select
                            new
                            {
                                __loop39_item___noname35 = __loop39_tmp_item___noname35,
                                __loop39_item_intf = __loop39_tmp_item_intf,
                            }).ToArray();
                    foreach (var __loop39_item in __loop39_result)
                    {
                        var __noname35 = __loop39_item.__loop39_item___noname35;
                        var intf = __loop39_item.__loop39_item_intf;
                        ++__loop39_iteration;
                        __printer.WriteTemplateOutput("                ");
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		private static void Test");
                        __printer.Write(intf.Name);
                        __printer.WriteTemplateOutput("(string endpoint, string url, bool close = true)");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		{");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("			Console.WriteLine(endpoint);");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("			try");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("			{");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("				var factory = new ChannelFactory<");
                        __printer.Write(intf.Name);
                        __printer.WriteTemplateOutput(">(\"");
                        __printer.Write(intf.Namespace.FullName);
                        __printer.WriteTemplateOutput(".\"+endpoint);");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("				var address = new EndpointAddress(new Uri(string.Format(url, endpoint)), EndpointIdentity.CreateDnsIdentity(\"WspService\"));");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("				");
                        __printer.Write(intf.Name);
                        __printer.WriteTemplateOutput(" service = factory.CreateChannel(address);");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("				try");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("				{");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("					// call service");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("					try");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("					{");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("						if (close)");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                        {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                            ((IDisposable)service).Dispose();");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                        }");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("					}");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("					catch (Exception ex)");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("					{");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("						Console.WriteLine(\"Close failed.\");");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("						if (PrintExceptions) Console.WriteLine(ex);");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("					}");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("				}");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("				catch (Exception ex)");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("				{");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("					Console.WriteLine(\"Call failed.\");");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("					if (PrintExceptions) Console.WriteLine(ex);");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("				}");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("			}");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("			catch (Exception ex)");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("			{");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	            Console.WriteLine(\"Init failed.\");");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	            if (PrintExceptions) Console.WriteLine(ex);");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("			}");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("			Console.WriteLine(\"----\");");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		}");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("		");
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("}");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateInstallCertificates()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("^");
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
        
