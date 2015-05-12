using Ionic.Zip;
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
    public partial class IbmRadGenerator : Generator<IEnumerable<SoaObject>, GeneratorContext>
    {
        public JavaGenerator JavaGenerator { get; private set; }
        public XsdWsdlGenerator XsdWsdlGenerator { get; private set; }
        
        public IbmRadGenerator(IEnumerable<SoaObject> instances, GeneratorContext context)
            : base(instances, context)
        {
            this.Properties = new PropertyGroup_Properties();
            this.JavaGenerator = new JavaGenerator(instances, context);
            this.XsdWsdlGenerator = new XsdWsdlGenerator(instances, context);
        }
        
            #region functions from "C:\Users\Balazs\Documents\Visual Studio 2013\Projects\SoaMM\SoaGeneratorLib\IbmRadGenerator.mcg"
            public PropertyGroup_Properties Properties { get; private set; }
            
            public class PropertyGroup_Properties
            {
                public PropertyGroup_Properties()
                {
                    this.ProjectName = "RadProject";
                    this.ResourcesDir = "../Resources";
                    this.OutputDir = "../../Output";
                    this.NoImplementationDelegates = true;
                    this.ThrowNotImplementedException = true;
                    this.GenerateProxyFeatureConstructors = false;
                    this.GenerateImplementationBase = false;
                }
                
                public string ProjectName { get; set; }
                public string ResourcesDir { get; set; }
                public string OutputDir { get; set; }
                public bool NoImplementationDelegates { get; set; }
                public bool ThrowNotImplementedException { get; set; }
                public bool GenerateProxyFeatureConstructors { get; set; }
                public bool GenerateImplementationBase { get; set; }
            }
            
            public override void Generated_Main()
            {
                JavaGenerator.Properties.NoImplementationDelegates = Properties.NoImplementationDelegates;
                JavaGenerator.Properties.ThrowNotImplementedException = Properties.ThrowNotImplementedException;
                JavaGenerator.Properties.GenerateProxyFeatureConstructors = Properties.GenerateProxyFeatureConstructors;
                JavaGenerator.Properties.GenerateImplementationBase = Properties.GenerateImplementationBase;
                XsdWsdlGenerator.Properties.GeneratePolicies = false;
                XsdWsdlGenerator.Properties.Ibm = true;
                XsdWsdlGenerator.Properties.XPathSignEncrypt = true;
                XsdWsdlGenerator.Properties.WsPolicyNamespace = "http://schemas.xmlsoap.org/ws/2004/09/policy";
                XsdWsdlGenerator.Properties.WsSecurityPolicyNamespace = "http://docs.oasis-open.org/ws-sx/ws-securitypolicy/200512";
                Context.SetOutputFolder(Properties.OutputDir);
                Context.CreateFolder("IbmRad");
                Generated_GeneratePolicies("IbmRad/Policies");
                Generated_GenerateBindings("IbmRad/Bindings");
                Context.SetOutput("IbmRad/" + Generated_GetProjectName() + "_websphere_script.py");
                Context.Output(Generated_Generate_websphere_script());
                Context.CreateFolder("IbmRad/" + Generated_GetProjectName());
                Context.SetOutput("IbmRad/" + Generated_GetProjectName() + "/.project");
                Context.Output(Generated_Generate_project());
                Context.SetOutput("IbmRad/" + Generated_GetProjectName() + "/.classpath");
                Context.Output(Generated_Generate_classpath());
                Context.SetOutput("IbmRad/" + Generated_GetProjectName() + "/.factorypath");
                Context.Output(Generated_Generate_factorypath());
                Context.CreateFolder("IbmRad/" + Generated_GetProjectName() + "/.apt_generated");
                Context.CreateFolder("IbmRad/" + Generated_GetProjectName() + "/.settings");
                Context.SetOutput("IbmRad/" + Generated_GetProjectName() + "/.settings/.jsdtscope");
                Context.Output(Generated_Generate_jsdtscope());
                Context.SetOutput("IbmRad/" + Generated_GetProjectName() + "/.settings/org.eclipse.wst.common.component");
                Context.Output(Generated_Generate_common_component());
                Context.SetOutput("IbmRad/" + Generated_GetProjectName() + "/.settings/org.eclipse.wst.jsdt.ui.superType.container");
                Context.Output(Generated_Generate_superType_container());
                Context.SetOutput("IbmRad/" + Generated_GetProjectName() + "/.settings/org.eclipse.wst.jsdt.ui.superType.name");
                Context.Output(Generated_Generate_superType_name());
                Context.SetOutput("IbmRad/" + Generated_GetProjectName() + "/.settings/com.ibm.etools.references.prefs");
                Context.Output(Generated_Generate_etools_references_prefs());
                Context.SetOutput("IbmRad/" + Generated_GetProjectName() + "/.settings/org.eclipse.jdt.apt.core.prefs");
                Context.Output(Generated_Generate_apt_core_prefs());
                Context.SetOutput("IbmRad/" + Generated_GetProjectName() + "/.settings/org.eclipse.jdt.core.prefs");
                Context.Output(Generated_Generate_core_prefs());
                Context.SetOutput("IbmRad/" + Generated_GetProjectName() + "/.settings/org.eclipse.wst.ws.service.policy.prefs");
                Context.Output(Generated_Generate_service_policy_prefs());
                Context.SetOutput("IbmRad/" + Generated_GetProjectName() + "/.settings/org.eclipse.wst.common.project.facet.core.xml");
                Context.Output(Generated_Generate_facet_core());
                Context.CreateFolder("IbmRad/" + Generated_GetProjectName() + "/build");
                Context.CreateFolder("IbmRad/" + Generated_GetProjectName() + "/WebContent");
                Context.CreateFolder("IbmRad/" + Generated_GetProjectName() + "/WebContent/META-INF");
                Context.SetOutput("IbmRad/" + Generated_GetProjectName() + "/WebContent/META-INF/MANIFEST.MF");
                Context.Output(Generated_Generate_MetaInf_Manifest());
                Context.CreateFolder("IbmRad/" + Generated_GetProjectName() + "/WebContent/WEB-INF");
                Context.CreateFolder("IbmRad/" + Generated_GetProjectName() + "/WebContent/WEB-INF/lib");
                Context.SetOutput("IbmRad/" + Generated_GetProjectName() + "/WebContent/WEB-INF/web.xml");
                Context.Output(Generated_Generate_web_xml());
                Context.SetOutput("IbmRad/" + Generated_GetProjectName() + "/WebContent/WEB-INF/ibm-web-bnd.xml");
                Context.Output(Generated_Generate_ibm_web_bnd());
                Context.SetOutput("IbmRad/" + Generated_GetProjectName() + "/WebContent/WEB-INF/ibm-web-ext.xml");
                Context.Output(Generated_Generate_ibm_web_ext());
                JavaGenerator.Properties.GenerateServerStubs = true;
                JavaGenerator.Properties.GenerateClientProxies = false;
                JavaGenerator.Generated_GenerateJavaCode("IbmRad/" + Generated_GetProjectName() + "/src");
                Context.CreateFolder("IbmRad/" + Generated_GetProjectName() + "/WebContent/WEB-INF");
                Context.SetOutputFolder(Properties.OutputDir + "/IbmRad/" + Generated_GetProjectName() + "/WebContent/WEB-INF");
                XsdWsdlGenerator.Properties.OutputDir = Properties.OutputDir + "/IbmRad/" + Generated_GetProjectName() + "/WebContent/WEB-INF";
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
                    XsdWsdlGenerator.Generated_GenerateXsdWsdl(ns);
                }
                Context.SetOutputFolder(Properties.OutputDir);
                Context.CreateFolder("IbmRad/" + Generated_GetEarProjectName());
                Context.SetOutput("IbmRad/" + Generated_GetEarProjectName() + "/.project");
                Context.Output(Generated_Generate_project_ear());
                Context.CreateFolder("IbmRad/" + Generated_GetEarProjectName() + "/.settings");
                Context.SetOutput("IbmRad/" + Generated_GetEarProjectName() + "/.settings/org.eclipse.wst.common.component");
                Context.Output(Generated_Generate_common_component_ear());
                Context.SetOutput("IbmRad/" + Generated_GetEarProjectName() + "/.settings/org.eclipse.wst.ws.service.policy.prefs");
                Context.Output(Generated_Generate_service_policy_prefs_ear());
                Context.SetOutput("IbmRad/" + Generated_GetEarProjectName() + "/.settings/org.eclipse.wst.common.project.facet.core.xml");
                Context.Output(Generated_Generate_facet_core_ear());
                Context.CreateFolder("IbmRad/" + Generated_GetEarProjectName() + "/META-INF");
                Context.SetOutput("IbmRad/" + Generated_GetEarProjectName() + "/META-INF/policyAttachments.xml");
                Context.Output(Generated_Generate_policy_attachments());
                Context.SetOutput("IbmRad/" + Generated_GetEarProjectName() + "/META-INF/wsPolicyServiceControl.xml");
                Context.Output(Generated_Generate_ws_policy_service_control());
            }
            
            public string Generated_GetProjectName()
            {
                return Properties.ProjectName;
            }
            
            public string Generated_GetEarProjectName()
            {
                return Properties.ProjectName + "EAR";
            }
            
            public List<string> Generated_Generate_web_services_jsp()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<%-- ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    Document   : services");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    Created on : Aug 19, 2011, 3:44:15 PM");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    Author     : sb");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("--%>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<%@page contentType=\"text/html\" pageEncoding=\"UTF-8\"%>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   \"http://www.w3.org/TR/html4/loose.dtd\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<html>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <head>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <title>JSP Page</title>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </head>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <body>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <h1>List of Services</h1>");
                    __printer.WriteLine();
                    int __loop2_iteration = 0;
                    var __loop2_result =
                        (from __loop2_tmp_item___noname2 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                        from __loop2_tmp_item_endpoint in EnumerableExtensions.Enumerate((__loop2_tmp_item___noname2).GetEnumerator()).OfType<Endpoint>()
                        select
                            new
                            {
                                __loop2_item___noname2 = __loop2_tmp_item___noname2,
                                __loop2_item_endpoint = __loop2_tmp_item_endpoint,
                            }).ToArray();
                    foreach (var __loop2_item in __loop2_result)
                    {
                        var __noname2 = __loop2_item.__loop2_item___noname2;
                        var endpoint = __loop2_item.__loop2_item_endpoint;
                        ++__loop2_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        <a href=\"");
                        __printer.Write(endpoint.Name);
                        __printer.WriteTemplateOutput("?wsdl\">");
                        __printer.Write(endpoint.Name);
                        __printer.WriteTemplateOutput("</a><br/>");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </body>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</html>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public void Generated_GeneratePolicies(string dir)
            {
                Context.CreateFolder(dir);
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
                    if (binding.HasPolicy())
                    {
                        Generated_GeneratePolicy(dir + "/" + binding.Name + "_policy/PolicySets/" + binding.Name + "_policy", binding);
                        Context.SetOutput(null);
                        File.Delete(dir + "/" + binding.Name + "_policy.zip");
                        ZipFile zip = new ZipFile(dir + "/" + binding.Name + "_policy.zip");
                        zip.AddDirectory(dir + "/" + binding.Name + "_policy/PolicySets", "PolicySets");
                        zip.Save();
                        zip.Dispose();
                        Directory.Delete(dir + "/" + binding.Name + "_policy", true);
                    }
                }
            }
            
            public void Generated_GeneratePolicy(string dir, Binding binding)
            {
                Context.CreateFolder(dir);
                Context.SetOutput(dir + "/policySet.xml");
                Context.Output(Generated_GeneratePolicySet(binding));
                Context.CreateFolder(dir + "/PolicyTypes");
                int __loop4_iteration = 0;
                var __loop4_result =
                    (from __loop4_tmp_item___noname4 in EnumerableExtensions.Enumerate((binding.Protocols).GetEnumerator())
                    from __loop4_tmp_item_addr in EnumerableExtensions.Enumerate((__loop4_tmp_item___noname4).GetEnumerator()).OfType<AddressingProtocolBindingElement>()
                    select
                        new
                        {
                            __loop4_item___noname4 = __loop4_tmp_item___noname4,
                            __loop4_item_addr = __loop4_tmp_item_addr,
                        }).ToArray();
                foreach (var __loop4_item in __loop4_result)
                {
                    var __noname4 = __loop4_item.__loop4_item___noname4;
                    var addr = __loop4_item.__loop4_item_addr;
                    ++__loop4_iteration;
                    Context.CreateFolder(dir + "/PolicyTypes/WSAddressing");
                    Context.SetOutput(dir + "/PolicyTypes/WSAddressing/policy.xml");
                    Context.Output(Generated_GenerateWSAddressingPolicy(binding));
                }
                int __loop5_iteration = 0;
                var __loop5_result =
                    (from __loop5_tmp_item___noname5 in EnumerableExtensions.Enumerate((binding.Protocols).GetEnumerator())
                    from __loop5_tmp_item_rm in EnumerableExtensions.Enumerate((__loop5_tmp_item___noname5).GetEnumerator()).OfType<ReliableMessagingProtocolBindingElement>()
                    select
                        new
                        {
                            __loop5_item___noname5 = __loop5_tmp_item___noname5,
                            __loop5_item_rm = __loop5_tmp_item_rm,
                        }).ToArray();
                foreach (var __loop5_item in __loop5_result)
                {
                    var __noname5 = __loop5_item.__loop5_item___noname5;
                    var rm = __loop5_item.__loop5_item_rm;
                    ++__loop5_iteration;
                    Context.CreateFolder(dir + "/PolicyTypes/WSReliableMessaging");
                    Context.SetOutput(dir + "/PolicyTypes/WSReliableMessaging/policy.xml");
                    Context.Output(Generated_GenerateWSReliableMessagingPolicy(binding));
                }
                int __loop6_iteration = 0;
                var __loop6_result =
                    (from __loop6_tmp_item___noname6 in EnumerableExtensions.Enumerate((binding.Protocols).GetEnumerator())
                    from __loop6_tmp_item_sec in EnumerableExtensions.Enumerate((__loop6_tmp_item___noname6).GetEnumerator()).OfType<SecurityProtocolBindingElement>()
                    select
                        new
                        {
                            __loop6_item___noname6 = __loop6_tmp_item___noname6,
                            __loop6_item_sec = __loop6_tmp_item_sec,
                        }).ToArray();
                foreach (var __loop6_item in __loop6_result)
                {
                    var __noname6 = __loop6_item.__loop6_item___noname6;
                    var sec = __loop6_item.__loop6_item_sec;
                    ++__loop6_iteration;
                    Context.CreateFolder(dir + "/PolicyTypes/WSSecurity");
                    Context.SetOutput(dir + "/PolicyTypes/WSSecurity/policy.xml");
                    Context.Output(Generated_GenerateWSSecurityPolicy(binding));
                }
            }
            
            public List<string> Generated_GeneratePolicySet(Binding binding)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<ps:PolicySet xmlns:ps=\"http://www.ibm.com/xmlns/prod/websphere/200605/policyset\" xmlns:psa=\"http://www.ibm.com/xmlns/prod/websphere/200605/policysetattachment\" name=\"");
                    __printer.Write(binding.Name);
                    __printer.WriteTemplateOutput("_policy\" type=\"application\" description=\"");
                    __printer.Write(binding.Name);
                    __printer.WriteTemplateOutput("_policy\" default=\"true\" version=\"7.0.0.0\">");
                    __printer.WriteLine();
                    int __loop7_iteration = 0;
                    var __loop7_result =
                        (from __loop7_tmp_item___noname7 in EnumerableExtensions.Enumerate((binding.Protocols).GetEnumerator())
                        from __loop7_tmp_item_addr in EnumerableExtensions.Enumerate((__loop7_tmp_item___noname7).GetEnumerator()).OfType<AddressingProtocolBindingElement>()
                        select
                            new
                            {
                                __loop7_item___noname7 = __loop7_tmp_item___noname7,
                                __loop7_item_addr = __loop7_tmp_item_addr,
                            }).ToArray();
                    foreach (var __loop7_item in __loop7_result)
                    {
                        var __noname7 = __loop7_item.__loop7_item___noname7;
                        var addr = __loop7_item.__loop7_item_addr;
                        ++__loop7_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    <ps:PolicyType type=\"WSAddressing\"  provides=\"\" enabled=\"true\">");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    </ps:PolicyType>");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    int __loop8_iteration = 0;
                    var __loop8_result =
                        (from __loop8_tmp_item___noname8 in EnumerableExtensions.Enumerate((binding.Protocols).GetEnumerator())
                        from __loop8_tmp_item_rm in EnumerableExtensions.Enumerate((__loop8_tmp_item___noname8).GetEnumerator()).OfType<ReliableMessagingProtocolBindingElement>()
                        select
                            new
                            {
                                __loop8_item___noname8 = __loop8_tmp_item___noname8,
                                __loop8_item_rm = __loop8_tmp_item_rm,
                            }).ToArray();
                    foreach (var __loop8_item in __loop8_result)
                    {
                        var __noname8 = __loop8_item.__loop8_item___noname8;
                        var rm = __loop8_item.__loop8_item_rm;
                        ++__loop8_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    <ps:PolicyType type=\"WSReliableMessaging\" provides=\"\" enabled=\"true\">");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    </ps:PolicyType>");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    int __loop9_iteration = 0;
                    var __loop9_result =
                        (from __loop9_tmp_item___noname9 in EnumerableExtensions.Enumerate((binding.Protocols).GetEnumerator())
                        from __loop9_tmp_item_sec in EnumerableExtensions.Enumerate((__loop9_tmp_item___noname9).GetEnumerator()).OfType<SecurityProtocolBindingElement>()
                        select
                            new
                            {
                                __loop9_item___noname9 = __loop9_tmp_item___noname9,
                                __loop9_item_sec = __loop9_tmp_item_sec,
                            }).ToArray();
                    foreach (var __loop9_item in __loop9_result)
                    {
                        var __noname9 = __loop9_item.__loop9_item___noname9;
                        var sec = __loop9_item.__loop9_item_sec;
                        ++__loop9_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    <ps:PolicyType type=\"WSSecurity\" provides=\"\" enabled=\"true\">");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    </ps:PolicyType>");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</ps:PolicySet>           ");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateWSAddressingPolicy(Binding binding)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<wsp:Policy wsu:Id=\"");
                    __printer.Write(binding.Name);
                    __printer.WriteTemplateOutput("_Policy\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    __printer.Write(XsdWsdlGenerator.Generated_GeneratePolicyNamespaces());
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput(">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    __printer.Write(XsdWsdlGenerator.Generated_GenerateAddressingPolicy(binding));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</wsp:Policy>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateWSReliableMessagingPolicy(Binding binding)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<wsp:Policy wsu:Id=\"");
                    __printer.Write(binding.Name);
                    __printer.WriteTemplateOutput("_Policy\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    __printer.Write(XsdWsdlGenerator.Generated_GeneratePolicyNamespaces());
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput(">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    __printer.Write(XsdWsdlGenerator.Generated_GenerateReliableMessagingPolicy(binding));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</wsp:Policy>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateWSSecurityPolicy(Binding binding)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<wsp:Policy wsu:Id=\"");
                    __printer.Write(binding.Name);
                    __printer.WriteTemplateOutput("_Policy\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    __printer.Write(XsdWsdlGenerator.Generated_GeneratePolicyNamespaces());
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput(">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    __printer.Write(XsdWsdlGenerator.Generated_GenerateSecurityPolicy(binding, false));
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    __printer.Write(Generated_GenerateWSSecurityExtPolicy());
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</wsp:Policy>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateWSSecurityExtPolicy()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("  <wsp:Policy wsu:Id=\"request:app_signparts\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sp:SignedParts>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <sp:Body/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <sp:Header Namespace=\"http://schemas.xmlsoap.org/ws/2004/08/addressing\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <sp:Header Namespace=\"http://www.w3.org/2005/08/addressing\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </sp:SignedParts>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sp:SignedElements>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      ");
                    __printer.Write("<sp:XPath>/*[namespace-uri()='http://schemas.xmlsoap.org/soap/envelope/' and local-name()='Envelope']/*[namespace-uri()='http://schemas.xmlsoap.org/soap/envelope/' and local-name()='Header']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='Security']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd' and local-name()='Timestamp']</sp:XPath>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      ");
                    __printer.Write("<sp:XPath>/*[namespace-uri()='http://schemas.xmlsoap.org/soap/envelope/' and local-name()='Envelope']/*[namespace-uri()='http://schemas.xmlsoap.org/soap/envelope/' and local-name()='Header']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='Security']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='UsernameToken']</sp:XPath>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      ");
                    __printer.Write("<sp:XPath>/*[namespace-uri()='http://www.w3.org/2003/05/soap-envelope' and local-name()='Envelope']/*[namespace-uri()='http://www.w3.org/2003/05/soap-envelope' and local-name()='Header']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='Security']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd' and local-name()='Timestamp']</sp:XPath>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      ");
                    __printer.Write("<sp:XPath>/*[namespace-uri()='http://www.w3.org/2003/05/soap-envelope' and local-name()='Envelope']/*[namespace-uri()='http://www.w3.org/2003/05/soap-envelope' and local-name()='Header']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='Security']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='UsernameToken']</sp:XPath>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </sp:SignedElements>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  </wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <wsp:Policy wsu:Id=\"request:app_encparts\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sp:EncryptedParts>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <sp:Body/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </sp:EncryptedParts>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sp:EncryptedElements>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      ");
                    __printer.Write("<sp:XPath>/*[namespace-uri()='http://schemas.xmlsoap.org/soap/envelope/' and local-name()='Envelope']/*[namespace-uri()='http://schemas.xmlsoap.org/soap/envelope/' and local-name()='Header']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='Security']/*[namespace-uri()='http://www.w3.org/2000/09/xmldsig#' and local-name()='Signature']</sp:XPath>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      ");
                    __printer.Write("<sp:XPath>/*[namespace-uri()='http://schemas.xmlsoap.org/soap/envelope/' and local-name()='Envelope']/*[namespace-uri()='http://schemas.xmlsoap.org/soap/envelope/' and local-name()='Header']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='Security']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='UsernameToken']</sp:XPath>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      ");
                    __printer.Write("<sp:XPath>/*[namespace-uri()='http://www.w3.org/2003/05/soap-envelope' and local-name()='Envelope']/*[namespace-uri()='http://www.w3.org/2003/05/soap-envelope' and local-name()='Header']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='Security']/*[namespace-uri()='http://www.w3.org/2000/09/xmldsig#' and local-name()='Signature']</sp:XPath>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      ");
                    __printer.Write("<sp:XPath>/*[namespace-uri()='http://www.w3.org/2003/05/soap-envelope' and local-name()='Envelope']/*[namespace-uri()='http://www.w3.org/2003/05/soap-envelope' and local-name()='Header']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='Security']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='UsernameToken']</sp:XPath>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </sp:EncryptedElements>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  </wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <wsp:Policy wsu:Id=\"response:app_signparts\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sp:SignedParts>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <sp:Body/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <sp:Header Namespace=\"http://schemas.xmlsoap.org/ws/2004/08/addressing\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <sp:Header Namespace=\"http://www.w3.org/2005/08/addressing\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </sp:SignedParts>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sp:SignedElements>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      ");
                    __printer.Write("<sp:XPath>/*[namespace-uri()='http://schemas.xmlsoap.org/soap/envelope/' and local-name()='Envelope']/*[namespace-uri()='http://schemas.xmlsoap.org/soap/envelope/' and local-name()='Header']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='Security']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd' and local-name()='Timestamp']</sp:XPath>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      ");
                    __printer.Write("<sp:XPath>/*[namespace-uri()='http://www.w3.org/2003/05/soap-envelope' and local-name()='Envelope']/*[namespace-uri()='http://www.w3.org/2003/05/soap-envelope' and local-name()='Header']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='Security']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd' and local-name()='Timestamp']</sp:XPath>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </sp:SignedElements>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  </wsp:Policy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <wsp:Policy wsu:Id=\"response:app_encparts\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sp:EncryptedParts>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <sp:Body/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </sp:EncryptedParts>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <sp:EncryptedElements>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      ");
                    __printer.Write("<sp:XPath>/*[namespace-uri()='http://schemas.xmlsoap.org/soap/envelope/' and local-name()='Envelope']/*[namespace-uri()='http://schemas.xmlsoap.org/soap/envelope/' and local-name()='Header']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='Security']/*[namespace-uri()='http://www.w3.org/2000/09/xmldsig#' and local-name()='Signature']</sp:XPath>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      ");
                    __printer.Write("<sp:XPath>/*[namespace-uri()='http://www.w3.org/2003/05/soap-envelope' and local-name()='Envelope']/*[namespace-uri()='http://www.w3.org/2003/05/soap-envelope' and local-name()='Header']/*[namespace-uri()='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd' and local-name()='Security']/*[namespace-uri()='http://www.w3.org/2000/09/xmldsig#' and local-name()='Signature']</sp:XPath>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </sp:EncryptedElements>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  </wsp:Policy>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public void Generated_GenerateBindings(string dir)
            {
                Context.CreateFolder(dir);
                Generated_GenerateBinding(dir + "/" + Generated_GetProjectName() + "_binding/bindings/" + Generated_GetProjectName() + "_binding");
                Context.SetOutput(null);
                File.Delete(dir + "/" + Generated_GetProjectName() + "_binding.zip");
                ZipFile zip = new ZipFile(dir + "/" + Generated_GetProjectName() + "_binding.zip");
                zip.AddDirectory(dir + "/" + Generated_GetProjectName() + "_binding/bindings", "bindings");
                zip.Save();
                zip.Dispose();
                Directory.Delete(dir + "/" + Generated_GetProjectName() + "_binding", true);
            }
            
            public void Generated_GenerateBinding(string dir)
            {
                Context.CreateFolder(dir);
                Context.SetOutput(dir + "/bindingDefinition.xml");
                Context.Output(Generated_GenerateBindingDefinition());
                Context.CreateFolder(dir + "/PolicyTypes");
                Context.CreateFolder(dir + "/PolicyTypes/HTTPTransport");
                Context.SetOutput(dir + "/PolicyTypes/HTTPTransport/bindings.xml");
                Context.Output(Generated_GenerateHttpTransportBinding());
                Context.CreateFolder(dir + "/PolicyTypes/JMSTransport");
                Context.SetOutput(dir + "/PolicyTypes/JMSTransport/bindings.xml");
                Context.Output(Generated_GenerateJmsTransportBinding());
                Context.CreateFolder(dir + "/PolicyTypes/SSLTransport");
                Context.SetOutput(dir + "/PolicyTypes/SSLTransport/bindings.xml");
                Context.Output(Generated_GenerateSslTransportBinding());
                Context.CreateFolder(dir + "/PolicyTypes/WSAddressing");
                Context.SetOutput(dir + "/PolicyTypes/WSAddressing/bindings.xml");
                Context.Output(Generated_GenerateWSAddressingBinding());
                Context.CreateFolder(dir + "/PolicyTypes/WSReliableMessaging");
                Context.SetOutput(dir + "/PolicyTypes/WSReliableMessaging/bindings.xml");
                Context.Output(Generated_GenerateWSReliableMessagingBinding());
                Context.CreateFolder(dir + "/PolicyTypes/WSSecurity");
                Context.SetOutput(dir + "/PolicyTypes/WSSecurity/bindings.xml");
                Context.Output(Generated_GenerateWSSecurityBinding());
            }
            
            public List<string> Generated_GenerateBindingDefinition()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<BindingDefinition xmlns=\"http://www.ibm.com/xmlns/prod/websphere/200711/bindingdefinition\" description=\"");
                    __printer.Write(Generated_GetProjectName());
                    __printer.WriteTemplateOutput("_binding\" type=\"provider\" version=\"7.0.0.0\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <Domain name=\"global\"></Domain>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</BindingDefinition>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateHttpTransportBinding()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<wsp:Policy");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:wshttp=\"http://www.ibm.com/xmlns/prod/websphere/200609/ws-httpTransport\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:wsp=\"http://schemas.xmlsoap.org/ws/2004/09/policy\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<!-- Do not edit this file. -->");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<wsp:ExactlyOne>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		<wsp:All>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			<wshttp:outRequestBasicAuth>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                                <wshttp:basicAuth userid=\"\" password=\"\"></wshttp:basicAuth>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			</wshttp:outRequestBasicAuth>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			<wshttp:outAsyncResponseBasicAuth> ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                                <wshttp:basicAuth userid=\"\" password=\"\"></wshttp:basicAuth>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            </wshttp:outAsyncResponseBasicAuth>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			<wshttp:outRequestProxy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                                <wshttp:connectInfo host=\"\" port=\"\"></wshttp:connectInfo>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                                <wshttp:basicAuth userid=\"\" password=\"\"></wshttp:basicAuth>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			</wshttp:outRequestProxy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			<wshttp:outAsyncResponseProxy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("								<wshttp:connectInfo host=\"\" port=\"\"></wshttp:connectInfo>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                                <wshttp:basicAuth userid=\"\" password=\"\"></wshttp:basicAuth>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			</wshttp:outAsyncResponseProxy>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		</wsp:All>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	</wsp:ExactlyOne>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</wsp:Policy>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateJmsTransportBinding()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<wsp:Policy");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    xmlns:wsjms=\"http://www.ibm.com/xmlns/prod/websphere/200801/ws-jmsTransport\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    xmlns:wsp=\"http://schemas.xmlsoap.org/ws/2004/09/policy\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <!-- Do not edit this file. -->");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <wsp:ExactlyOne>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <wsp:All>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <wsjms:outRequestBasicAuth>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                <wsjms:basicAuth userid=\"\" password=\"\"></wsjms:basicAuth>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            </wsjms:outRequestBasicAuth>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <wsjms:outAsyncResponseBasicAuth> ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                <wsjms:basicAuth userid=\"\" password=\"\"></wsjms:basicAuth>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            </wsjms:outAsyncResponseBasicAuth>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        </wsp:All>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </wsp:ExactlyOne>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</wsp:Policy>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateSslTransportBinding()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<wsp:Policy");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:wshttp=\"http://www.ibm.com/xmlns/prod/websphere/200609/ws-httpTransport\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:wsp=\"http://schemas.xmlsoap.org/ws/2004/09/policy\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<!-- Do not edit this file. -->");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<!-- This is binding for Https -->");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<wsp:ExactlyOne>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		<wsp:All>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			<wshttp:outRequestwithSSL>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("				<wshttp:configAlias name=\"NodeDefaultSSLSettings\"></wshttp:configAlias>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("				<wshttp:configFile path=\"${WAS_PROPS_DIR}/ssl.client.props\"></wshttp:configFile>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			</wshttp:outRequestwithSSL>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			<wshttp:outAsyncResponsewithSSL>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("				<wshttp:configAlias name=\"NodeDefaultSSLSettings\"></wshttp:configAlias>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("				<wshttp:configFile path=\"${WAS_PROPS_DIR}/ssl.client.props\"></wshttp:configFile>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			</wshttp:outAsyncResponsewithSSL>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			<wshttp:inResponsewithSSL>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("				<wshttp:configAlias name=\"NodeDefaultSSLSettings\"></wshttp:configAlias>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("				<wshttp:configFile path=\"${WAS_PROPS_DIR}/ssl.client.props\"></wshttp:configFile>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			</wshttp:inResponsewithSSL>			");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		</wsp:All>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	</wsp:ExactlyOne>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</wsp:Policy>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateWSAddressingBinding()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<wsp:Policy xmlns:wsp=\"http://www.w3.org/ns/ws-policy\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:wlm=\"http://www.ibm.com/ws/wsaddressing/jaxws/policyset/wlmset_200608\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<!-- Do not edit this file. -->");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<wsp:ExactlyOne>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		<wsp:All></wsp:All>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	</wsp:ExactlyOne>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</wsp:Policy>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateWSReliableMessagingBinding()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<rmBinding:busConfiguration xmlns:rmBinding=\"http://www.ibm.com/websphere/webservices/wsrmPolicyBinding\" xmlns:rmPolicy=\"http://www.ibm.com/websphere/webservices/wsrmPolicySet\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <rmBinding:busName></rmBinding:busName>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <rmBinding:messagingEngineName></rmBinding:messagingEngineName>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</rmBinding:busConfiguration>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateWSSecurityBinding()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<securityBindings xmlns=\"http://www.ibm.com/xmlns/prod/websphere/200710/ws-securitybinding\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<securityBinding name=\"application\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <securityOutboundBindingConfig>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <signingInfo order=\"1\" name=\"asymmetric-signingInfoResponse\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <signingPartReference>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <transform  algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      </signingPartReference>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <signingKeyInfo reference=\"gen_signkeyinfo\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </signingInfo>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <keyInfo type=\"STRREF\" name=\"gen_signkeyinfo\" classname=\"com.ibm.ws.wssecurity.wssapi.CommonContentGenerator\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <tokenReference reference=\"gen_signx509token\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </keyInfo>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <!-- Default Binding for X509Token -->");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <tokenGenerator name=\"gen_signx509token\" classname=\"com.ibm.ws.wssecurity.wssapi.token.impl.CommonTokenGenerator\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <valueType localName=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3\" uri=\"\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <callbackHandler classname=\"com.ibm.websphere.wssecurity.callbackhandler.X509GenerateCallbackHandler\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("          <key alias=\"wspservicepriv\" keypass=\"changeit\" name=\"CN=WspService, OU=IIT, O=BME, L=Budapest, S=Hungary, C=HU\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("          <keyStore storepass=\"changeit\" path=\"${USER_INSTALL_ROOT}\\etc\\ws-security\\");
                    __printer.Write(Generated_GetProjectName());
                    __printer.WriteTemplateOutput("\\server_keystore.jks\" type=\"JKS\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      </callbackHandler>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <jAASConfig configName=\"system.wss.generate.x509\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </tokenGenerator>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <signingInfo order=\"2\" name=\"symmetric-signingInfoResponse\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <signingPartReference>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <transform  algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      </signingPartReference>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <signingKeyInfo reference=\"gen_signsctkeyinfo\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </signingInfo>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <keyInfo type=\"STRREF\" name=\"gen_signsctkeyinfo\" classname=\"com.ibm.ws.wssecurity.wssapi.CommonContentGenerator\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <tokenReference reference=\"gen_scttoken\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <derivedKeyInfo>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("          <requireDerivedKeys/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("          <requireExplicitDerivedKeys/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      </derivedKeyInfo>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </keyInfo>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <tokenGenerator name=\"gen_scttoken\" classname=\"com.ibm.ws.wssecurity.wssapi.token.impl.CommonTokenGenerator\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <valueType localName=\"http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512/sct\" uri=\"\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <callbackHandler classname=\"com.ibm.ws.wssecurity.impl.auth.callback.WSTrustCallbackHandler\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <jAASConfig configName=\"system.wss.generate.sct\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </tokenGenerator>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <encryptionInfo order=\"3\" name=\"asymmetric-encryptionInfoResponse\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <keyEncryptionKeyInfo reference=\"gen_enckeyinfo\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <encryptionPartReference/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </encryptionInfo>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <keyInfo type=\"KEYID\" name=\"gen_enckeyinfo\" classname=\"com.ibm.ws.wssecurity.wssapi.CommonContentGenerator\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <tokenReference reference=\"gen_encx509token\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </keyInfo>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <tokenGenerator name=\"gen_encx509token\" classname=\"com.ibm.ws.wssecurity.wssapi.token.impl.CommonTokenGenerator\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("       <valueType localName=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3\" uri=\"\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("       <callbackHandler classname=\"com.ibm.websphere.wssecurity.callbackhandler.X509GenerateCallbackHandler\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("          <key alias=\"wspclientpub\" name=\"CN=WspClient, OU=IIT, O=BME, L=Budapest, S=Hungary, C=HU\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("          <keyStore storepass=\"changeit\" path=\"${USER_INSTALL_ROOT}\\etc\\ws-security\\");
                    __printer.Write(Generated_GetProjectName());
                    __printer.WriteTemplateOutput("\\server_truststore.jks\" type=\"JKS\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      </callbackHandler>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <jAASConfig configName=\"system.wss.generate.x509\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </tokenGenerator>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <encryptionInfo order=\"4\" name=\"symmetric-encryptionInfoResponse\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <encryptionPartReference>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <dataEncryptionKeyInfo reference=\"gen_encsctkeyinfo\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </encryptionPartReference>     ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </encryptionInfo>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <keyInfo type=\"STRREF\" name=\"gen_encsctkeyinfo\" classname=\"com.ibm.ws.wssecurity.wssapi.CommonContentGenerator\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <tokenReference reference=\"gen_scttoken\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <derivedKeyInfo>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("          <requireDerivedKeys/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("          <requireExplicitDerivedKeys/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      </derivedKeyInfo>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </keyInfo>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  </securityOutboundBindingConfig>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <securityInboundBindingConfig> ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <encryptionInfo name=\"asymmetric-encryptionInfoRequest\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <keyEncryptionKeyInfo reference=\"dec_keyinfo\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <encryptionPartReference/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </encryptionInfo>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <keyInfo name=\"dec_keyinfo\" classname=\"com.ibm.ws.wssecurity.wssapi.CommonContentConsumer\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <tokenReference reference=\"con_encx509token\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </keyInfo>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <tokenConsumer classname=\"com.ibm.ws.wssecurity.wssapi.token.impl.CommonTokenConsumer\" name=\"con_encx509token\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <valueType localName=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3\" uri=\"\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <callbackHandler classname=\"com.ibm.websphere.wssecurity.callbackhandler.X509ConsumeCallbackHandler\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <key alias=\"wspservicepriv\" keypass=\"changeit\" name=\"CN=WspService, OU=IIT, O=BME, L=Budapest, S=Hungary, C=HU\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <keyStore storepass=\"changeit\" path=\"${USER_INSTALL_ROOT}\\etc\\ws-security\\");
                    __printer.Write(Generated_GetProjectName());
                    __printer.WriteTemplateOutput("\\server_keystore.jks\" type=\"JKS\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <certPathSettings>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("          <trustAnyCertificate/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        </certPathSettings>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      </callbackHandler>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <jAASConfig configName=\"system.wss.consume.x509\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </tokenConsumer>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <encryptionInfo name=\"symmetric-encryptionInfoRequest\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <encryptionPartReference>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <dataEncryptionKeyInfo reference=\"dec_sctkeyinfo\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </encryptionPartReference>     ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </encryptionInfo>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <keyInfo name=\"dec_sctkeyinfo\" classname=\"com.ibm.ws.wssecurity.wssapi.CommonContentConsumer\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <tokenReference reference=\"con_scttoken\" /> ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <derivedKeyInfo>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("          <requireDerivedKeys/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("          <requireExplicitDerivedKeys/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      </derivedKeyInfo>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </keyInfo>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <tokenConsumer classname=\"com.ibm.ws.wssecurity.wssapi.token.impl.CommonTokenConsumer\" name=\"con_scttoken\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <valueType localName=\"http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512/sct\" uri=\"\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <callbackHandler classname=\"com.ibm.ws.wssecurity.impl.auth.callback.SCTConsumeCallbackHandler\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      </callbackHandler>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("     <jAASConfig configName=\"system.wss.consume.sct\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </tokenConsumer>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <signingInfo name=\"asymmetric-signingInfoRequest\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <signingPartReference>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <transform  algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      </signingPartReference>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <signingKeyInfo reference=\"con_signkeyinfo\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </signingInfo>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <keyInfo name=\"con_signkeyinfo\" classname=\"com.ibm.ws.wssecurity.wssapi.CommonContentConsumer\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <tokenReference reference=\"con_signx509token\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </keyInfo>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <tokenConsumer classname=\"com.ibm.ws.wssecurity.wssapi.token.impl.CommonTokenConsumer\" name=\"con_signx509token\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("       <valueType localName=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3\" uri=\"\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <callbackHandler classname=\"com.ibm.websphere.wssecurity.callbackhandler.X509ConsumeCallbackHandler\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("          <certPathSettings>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <trustAnchorRef reference=\"DigSigTrustAnchor\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <certStoreRef reference=\"DigSigCertStore\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("          </certPathSettings>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        </callbackHandler>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <jAASConfig configName=\"system.wss.consume.x509\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </tokenConsumer>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <trustAnchor name=\"DigSigTrustAnchor\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("       <keyStore storepass=\"changeit\" path=\"${USER_INSTALL_ROOT}\\etc\\ws-security\\");
                    __printer.Write(Generated_GetProjectName());
                    __printer.WriteTemplateOutput("\\server_truststore.jks\" type=\"JKS\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </trustAnchor>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <certStoreList>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <collectionCertStores provider=\"IBMCertPath\" name=\"DigSigCertStore\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("          <x509Certificates path=\"${USER_INSTALL_ROOT}\\etc\\ws-security\\");
                    __printer.Write(Generated_GetProjectName());
                    __printer.WriteTemplateOutput("\\WspService.crt\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("          <x509Certificates path=\"${USER_INSTALL_ROOT}\\etc\\ws-security\\");
                    __printer.Write(Generated_GetProjectName());
                    __printer.WriteTemplateOutput("\\WspClient.crt\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        </collectionCertStores>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </certStoreList>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <signingInfo name=\"symmetric-signingInfoRequest\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <signingPartReference>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <transform  algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      </signingPartReference>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <signingKeyInfo reference=\"con_sctsignkeyinfo\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </signingInfo>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <keyInfo name=\"con_sctsignkeyinfo\" classname=\"com.ibm.ws.wssecurity.wssapi.CommonContentConsumer\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <tokenReference reference=\"con_scttoken\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <derivedKeyInfo>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("          <requireDerivedKeys/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("          <requireExplicitDerivedKeys/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      </derivedKeyInfo>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </keyInfo>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <!-- Default Binding for UsernameToken -->");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <tokenConsumer name=\"con_unametoken\" classname=\"com.ibm.ws.wssecurity.wssapi.token.impl.CommonTokenConsumer\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <valueType localName=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#UsernameToken\" uri=\"\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <callbackHandler classname=\"com.ibm.websphere.wssecurity.callbackhandler.UNTConsumeCallbackHandler\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <properties name=\"com.ibm.wsspi.wssecurity.token.username.verifyTimestamp\" value=\"true\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <properties name=\"com.ibm.wsspi.wssecurity.token.username.verifyNonce\" value=\"true\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      </callbackHandler>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <jAASConfig configName=\"system.wss.consume.unt\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </tokenConsumer>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   <!-- Default Binding for LTPAToken -->");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <tokenConsumer name=\"con_ltpatoken\" classname=\"com.ibm.ws.wssecurity.wssapi.token.impl.CommonTokenConsumer\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <valueType localName=\"LTPAv2\" uri=\"http://www.ibm.com/websphere/appserver/tokentype\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <callbackHandler classname=\"com.ibm.websphere.wssecurity.callbackhandler.LTPAConsumeCallbackHandler\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <jAASConfig configName=\"system.wss.consume.ltpa\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </tokenConsumer>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   <!-- Default Binding for LTPA_PropagationToken -->");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <tokenConsumer name=\"con_ltpaproptoken\" classname=\"com.ibm.ws.wssecurity.wssapi.token.impl.CommonTokenConsumer\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <valueType localName=\"LTPA_PROPAGATION\" uri=\"http://www.ibm.com/websphere/appserver/tokentype\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <callbackHandler classname=\"com.ibm.websphere.wssecurity.callbackhandler.LTPAConsumeCallbackHandler\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <jAASConfig configName=\"system.wss.consume.ltpaProp\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </tokenConsumer>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  </securityInboundBindingConfig>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput(" </securityBinding>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</securityBindings>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_Generate_websphere_script()
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
        
