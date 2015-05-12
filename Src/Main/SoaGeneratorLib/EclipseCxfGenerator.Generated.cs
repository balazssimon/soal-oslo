using OsloExtensions;
using OsloExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoaMetaModel
{
    // The main file of the generator.
    public partial class EclipseCxfGenerator : Generator<IEnumerable<SoaObject>, GeneratorContext>
    {
        
        public EclipseCxfGenerator(IEnumerable<SoaObject> instances, GeneratorContext context)
            : base(instances, context)
        {
            this.Properties = new PropertyGroup_Properties();
        }
        
            #region functions from "C:\Users\Balazs\Documents\Visual Studio 2013\Projects\SoaMM\SoaGeneratorLib\EclipseCxfGenerator.mcg"
            public PropertyGroup_Properties Properties { get; private set; }
            
            public class PropertyGroup_Properties
            {
                public PropertyGroup_Properties()
                {
                    this.ProjectName = "CxfProject";
                    this.ResourcesDir = "../Resources";
                    this.OutputDir = "../../Output";
                    this.CxfVersion = "2.7.11";
                    this.GenerateJksService = true;
                    this.GenerateJksClient = true;
                }
                
                public string ProjectName { get; set; }
                public string ResourcesDir { get; set; }
                public string OutputDir { get; set; }
                public string CxfVersion { get; set; }
                public bool GenerateJksService { get; set; }
                public bool GenerateJksClient { get; set; }
            }
            
            public override void Generated_Main()
            {
            }
            
            public string Generated_GetProjectName()
            {
                return Properties.ProjectName;
            }
            
            public string Generated_GetClientProjectName()
            {
                return Properties.ProjectName + "Client";
            }
            
            public List<string> Generated_Generate_server_project()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<projectDescription>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<name>");
                    __printer.Write(Generated_GetProjectName());
                    __printer.WriteTemplateOutput("</name>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<comment></comment>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<projects></projects>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<buildSpec>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		<buildCommand>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			<name>org.eclipse.wst.jsdt.core.javascriptValidator</name>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			<arguments>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			</arguments>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		</buildCommand>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		<buildCommand>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			<name>org.eclipse.jdt.core.javabuilder</name>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			<arguments>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			</arguments>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		</buildCommand>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		<buildCommand>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			<name>org.eclipse.wst.common.project.facet.core.builder</name>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			<arguments>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			</arguments>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		</buildCommand>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		<buildCommand>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			<name>org.jboss.tools.jst.web.kb.kbbuilder</name>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			<arguments>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			</arguments>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		</buildCommand>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		<buildCommand>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			<name>org.eclipse.wst.validation.validationbuilder</name>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			<arguments>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			</arguments>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		</buildCommand>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	</buildSpec>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<natures>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		<nature>org.eclipse.jem.workbench.JavaEMFNature</nature>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		<nature>org.eclipse.wst.common.modulecore.ModuleCoreNature</nature>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		<nature>org.eclipse.wst.common.project.facet.core.nature</nature>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		<nature>org.eclipse.jdt.core.javanature</nature>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		<nature>org.eclipse.wst.jsdt.core.jsNature</nature>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		<nature>org.jboss.tools.jst.web.kb.kbnature</nature>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		<nature>org.jboss.tools.jsf.jsfnature</nature>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	</natures>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</projectDescription>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_Generate_server_classpath()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<classpath>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<classpathentry kind=\"src\" path=\"src\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<classpathentry kind=\"con\" path=\"org.eclipse.jdt.launching.JRE_CONTAINER/org.eclipse.jdt.internal.debug.ui.launcher.StandardVMType/jdk1.7.0\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		<attributes>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			<attribute name=\"owner.project.facets\" value=\"java\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		</attributes>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	</classpathentry>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<classpathentry kind=\"con\" path=\"org.eclipse.jst.server.core.container/org.jboss.ide.eclipse.as.core.server.runtime.runtimeTarget/JBoss 7.1 Runtime\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		<attributes>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			<attribute name=\"owner.project.facets\" value=\"jst.web\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		</attributes>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	</classpathentry>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<classpathentry kind=\"con\" path=\"org.eclipse.jst.j2ee.internal.web.container\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<classpathentry kind=\"con\" path=\"org.eclipse.jst.j2ee.internal.module.container\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<classpathentry kind=\"output\" path=\"build/classes\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</classpath>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_Generate_client_project()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<projectDescription>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<name>");
                    __printer.Write(Generated_GetClientProjectName());
                    __printer.WriteTemplateOutput("</name>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<comment></comment>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<projects>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	</projects>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<buildSpec>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		<buildCommand>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			<name>org.eclipse.jdt.core.javabuilder</name>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			<arguments>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			</arguments>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		</buildCommand>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	</buildSpec>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<natures>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		<nature>org.eclipse.jdt.core.javanature</nature>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	</natures>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</projectDescription>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_Generate_client_classpath()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<classpath>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<classpathentry kind=\"src\" path=\"src\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<classpathentry kind=\"con\" path=\"org.eclipse.jdt.launching.JRE_CONTAINER/org.eclipse.jdt.internal.debug.ui.launcher.StandardVMType/jdk1.7.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<classpathentry kind=\"con\" path=\"org.eclipse.jst.ws.cxf.core.CXF_CLASSPATH_CONTAINER/Apache CXF/");
                    __printer.Write(Properties.CxfVersion);
                    __printer.WriteTemplateOutput("\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<classpathentry kind=\"con\" path=\"org.eclipse.jdt.USER_LIBRARY/Spring\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<classpathentry kind=\"output\" path=\"bin\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</classpath>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_Generate_jsdtscope()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<classpath>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<classpathentry kind=\"src\" path=\"WebContent\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<classpathentry kind=\"con\" path=\"org.eclipse.wst.jsdt.launching.JRE_CONTAINER\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<classpathentry kind=\"con\" path=\"org.eclipse.wst.jsdt.launching.WebProject\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		<attributes>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			<attribute name=\"hide\" value=\"true\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		</attributes>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	</classpathentry>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<classpathentry kind=\"con\" path=\"org.eclipse.wst.jsdt.launching.baseBrowserLibrary\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<classpathentry kind=\"output\" path=\"\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</classpath>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_Generate_core_prefs()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("eclipse.preferences.version=1");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("org.eclipse.jdt.core.compiler.codegen.inlineJsrBytecode=enabled");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("org.eclipse.jdt.core.compiler.codegen.targetPlatform=1.7");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("org.eclipse.jdt.core.compiler.codegen.unusedLocal=preserve");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("org.eclipse.jdt.core.compiler.compliance=1.7");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("org.eclipse.jdt.core.compiler.debug.lineNumber=generate");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("org.eclipse.jdt.core.compiler.debug.localVariable=generate");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("org.eclipse.jdt.core.compiler.debug.sourceFile=generate");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("org.eclipse.jdt.core.compiler.problem.assertIdentifier=error");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("org.eclipse.jdt.core.compiler.problem.enumIdentifier=error");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("org.eclipse.jdt.core.compiler.source=1.7");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_Generate_ws_cxf_core_prefs()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("eclipse.preferences.version=1");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("org.eclipse.jst.ws.cxf.core.runtime.version=");
                    __printer.Write(Properties.CxfVersion);
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_Generate_common_component()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<project-modules id=\"moduleCoreId\" project-version=\"1.5.0\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <wb-module deploy-name=\"");
                    __printer.Write(Generated_GetProjectName());
                    __printer.WriteTemplateOutput("\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <wb-resource deploy-path=\"/\" source-path=\"/WebContent\" tag=\"defaultRootSource\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <wb-resource deploy-path=\"/WEB-INF/classes\" source-path=\"/src\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <property name=\"context-root\" value=\"");
                    __printer.Write(Generated_GetProjectName());
                    __printer.WriteTemplateOutput("\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <property name=\"java-output-path\" value=\"/");
                    __printer.Write(Generated_GetProjectName());
                    __printer.WriteTemplateOutput("/build/classes\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </wb-module>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</project-modules>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_Generate_facet_core()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<faceted-project>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <runtime name=\"JBoss 7.1 Runtime\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <fixed facet=\"java\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <fixed facet=\"jst.web\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <fixed facet=\"wst.jsdt.web\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <installed facet=\"java\" version=\"1.6\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <installed facet=\"jst.web\" version=\"3.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <installed facet=\"wst.jsdt.web\" version=\"1.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</faceted-project>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_Generate_superType_container()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("org.eclipse.wst.jsdt.launching.baseBrowserLibrary");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_Generate_superType_name()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("Window");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_Generate_MetaInf_Manifest()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("Manifest-Version: 1.0");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("Class-Path: ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("Dependencies: org.springframework.spring,org.apache.cxf,org.apache.ws.security");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_Generate_SecurityCallbackHandler(string packageName)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    if (packageName != "")
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("package ");
                        __printer.Write(packageName);
                        __printer.WriteTemplateOutput(";");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("^");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import java.io.IOException;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.security.auth.callback.Callback;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.security.auth.callback.CallbackHandler;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.security.auth.callback.UnsupportedCallbackException;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import org.apache.ws.security.WSPasswordCallback;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("public class SecurityCallbackHandler implements CallbackHandler {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    public void handle(Callback");
                    __printer.Write("[]");
                    __printer.WriteTemplateOutput(" callbacks) throws IOException, UnsupportedCallbackException {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        for (int i = 0; i < callbacks.length; i++) {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            WSPasswordCallback pc = (WSPasswordCallback)callbacks");
                    __printer.Write("[");
                    __printer.WriteTemplateOutput("i");
                    __printer.Write("]");
                    __printer.WriteTemplateOutput(";");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            if (pc.getIdentifier().endsWith(\"priv\")) {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            	pc.setPassword(\"changeit\");");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        }");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    } ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("}");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_Generate_cxf_xml(bool service, bool jks)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<beans xmlns='http://www.springframework.org/schema/beans'");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:beans='http://www.springframework.org/schema/beans'");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:jaxws='http://cxf.apache.org/jaxws' xmlns:cxf=\"http://cxf.apache.org/core\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:http='http://cxf.apache.org/transports/http/configuration'");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:sec='http://cxf.apache.org/configuration/security'");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:p=\"http://cxf.apache.org/policy\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:wsa=\"http://cxf.apache.org/ws/addressing\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:wsrm-policy=\"http://schemas.xmlsoap.org/ws/2005/02/rm/policy\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:wsrm-mgr=\"http://cxf.apache.org/ws/rm/manager\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xsi:schemaLocation='http://cxf.apache.org/configuration/security");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      http://cxf.apache.org/schemas/configuration/security.xsd");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      http://cxf.apache.org/transports/http/configuration");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      http://cxf.apache.org/schemas/configuration/http-conf.xsd");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      http://www.springframework.org/schema/beans");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      http://www.springframework.org/schema/beans/spring-beans.xsd");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      http://schemas.xmlsoap.org/ws/2005/02/rm/policy");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      http://schemas.xmlsoap.org/ws/2005/02/rm/wsrm-policy.xsd");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      http://cxf.apache.org/jaxws");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      http://cxf.apache.org/schemas/jaxws.xsd");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      http://cxf.apache.org/core ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      http://cxf.apache.org/schemas/core.xsd");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      http://cxf.apache.org/ws/rm/manager");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      http://cxf.apache.org/schemas/configuration/wsrm-manager.xsd");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  '>");
                    __printer.WriteLine();
                    int __loop1_iteration = 0;
                    var __loop1_result =
                        (from __loop1_tmp_item___noname1 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                        from __loop1_tmp_item_endp in EnumerableExtensions.Enumerate((__loop1_tmp_item___noname1).GetEnumerator()).OfType<Endpoint>()
                        select
                            new
                            {
                                __loop1_item___noname1 = __loop1_tmp_item___noname1,
                                __loop1_item_endp = __loop1_tmp_item_endp,
                            }).ToArray();
                    foreach (var __loop1_item in __loop1_result)
                    {
                        var __noname1 = __loop1_item.__loop1_item___noname1;
                        var endp = __loop1_item.__loop1_item_endp;
                        ++__loop1_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("	");
                        if (endp.HasReliableMessaging() || endp.HasSecurity())
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                            if (service)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("    <jaxws:endpoint id=\"");
                                __printer.Write(endp.Name);
                                __printer.WriteTemplateOutput("\" implementor=\"");
                                __printer.Write(Generated_GetPackage(endp.Namespace).ToLower());
                                __printer.WriteTemplateOutput(".");
                                __printer.Write(endp.Name);
                                __printer.WriteTemplateOutput("\">");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("	");
                            }
                            else
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("    <jaxws:client id=\"");
                                __printer.Write(endp.Name);
                                __printer.WriteTemplateOutput("\" serviceClass=\"");
                                __printer.Write(Generated_GetPackage(endp.Namespace).ToLower());
                                __printer.WriteTemplateOutput(".");
                                __printer.Write(endp.Name);
                                __printer.WriteTemplateOutput("Service\" address=\"");
                                __printer.Write(endp.Address.Uri);
                                __printer.WriteTemplateOutput("\">");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("	");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		");
                            if (endp.HasReliableMessaging())
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        <jaxws:features>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("            <wsa:addressing/>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("            <wsrm-mgr:reliableMessaging>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("                <wsrm-policy:RMAssertion/>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("                <wsrm-mgr:deliveryAssurance>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("				");
                                int __loop2_iteration = 0;
                                var __loop2_result =
                                    (from __loop2_tmp_item_rm in EnumerableExtensions.Enumerate((endp.GetReliableMessaging()).GetEnumerator())
                                    select
                                        new
                                        {
                                            __loop2_item_rm = __loop2_tmp_item_rm,
                                        }).ToArray();
                                foreach (var __loop2_item in __loop2_result)
                                {
                                    var rm = __loop2_item.__loop2_item_rm;
                                    ++__loop2_iteration;
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("                    ");
                                    if (rm.Delivery == ReliableMessagingDelivery.AtLeastOnce)
                                    {
                                        __printer.TrimLine();
                                        __printer.WriteLine();
                                        __printer.WriteTemplateOutput("                	<wsrm-mgr:AtLeastOnce/>");
                                        __printer.WriteLine();
                                        __printer.WriteTemplateOutput("                    ");
                                    }
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("                    ");
                                    if (rm.Delivery == ReliableMessagingDelivery.AtMostOnce)
                                    {
                                        __printer.TrimLine();
                                        __printer.WriteLine();
                                        __printer.WriteTemplateOutput("                	<wsrm-mgr:AtMostOnce/>");
                                        __printer.WriteLine();
                                        __printer.WriteTemplateOutput("                    ");
                                    }
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("                    ");
                                    if (rm.Delivery == ReliableMessagingDelivery.ExactlyOnce)
                                    {
                                        __printer.TrimLine();
                                        __printer.WriteLine();
                                        __printer.WriteTemplateOutput("                	<wsrm-mgr:ExactlyOnce/>");
                                        __printer.WriteLine();
                                        __printer.WriteTemplateOutput("                    ");
                                    }
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("                    ");
                                    if (rm.InOrder == true)
                                    {
                                        __printer.TrimLine();
                                        __printer.WriteLine();
                                        __printer.WriteTemplateOutput("                	<wsrm-mgr:InOrder/>");
                                        __printer.WriteLine();
                                        __printer.WriteTemplateOutput("                    ");
                                    }
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("				");
                                }
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("                </wsrm-mgr:deliveryAssurance>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("                <wsrm-mgr:destinationPolicy acceptOffers=\"true\"/>           ");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("            </wsrm-mgr:reliableMessaging>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("        </jaxws:features>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("		");
                            if (endp.HasSecurity())
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("	    <jaxws:properties>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		   ");
                                if (service && jks)
                                {
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("	       <entry key=\"ws-security.signature.properties\" value=\"server_signature.properties\"/>");
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("	       <entry key=\"ws-security.signature.username\" value=\"wspservicepriv\"/>");
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("	       <entry key=\"ws-security.encryption.username\" value=\"useReqSigCert\"/>");
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("	       <entry key=\"ws-security.callback-handler\" value=\"SecurityCallbackHandler\"/>");
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("		   ");
                                }
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		   ");
                                if (!service && jks)
                                {
                                    __printer.TrimLine();
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("	       <entry key=\"ws-security.signature.properties\" value=\"client_signature.properties\"/>");
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("	       <entry key=\"ws-security.signature.username\" value=\"wspclientpriv\"/>");
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("	       <entry key=\"ws-security.encryption.properties\" value=\"client_encryption.properties\"/>");
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("	       <entry key=\"ws-security.encryption.username\" value=\"wspservicepub\"/>");
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("	       <entry key=\"ws-security.callback-handler\" value=\"SecurityCallbackHandler\"/>");
                                    __printer.WriteLine();
                                    __printer.WriteTemplateOutput("		   ");
                                }
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("	    </jaxws:properties>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("		");
                            }
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                            if (service)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("    </jaxws:endpoint>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("	");
                            }
                            else
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("    </jaxws:client>");
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
                            if (service)
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("    <jaxws:endpoint id=\"");
                                __printer.Write(endp.Name);
                                __printer.WriteTemplateOutput("\" implementor=\"");
                                __printer.Write(Generated_GetPackage(endp.Namespace).ToLower());
                                __printer.WriteTemplateOutput(".");
                                __printer.Write(endp.Name);
                                __printer.WriteTemplateOutput("\"/>");
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("	");
                            }
                            else
                            {
                                __printer.TrimLine();
                                __printer.WriteLine();
                                __printer.WriteTemplateOutput("    <jaxws:client id=\"");
                                __printer.Write(endp.Name);
                                __printer.WriteTemplateOutput("\" serviceClass=\"");
                                __printer.Write(Generated_GetPackage(endp.Namespace).ToLower());
                                __printer.WriteTemplateOutput(".");
                                __printer.Write(endp.Name);
                                __printer.WriteTemplateOutput("Service\" address=\"");
                                __printer.Write(endp.Address.Uri);
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
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</beans>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_Generate_security_properties(string jksFileName)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("org.apache.ws.security.crypto.provider=org.apache.ws.security.components.crypto.Merlin");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("org.apache.ws.security.crypto.merlin.file=");
                    __printer.Write(jksFileName);
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("org.apache.ws.security.crypto.merlin.keystore.type=jks");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("org.apache.ws.security.crypto.merlin.keystore.password=changeit");
                    __printer.WriteLine();
                }
                return __result;
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
                    int __loop3_iteration = 0;
                    var __loop3_result =
                        (from __loop3_tmp_item___noname2 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                        from __loop3_tmp_item_endpoint in EnumerableExtensions.Enumerate((__loop3_tmp_item___noname2).GetEnumerator()).OfType<Endpoint>()
                        select
                            new
                            {
                                __loop3_item___noname2 = __loop3_tmp_item___noname2,
                                __loop3_item_endpoint = __loop3_tmp_item_endpoint,
                            }).ToArray();
                    foreach (var __loop3_item in __loop3_result)
                    {
                        var __noname2 = __loop3_item.__loop3_item___noname2;
                        var endpoint = __loop3_item.__loop3_item_endpoint;
                        ++__loop3_iteration;
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
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import java.io.Closeable;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import java.net.URL;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import java.text.MessageFormat;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import java.util.HashMap;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import java.util.Random;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.namespace.QName;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.ws.BindingProvider;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import javax.xml.ws.Service;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import org.springframework.context.ApplicationContext;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import org.springframework.context.support.ClassPathXmlApplicationContext;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("import ");
                    __printer.Write(Generated_GetPackage(ns).ToLower());
                    __printer.WriteTemplateOutput(".*;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("public class Program {");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
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
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    private static final ApplicationContext context = new ClassPathXmlApplicationContext(new String");
                    __printer.Write("[]");
                    __printer.WriteTemplateOutput(" { \"META-INF/cxf-client.xml\" });");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    private static final boolean PRINT_EXCEPTIONS = true;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    private static final HashMap<TargetFramework, String> URLS = new HashMap<>();");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    private static final TargetFramework TARGET = TargetFramework.TomcatCxf;");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("^");
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
                    int __loop4_iteration = 0;
                    var __loop4_result =
                        (from __loop4_tmp_item___noname3 in EnumerableExtensions.Enumerate((ns).GetEnumerator())
                        from __loop4_tmp_item_Declarations in EnumerableExtensions.Enumerate((__loop4_tmp_item___noname3.Declarations).GetEnumerator())
                        from __loop4_tmp_item_endp in EnumerableExtensions.Enumerate((__loop4_tmp_item_Declarations).GetEnumerator()).OfType<Endpoint>()
                        select
                            new
                            {
                                __loop4_item___noname3 = __loop4_tmp_item___noname3,
                                __loop4_item_Declarations = __loop4_tmp_item_Declarations,
                                __loop4_item_endp = __loop4_tmp_item_endp,
                            }).ToArray();
                    foreach (var __loop4_item in __loop4_result)
                    {
                        var __noname3 = __loop4_item.__loop4_item___noname3;
                        var Declarations = __loop4_item.__loop4_item_Declarations;
                        var endp = __loop4_item.__loop4_item_endp;
                        ++__loop4_iteration;
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
                    __printer.WriteTemplateOutput("^");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	");
                    int __loop5_iteration = 0;
                    var __loop5_result =
                        (from __loop5_tmp_item___noname4 in EnumerableExtensions.Enumerate((ns).GetEnumerator())
                        from __loop5_tmp_item_Declarations in EnumerableExtensions.Enumerate((__loop5_tmp_item___noname4.Declarations).GetEnumerator())
                        from __loop5_tmp_item_intf in EnumerableExtensions.Enumerate((__loop5_tmp_item_Declarations).GetEnumerator()).OfType<Interface>()
                        select
                            new
                            {
                                __loop5_item___noname4 = __loop5_tmp_item___noname4,
                                __loop5_item_Declarations = __loop5_tmp_item_Declarations,
                                __loop5_item_intf = __loop5_tmp_item_intf,
                            }).ToArray();
                    foreach (var __loop5_item in __loop5_result)
                    {
                        var __noname4 = __loop5_item.__loop5_item___noname4;
                        var Declarations = __loop5_item.__loop5_item_Declarations;
                        var intf = __loop5_item.__loop5_item_intf;
                        ++__loop5_iteration;
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
                        __printer.WriteTemplateOutput("            ");
                        __printer.Write(intf.Name);
                        __printer.WriteTemplateOutput(" service = (");
                        __printer.Write(intf.Name);
                        __printer.WriteTemplateOutput(")context.getBean(endpoint);");
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
                    __printer.WriteTemplateOutput("^");
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
        
