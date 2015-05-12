using OsloExtensions;
using OsloExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoaMetaModel
{
    // Inheritace from 'Generator<List<object>, GeneratorContext>' and constructor is only generated into the main file.
    public partial class IbmRadGenerator
    {
            #region functions from "C:\Users\Balazs\Documents\Visual Studio 2013\Projects\SoaMM\SoaGeneratorLib\IbmRadGeneratorLib.mcg"
            public List<string> Generated_Generate_project()
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
                    __printer.WriteTemplateOutput("	<projects>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	</projects>");
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
                    __printer.WriteTemplateOutput("	</natures>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</projectDescription>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_Generate_classpath()
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
                    __printer.WriteTemplateOutput("	<classpathentry kind=\"con\" path=\"org.eclipse.jdt.launching.JRE_CONTAINER/org.eclipse.jdt.internal.debug.ui.launcher.StandardVMType/WebSphere Application Server V8.5 JRE\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		<attributes>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			<attribute name=\"owner.project.facets\" value=\"java\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		</attributes>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	</classpathentry>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<classpathentry kind=\"src\" path=\".apt_generated\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		<attributes>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			<attribute name=\"optional\" value=\"true\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		</attributes>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	</classpathentry>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<classpathentry kind=\"con\" path=\"org.eclipse.jst.server.core.container/com.ibm.ws.ast.st.runtime.runtimeTarget.v85/was.base.v85\">");
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
                    __printer.WriteTemplateOutput("	<classpathentry kind=\"output\" path=\"WebContent/WEB-INF/classes\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</classpath>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_Generate_factorypath()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<factorypath>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <factorypathentry kind=\"PLUGIN\" id=\"com.ibm.jee.annotations.processor\" enabled=\"true\" runInBatchMode=\"false\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <factorypathentry kind=\"PLUGIN\" id=\"com.ibm.ws.ast.wsfp.annotations.processor\" enabled=\"true\" runInBatchMode=\"false\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <factorypathentry kind=\"PLUGIN\" id=\"com.ibm.etools.webtools.jpa.managerbean\" enabled=\"true\" runInBatchMode=\"false\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <factorypathentry kind=\"PLUGIN\" id=\"com.ibm.jaxrs.annotations.processor\" enabled=\"false\" runInBatchMode=\"false\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</factorypath> ");
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
            
            public List<string> Generated_Generate_apt_core_prefs()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("org.eclipse.jdt.apt.processorOptions/com.ibm.ws.ast.jws.annotations.processor.validateWSDL=on");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("org.eclipse.jdt.apt.aptEnabled=true");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("eclipse.preferences.version=1");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("org.eclipse.jdt.apt.reconcileEnabled=true");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_Generate_core_prefs()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("org.eclipse.jdt.core.compiler.problem.enumIdentifier=error");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("org.eclipse.jdt.core.compiler.codegen.targetPlatform=1.6");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("eclipse.preferences.version=1");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("org.eclipse.jdt.core.compiler.codegen.inlineJsrBytecode=enabled");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("org.eclipse.jdt.core.compiler.problem.assertIdentifier=error");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("org.eclipse.jdt.core.compiler.source=1.6");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("org.eclipse.jdt.core.compiler.processAnnotations=enabled");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("org.eclipse.jdt.core.compiler.compliance=1.6");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_Generate_common_component()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\" encoding=\"UTF-8\"?><project-modules id=\"moduleCoreId\" project-version=\"1.5.0\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <wb-module deploy-name=\"");
                    __printer.Write(Generated_GetProjectName());
                    __printer.WriteTemplateOutput("\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <wb-resource deploy-path=\"/\" source-path=\"/WebContent\" tag=\"defaultRootSource\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <wb-resource deploy-path=\"/WEB-INF/classes\" source-path=\"/src\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <wb-resource deploy-path=\"/WEB-INF/classes\" source-path=\"/.apt_generated\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <property name=\"context-root\" value=\"");
                    __printer.Write(Generated_GetProjectName());
                    __printer.WriteTemplateOutput("\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <property name=\"java-output-path\" value=\"/");
                    __printer.Write(Generated_GetProjectName());
                    __printer.WriteTemplateOutput("/WebContent/WEB-INF/classes\"/>");
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
                    __printer.WriteTemplateOutput("  <runtime name=\"was.base.v85\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <fixed facet=\"java\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <fixed facet=\"wst.jsdt.web\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <fixed facet=\"jst.web\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <installed facet=\"java\" version=\"1.6\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <installed facet=\"jst.web\" version=\"3.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <installed facet=\"com.ibm.websphere.coexistence.web\" version=\"8.5\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <installed facet=\"com.ibm.websphere.extended.web\" version=\"8.5\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <installed facet=\"wst.jsdt.web\" version=\"1.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</faceted-project>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_Generate_etools_references_prefs()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("com.ibm.etools.references.ui.validation.projectPropertiesEnabled=true");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("eclipse.preferences.version=1");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("com.ibm.etools.references.ui.validation.severityLevel=0");
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
            
            public List<string> Generated_Generate_service_policy_prefs()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("org.eclipse.wst.ws.service.policy.projectEnabled=false");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("com.ibm.ast.ws.jaxws.annotations.v7.jaxws.default.value.key=com.ibm.ast.ws.jaxws.annotations.v7.default");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("eclipse.preferences.version=1");
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
                }
                return __result;
            }
            
            public List<string> Generated_Generate_ibm_web_bnd()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<web-bnd ");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns=\"http://websphere.ibm.com/xml/ns/javaee\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xsi:schemaLocation=\"http://websphere.ibm.com/xml/ns/javaee http://websphere.ibm.com/xml/ns/javaee/ibm-web-bnd_1_1.xsd\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	version=\"1.1\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<virtual-host name=\"default_host\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</web-bnd>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_Generate_ibm_web_ext()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<web-ext");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns=\"http://websphere.ibm.com/xml/ns/javaee\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	xsi:schemaLocation=\"http://websphere.ibm.com/xml/ns/javaee http://websphere.ibm.com/xml/ns/javaee/ibm-web-ext_1_1.xsd\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	version=\"1.1\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<reload-interval value=\"3\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<enable-directory-browsing value=\"false\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<enable-file-serving value=\"true\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<enable-reloading value=\"true\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<enable-serving-servlets-by-class-name value=\"false\" />");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</web-ext>");
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
            
            public List<string> Generated_Generate_project_ear()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<projectDescription>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<name>");
                    __printer.Write(Generated_GetEarProjectName());
                    __printer.WriteTemplateOutput("</name>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<comment></comment>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<projects>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		<project>");
                    __printer.Write(Generated_GetProjectName());
                    __printer.WriteTemplateOutput("</project>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	</projects>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	<buildSpec>");
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
                    __printer.WriteTemplateOutput("		<nature>org.eclipse.wst.common.project.facet.core.nature</nature>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("		<nature>org.eclipse.wst.common.modulecore.ModuleCoreNature</nature>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("	</natures>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</projectDescription>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_Generate_service_policy_prefs_ear()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("org.eclipse.wst.ws.service.policy.projectEnabled=false");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("com.ibm.ast.ws.jaxws.annotations.v7.jaxws.default.value.key=com.ibm.ast.ws.jaxws.annotations.v7.default");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("eclipse.preferences.version=1");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_Generate_common_component_ear()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<project-modules id=\"moduleCoreId\" project-version=\"1.5.0\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    <wb-module deploy-name=\"");
                    __printer.Write(Generated_GetEarProjectName());
                    __printer.WriteTemplateOutput("\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <wb-resource deploy-path=\"/\" source-path=\"/\" tag=\"defaultRootSource\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        <dependent-module archiveName=\"");
                    __printer.Write(Generated_GetProjectName());
                    __printer.WriteTemplateOutput(".war\" deploy-path=\"/\" handle=\"module:/resource/");
                    __printer.Write(Generated_GetProjectName());
                    __printer.WriteTemplateOutput("/");
                    __printer.Write(Generated_GetProjectName());
                    __printer.WriteTemplateOutput("\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <dependent-object></dependent-object>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <dependency-type>uses</dependency-type>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("        </dependent-module>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("    </wb-module>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</project-modules>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_Generate_facet_core_ear()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<faceted-project>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <runtime name=\"was.base.v85\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <fixed facet=\"jst.ear\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <installed facet=\"jst.ear\" version=\"6.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <installed facet=\"com.ibm.websphere.coexistence.ear\" version=\"8.5\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("  <installed facet=\"com.ibm.websphere.extended.ear\" version=\"8.5\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</faceted-project>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_Generate_policy_attachments()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<psa:PolicySetAttachment xmlns:psa=\"http://www.ibm.com/xmlns/prod/websphere/200605/policysetattachment\" xmlns:ps=\"http://www.ibm.com/xmlns/prod/websphere/200605/policyset\">");
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
                        if (endp.Binding.HasPolicy())
                        {
                            __printer.TrimLine();
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    <psa:PolicySetReference name=\"");
                            __printer.Write(endp.Binding.Name);
                            __printer.WriteTemplateOutput("_Policy\">");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        <psa:PolicySetBinding name=\"");
                            __printer.Write(endp.Binding.Name);
                            __printer.WriteTemplateOutput("_Binding\" scope=\"domain\"/>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("        <psa:Resource pattern=\"WebService:/");
                            __printer.Write(Generated_GetProjectName());
                            __printer.WriteTemplateOutput(".war:{");
                            __printer.Write(Generated_GetUri(endp.Namespace));
                            __printer.WriteTemplateOutput("}");
                            __printer.Write(endp.Name);
                            __printer.WriteTemplateOutput("\"/>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("    </psa:PolicySetReference>");
                            __printer.WriteLine();
                            __printer.WriteTemplateOutput("	");
                        }
                        __printer.TrimLine();
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</psa:PolicySetAttachment>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_Generate_ws_policy_service_control()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<ns2:WSPolicyServiceControl xmlns:ns2=\"http://www.ibm.com/xmlns/prod/websphere/200709/WSPolicyServiceControl\" Version=\"1.0\"/>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            #endregion
        }
    }
    
