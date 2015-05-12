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
    public partial class TomcatCxfGenerator : Generator<IEnumerable<SoaObject>, GeneratorContext>
    {
        public EclipseCxfGenerator EclipseCxfGenerator { get; private set; }
        public JavaGenerator JavaGenerator { get; private set; }
        public XsdWsdlGenerator XsdWsdlGenerator { get; private set; }
        
        public TomcatCxfGenerator(IEnumerable<SoaObject> instances, GeneratorContext context)
            : base(instances, context)
        {
            this.Properties = new PropertyGroup_Properties();
            this.EclipseCxfGenerator = new EclipseCxfGenerator(instances, context);
            this.JavaGenerator = new JavaGenerator(instances, context);
            this.XsdWsdlGenerator = new XsdWsdlGenerator(instances, context);
        }
        
            #region functions from "C:\Users\Balazs\Documents\Visual Studio 2013\Projects\SoaMM\SoaGeneratorLib\TomcatCxfGenerator.mcg"
            public PropertyGroup_Properties Properties { get; private set; }
            
            public class PropertyGroup_Properties
            {
                public PropertyGroup_Properties()
                {
                    this.ProjectName = "TomcatProject";
                    this.ResourcesDir = "../Resources";
                    this.OutputDir = "../../Output";
                    this.NoImplementationDelegates = true;
                    this.ThrowNotImplementedException = true;
                    this.GenerateProxyFeatureConstructors = false;
                    this.GenerateImplementationBase = false;
                    this.CxfVersion = "2.7.11";
                    this.GenerateJksService = true;
                    this.GenerateJksClient = true;
                }
                
                public string ProjectName { get; set; }
                public string ResourcesDir { get; set; }
                public string OutputDir { get; set; }
                public bool NoImplementationDelegates { get; set; }
                public bool ThrowNotImplementedException { get; set; }
                public bool GenerateProxyFeatureConstructors { get; set; }
                public bool GenerateImplementationBase { get; set; }
                public string CxfVersion { get; set; }
                public bool GenerateJksService { get; set; }
                public bool GenerateJksClient { get; set; }
            }
            
            public override void Generated_Main()
            {
                JavaGenerator.Properties.NoImplementationDelegates = Properties.NoImplementationDelegates;
                JavaGenerator.Properties.ThrowNotImplementedException = Properties.ThrowNotImplementedException;
                JavaGenerator.Properties.GenerateProxyFeatureConstructors = Properties.GenerateProxyFeatureConstructors;
                JavaGenerator.Properties.GenerateImplementationBase = Properties.GenerateImplementationBase;
                EclipseCxfGenerator.Properties.ProjectName = Properties.ProjectName;
                EclipseCxfGenerator.Properties.ResourcesDir = Properties.ResourcesDir;
                EclipseCxfGenerator.Properties.OutputDir = Properties.OutputDir;
                EclipseCxfGenerator.Properties.CxfVersion = Properties.CxfVersion;
                EclipseCxfGenerator.Properties.GenerateJksService = Properties.GenerateJksService;
                EclipseCxfGenerator.Properties.GenerateJksClient = Properties.GenerateJksClient;
                Context.SetOutputFolder(Properties.OutputDir);
                Context.CreateFolder("Tomcat");
                JavaGenerator.Properties.WsdlDirectory = "WEB-INF/wsdl/";
                Context.CreateFolder("Tomcat/" + Generated_GetProjectName());
                Context.SetOutput("Tomcat/" + Generated_GetProjectName() + "/.project");
                Context.Output(EclipseCxfGenerator.Generated_Generate_server_project());
                Context.SetOutput("Tomcat/" + Generated_GetProjectName() + "/.classpath");
                Context.Output(EclipseCxfGenerator.Generated_Generate_server_classpath());
                Context.CreateFolder("Tomcat/" + Generated_GetProjectName() + "/.settings");
                Context.SetOutput("Tomcat/" + Generated_GetProjectName() + "/.settings/.jsdtscope");
                Context.Output(EclipseCxfGenerator.Generated_Generate_jsdtscope());
                Context.SetOutput("Tomcat/" + Generated_GetProjectName() + "/.settings/org.eclipse.jdt.core.prefs");
                Context.Output(EclipseCxfGenerator.Generated_Generate_core_prefs());
                Context.SetOutput("Tomcat/" + Generated_GetProjectName() + "/.settings/org.eclipse.wst.common.component");
                Context.Output(EclipseCxfGenerator.Generated_Generate_common_component());
                Context.SetOutput("Tomcat/" + Generated_GetProjectName() + "/.settings/org.eclipse.wst.common.project.facet.core.xml");
                Context.Output(EclipseCxfGenerator.Generated_Generate_facet_core());
                Context.SetOutput("Tomcat/" + Generated_GetProjectName() + "/.settings/org.eclipse.wst.jsdt.ui.superType.container");
                Context.Output(EclipseCxfGenerator.Generated_Generate_superType_container());
                Context.SetOutput("Tomcat/" + Generated_GetProjectName() + "/.settings/org.eclipse.wst.jsdt.ui.superType.name");
                Context.Output(EclipseCxfGenerator.Generated_Generate_superType_name());
                Context.CreateFolder("Tomcat/" + Generated_GetProjectName() + "/build");
                Context.CreateFolder("Tomcat/" + Generated_GetProjectName() + "/WebContent");
                Context.SetOutput("Tomcat/" + Generated_GetProjectName() + "/WebContent/services.jsp");
                Context.Output(EclipseCxfGenerator.Generated_Generate_web_services_jsp());
                Context.CreateFolder("Tomcat/" + Generated_GetProjectName() + "/WebContent/META-INF");
                Context.SetOutput("Tomcat/" + Generated_GetProjectName() + "/WebContent/META-INF/MANIFEST.MF");
                Context.Output(EclipseCxfGenerator.Generated_Generate_MetaInf_Manifest());
                Context.CreateFolder("Tomcat/" + Generated_GetProjectName() + "/WebContent/WEB-INF");
                Context.CreateFolder("Tomcat/" + Generated_GetProjectName() + "/WebContent/WEB-INF/lib");
                Context.SetOutput("Tomcat/" + Generated_GetProjectName() + "/WebContent/WEB-INF/web.xml");
                Context.Output(Generated_Generate_web_xml());
                Context.SetOutput("Tomcat/" + Generated_GetProjectName() + "/WebContent/WEB-INF/cxf-beans.xml");
                Context.Output(EclipseCxfGenerator.Generated_Generate_cxf_xml(true, Properties.GenerateJksService));
                Context.CreateFolder("Tomcat/" + Generated_GetProjectName() + "/src");
                if (Properties.GenerateJksService)
                {
                    Context.SetOutput("Tomcat/" + Generated_GetProjectName() + "/src/SecurityCallbackHandler.java");
                    Context.Output(EclipseCxfGenerator.Generated_Generate_SecurityCallbackHandler(""));
                }
                if (Properties.GenerateJksService)
                {
                    Context.SetOutput("Tomcat/" + Generated_GetProjectName() + "/src/server_signature.properties");
                    Context.Output(EclipseCxfGenerator.Generated_Generate_security_properties("server_keystore.jks"));
                    Context.SetOutput("Tomcat/" + Generated_GetProjectName() + "/src/server_encryption.properties");
                    Context.Output(EclipseCxfGenerator.Generated_Generate_security_properties("server_truststore.jks"));
                    File.Copy(Properties.ResourcesDir + "/Java/server_keystore.jks", "Tomcat/" + Generated_GetProjectName() + "/src/server_keystore.jks", true);
                    File.Copy(Properties.ResourcesDir + "/Java/server_truststore.jks", "Tomcat/" + Generated_GetProjectName() + "/src/server_truststore.jks", true);
                }
                JavaGenerator.Properties.GenerateServerStubs = true;
                JavaGenerator.Properties.GenerateClientProxies = false;
                JavaGenerator.Generated_GenerateJavaCode("Tomcat/" + Generated_GetProjectName() + "/src");
                Context.CreateFolder("Tomcat/" + Generated_GetProjectName() + "/WebContent/WEB-INF");
                Context.SetOutputFolder(Properties.OutputDir + "/Tomcat/" + Generated_GetProjectName() + "/WebContent/WEB-INF");
                XsdWsdlGenerator.Properties.OutputDir = Properties.OutputDir + "/Tomcat/" + Generated_GetProjectName() + "/WebContent/WEB-INF";
                XsdWsdlGenerator.Properties.GenerateServiceUrl = true;
                XsdWsdlGenerator.Properties.ServiceUrlPattern = "http://localhost:9080/" + Generated_GetProjectName() + "/services/{0}";
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
                JavaGenerator.Properties.WsdlDirectory = "META-INF";
                Context.CreateFolder("Tomcat/" + Generated_GetClientProjectName());
                Context.SetOutput("Tomcat/" + Generated_GetClientProjectName() + "/.project");
                Context.Output(EclipseCxfGenerator.Generated_Generate_client_project());
                Context.SetOutput("Tomcat/" + Generated_GetClientProjectName() + "/.classpath");
                Context.Output(EclipseCxfGenerator.Generated_Generate_client_classpath());
                Context.CreateFolder("Tomcat/" + Generated_GetClientProjectName() + "/.settings");
                Context.SetOutput("Tomcat/" + Generated_GetClientProjectName() + "/.settings/org.eclipse.jdt.core.prefs");
                Context.Output(EclipseCxfGenerator.Generated_Generate_core_prefs());
                Context.SetOutput("Tomcat/" + Generated_GetClientProjectName() + "/.settings/org.eclipse.jst.ws.cxf.core.prefs");
                Context.Output(EclipseCxfGenerator.Generated_Generate_ws_cxf_core_prefs());
                Context.CreateFolder("Tomcat/" + Generated_GetClientProjectName() + "/bin");
                Context.CreateFolder("Tomcat/" + Generated_GetClientProjectName() + "/src/META-INF");
                Context.SetOutput("Tomcat/" + Generated_GetClientProjectName() + "/src/META-INF/cxf-client.xml");
                Context.Output(EclipseCxfGenerator.Generated_Generate_cxf_xml(false, Properties.GenerateJksClient));
                Context.CreateFolder("Tomcat/" + Generated_GetClientProjectName() + "/src/META-INF");
                Context.SetOutput("Tomcat/" + Generated_GetClientProjectName() + "/src/META-INF/MANIFEST.MF");
                Context.Output(EclipseCxfGenerator.Generated_Generate_MetaInf_Manifest());
                if (Properties.GenerateJksClient)
                {
                    Context.SetOutput("Tomcat/" + Generated_GetClientProjectName() + "/src/SecurityCallbackHandler.java");
                    Context.Output(EclipseCxfGenerator.Generated_Generate_SecurityCallbackHandler(""));
                }
                if (Properties.GenerateJksClient)
                {
                    Context.CreateFolder("Tomcat/" + Generated_GetClientProjectName() + "/src/META-INF");
                    Context.SetOutput("Tomcat/" + Generated_GetClientProjectName() + "/src/META-INF/client_signature.properties");
                    Context.Output(EclipseCxfGenerator.Generated_Generate_security_properties("client_keystore.jks"));
                    Context.SetOutput("Tomcat/" + Generated_GetClientProjectName() + "/src/META-INF/client_encryption.properties");
                    Context.Output(EclipseCxfGenerator.Generated_Generate_security_properties("client_truststore.jks"));
                    File.Copy(Properties.ResourcesDir + "/Java/client_keystore.jks", "Tomcat/" + Generated_GetClientProjectName() + "/src/META-INF/client_keystore.jks", true);
                    File.Copy(Properties.ResourcesDir + "/Java/client_truststore.jks", "Tomcat/" + Generated_GetClientProjectName() + "/src/META-INF/client_truststore.jks", true);
                }
                JavaGenerator.Properties.GenerateServerStubs = false;
                JavaGenerator.Properties.GenerateClientProxies = true;
                JavaGenerator.Generated_GenerateJavaCode("Tomcat/" + Generated_GetClientProjectName() + "/src");
                int __loop2_iteration = 0;
                var __loop2_result =
                    (from __loop2_tmp_item___noname2 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                    from __loop2_tmp_item_ns in EnumerableExtensions.Enumerate((__loop2_tmp_item___noname2).GetEnumerator()).OfType<Namespace>()
                    select
                        new
                        {
                            __loop2_item___noname2 = __loop2_tmp_item___noname2,
                            __loop2_item_ns = __loop2_tmp_item_ns,
                        }).ToArray();
                foreach (var __loop2_item in __loop2_result)
                {
                    var __noname2 = __loop2_item.__loop2_item___noname2;
                    var ns = __loop2_item.__loop2_item_ns;
                    ++__loop2_iteration;
                    Context.CreateFolder("Tomcat/" + Generated_GetClientProjectName() + "/src/" + Generated_GetPackage(ns).ToLower() + "client");
                    Context.SetOutput("Tomcat/" + Generated_GetClientProjectName() + "/src/" + Generated_GetPackage(ns).ToLower() + "client/Program.java");
                    Context.Output(EclipseCxfGenerator.Generated_Generate_Program_java(ns));
                }
                Context.CreateFolder("Tomcat/" + Generated_GetClientProjectName() + "/src/META-INF");
                Context.SetOutputFolder(Properties.OutputDir + "/Tomcat/" + Generated_GetClientProjectName() + "/src/META-INF");
                XsdWsdlGenerator.Properties.OutputDir = Properties.OutputDir + "/Tomcat/" + Generated_GetClientProjectName() + "/META-INF";
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
                    XsdWsdlGenerator.Generated_GenerateXsdWsdl(ns);
                }
                Context.SetOutputFolder(Properties.OutputDir);
            }
            
            public string Generated_GetProjectName()
            {
                return Properties.ProjectName;
            }
            
            public string Generated_GetClientProjectName()
            {
                return Properties.ProjectName + "Client";
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
                    int __loop4_iteration = 0;
                    var __loop4_result =
                        (from __loop4_tmp_item___noname4 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                        from __loop4_tmp_item_endpoint in EnumerableExtensions.Enumerate((__loop4_tmp_item___noname4).GetEnumerator()).OfType<Endpoint>()
                        select
                            new
                            {
                                __loop4_item___noname4 = __loop4_tmp_item___noname4,
                                __loop4_item_endpoint = __loop4_tmp_item_endpoint,
                            }).ToArray();
                    foreach (var __loop4_item in __loop4_result)
                    {
                        var __noname4 = __loop4_item.__loop4_item___noname4;
                        var endpoint = __loop4_item.__loop4_item_endpoint;
                        ++__loop4_iteration;
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
            
            public List<string> Generated_Generate_web_xml()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<web-app");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput(" version=\"2.5\" xmlns=\"http://java.sun.com/xml/ns/javaee\" ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput(" xsi:schemaLocation=\"http://java.sun.com/xml/ns/javaee http://java.sun.com/xml/ns/javaee/web-app_2_5.xsd\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</web-app>");
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
        
