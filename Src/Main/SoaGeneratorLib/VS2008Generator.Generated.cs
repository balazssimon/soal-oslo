using OsloExtensions;
using OsloExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoaMetaModel
{
    // The main file of the generator.
    public partial class VS2008Generator : Generator<IEnumerable<SoaObject>, GeneratorContext>
    {
        
        public VS2008Generator(IEnumerable<SoaObject> instances, GeneratorContext context)
            : base(instances, context)
        {
            this.Properties = new PropertyGroup_Properties();
        }
        
            #region functions from "C:\Users\Balazs\Documents\Visual Studio 2013\Projects\SoaMM\SoaGeneratorLib\VS2008Generator.mcg"
            public PropertyGroup_Properties Properties { get; private set; }
            
            public class PropertyGroup_Properties
            {
                public PropertyGroup_Properties()
                {
                    this.ProjectName = "VisualStudioProject";
                    this.WebSiteGuid = "E24C65DC-7377-472B-9ABA-BC803B73C61A";
                    this.CSGuid = "FAE04EC0-301F-11D3-BF4B-00C04F79EFBC";
                    this.WebProjectGuid = Guid.NewGuid().ToString().ToUpper();
                    this.CSProjectGuid = Guid.NewGuid().ToString().ToUpper();
                    this.AssemblyInfoGuid = Guid.NewGuid().ToString().ToUpper();
                }
                
                public string ProjectName { get; set; }
                public string WebSiteGuid { get; set; }
                public string CSGuid { get; set; }
                public string WebProjectGuid { get; set; }
                public string CSProjectGuid { get; set; }
                public string AssemblyInfoGuid { get; set; }
            }
            
            public override void Generated_Main()
            {
                Context.CreateFolder("VisualStudio2008");
                Context.CreateFolder("VisualStudio2008/Projects");
                Context.CreateFolder("VisualStudio2008/Projects/" + Properties.ProjectName);
                Context.SetOutput("VisualStudio2008/Projects/" + Properties.ProjectName + "/" + Properties.ProjectName + ".sln");
                Context.Output(Generated_GenerateSln());
                Context.CreateFolder("VisualStudio2008/Projects/" + Properties.ProjectName + "/" + Properties.ProjectName + "Lib");
                Context.SetOutput("VisualStudio2008/Projects/" + Properties.ProjectName + "/" + Properties.ProjectName + "Lib/app.config");
                Context.Output(Generated_GenerateAppConfig());
                Context.SetOutput("VisualStudio2008/Projects/" + Properties.ProjectName + "/" + Properties.ProjectName + "Lib/" + Properties.ProjectName + "Lib.cs");
                Context.Output(Generated_GenerateLibCs());
                Context.SetOutput("VisualStudio2008/Projects/" + Properties.ProjectName + "/" + Properties.ProjectName + "Lib/" + Properties.ProjectName + "Lib.csproj");
                Context.Output(Generated_GenerateCsproj());
                Context.CreateFolder("VisualStudio2008/Projects/" + Properties.ProjectName + "/" + Properties.ProjectName + "Lib/Properties");
                Context.SetOutput("VisualStudio2008/Projects/" + Properties.ProjectName + "/" + Properties.ProjectName + "Lib/Properties/AssemblyInfo.cs");
                Context.Output(Generated_GenerateAssemblyInfo());
                Context.CreateFolder("VisualStudio2008/WebSites");
                Context.CreateFolder("VisualStudio2008/WebSites/" + Properties.ProjectName);
                Context.CreateFolder("VisualStudio2008/WebSites/" + Properties.ProjectName + "/App_Code");
                int __loop1_iteration = 0;
                var __loop1_result =
                    (from __loop1_tmp_item___noname1 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                    from __loop1_tmp_item_intf in EnumerableExtensions.Enumerate((__loop1_tmp_item___noname1).GetEnumerator()).OfType<Interface>()
                    select
                        new
                        {
                            __loop1_item___noname1 = __loop1_tmp_item___noname1,
                            __loop1_item_intf = __loop1_tmp_item_intf,
                        }).ToArray();
                foreach (var __loop1_item in __loop1_result)
                {
                    var __noname1 = __loop1_item.__loop1_item___noname1;
                    var intf = __loop1_item.__loop1_item_intf;
                    ++__loop1_iteration;
                    Context.SetOutput("VisualStudio2008/WebSites/" + Properties.ProjectName + "/App_Code/" + intf.Name + ".cs");
                    Context.Output(Generated_GenerateAppCode(intf));
                }
                Context.CreateFolder("VisualStudio2008/WebSites/" + Properties.ProjectName + "/App_Data");
                Context.CreateFolder("VisualStudio2008/WebSites/" + Properties.ProjectName + "/Bin");
                Context.CreateFolder("VisualStudio2008/WebSites/" + Properties.ProjectName + "/Services");
                int __loop2_iteration = 0;
                var __loop2_result =
                    (from __loop2_tmp_item___noname2 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                    from __loop2_tmp_item_intf in EnumerableExtensions.Enumerate((__loop2_tmp_item___noname2).GetEnumerator()).OfType<Interface>()
                    select
                        new
                        {
                            __loop2_item___noname2 = __loop2_tmp_item___noname2,
                            __loop2_item_intf = __loop2_tmp_item_intf,
                        }).ToArray();
                foreach (var __loop2_item in __loop2_result)
                {
                    var __noname2 = __loop2_item.__loop2_item___noname2;
                    var intf = __loop2_item.__loop2_item_intf;
                    ++__loop2_iteration;
                    Context.SetOutput("VisualStudio2008/WebSites/" + Properties.ProjectName + "/Services/" + intf.Name + ".svc");
                    Context.Output(Generated_GenerateService(intf));
                }
                Context.SetOutput("VisualStudio2008/WebSites/" + Properties.ProjectName + "/Default.aspx");
                Context.Output(Generated_GenerateDefaultAspx());
                Context.SetOutput("VisualStudio2008/WebSites/" + Properties.ProjectName + "/Default.aspx.cs");
                Context.Output(Generated_GenerateDefaultAspxCs());
                Context.SetOutput("VisualStudio2008/WebSites/" + Properties.ProjectName + "/Web.config");
                Context.Output(Generated_GenerateWebConfig());
            }
            
            public List<string> Generated_PrintType(Type type)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    if (type is BuiltInType)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.Write(type.Name);
                        __printer.WriteLine();
                    }
                    else
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                        if (type is StructType || type is EnumType || type is ExceptionType)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.Write(type.Namespace.Name.ToLower());
                            __printer.WriteTemplateOutput(".");
                            __printer.Write(type.Name);
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                        if (type is NullableType)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.Write(Generated_FirstLetterUp(((NullableType)type).InnerType.Name));
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                        if (type is ArrayType)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.Write(Generated_PrintType(((ArrayType)type).ItemType));
                            __printer.Write("[]");
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
            
            public List<string> Generated_GenerateSln()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("Microsoft Visual Studio Solution File, Format Version 10.00");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("# Visual Studio 2008");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("Project(\"{");
                    __printer.Write(Properties.WebSiteGuid);
                    __printer.WriteTemplateOutput("}\") = \"");
                    __printer.Write(Properties.ProjectName);
                    __printer.WriteTemplateOutput("\", \"..\\..\\WebSites\\");
                    __printer.Write(Properties.ProjectName);
                    __printer.WriteTemplateOutput("\\\", \"{");
                    __printer.Write(Properties.WebProjectGuid);
                    __printer.WriteTemplateOutput("}\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	ProjectSection(WebsiteProperties) = preProject");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		TargetFramework = \"3.5\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		ProjectReferences = \"{");
                    __printer.Write(Properties.CSProjectGuid);
                    __printer.WriteTemplateOutput("}|");
                    __printer.Write(Properties.ProjectName);
                    __printer.WriteTemplateOutput("Lib.dll;\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		Debug.AspNetCompiler.VirtualPath = \"/");
                    __printer.Write(Properties.ProjectName);
                    __printer.WriteTemplateOutput("\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		Debug.AspNetCompiler.PhysicalPath = \"..\\..\\WebSites\\");
                    __printer.Write(Properties.ProjectName);
                    __printer.WriteTemplateOutput("\\\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		Debug.AspNetCompiler.TargetPath = \"PrecompiledWeb\\");
                    __printer.Write(Properties.ProjectName);
                    __printer.WriteTemplateOutput("\\\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		Debug.AspNetCompiler.Updateable = \"true\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		Debug.AspNetCompiler.ForceOverwrite = \"true\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		Debug.AspNetCompiler.FixedNames = \"false\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		Debug.AspNetCompiler.Debug = \"True\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		Release.AspNetCompiler.VirtualPath = \"/");
                    __printer.Write(Properties.ProjectName);
                    __printer.WriteTemplateOutput("\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		Release.AspNetCompiler.PhysicalPath = \"..\\..\\WebSites\\");
                    __printer.Write(Properties.ProjectName);
                    __printer.WriteTemplateOutput("\\\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		Release.AspNetCompiler.TargetPath = \"PrecompiledWeb\\");
                    __printer.Write(Properties.ProjectName);
                    __printer.WriteTemplateOutput("\\\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		Release.AspNetCompiler.Updateable = \"true\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		Release.AspNetCompiler.ForceOverwrite = \"true\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		Release.AspNetCompiler.FixedNames = \"false\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		Release.AspNetCompiler.Debug = \"False\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		VWDPort = \"4347\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		DefaultWebSiteLanguage = \"Visual C#\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	EndProjectSection");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("EndProject");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("Project(\"{");
                    __printer.Write(Properties.CSGuid);
                    __printer.WriteTemplateOutput("}\") = \"");
                    __printer.Write(Properties.ProjectName);
                    __printer.WriteTemplateOutput("Lib\", \"");
                    __printer.Write(Properties.ProjectName);
                    __printer.WriteTemplateOutput("Lib\\");
                    __printer.Write(Properties.ProjectName);
                    __printer.WriteTemplateOutput("Lib.csproj\", \"{");
                    __printer.Write(Properties.CSProjectGuid);
                    __printer.WriteTemplateOutput("}\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("EndProject");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("Global");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	GlobalSection(SolutionConfigurationPlatforms) = preSolution");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		Debug|.NET = Debug|.NET");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		Debug|Any CPU = Debug|Any CPU");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		Debug|Mixed Platforms = Debug|Mixed Platforms");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		Release|.NET = Release|.NET");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		Release|Any CPU = Release|Any CPU");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		Release|Mixed Platforms = Release|Mixed Platforms");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	EndGlobalSection");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	GlobalSection(ProjectConfigurationPlatforms) = postSolution");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		{32AB86A0-075F-0D2F-6153-D5EED078701D}.Debug|.NET.ActiveCfg = Debug|.NET");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		{32AB86A0-075F-0D2F-6153-D5EED078701D}.Debug|.NET.Build.0 = Debug|.NET");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		{32AB86A0-075F-0D2F-6153-D5EED078701D}.Debug|Any CPU.ActiveCfg = Debug|.NET");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		{32AB86A0-075F-0D2F-6153-D5EED078701D}.Debug|Mixed Platforms.ActiveCfg = Debug|.NET");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		{32AB86A0-075F-0D2F-6153-D5EED078701D}.Debug|Mixed Platforms.Build.0 = Debug|.NET");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		{32AB86A0-075F-0D2F-6153-D5EED078701D}.Release|.NET.ActiveCfg = Debug|.NET");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		{32AB86A0-075F-0D2F-6153-D5EED078701D}.Release|.NET.Build.0 = Debug|.NET");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		{32AB86A0-075F-0D2F-6153-D5EED078701D}.Release|Any CPU.ActiveCfg = Debug|.NET");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		{32AB86A0-075F-0D2F-6153-D5EED078701D}.Release|Mixed Platforms.ActiveCfg = Debug|.NET");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		{32AB86A0-075F-0D2F-6153-D5EED078701D}.Release|Mixed Platforms.Build.0 = Debug|.NET");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		{AFDE8247-8FFF-7284-CC04-6960A0825808}.Debug|.NET.ActiveCfg = Debug|Any CPU");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		{AFDE8247-8FFF-7284-CC04-6960A0825808}.Debug|Any CPU.ActiveCfg = Debug|Any CPU");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		{AFDE8247-8FFF-7284-CC04-6960A0825808}.Debug|Any CPU.Build.0 = Debug|Any CPU");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		{AFDE8247-8FFF-7284-CC04-6960A0825808}.Debug|Mixed Platforms.ActiveCfg = Debug|Any CPU");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		{AFDE8247-8FFF-7284-CC04-6960A0825808}.Debug|Mixed Platforms.Build.0 = Debug|Any CPU");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		{AFDE8247-8FFF-7284-CC04-6960A0825808}.Release|.NET.ActiveCfg = Release|Any CPU");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		{AFDE8247-8FFF-7284-CC04-6960A0825808}.Release|Any CPU.ActiveCfg = Release|Any CPU");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		{AFDE8247-8FFF-7284-CC04-6960A0825808}.Release|Any CPU.Build.0 = Release|Any CPU");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		{AFDE8247-8FFF-7284-CC04-6960A0825808}.Release|Mixed Platforms.ActiveCfg = Release|Any CPU");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		{AFDE8247-8FFF-7284-CC04-6960A0825808}.Release|Mixed Platforms.Build.0 = Release|Any CPU");
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
            
            public List<string> Generated_GenerateAppConfig()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<configuration>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <system.serviceModel>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    ");
                    __printer.Write(Generated_GenerateBindings());
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    ");
                    __printer.Write(Generated_GenerateClient());
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  </system.serviceModel>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</configuration>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateBindings()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<bindings>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <customBinding>");
                    __printer.WriteLine();
                    int __loop3_iteration = 0;
                    var __loop3_result =
                        (from __loop3_tmp_item___noname3 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                        from __loop3_tmp_item_binding in EnumerableExtensions.Enumerate((__loop3_tmp_item___noname3).GetEnumerator()).OfType<Binding>()
                        select
                            new
                            {
                                __loop3_item___noname3 = __loop3_tmp_item___noname3,
                                __loop3_item_binding = __loop3_tmp_item_binding,
                            }).ToArray();
                    foreach (var __loop3_item in __loop3_result)
                    {
                        var __noname3 = __loop3_item.__loop3_item___noname3;
                        var binding = __loop3_item.__loop3_item_binding;
                        ++__loop3_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    <binding name=\"");
                        __printer.Write(binding.Name);
                        __printer.WriteTemplateOutput("\">");
                        __printer.WriteLine();
                        int __loop4_iteration = 0;
                        var __loop4_result =
                            (from __loop4_tmp_item___noname4 in EnumerableExtensions.Enumerate((binding.Protocols).GetEnumerator())
                            from __loop4_tmp_item_security in EnumerableExtensions.Enumerate((__loop4_tmp_item___noname4).GetEnumerator()).OfType<SecurityProtocolBindingElement>()
                            select
                                new
                                {
                                    __loop4_item___noname4 = __loop4_tmp_item___noname4,
                                    __loop4_item_security = __loop4_tmp_item_security,
                                }).ToArray();
                        foreach (var __loop4_item in __loop4_result)
                        {
                            var __noname4 = __loop4_item.__loop4_item___noname4;
                            var security = __loop4_item.__loop4_item_security;
                            ++__loop4_iteration;
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("      <security defaultAlgorithmSuite=\"");
                            __printer.Write(security.AlgorithmSuite.ToString());
                            __printer.WriteTemplateOutput("\" securityHeaderLayout=\"");
                            __printer.Write(security.HeaderLayout.ToString());
                            __printer.WriteTemplateOutput("\" messageProtectionOrder=\"");
                            __printer.Write(security.ProtectionOrder.ToString());
                            __printer.WriteTemplateOutput("\" \\");
                            __printer.WriteLine();
                            if (security is MutualCertificateSecurityProtocolBindingElement)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("authenticationMode=\"MutualCertificateDuplex\" messageSecurityVersion=\"WSSecurity10WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10\">");
                                __printer.WriteLine();
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            if (security is StsSecurityProtocolBindingElement)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("authenticationMode=\"IssuedToken\" messageSecurityVersion=\"WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10\" requireSignatureConfirmation=\"");
                                __printer.Write(((StsSecurityProtocolBindingElement)security).SignatureConfirmation.ToString().ToLower());
                                __printer.WriteTemplateOutput("\" requireDerivedKeys=\"");
                                __printer.Write(((StsSecurityProtocolBindingElement)security).DerivedKeys.ToString().ToLower());
                                __printer.WriteTemplateOutput("\">");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        ");
                                __printer.Write(Generated_GenerateIssuedTokenParameters(((StsSecurityProtocolBindingElement)security).TokenVersion, ((StsSecurityProtocolBindingElement)security).TokenType, ((StsSecurityProtocolBindingElement)security).TokenIssuer, null));
                                __printer.WriteLine();
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            if (security is SamlSecurityProtocolBindingElement)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("authenticationMode=\"IssuedToken\" messageSecurityVersion=\"WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10\" requireSignatureConfirmation=\"");
                                __printer.Write(((SamlSecurityProtocolBindingElement)security).SignatureConfirmation.ToString().ToLower());
                                __printer.WriteTemplateOutput("\">");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        ");
                                __printer.Write(Generated_GenerateIssuedTokenParameters(((SamlSecurityProtocolBindingElement)security).TokenVersion, ((SamlSecurityProtocolBindingElement)security).TokenType, ((SamlSecurityProtocolBindingElement)security).TokenIssuer, ((SamlSecurityProtocolBindingElement)security).Claims));
                                __printer.WriteLine();
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            if (security is SecureConversationSecurityProtocolBindingElement)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("authenticationMode=\"IssuedToken\" messageSecurityVersion=\"WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10\" requireDerivedKeys=\"");
                                __printer.Write(((SecureConversationSecurityProtocolBindingElement)security).DerivedKeys.ToString().ToLower());
                                __printer.WriteTemplateOutput("\">");
                                __printer.WriteLine();
                                if (((SecureConversationSecurityProtocolBindingElement)security).Bootstrap != null)
                                {
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("        <secureConversationBootstrap defaultAlgorithmSuite=\"");
                                    __printer.Write(security.AlgorithmSuite.ToString());
                                    __printer.WriteTemplateOutput("\" securityHeaderLayout=\"");
                                    __printer.Write(security.HeaderLayout.ToString());
                                    __printer.WriteTemplateOutput("\" messageProtectionOrder=\"");
                                    __printer.Write(security.ProtectionOrder.ToString());
                                    __printer.WriteTemplateOutput("\" \\");
                                    __printer.WriteLine();
                                    if (((SecureConversationSecurityProtocolBindingElement)security).Bootstrap is MutualCertificateBootstrapProtocolBindingElement)
                                    {
                                        __printer.TrimLine();
                                        __printer.WriteLine();
                                        __printer.WriteTemplateOutput("authenticationMode=\"MutualCertificateDuplex\" messageSecurityVersion=\"WSSecurity10WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10\" />");
                                        __printer.WriteLine();
                                    }
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    if (((SecureConversationSecurityProtocolBindingElement)security).Bootstrap is StsBootstrapProtocolBindingElement)
                                    {
                                        __printer.TrimLine();
                                        __printer.WriteLine();
                                        __printer.WriteTemplateOutput("authenticationMode=\"IssuedToken\" messageSecurityVersion=\"WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10\" requireSignatureConfirmation=\"");
                                        __printer.Write(((StsBootstrapProtocolBindingElement)((SecureConversationSecurityProtocolBindingElement)security).Bootstrap).SignatureConfirmation.ToString().ToLower());
                                        __printer.WriteTemplateOutput("\" requireDerivedKeys=\"");
                                        __printer.Write(((StsBootstrapProtocolBindingElement)((SecureConversationSecurityProtocolBindingElement)security).Bootstrap).DerivedKeys.ToString().ToLower());
                                        __printer.WriteTemplateOutput("\">");
                                        __printer.WriteLine();
                                        __printer.WriteTemplateOutput("          ");
                                        __printer.Write(Generated_GenerateIssuedTokenParameters(((StsBootstrapProtocolBindingElement)((SecureConversationSecurityProtocolBindingElement)security).Bootstrap).TokenVersion, ((StsBootstrapProtocolBindingElement)((SecureConversationSecurityProtocolBindingElement)security).Bootstrap).TokenType, ((StsBootstrapProtocolBindingElement)((SecureConversationSecurityProtocolBindingElement)security).Bootstrap).TokenIssuer, null));
                                        __printer.WriteLine();
                                        __printer.WriteTemplateOutput("        </secureConversationBootstrap>");
                                        __printer.WriteLine();
                                    }
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    if (((SecureConversationSecurityProtocolBindingElement)security).Bootstrap is SamlBootstrapProtocolBindingElement)
                                    {
                                        __printer.TrimLine();
                                        __printer.WriteLine();
                                        __printer.WriteTemplateOutput("authenticationMode=\"SecureConversation\" messageSecurityVersion=\"WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10\" requireSignatureConfirmation=\"");
                                        __printer.Write(((SamlBootstrapProtocolBindingElement)((SecureConversationSecurityProtocolBindingElement)security).Bootstrap).SignatureConfirmation.ToString().ToLower());
                                        __printer.WriteTemplateOutput("\">");
                                        __printer.WriteLine();
                                        __printer.WriteTemplateOutput("          ");
                                        __printer.Write(Generated_GenerateIssuedTokenParameters(((SamlBootstrapProtocolBindingElement)((SecureConversationSecurityProtocolBindingElement)security).Bootstrap).TokenVersion, ((SamlBootstrapProtocolBindingElement)((SecureConversationSecurityProtocolBindingElement)security).Bootstrap).TokenType, ((SamlBootstrapProtocolBindingElement)((SecureConversationSecurityProtocolBindingElement)security).Bootstrap).TokenIssuer, ((SamlBootstrapProtocolBindingElement)((SecureConversationSecurityProtocolBindingElement)security).Bootstrap).Claims));
                                        __printer.WriteLine();
                                        __printer.WriteTemplateOutput("        </secureConversationBootstrap>");
                                        __printer.WriteLine();
                                    }
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                }
                                else
                                {
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("        <secureConversationBootstrap />");
                                    __printer.WriteLine();
                                }
                                __printer.TrimLine();
                                __printer.WriteLine();
                            }
                            else
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        <secureConversationBootstrap />");
                                __printer.WriteLine();
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("      </security>");
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        int __loop5_iteration = 0;
                        var __loop5_result =
                            (from __loop5_tmp_item___noname5 in EnumerableExtensions.Enumerate((binding.Protocols).GetEnumerator())
                            from __loop5_tmp_item_transaction in EnumerableExtensions.Enumerate((__loop5_tmp_item___noname5).GetEnumerator()).OfType<AtomicTransactionProtocolBindingElement>()
                            select
                                new
                                {
                                    __loop5_item___noname5 = __loop5_tmp_item___noname5,
                                    __loop5_item_transaction = __loop5_tmp_item_transaction,
                                }).ToArray();
                        foreach (var __loop5_item in __loop5_result)
                        {
                            var __noname5 = __loop5_item.__loop5_item___noname5;
                            var transaction = __loop5_item.__loop5_item_transaction;
                            ++__loop5_iteration;
                            __printer.TrimLine();
                            __printer.WriteLine();
                            if (transaction.Version == AtomicTransactionVersion.AtomicTransaction10)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("      <transactionFlow transactionProtocol=\"WSAtomicTransactionOctober2004\" />");
                                __printer.WriteLine();
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        int __loop6_iteration = 0;
                        var __loop6_result =
                            (from __loop6_tmp_item___noname6 in EnumerableExtensions.Enumerate((binding.Protocols).GetEnumerator())
                            from __loop6_tmp_item_reliable in EnumerableExtensions.Enumerate((__loop6_tmp_item___noname6).GetEnumerator()).OfType<ReliableMessagingProtocolBindingElement>()
                            select
                                new
                                {
                                    __loop6_item___noname6 = __loop6_tmp_item___noname6,
                                    __loop6_item_reliable = __loop6_tmp_item_reliable,
                                }).ToArray();
                        foreach (var __loop6_item in __loop6_result)
                        {
                            var __noname6 = __loop6_item.__loop6_item___noname6;
                            var reliable = __loop6_item.__loop6_item_reliable;
                            ++__loop6_iteration;
                            __printer.TrimLine();
                            __printer.WriteLine();
                            if (reliable.Version == ReliableMessagingVersion.ReliableMessaging11)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                if (reliable.InOrder == true)
                                {
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("      <reliableSession reliableMessagingVersion=\"WSReliableMessaging11\" ordered=\"true\" />");
                                    __printer.WriteLine();
                                }
                                else
                                {
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("      <reliableSession reliableMessagingVersion=\"WSReliableMessaging11\" ordered=\"false\" />");
                                    __printer.WriteLine();
                                }
                                __printer.TrimLine();
                                __printer.WriteLine();
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        if (binding.Encoding is SoapEncodingBindingElement)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            if (((SoapEncodingBindingElement)binding.Encoding).MtomEnabled == true)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("      <mtomMessageEncoding messageVersion=\"\\");
                                __printer.WriteLine();
                            }
                            else
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("      <textMessageEncoding messageVersion=\"\\");
                                __printer.WriteLine();
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            if (((SoapEncodingBindingElement)binding.Encoding).Version == SoapVersion.Soap11)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("Soap11\\");
                                __printer.WriteLine();
                            }
                            else
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("Soap12\\");
                                __printer.WriteLine();
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            int __loop7_iteration = 0;
                            var __loop7_result =
                                (from __loop7_tmp_item___noname7 in EnumerableExtensions.Enumerate((binding.Protocols).GetEnumerator())
                                from __loop7_tmp_item_addressing in EnumerableExtensions.Enumerate((__loop7_tmp_item___noname7).GetEnumerator()).OfType<AddressingProtocolBindingElement>()
                                select
                                    new
                                    {
                                        __loop7_item___noname7 = __loop7_tmp_item___noname7,
                                        __loop7_item_addressing = __loop7_tmp_item_addressing,
                                    }).ToArray();
                            foreach (var __loop7_item in __loop7_result)
                            {
                                var __noname7 = __loop7_item.__loop7_item___noname7;
                                var addressing = __loop7_item.__loop7_item_addressing;
                                ++__loop7_iteration;
                                __printer.TrimLine();
                                __printer.WriteLine();
                                if (addressing.Version == AddressingVersion.Addressing10)
                                {
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("WSAddressing10\\");
                                    __printer.WriteLine();
                                }
                                else
                                {
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("WSAddressingAugust2004\\");
                                    __printer.WriteLine();
                                }
                                __printer.TrimLine();
                                __printer.WriteLine();
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("\" />");
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        if (binding.Transport is HttpTransportBindingElement)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("      <httpTransport />");
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        if (binding.Transport is HttpsTransportBindingElement)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            if (((HttpsTransportBindingElement)binding.Transport).ClientAuthentication == HttpsClientAuthentication.Certificate)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("      <httpsTransport requireClientCertificate=\"true\" />");
                                __printer.WriteLine();
                            }
                            else
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("      <httpsTransport requireClientCertificate=\"false\" />");
                                __printer.WriteLine();
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    </binding>");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  </customBinding>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</bindings>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateIssuedTokenParameters(IssuedTokenVersion tokenVersion, IssuedTokenType tokenType, IssuedTokenIssuer tokenIssuer, IEnumerable<ClaimsetType> tokenClaims)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<issuedTokenParameters \\");
                    __printer.WriteLine();
                    if (tokenVersion == IssuedTokenVersion.Token10)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("tokenType=\"urn:oasis:names:tc:SAML:1.0:assertion\" \\");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    if (tokenVersion == IssuedTokenVersion.Token11)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("tokenType=\"http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV1.1\" \\");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    if (tokenVersion == IssuedTokenVersion.Token20)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("tokenType=\"urn:oasis:names:tc:SAML:2.0:assertion\" \\");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    if (tokenType == IssuedTokenType.Symmetric128)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("keyType=\"SymmetricKey\" keySize=\"128\">");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    if (tokenType == IssuedTokenType.Symmetric192)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("keyType=\"SymmetricKey\" keySize=\"192\">");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    if (tokenType == IssuedTokenType.Symmetric256)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("keyType=\"SymmetricKey\" keySize=\"256\">");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    if (tokenType == IssuedTokenType.Asymmetric1024)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("keyType=\"AsymmetricKey\" keySize=\"1024\">");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    if (tokenType == IssuedTokenType.Asymmetric2048)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("keyType=\"AsymmetricKey\" keySize=\"2048\">");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    if (tokenType == IssuedTokenType.Asymmetric3072)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("keyType=\"AsymmetricKey\" keySize=\"3072\">");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    if (tokenClaims != null)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("  <claimTypeRequirements>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("  ");
                        int __loop8_iteration = 0;
                        var __loop8_result =
                            (from __loop8_tmp_item___noname8 in EnumerableExtensions.Enumerate((tokenClaims).GetEnumerator())
                            from __loop8_tmp_item_claim in EnumerableExtensions.Enumerate((__loop8_tmp_item___noname8).GetEnumerator()).OfType<ClaimsetType>()
                            select
                                new
                                {
                                    __loop8_item___noname8 = __loop8_tmp_item___noname8,
                                    __loop8_item_claim = __loop8_tmp_item_claim,
                                }).ToArray();
                        foreach (var __loop8_item in __loop8_result)
                        {
                            var __noname8 = __loop8_item.__loop8_item___noname8;
                            var claim = __loop8_item.__loop8_item_claim;
                            ++__loop8_iteration;
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    <add claimType=\"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/");
                            __printer.Write(claim.Name);
                            __printer.WriteTemplateOutput("\" isOptional=\"false\" />");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("  ");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("  </claimTypeRequirements>");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    if (tokenIssuer != null)
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("  <issuer address=\"");
                        __printer.Write(tokenIssuer.Address);
                        __printer.WriteTemplateOutput("\" />");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("  ");
                        if (tokenIssuer.MetadataAddress != null && tokenIssuer.MetadataAddress.Length > 0)
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("  <issuerMetadata address=\"");
                            __printer.Write(tokenIssuer.MetadataAddress);
                            __printer.WriteTemplateOutput("\" />");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("  ");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</issuedTokenParameters>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateBehaviors()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<behaviors>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <serviceBehaviors>");
                    __printer.WriteLine();
                    int __loop9_iteration = 0;
                    var __loop9_result =
                        (from __loop9_tmp_item___noname9 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                        from __loop9_tmp_item_endpoint in EnumerableExtensions.Enumerate((__loop9_tmp_item___noname9).GetEnumerator()).OfType<Endpoint>()
                        select
                            new
                            {
                                __loop9_item___noname9 = __loop9_tmp_item___noname9,
                                __loop9_item_endpoint = __loop9_tmp_item_endpoint,
                            }).ToArray();
                    foreach (var __loop9_item in __loop9_result)
                    {
                        var __noname9 = __loop9_item.__loop9_item___noname9;
                        var endpoint = __loop9_item.__loop9_item_endpoint;
                        ++__loop9_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    <behavior name=\"");
                        __printer.Write(endpoint.Name);
                        __printer.WriteTemplateOutput("Behavior\">");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("      <serviceMetadata httpGetEnabled=\"true\"/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("      <serviceDebug includeExceptionDetailInFaults=\"false\" />");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                        int __loop10_iteration = 0;
                        var __loop10_result =
                            (from __loop10_tmp_item___noname10 in EnumerableExtensions.Enumerate((endpoint).GetEnumerator())
                            from __loop10_tmp_item_binding in EnumerableExtensions.Enumerate((__loop10_tmp_item___noname10.Binding).GetEnumerator())
                            select
                                new
                                {
                                    __loop10_item___noname10 = __loop10_tmp_item___noname10,
                                    __loop10_item_binding = __loop10_tmp_item_binding,
                                }).ToArray();
                        foreach (var __loop10_item in __loop10_result)
                        {
                            var __noname10 = __loop10_item.__loop10_item___noname10;
                            var binding = __loop10_item.__loop10_item_binding;
                            ++__loop10_iteration;
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		");
                            int __loop11_iteration = 0;
                            var __loop11_result =
                                (from __loop11_tmp_item___noname11 in EnumerableExtensions.Enumerate((binding).GetEnumerator())
                                from __loop11_tmp_item_Protocols in EnumerableExtensions.Enumerate((__loop11_tmp_item___noname11.Protocols).GetEnumerator())
                                from __loop11_tmp_item_security in EnumerableExtensions.Enumerate((__loop11_tmp_item_Protocols).GetEnumerator()).OfType<SecurityProtocolBindingElement>()
                                select
                                    new
                                    {
                                        __loop11_item___noname11 = __loop11_tmp_item___noname11,
                                        __loop11_item_Protocols = __loop11_tmp_item_Protocols,
                                        __loop11_item_security = __loop11_tmp_item_security,
                                    }).ToArray();
                            foreach (var __loop11_item in __loop11_result)
                            {
                                var __noname11 = __loop11_item.__loop11_item___noname11;
                                var Protocols = __loop11_item.__loop11_item_Protocols;
                                var security = __loop11_item.__loop11_item_security;
                                ++__loop11_iteration;
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("      <serviceCredentials>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        <serviceCertificate storeLocation=\"LocalMachine\" storeName=\"My\" x509FindType=\"FindBySubjectName\" findValue=\"WspService\"/>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        <clientCertificate>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("          <authentication certificateValidationMode=\"PeerOrChainTrust\" trustedStoreLocation=\"LocalMachine\"/>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        </clientCertificate>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("      </serviceCredentials>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    </behavior>");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  </serviceBehaviors>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</behaviors>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateServices()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<services>");
                    __printer.WriteLine();
                    int __loop12_iteration = 0;
                    var __loop12_result =
                        (from __loop12_tmp_item___noname12 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                        from __loop12_tmp_item_endpoint in EnumerableExtensions.Enumerate((__loop12_tmp_item___noname12).GetEnumerator()).OfType<Endpoint>()
                        select
                            new
                            {
                                __loop12_item___noname12 = __loop12_tmp_item___noname12,
                                __loop12_item_endpoint = __loop12_tmp_item_endpoint,
                            }).ToArray();
                    foreach (var __loop12_item in __loop12_result)
                    {
                        var __noname12 = __loop12_item.__loop12_item___noname12;
                        var endpoint = __loop12_item.__loop12_item_endpoint;
                        ++__loop12_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("  <service behaviorConfiguration=\"");
                        __printer.Write(endpoint.Name);
                        __printer.WriteTemplateOutput("Behavior\" name=\"");
                        __printer.Write(endpoint.Interface.Namespace.Name);
                        __printer.WriteTemplateOutput(".");
                        __printer.Write(endpoint.Interface.Name);
                        __printer.WriteTemplateOutput("\">");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    <endpoint address=\"");
                        __printer.Write(endpoint.Address.Uri);
                        __printer.WriteTemplateOutput("\" binding=\"customBinding\" bindingConfiguration=\"");
                        __printer.Write(endpoint.Binding.Name);
                        __printer.WriteTemplateOutput("\" contract=\"");
                        __printer.Write(endpoint.Interface.Namespace.Name);
                        __printer.WriteTemplateOutput(".I");
                        __printer.Write(endpoint.Interface.Name);
                        __printer.WriteTemplateOutput("\">");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("      <identity>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        <dns value=\"localhost\" />");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("      </identity>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    </endpoint>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    <endpoint address=\"");
                        __printer.Write(endpoint.Address.Uri);
                        __printer.WriteTemplateOutput("/mex\" binding=\"mexHttpBinding\" contract=\"IMetadataExchange\" />");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("  </service>");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</services>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateClient()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<client>");
                    __printer.WriteLine();
                    int __loop13_iteration = 0;
                    var __loop13_result =
                        (from __loop13_tmp_item___noname13 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                        from __loop13_tmp_item_endpoint in EnumerableExtensions.Enumerate((__loop13_tmp_item___noname13).GetEnumerator()).OfType<Endpoint>()
                        select
                            new
                            {
                                __loop13_item___noname13 = __loop13_tmp_item___noname13,
                                __loop13_item_endpoint = __loop13_tmp_item_endpoint,
                            }).ToArray();
                    foreach (var __loop13_item in __loop13_result)
                    {
                        var __noname13 = __loop13_item.__loop13_item___noname13;
                        var endpoint = __loop13_item.__loop13_item_endpoint;
                        ++__loop13_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("  <endpoint address=\"");
                        __printer.Write(endpoint.Address.Uri);
                        __printer.WriteTemplateOutput("\" binding=\"customBinding\" bindingConfiguration=\"");
                        __printer.Write(endpoint.Binding.Name);
                        __printer.WriteTemplateOutput("\" contract=\"");
                        __printer.Write(endpoint.Interface.Namespace.Name);
                        __printer.WriteTemplateOutput(".I");
                        __printer.Write(endpoint.Interface.Name);
                        __printer.WriteTemplateOutput("\" name=\"");
                        __printer.Write(endpoint.Name);
                        __printer.WriteTemplateOutput("\" />");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</client>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateLibCs()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("//------------------------------------------------------------------------------");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("// <auto-generated>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("//     This code was generated by a tool.");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("//     Runtime Version:2.0.50727.4952");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("//");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("//     Changes to this file may cause incorrect behavior and will be lost if");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("//     the code is regenerated.");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("// </auto-generated>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("//------------------------------------------------------------------------------");
                    __printer.WriteLine();
                    int __loop14_iteration = 0;
                    var __loop14_result =
                        (from __loop14_tmp_item___noname14 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                        from __loop14_tmp_item_ns in EnumerableExtensions.Enumerate((__loop14_tmp_item___noname14).GetEnumerator()).OfType<Namespace>()
                        select
                            new
                            {
                                __loop14_item___noname14 = __loop14_tmp_item___noname14,
                                __loop14_item_ns = __loop14_tmp_item_ns,
                            }).ToArray();
                    foreach (var __loop14_item in __loop14_result)
                    {
                        var __noname14 = __loop14_item.__loop14_item___noname14;
                        var ns = __loop14_item.__loop14_item_ns;
                        ++__loop14_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("namespace ");
                        __printer.Write(ns.Name);
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("{");
                        __printer.WriteLine();
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
                            __printer.WriteTemplateOutput("^");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                            __printer.Write("[System.CodeDom.Compiler.GeneratedCodeAttribute(\"System.ServiceModel\", \"3.0.0.0\")]");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                            __printer.Write("[");
                            __printer.WriteTemplateOutput("System.ServiceModel.ServiceContractAttribute(Namespace=\"");
                            __printer.Write(intf.Namespace.Uri);
                            __printer.WriteTemplateOutput("\", ConfigurationName=\"");
                            __printer.Write(intf.Namespace.Name);
                            __printer.WriteTemplateOutput(".I");
                            __printer.Write(intf.Name);
                            __printer.WriteTemplateOutput("\")");
                            __printer.Write("]");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    public interface I");
                            __printer.Write(intf.Name);
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    {");
                            __printer.WriteLine();
                            int __loop16_iteration = 0;
                            var __loop16_result =
                                (from __loop16_tmp_item___noname16 in EnumerableExtensions.Enumerate((intf.Operations).GetEnumerator())
                                from __loop16_tmp_item_op in EnumerableExtensions.Enumerate((__loop16_tmp_item___noname16).GetEnumerator()).OfType<Operation>()
                                select
                                    new
                                    {
                                        __loop16_item___noname16 = __loop16_tmp_item___noname16,
                                        __loop16_item_op = __loop16_tmp_item_op,
                                    }).ToArray();
                            foreach (var __loop16_item in __loop16_result)
                            {
                                var __noname16 = __loop16_item.__loop16_item___noname16;
                                var op = __loop16_item.__loop16_item_op;
                                ++__loop16_iteration;
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("^");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        ");
                                __printer.Write("[");
                                __printer.WriteTemplateOutput("System.ServiceModel.OperationContractAttribute(Action=\"");
                                __printer.Write(Generated_GetUriWithSlash(intf.Namespace) + intf.Name + "/" + op.Name);
                                __printer.WriteTemplateOutput("\", ReplyAction=\"");
                                __printer.Write(Generated_GetUriWithSlash(intf.Namespace) + intf.Name + "/" + op.Name);
                                __printer.WriteTemplateOutput("Response\")");
                                __printer.Write("]");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        ");
                                __printer.Write(Generated_PrintType(op.ReturnType));
                                __printer.WriteTemplateOutput(" ");
                                __printer.Write(Generated_FirstLetterUp(op.Name));
                                __printer.WriteTemplateOutput("(\\");
                                __printer.WriteLine();
                                int __loop17_iteration = 0;
                                string comma = "";
                                var __loop17_result =
                                    (from __loop17_tmp_item___noname17 in EnumerableExtensions.Enumerate((op.Parameters).GetEnumerator())
                                    from __loop17_tmp_item_pa in EnumerableExtensions.Enumerate((__loop17_tmp_item___noname17).GetEnumerator()).OfType<OperationParameter>()
                                    select
                                        new
                                        {
                                            __loop17_item___noname17 = __loop17_tmp_item___noname17,
                                            __loop17_item_pa = __loop17_tmp_item_pa,
                                        }).ToArray();
                                foreach (var __loop17_item in __loop17_result)
                                {
                                    var __noname17 = __loop17_item.__loop17_item___noname17;
                                    var pa = __loop17_item.__loop17_item_pa;
                                    ++__loop17_iteration;
                                    if (__loop17_iteration >= 2)
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
                                __printer.WriteTemplateOutput(");");
                                __printer.WriteLine();
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    }");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("^");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                            __printer.Write("[");
                            __printer.WriteTemplateOutput("System.CodeDom.Compiler.GeneratedCodeAttribute(\"System.ServiceModel\", \"3.0.0.0\")");
                            __printer.Write("]");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    public interface I");
                            __printer.Write(intf.Name);
                            __printer.WriteTemplateOutput("Channel : ");
                            __printer.Write(ns.Name);
                            __printer.WriteTemplateOutput(".I");
                            __printer.Write(intf.Name);
                            __printer.WriteTemplateOutput(", System.ServiceModel.IClientChannel");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    {");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    }");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("^");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                            __printer.Write("[");
                            __printer.WriteTemplateOutput("System.Diagnostics.DebuggerStepThroughAttribute()");
                            __printer.Write("]");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    ");
                            __printer.Write("[");
                            __printer.WriteTemplateOutput("System.CodeDom.Compiler.GeneratedCodeAttribute(\"System.ServiceModel\", \"3.0.0.0\")");
                            __printer.Write("]");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    public partial class ");
                            __printer.Write(intf.Name);
                            __printer.WriteTemplateOutput("Client : System.ServiceModel.ClientBase<");
                            __printer.Write(intf.Namespace.Name);
                            __printer.WriteTemplateOutput(".I");
                            __printer.Write(intf.Name);
                            __printer.WriteTemplateOutput(">, ");
                            __printer.Write(intf.Namespace.Name);
                            __printer.WriteTemplateOutput(".I");
                            __printer.Write(intf.Name);
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    {");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("^");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        public ");
                            __printer.Write(intf.Name);
                            __printer.WriteTemplateOutput("Client()");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        {");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        }");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("^");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        public ");
                            __printer.Write(intf.Name);
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
                            __printer.Write(intf.Name);
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
                            __printer.Write(intf.Name);
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
                            __printer.Write(intf.Name);
                            __printer.WriteTemplateOutput("Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : ");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("                base(binding, remoteAddress)");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        {");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        }");
                            __printer.WriteLine();
                            int __loop18_iteration = 0;
                            var __loop18_result =
                                (from __loop18_tmp_item___noname18 in EnumerableExtensions.Enumerate((intf.Operations).GetEnumerator())
                                from __loop18_tmp_item_op in EnumerableExtensions.Enumerate((__loop18_tmp_item___noname18).GetEnumerator()).OfType<Operation>()
                                select
                                    new
                                    {
                                        __loop18_item___noname18 = __loop18_tmp_item___noname18,
                                        __loop18_item_op = __loop18_tmp_item_op,
                                    }).ToArray();
                            foreach (var __loop18_item in __loop18_result)
                            {
                                var __noname18 = __loop18_item.__loop18_item___noname18;
                                var op = __loop18_item.__loop18_item_op;
                                ++__loop18_iteration;
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("^");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        public ");
                                __printer.Write(Generated_PrintType(op.ReturnType));
                                __printer.WriteTemplateOutput(" ");
                                __printer.Write(Generated_FirstLetterUp(op.Name));
                                __printer.WriteTemplateOutput("(\\");
                                __printer.WriteLine();
                                int __loop19_iteration = 0;
                                string comma = "";
                                var __loop19_result =
                                    (from __loop19_tmp_item___noname19 in EnumerableExtensions.Enumerate((op.Parameters).GetEnumerator())
                                    from __loop19_tmp_item_pa in EnumerableExtensions.Enumerate((__loop19_tmp_item___noname19).GetEnumerator()).OfType<OperationParameter>()
                                    select
                                        new
                                        {
                                            __loop19_item___noname19 = __loop19_tmp_item___noname19,
                                            __loop19_item_pa = __loop19_tmp_item_pa,
                                        }).ToArray();
                                foreach (var __loop19_item in __loop19_result)
                                {
                                    var __noname19 = __loop19_item.__loop19_item___noname19;
                                    var pa = __loop19_item.__loop19_item_pa;
                                    ++__loop19_iteration;
                                    if (__loop19_iteration >= 2)
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
                                __printer.WriteTemplateOutput("        {");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("            base.Channel.");
                                __printer.Write(op.Name);
                                __printer.WriteTemplateOutput("(\\");
                                __printer.WriteLine();
                                int __loop20_iteration = 0;
                                string comma2 = "";
                                var __loop20_result =
                                    (from __loop20_tmp_item___noname20 in EnumerableExtensions.Enumerate((op.Parameters).GetEnumerator())
                                    from __loop20_tmp_item_pa in EnumerableExtensions.Enumerate((__loop20_tmp_item___noname20).GetEnumerator()).OfType<OperationParameter>()
                                    select
                                        new
                                        {
                                            __loop20_item___noname20 = __loop20_tmp_item___noname20,
                                            __loop20_item_pa = __loop20_tmp_item_pa,
                                        }).ToArray();
                                foreach (var __loop20_item in __loop20_result)
                                {
                                    var __noname20 = __loop20_item.__loop20_item___noname20;
                                    var pa = __loop20_item.__loop20_item_pa;
                                    ++__loop20_iteration;
                                    if (__loop20_iteration >= 2)
                                    {
                                        comma2 = ", ";
                                    }
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    __printer.Write(comma2);
                                    __printer.Write(pa.Name);
                                    __printer.WriteTemplateOutput("\\");
                                    __printer.WriteLine();
                                }
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput(");");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        }");
                                __printer.WriteLine();
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
                    __printer.TrimLine();
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateCsproj()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<Project ToolsVersion=\"3.5\" DefaultTargets=\"Build\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <PropertyGroup>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <Configuration Condition=\" '$(Configuration)' == '' \">Debug</Configuration>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <Platform Condition=\" '$(Platform)' == '' \">AnyCPU</Platform>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <ProductVersion>9.0.21022</ProductVersion>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <SchemaVersion>2.0</SchemaVersion>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <ProjectGuid>{");
                    __printer.Write(Properties.CSProjectGuid);
                    __printer.WriteTemplateOutput("}</ProjectGuid>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <OutputType>Library</OutputType>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <AppDesignerFolder>Properties</AppDesignerFolder>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <RootNamespace>");
                    __printer.Write(Properties.ProjectName);
                    __printer.WriteTemplateOutput("Lib</RootNamespace>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <AssemblyName>");
                    __printer.Write(Properties.ProjectName);
                    __printer.WriteTemplateOutput("Lib</AssemblyName>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <TargetFrameworkVersion>v3.5</TargetFrameworkVersion>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <FileAlignment>512</FileAlignment>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  </PropertyGroup>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' \">");
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
                    __printer.WriteTemplateOutput("  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' \">");
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
                    __printer.WriteTemplateOutput("    <Reference Include=\"System.Core\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <RequiredTargetFramework>3.5</RequiredTargetFramework>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </Reference>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <Reference Include=\"System.Runtime.Serialization\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <RequiredTargetFramework>3.0</RequiredTargetFramework>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </Reference>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <Reference Include=\"System.ServiceModel\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <RequiredTargetFramework>3.0</RequiredTargetFramework>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </Reference>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <Reference Include=\"System.Xml.Linq\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <RequiredTargetFramework>3.5</RequiredTargetFramework>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </Reference>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <Reference Include=\"System.Data.DataSetExtensions\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <RequiredTargetFramework>3.5</RequiredTargetFramework>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </Reference>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <Reference Include=\"System.Data\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <Reference Include=\"System.Xml\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  </ItemGroup>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <ItemGroup>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <Compile Include=\"Properties\\AssemblyInfo.cs\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <Compile Include=\"");
                    __printer.Write(Properties.ProjectName);
                    __printer.WriteTemplateOutput("Lib.cs\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  </ItemGroup>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <ItemGroup>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <None Include=\"app.config\" />");
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
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("// General Information about an assembly is controlled through the following ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("// set of attributes. Change these attribute values to modify the information");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("// associated with an assembly.");
                    __printer.WriteLine();
                    __printer.Write("[assembly: AssemblyTitle(\"" + Properties.ProjectName + "Lib\")]");
                    __printer.WriteLine();
                    __printer.Write("[assembly: AssemblyDescription(\"\")]");
                    __printer.WriteLine();
                    __printer.Write("[assembly: AssemblyConfiguration(\"\")]");
                    __printer.WriteLine();
                    __printer.Write("[assembly: AssemblyCompany(\"BME IK\")]");
                    __printer.WriteLine();
                    __printer.Write("[assembly: AssemblyProduct(\"" + Properties.ProjectName + "Lib\")]");
                    __printer.WriteLine();
                    __printer.Write("[assembly: AssemblyCopyright(\"Copyright © BME IK 2010\")]");
                    __printer.WriteLine();
                    __printer.Write("[assembly: AssemblyTrademark(\"\")]");
                    __printer.WriteLine();
                    __printer.Write("[assembly: AssemblyCulture(\"\")]");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("// Setting ComVisible to false makes the types in this assembly not visible");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("// to COM components.  If you need to access a type in this assembly from");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("// COM, set the ComVisible attribute to true on that type.");
                    __printer.WriteLine();
                    __printer.Write("[assembly: ComVisible(false)]");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("// The following GUID is for the ID of the typelib if this project is exposed to COM");
                    __printer.WriteLine();
                    __printer.Write("[assembly: Guid(\"" + Properties.AssemblyInfoGuid + "\")]");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
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
                    __printer.Write("[assembly: AssemblyVersion(\"1.0.*\")]");
                    __printer.WriteLine();
                    __printer.Write("[assembly: AssemblyVersion(\"1.0.0.0\")]");
                    __printer.WriteLine();
                    __printer.Write("[assembly: AssemblyFileVersion(\"1.0.0.0\")]");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateDefaultAspx()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<%@ Page Language=\"C#\" AutoEventWireup=\"true\"  CodeFile=\"Default.aspx.cs\" Inherits=\"_Default\" %>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<head runat=\"server\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <title>Untitled Page</title>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</head>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<body>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <form id=\"form1\" runat=\"server\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <div>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </div>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </form>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</body>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</html>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateDefaultAspxCs()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("using System;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Configuration;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Data;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Linq;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Web;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Web.Security;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Web.UI;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Web.UI.HtmlControls;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Web.UI.WebControls;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Web.UI.WebControls.WebParts;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("using System.Xml.Linq;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("public partial class _Default : System.Web.UI.Page ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("{");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    protected void Page_Load(object sender, EventArgs e)");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("}");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateWebConfig()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<!-- ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    Note: As an alternative to hand editing this file you can use the ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    web admin tool to configure settings for your application. Use");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    the Website->Asp.Net Configuration option in Visual Studio.");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    A full list of settings and comments can be found in ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    machine.config.comments usually located in ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    \\Windows\\Microsoft.Net\\Framework\\v2.x\\Config ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("-->");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<configuration>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <configSections>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sectionGroup name=\"system.web.extensions\" type=\"System.Web.Configuration.SystemWebExtensionsSectionGroup, System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <sectionGroup name=\"scripting\" type=\"System.Web.Configuration.ScriptingSectionGroup, System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <section name=\"scriptResourceHandler\" type=\"System.Web.Configuration.ScriptingScriptResourceHandlerSection, System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35\" requirePermission=\"false\" allowDefinition=\"MachineToApplication\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <sectionGroup name=\"webServices\" type=\"System.Web.Configuration.ScriptingWebServicesSectionGroup, System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("          <section name=\"jsonSerialization\" type=\"System.Web.Configuration.ScriptingJsonSerializationSection, System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35\" requirePermission=\"false\" allowDefinition=\"Everywhere\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("          <section name=\"profileService\" type=\"System.Web.Configuration.ScriptingProfileServiceSection, System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35\" requirePermission=\"false\" allowDefinition=\"MachineToApplication\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("          <section name=\"authenticationService\" type=\"System.Web.Configuration.ScriptingAuthenticationServiceSection, System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35\" requirePermission=\"false\" allowDefinition=\"MachineToApplication\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("          <section name=\"roleService\" type=\"System.Web.Configuration.ScriptingRoleServiceSection, System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35\" requirePermission=\"false\" allowDefinition=\"MachineToApplication\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        </sectionGroup>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      </sectionGroup>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </sectionGroup>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  </configSections>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <appSettings/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <connectionStrings/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <system.web>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <!-- ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            Set compilation debug=\"true\" to insert debugging ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            symbols into the compiled page. Because this ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            affects performance, set this value to true only ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            during development.");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        -->");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <compilation debug=\"false\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <assemblies>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <add assembly=\"System.Core, Version=3.5.0.0, Culture=neutral, PublicKeyToken=B77A5C561934E089\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <add assembly=\"System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <add assembly=\"System.Data.DataSetExtensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=B77A5C561934E089\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <add assembly=\"System.Xml.Linq, Version=3.5.0.0, Culture=neutral, PublicKeyToken=B77A5C561934E089\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      </assemblies>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </compilation>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <!--");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            The <authentication> section enables configuration ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            of the security authentication mode used by ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            ASP.NET to identify an incoming user. ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        -->");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <authentication mode=\"Windows\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <!--");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            The <customErrors> section enables configuration ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            of what to do if/when an unhandled error occurs ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            during the execution of a request. Specifically, ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            it enables developers to configure html error pages ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            to be displayed in place of a error stack trace.");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <customErrors mode=\"RemoteOnly\" defaultRedirect=\"GenericErrorPage.htm\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <error statusCode=\"403\" redirect=\"NoAccess.htm\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <error statusCode=\"404\" redirect=\"FileNotFound.htm\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        </customErrors>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        -->");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <pages>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <controls>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <add tagPrefix=\"asp\" namespace=\"System.Web.UI\" assembly=\"System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <add tagPrefix=\"asp\" namespace=\"System.Web.UI.WebControls\" assembly=\"System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      </controls>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </pages>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <httpHandlers>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <remove verb=\"*\" path=\"*.asmx\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <add verb=\"*\" path=\"*.asmx\" validate=\"false\" type=\"System.Web.Script.Services.ScriptHandlerFactory, System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <add verb=\"*\" path=\"*_AppService.axd\" validate=\"false\" type=\"System.Web.Script.Services.ScriptHandlerFactory, System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <add verb=\"GET,HEAD\" path=\"ScriptResource.axd\" type=\"System.Web.Handlers.ScriptResourceHandler, System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35\" validate=\"false\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </httpHandlers>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <httpModules>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <add name=\"ScriptModule\" type=\"System.Web.Handlers.ScriptModule, System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </httpModules>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  </system.web>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <system.codedom>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <compilers>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <compiler language=\"c#;cs;csharp\" extension=\".cs\" warningLevel=\"4\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        type=\"Microsoft.CSharp.CSharpCodeProvider, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <providerOption name=\"CompilerVersion\" value=\"v3.5\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <providerOption name=\"WarnAsError\" value=\"false\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      </compiler>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <compiler language=\"vb;vbs;visualbasic;vbscript\" extension=\".vb\" warningLevel=\"4\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        type=\"Microsoft.VisualBasic.VBCodeProvider, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <providerOption name=\"CompilerVersion\" value=\"v3.5\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <providerOption name=\"OptionInfer\" value=\"true\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <providerOption name=\"WarnAsError\" value=\"false\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      </compiler>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </compilers>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  </system.codedom>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <!-- ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        The system.webServer section is required for running ASP.NET AJAX under Internet");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        Information Services 7.0.  It is not necessary for previous version of IIS.");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    -->");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <system.webServer>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <validation validateIntegratedModeConfiguration=\"false\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <modules>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <remove name=\"ScriptModule\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <add name=\"ScriptModule\" preCondition=\"managedHandler\" type=\"System.Web.Handlers.ScriptModule, System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </modules>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <handlers>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <remove name=\"WebServiceHandlerFactory-Integrated\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <remove name=\"ScriptHandlerFactory\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <remove name=\"ScriptHandlerFactoryAppServices\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <remove name=\"ScriptResource\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <add name=\"ScriptHandlerFactory\" verb=\"*\" path=\"*.asmx\" preCondition=\"integratedMode\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      type=\"System.Web.Script.Services.ScriptHandlerFactory, System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <add name=\"ScriptHandlerFactoryAppServices\" verb=\"*\" path=\"*_AppService.axd\" preCondition=\"integratedMode\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      type=\"System.Web.Script.Services.ScriptHandlerFactory, System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <add name=\"ScriptResource\" preCondition=\"integratedMode\" verb=\"GET,HEAD\" path=\"ScriptResource.axd\" type=\"System.Web.Handlers.ScriptResourceHandler, System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </handlers>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  </system.webServer>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <runtime>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <assemblyBinding xmlns=\"urn:schemas-microsoft-com:asm.v1\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <dependentAssembly>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <assemblyIdentity name=\"System.Web.Extensions\" publicKeyToken=\"31bf3856ad364e35\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <bindingRedirect oldVersion=\"1.0.0.0-1.1.0.0\" newVersion=\"3.5.0.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      </dependentAssembly>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <dependentAssembly>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <assemblyIdentity name=\"System.Web.Extensions.Design\" publicKeyToken=\"31bf3856ad364e35\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <bindingRedirect oldVersion=\"1.0.0.0-1.1.0.0\" newVersion=\"3.5.0.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      </dependentAssembly>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </assemblyBinding>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  </runtime>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <system.serviceModel>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    ");
                    __printer.Write(Generated_GenerateBindings());
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    ");
                    __printer.Write(Generated_GenerateBehaviors());
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    ");
                    __printer.Write(Generated_GenerateServices());
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  </system.serviceModel>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</configuration>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateAppCode(Interface intf)
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
                    __printer.Write(intf.Namespace.Name);
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("{");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	public class ");
                    __printer.Write(intf.Name);
                    __printer.WriteTemplateOutput(" : I");
                    __printer.Write(intf.Name);
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	{");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	}");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("}");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateService(Interface intf)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<%@ ServiceHost Language=\"C#\" Debug=\"true\" Service=\"");
                    __printer.Write(intf.Namespace.Name);
                    __printer.WriteTemplateOutput(".");
                    __printer.Write(intf.Name);
                    __printer.WriteTemplateOutput("\" CodeBehind=\"~/App_Code/");
                    __printer.Write(intf.Name);
                    __printer.WriteTemplateOutput(".cs\" %>");
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
        
