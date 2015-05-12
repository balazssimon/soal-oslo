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
    public partial class NetbeansGenerator : Generator<IEnumerable<SoaObject>, GeneratorContext>
    {
        public JavaGenerator JavaGenerator { get; private set; }
        public XsdWsdlGenerator XsdWsdlGenerator { get; private set; }
        
        public NetbeansGenerator(IEnumerable<SoaObject> instances, GeneratorContext context)
            : base(instances, context)
        {
            this.Properties = new PropertyGroup_Properties();
            this.JavaGenerator = new JavaGenerator(instances, context);
            this.XsdWsdlGenerator = new XsdWsdlGenerator(instances, context);
        }
        
            #region functions from "C:\Users\Balazs\Documents\Visual Studio 2013\Projects\SoaMM\SoaGeneratorLib\NetbeansGenerator.mcg"
            public PropertyGroup_Properties Properties { get; private set; }
            
            public class PropertyGroup_Properties
            {
                public PropertyGroup_Properties()
                {
                    this.NetbeansVersion = NetbeansVersion.Netbeans8;
                    this.ProjectName = "NetbeansProject";
                    this.ResourcesDir = "../Resources";
                    this.OutputDir = "../../Output";
                    this.NoImplementationDelegates = true;
                    this.ThrowNotImplementedException = true;
                    this.GenerateProxyFeatureConstructors = false;
                    this.GenerateImplementationBase = false;
                    this.GenerateMetroJksService = true;
                    this.GenerateMetroJksClient = true;
                }
                
                public NetbeansVersion NetbeansVersion { get; set; }
                public string ProjectName { get; set; }
                public string ResourcesDir { get; set; }
                public string OutputDir { get; set; }
                public bool NoImplementationDelegates { get; set; }
                public bool ThrowNotImplementedException { get; set; }
                public bool GenerateProxyFeatureConstructors { get; set; }
                public bool GenerateImplementationBase { get; set; }
                public bool GenerateMetroJksService { get; set; }
                public bool GenerateMetroJksClient { get; set; }
            }
            
            public override void Generated_Main()
            {
                JavaGenerator.Properties.NoImplementationDelegates = Properties.NoImplementationDelegates;
                JavaGenerator.Properties.ThrowNotImplementedException = Properties.ThrowNotImplementedException;
                JavaGenerator.Properties.GenerateProxyFeatureConstructors = Properties.GenerateProxyFeatureConstructors;
                JavaGenerator.Properties.GenerateImplementationBase = Properties.GenerateImplementationBase;
                Context.SetOutputFolder(Properties.OutputDir);
                Context.CreateFolder("Netbeans");
                Context.CreateFolder("Netbeans/" + Generated_GetProjectName());
                Context.SetOutput("Netbeans/" + Generated_GetProjectName() + "/build.xml");
                Context.Output(Generated_Generate_build_xml());
                Context.CreateFolder("Netbeans/" + Generated_GetProjectName() + "/nbproject");
                File.Copy(Properties.ResourcesDir + "/Netbeans/ant-deploy.xml", "Netbeans/" + Generated_GetProjectName() + "/nbproject/ant-deploy.xml", true);
                Context.SetOutput("Netbeans/" + Generated_GetProjectName() + "/nbproject/build-impl.xml");
                Context.Output(Generated_Generate_nbproject_build_impl());
                Context.SetOutput("Netbeans/" + Generated_GetProjectName() + "/nbproject/jax-ws.xml");
                Context.Output(Generated_Generate_nbproject_jax_ws());
                Context.SetOutput("Netbeans/" + Generated_GetProjectName() + "/nbproject/jaxws-build.xml");
                Context.Output(Generated_Generate_nbproject_jaxws_build());
                Context.SetOutput("Netbeans/" + Generated_GetProjectName() + "/nbproject/project.properties");
                Context.Output(Generated_Generate_nbproject_project_properties());
                Context.SetOutput("Netbeans/" + Generated_GetProjectName() + "/nbproject/project.xml");
                Context.Output(Generated_Generate_nbproject_project_xml());
                Context.SetOutput("Netbeans/" + Generated_GetProjectName() + "/nbproject/wsit-deploy.xml");
                Context.Output(Generated_Generate_nbproject_wsit_deploy());
                Context.CreateFolder("Netbeans/" + Generated_GetProjectName() + "/src");
                Context.CreateFolder("Netbeans/" + Generated_GetProjectName() + "/src/conf");
                File.Copy(Properties.ResourcesDir + "/Netbeans/MANIFEST.MF", "Netbeans/" + Generated_GetProjectName() + "/src/conf/MANIFEST.MF", true);
                Context.CreateFolder("Netbeans/" + Generated_GetProjectName() + "/src/java");
                JavaGenerator.Properties.GenerateServerStubs = true;
                JavaGenerator.Properties.GenerateClientProxies = false;
                JavaGenerator.Generated_GenerateJavaCode("Netbeans/" + Generated_GetProjectName() + "/src/java");
                Context.CreateFolder("Netbeans/" + Generated_GetProjectName() + "/src/java/META-INF");
                File.Copy(Properties.ResourcesDir + "/Java/server_keystore.jks", "Netbeans/" + Generated_GetProjectName() + "/src/java/META-INF/server_keystore.jks", true);
                File.Copy(Properties.ResourcesDir + "/Java/server_truststore.jks", "Netbeans/" + Generated_GetProjectName() + "/src/java/META-INF/server_truststore.jks", true);
                Context.CreateFolder("Netbeans/" + Generated_GetProjectName() + "/test");
                Context.CreateFolder("Netbeans/" + Generated_GetProjectName() + "/web");
                Context.SetOutput("Netbeans/" + Generated_GetProjectName() + "/web/services.jsp");
                Context.Output(Generated_Generate_web_services_jsp());
                Context.CreateFolder("Netbeans/" + Generated_GetProjectName() + "/web/WEB-INF");
                Context.CreateFolder("Netbeans/" + Generated_GetProjectName() + "/web/WEB-INF/lib");
                File.Copy(Properties.ResourcesDir + "/Netbeans/SAMLHelper.jar", "Netbeans/" + Generated_GetProjectName() + "/web/WEB-INF/lib/SAMLHelper.jar", true);
                File.Copy(Properties.ResourcesDir + "/Netbeans/beans.xml", "Netbeans/" + Generated_GetProjectName() + "/web/WEB-INF/beans.xml", true);
                Context.SetOutput("Netbeans/" + Generated_GetProjectName() + "/web/WEB-INF/sun-web.xml");
                Context.Output(Generated_Generate_web_WEB_INF_sun_web());
                if (Properties.NetbeansVersion == NetbeansVersion.Netbeans8)
                {
                    Context.SetOutput("Netbeans/" + Generated_GetProjectName() + "/web/WEB-INF/sun-jaxws.xml");
                    Context.Output(Generated_Generate_sun_jaxws());
                }
                File.Copy(Properties.ResourcesDir + "/Netbeans/web.xml", "Netbeans/" + Generated_GetProjectName() + "/web/WEB-INF/web.xml", true);
                Context.CreateFolder("Netbeans/" + Generated_GetProjectName() + "/web/WEB-INF");
                Context.SetOutputFolder(Properties.OutputDir + "/Netbeans/" + Generated_GetProjectName() + "/web/WEB-INF");
                XsdWsdlGenerator.Properties.OutputDir = Properties.OutputDir + "/Netbeans/" + Generated_GetProjectName() + "/web/WEB-INF";
                XsdWsdlGenerator.Properties.GenerateMetroJksService = Properties.GenerateMetroJksService;
                XsdWsdlGenerator.Properties.GenerateMetroJksClient = false;
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
                JavaGenerator.Properties.WsdlDirectory = "META-INF/wsdl/";
                JavaGenerator.Properties.GenerateServerStubs = false;
                JavaGenerator.Properties.GenerateClientProxies = true;
                Context.CreateFolder("Netbeans/" + Generated_GetClientProjectName());
                Context.CreateFolder("Netbeans/" + Generated_GetClientProjectName());
                Context.SetOutput("Netbeans/" + Generated_GetClientProjectName() + "/build.xml");
                Context.Output(Generated_Generate_Client_build_xml());
                Context.CreateFolder("Netbeans/" + Generated_GetClientProjectName() + "/nbproject");
                Context.SetOutput("Netbeans/" + Generated_GetClientProjectName() + "/nbproject/build-impl.xml");
                Context.Output(Generated_Generate_Client_nbproject_build_impl());
                Context.SetOutput("Netbeans/" + Generated_GetClientProjectName() + "/nbproject/project.properties");
                Context.Output(Generated_Generate_Client_nbproject_project_properties());
                Context.SetOutput("Netbeans/" + Generated_GetClientProjectName() + "/nbproject/project.xml");
                Context.Output(Generated_Generate_Client_nbproject_project_xml());
                Context.SetOutput("Netbeans/" + Generated_GetClientProjectName() + "/manifest.mf");
                Context.Output(Generated_Generate_Client_manifest_mf());
                Context.CreateFolder("Netbeans/" + Generated_GetClientProjectName() + "/src");
                Context.CreateFolder("Netbeans/" + Generated_GetClientProjectName() + "/src/META-INF");
                Context.SetOutputFolder(Properties.OutputDir + "/Netbeans/" + Generated_GetClientProjectName() + "/src/META-INF");
                XsdWsdlGenerator.Properties.GenerateMetroJksService = false;
                XsdWsdlGenerator.Properties.GenerateMetroJksClient = Properties.GenerateMetroJksClient;
                XsdWsdlGenerator.Properties.OutputDir = Properties.OutputDir + "/Netbeans/" + Generated_GetClientProjectName() + "/META-INF";
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
                    XsdWsdlGenerator.Generated_GenerateXsdWsdl(ns);
                    Context.SetOutputFolder(Properties.OutputDir + "/Netbeans/" + Generated_GetClientProjectName() + "/src/META-INF/wsdl");
                    XsdWsdlGenerator.Properties.GenerateServiceUrl = true;
                    Context.SetOutput(ns.FullName + "EndpointWcf.wsdl");
                    XsdWsdlGenerator.Properties.ServiceUrlPattern = "http://localhost/WsInteropTest/Services/{0}.svc";
                    Context.Output(XsdWsdlGenerator.Generated_GenerateWsdlEndpoint(ns));
                    Context.SetOutput(ns.FullName + "EndpointMetro.wsdl");
                    XsdWsdlGenerator.Properties.ServiceUrlPattern = "http://localhost:8080/WsInteropTest/services/{0}";
                    Context.Output(XsdWsdlGenerator.Generated_GenerateWsdlEndpoint(ns));
                    Context.SetOutput(ns.FullName + "EndpointJBossCxf.wsdl");
                    XsdWsdlGenerator.Properties.ServiceUrlPattern = "http://localhost:8080/WsInteropTest/services/{0}";
                    Context.Output(XsdWsdlGenerator.Generated_GenerateWsdlEndpoint(ns));
                    Context.SetOutput(ns.FullName + "EndpointTomcatCxf.wsdl");
                    XsdWsdlGenerator.Properties.ServiceUrlPattern = "http://localhost:9080/WsInteropTest/services/{0}";
                    Context.Output(XsdWsdlGenerator.Generated_GenerateWsdlEndpoint(ns));
                    Context.SetOutput(ns.FullName + "EndpointOracle.wsdl");
                    XsdWsdlGenerator.Properties.ServiceUrlPattern = "http://192.168.136.128:7101/WsInteropTest/services/{0}";
                    Context.Output(XsdWsdlGenerator.Generated_GenerateWsdlEndpoint(ns));
                    Context.SetOutput(ns.FullName + "EndpointIbm.wsdl");
                    XsdWsdlGenerator.Properties.ServiceUrlPattern = "http://192.168.136.128:9080/WsInteropTest/{0}";
                    Context.Output(XsdWsdlGenerator.Generated_GenerateWsdlEndpoint(ns));
                }
                Context.SetOutputFolder(Properties.OutputDir);
                JavaGenerator.Properties.GenerateServerStubs = false;
                JavaGenerator.Properties.GenerateClientProxies = true;
                JavaGenerator.Generated_GenerateJavaCode("Netbeans/" + Generated_GetClientProjectName() + "/src");
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
                    Context.CreateFolder("Netbeans/" + Generated_GetClientProjectName() + "/src/" + Generated_GetPackage(ns).ToLower() + "client");
                    Context.SetOutput("Netbeans/" + Generated_GetClientProjectName() + "/src/" + Generated_GetPackage(ns).ToLower() + "client/Program.java");
                    Context.Output(Generated_Generate_Program_java(ns));
                }
                File.Copy(Properties.ResourcesDir + "/Java/client_keystore.jks", "Netbeans/" + Generated_GetClientProjectName() + "/src/META-INF/client_keystore.jks", true);
                File.Copy(Properties.ResourcesDir + "/Java/client_truststore.jks", "Netbeans/" + Generated_GetClientProjectName() + "/src/META-INF/client_truststore.jks", true);
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
            
            public List<string> Generated_Generate_Program_java(Namespace ns)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("/*");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput(" * To change this license header, choose License Headers in Project Properties.");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput(" * To change this template file, choose Tools | Templates");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput(" * and open the template in the editor.");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput(" */");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("package ");
                    __printer.Write(Generated_GetPackage(ns).ToLower());
                    __printer.WriteTemplateOutput("client;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import java.io.Closeable;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import java.net.URL;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import java.text.MessageFormat;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import java.util.HashMap;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.namespace.QName;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.ws.BindingProvider;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.ws.Service;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import ");
                    __printer.Write(Generated_GetPackage(ns).ToLower());
                    __printer.WriteTemplateOutput(".*;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("/*");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("VM options for logging:");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("-Dcom.sun.metro.soap.dump=true");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("*/");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("public class Program {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    enum TargetFramework {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        Metro,");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        Wcf,");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        TomcatCxf,");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        Oracle,");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        Ibm");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    private static final boolean PRINT_EXCEPTIONS = true;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    private static final String NAMESPACE_URI = \"");
                    __printer.Write(Generated_GetUri(ns));
                    __printer.WriteTemplateOutput("\";");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    private static final String WSDL_NAME = \"");
                    __printer.Write(ns.FullName);
                    __printer.WriteTemplateOutput("Endpoint{0}.wsdl\";");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    private static final HashMap<TargetFramework, String> URLS = new HashMap<>();");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    private static final TargetFramework TARGET = TargetFramework.Metro;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    /**");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("     * @param args the command line arguments");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("     */");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    public static void main(String");
                    __printer.Write("[]");
                    __printer.WriteTemplateOutput(" args) {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        try {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            URLS.put(TargetFramework.Metro, \"http://localhost:8080/");
                    __printer.Write(Generated_GetProjectName());
                    __printer.WriteTemplateOutput("/services/{0}\");");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            URLS.put(TargetFramework.Wcf, \"http://localhost/");
                    __printer.Write(Generated_GetProjectName());
                    __printer.WriteTemplateOutput("/Services/{0}.svc\");");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            URLS.put(TargetFramework.TomcatCxf, \"http://localhost:9080/");
                    __printer.Write(Generated_GetProjectName());
                    __printer.WriteTemplateOutput("/services/{0}\");");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            URLS.put(TargetFramework.Oracle, \"http://192.168.136.128:7101/");
                    __printer.Write(Generated_GetProjectName());
                    __printer.WriteTemplateOutput("/services/{0}\");");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            URLS.put(TargetFramework.Ibm, \"http://192.168.136.128:9080/");
                    __printer.Write(Generated_GetProjectName());
                    __printer.WriteTemplateOutput("/{0}\");");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            String url = URLS.get(TARGET);");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			");
                    int __loop5_iteration = 0;
                    var __loop5_result =
                        (from __loop5_tmp_item___noname5 in EnumerableExtensions.Enumerate((ns).GetEnumerator())
                        from __loop5_tmp_item_Declarations in EnumerableExtensions.Enumerate((__loop5_tmp_item___noname5.Declarations).GetEnumerator())
                        from __loop5_tmp_item_endp in EnumerableExtensions.Enumerate((__loop5_tmp_item_Declarations).GetEnumerator()).OfType<Endpoint>()
                        select
                            new
                            {
                                __loop5_item___noname5 = __loop5_tmp_item___noname5,
                                __loop5_item_Declarations = __loop5_tmp_item_Declarations,
                                __loop5_item_endp = __loop5_tmp_item_endp,
                            }).ToArray();
                    foreach (var __loop5_item in __loop5_result)
                    {
                        var __noname5 = __loop5_item.__loop5_item___noname5;
                        var Declarations = __loop5_item.__loop5_item_Declarations;
                        var endp = __loop5_item.__loop5_item_endp;
                        ++__loop5_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("            test");
                        __printer.Write(endp.Interface.Name);
                        __printer.WriteTemplateOutput("(\"");
                        __printer.Write(endp.Name);
                        __printer.WriteTemplateOutput("\", \"");
                        __printer.Write(endp.Interface.Name);
                        __printer.WriteTemplateOutput("_");
                        __printer.Write(endp.Binding.Name);
                        __printer.WriteTemplateOutput("_Port\", url, true);");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("			");
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        } catch (Exception ex) {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            ex.printStackTrace();");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    int __loop6_iteration = 0;
                    var __loop6_result =
                        (from __loop6_tmp_item___noname6 in EnumerableExtensions.Enumerate((ns).GetEnumerator())
                        from __loop6_tmp_item_Declarations in EnumerableExtensions.Enumerate((__loop6_tmp_item___noname6.Declarations).GetEnumerator())
                        from __loop6_tmp_item_intf in EnumerableExtensions.Enumerate((__loop6_tmp_item_Declarations).GetEnumerator()).OfType<Interface>()
                        select
                            new
                            {
                                __loop6_item___noname6 = __loop6_tmp_item___noname6,
                                __loop6_item_Declarations = __loop6_tmp_item_Declarations,
                                __loop6_item_intf = __loop6_tmp_item_intf,
                            }).ToArray();
                    foreach (var __loop6_item in __loop6_result)
                    {
                        var __noname6 = __loop6_item.__loop6_item___noname6;
                        var Declarations = __loop6_item.__loop6_item_Declarations;
                        var intf = __loop6_item.__loop6_item_intf;
                        ++__loop6_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    private static void test");
                        __printer.Write(intf.Name);
                        __printer.WriteTemplateOutput("(String endpoint, String port, String url, boolean close) {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        System.out.println(endpoint);");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        try {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("            String address = MessageFormat.format(url, endpoint);");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("            Service factory = Service.create(new URL(\"file:META-INF/wsdl/\"+MessageFormat.format(WSDL_NAME, TARGET)), new QName(NAMESPACE_URI, endpoint));");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("            ");
                        __printer.Write(intf.Name);
                        __printer.WriteTemplateOutput(" service = factory.getPort(new QName(NAMESPACE_URI, port), IInterop.class); ");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("            BindingProvider bp = (BindingProvider)service;");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("            bp.getRequestContext().put(BindingProvider.ENDPOINT_ADDRESS_PROPERTY, address);");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("            try {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                // call the service");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                try {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                    if (close) {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                        ((Closeable)service).close();");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                    }");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                } catch (Exception ex) {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                    System.out.println(\"Close failed.\");");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                    if (PRINT_EXCEPTIONS) {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                        ex.printStackTrace();");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                    }");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                }");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("            } catch (Exception ex) {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                System.out.println(\"Call failed.\");");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                if (PRINT_EXCEPTIONS) {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                    ex.printStackTrace();");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                }");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("            }");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        } catch (Exception ex) {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("            System.out.println(\"Init failed.\");");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("            if (PRINT_EXCEPTIONS) {");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                ex.printStackTrace();");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("            }");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        }");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("        System.out.println(\"----\");");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    }");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                    }
                    __printer.TrimLine();
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
        
