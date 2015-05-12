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
    public partial class OracleGenerator : Generator<IEnumerable<SoaObject>, GeneratorContext>
    {
        public JavaGenerator JavaGenerator { get; private set; }
        public XsdWsdlGenerator XsdWsdlGenerator { get; private set; }
        
        public OracleGenerator(IEnumerable<SoaObject> instances, GeneratorContext context)
            : base(instances, context)
        {
            this.Properties = new PropertyGroup_Properties();
            this.JavaGenerator = new JavaGenerator(instances, context);
            this.XsdWsdlGenerator = new XsdWsdlGenerator(instances, context);
        }
        
            #region functions from "C:\Users\Balazs\Documents\Visual Studio 2013\Projects\SoaMM\SoaGeneratorLib\OracleGenerator.mcg"
            public PropertyGroup_Properties Properties { get; private set; }
            
            public class PropertyGroup_Properties
            {
                public PropertyGroup_Properties()
                {
                    this.ProjectName = "JDeveloperProject";
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
                Context.SetOutputFolder(Properties.OutputDir);
                Context.CreateFolder("Oracle");
                Context.SetOutput("Oracle/" + Generated_GetProjectName() + "_weblogic_script.py");
                Context.Output(Generated_Generate_weblogic_script());
                Context.CreateFolder("Oracle/" + Generated_GetProjectName() + "App");
                Context.SetOutput("Oracle/" + Generated_GetProjectName() + "App/" + Generated_GetProjectName() + "App.jws");
                Context.Output(Generated_GenerateApplicationFile());
                Context.CreateFolder("Oracle/" + Generated_GetProjectName() + "App/" + Generated_GetProjectName());
                Context.SetOutput("Oracle/" + Generated_GetProjectName() + "App/" + Generated_GetProjectName() + "/" + Generated_GetProjectName() + ".jpr");
                Context.Output(Generated_GenerateProjectFile());
                Context.CreateFolder("Oracle/" + Generated_GetProjectName() + "App/" + Generated_GetProjectName() + "/src");
                JavaGenerator.Properties.WsdlSuffix = "";
                JavaGenerator.Properties.GenerateOracleAnnotations = true;
                JavaGenerator.Properties.GenerateServerStubs = true;
                JavaGenerator.Properties.GenerateClientProxies = false;
                JavaGenerator.Properties.SeparateWsdlsForEndpoints = true;
                JavaGenerator.Generated_GenerateJavaCode("Oracle/" + Generated_GetProjectName() + "App/" + Generated_GetProjectName() + "/src");
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
                    Context.SetOutput("Oracle/" + Generated_GetProjectName() + "App/" + Generated_GetProjectName() + "/src/" + JavaGenerator.Generated_NamespaceToPath(endp.Namespace) + "/" + endp.Name + ".jaxrpc");
                    Context.Output(Generated_GenerateJaxRpc(endp));
                }
                Context.CreateFolder(Properties.OutputDir + "/Oracle/" + Generated_GetProjectName() + "App/" + Generated_GetProjectName() + "/public_html/WEB-INF");
                Context.SetOutputFolder(Properties.OutputDir + "/Oracle/" + Generated_GetProjectName() + "App/" + Generated_GetProjectName() + "/public_html/WEB-INF");
                Context.SetOutput("web.xml");
                Context.Output(Generated_GenerateWebXml());
                Context.CreateFolder(Properties.OutputDir + "/Oracle/" + Generated_GetProjectName() + "App/" + Generated_GetProjectName() + "/public_html/WEB-INF/wsdl");
                Context.SetOutputFolder(Properties.OutputDir + "/Oracle/" + Generated_GetProjectName() + "App/" + Generated_GetProjectName() + "/public_html/WEB-INF/wsdl");
                XsdWsdlGenerator.Properties.OutputDir = Properties.OutputDir + "/Oracle/" + Generated_GetProjectName() + "/public_html/WEB-INF/wsdl";
                XsdWsdlGenerator.Properties.GenerateSingleWsdl = true;
                XsdWsdlGenerator.Properties.GenerateSeparateXsdWsdlFolder = false;
                XsdWsdlGenerator.Properties.GeneratePolicies = false;
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
                    Context.SetOutput(ns.FullName + ".xsd");
                    Context.Output(XsdWsdlGenerator.Generated_GenerateXsd(ns));
                    int __loop3_iteration = 0;
                    var __loop3_result =
                        (from __loop3_tmp_item___noname3 in EnumerableExtensions.Enumerate((ns).GetEnumerator())
                        from __loop3_tmp_item_Declarations in EnumerableExtensions.Enumerate((__loop3_tmp_item___noname3.Declarations).GetEnumerator())
                        from __loop3_tmp_item_endp in EnumerableExtensions.Enumerate((__loop3_tmp_item_Declarations).GetEnumerator()).OfType<Endpoint>()
                        select
                            new
                            {
                                __loop3_item___noname3 = __loop3_tmp_item___noname3,
                                __loop3_item_Declarations = __loop3_tmp_item_Declarations,
                                __loop3_item_endp = __loop3_tmp_item_endp,
                            }).ToArray();
                    foreach (var __loop3_item in __loop3_result)
                    {
                        var __noname3 = __loop3_item.__loop3_item___noname3;
                        var Declarations = __loop3_item.__loop3_item_Declarations;
                        var endp = __loop3_item.__loop3_item_endp;
                        ++__loop3_iteration;
                        Context.SetOutput(endp.Name + ".wsdl");
                        Context.Output(XsdWsdlGenerator.Generated_GenerateSingleWsdl(endp));
                    }
                }
                Context.CreateFolder(Properties.OutputDir + "/Oracle/" + Generated_GetProjectName() + "App/" + Generated_GetProjectName() + "/public_html/WEB-INF/policies");
                Context.SetOutputFolder(Properties.OutputDir + "/Oracle/" + Generated_GetProjectName() + "App/" + Generated_GetProjectName() + "/public_html/WEB-INF/policies");
                int __loop4_iteration = 0;
                var __loop4_result =
                    (from __loop4_tmp_item___noname4 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                    from __loop4_tmp_item_ns in EnumerableExtensions.Enumerate((__loop4_tmp_item___noname4).GetEnumerator()).OfType<Namespace>()
                    from __loop4_tmp_item_Declarations in EnumerableExtensions.Enumerate((__loop4_tmp_item_ns.Declarations).GetEnumerator())
                    from __loop4_tmp_item_binding in EnumerableExtensions.Enumerate((__loop4_tmp_item_Declarations).GetEnumerator()).OfType<Binding>()
                    select
                        new
                        {
                            __loop4_item___noname4 = __loop4_tmp_item___noname4,
                            __loop4_item_ns = __loop4_tmp_item_ns,
                            __loop4_item_Declarations = __loop4_tmp_item_Declarations,
                            __loop4_item_binding = __loop4_tmp_item_binding,
                        }).ToArray();
                foreach (var __loop4_item in __loop4_result)
                {
                    var __noname4 = __loop4_item.__loop4_item___noname4;
                    var ns = __loop4_item.__loop4_item_ns;
                    var Declarations = __loop4_item.__loop4_item_Declarations;
                    var binding = __loop4_item.__loop4_item_binding;
                    ++__loop4_iteration;
                    if (binding.HasPolicy())
                    {
                        Context.SetOutput(binding.Name + "_Policy.xml");
                        Context.Output(XsdWsdlGenerator.Generated_GeneratePolicy(ns, binding, true, true));
                    }
                }
                Context.SetOutputFolder(Properties.OutputDir);
                Context.CreateFolder("Oracle/" + Generated_GetProjectName() + "App/" + Generated_GetClientProjectName());
                Context.SetOutput("Oracle/" + Generated_GetProjectName() + "App/" + Generated_GetClientProjectName() + "/" + Generated_GetClientProjectName() + ".jpr");
                Context.Output(Generated_GenerateClientProjectFile());
                Context.CreateFolder("Oracle/" + Generated_GetProjectName() + "App/" + Generated_GetClientProjectName() + "/src");
                JavaGenerator.Properties.WsdlSuffix = "";
                JavaGenerator.Properties.WsdlDirectory = "META-INF/wsdl/";
                JavaGenerator.Properties.GenerateOracleAnnotations = true;
                JavaGenerator.Properties.GenerateServerStubs = false;
                JavaGenerator.Properties.GenerateClientProxies = true;
                JavaGenerator.Properties.SeparateWsdlsForEndpoints = true;
                JavaGenerator.Generated_GenerateJavaCode("Oracle/" + Generated_GetProjectName() + "App/" + Generated_GetClientProjectName() + "/src");
                int __loop5_iteration = 0;
                var __loop5_result =
                    (from __loop5_tmp_item___noname5 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                    from __loop5_tmp_item_endp in EnumerableExtensions.Enumerate((__loop5_tmp_item___noname5).GetEnumerator()).OfType<Endpoint>()
                    select
                        new
                        {
                            __loop5_item___noname5 = __loop5_tmp_item___noname5,
                            __loop5_item_endp = __loop5_tmp_item_endp,
                        }).ToArray();
                foreach (var __loop5_item in __loop5_result)
                {
                    var __noname5 = __loop5_item.__loop5_item___noname5;
                    var endp = __loop5_item.__loop5_item_endp;
                    ++__loop5_iteration;
                    Context.SetOutput("Oracle/" + Generated_GetProjectName() + "App/" + Generated_GetClientProjectName() + "/src/" + JavaGenerator.Generated_NamespaceToPath(endp.Namespace) + "/" + endp.Name + ".proxy");
                    Context.Output(Generated_GenerateJaxRpcProxy(endp));
                }
                Context.CreateFolder(Properties.OutputDir + "/Oracle/" + Generated_GetProjectName() + "App/" + Generated_GetClientProjectName() + "/src/META-INF/wsdl");
                Context.SetOutputFolder(Properties.OutputDir + "/Oracle/" + Generated_GetProjectName() + "App/" + Generated_GetClientProjectName() + "/src/META-INF/wsdl");
                XsdWsdlGenerator.Properties.OutputDir = Properties.OutputDir + "/Oracle/" + Generated_GetClientProjectName() + "/src/META-INF/wsdl";
                XsdWsdlGenerator.Properties.GenerateSingleWsdl = true;
                XsdWsdlGenerator.Properties.GenerateSeparateXsdWsdlFolder = false;
                XsdWsdlGenerator.Properties.GeneratePolicies = false;
                int __loop6_iteration = 0;
                var __loop6_result =
                    (from __loop6_tmp_item___noname6 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                    from __loop6_tmp_item_ns in EnumerableExtensions.Enumerate((__loop6_tmp_item___noname6).GetEnumerator()).OfType<Namespace>()
                    select
                        new
                        {
                            __loop6_item___noname6 = __loop6_tmp_item___noname6,
                            __loop6_item_ns = __loop6_tmp_item_ns,
                        }).ToArray();
                foreach (var __loop6_item in __loop6_result)
                {
                    var __noname6 = __loop6_item.__loop6_item___noname6;
                    var ns = __loop6_item.__loop6_item_ns;
                    ++__loop6_iteration;
                    Context.SetOutput(ns.FullName + ".xsd");
                    Context.Output(XsdWsdlGenerator.Generated_GenerateXsd(ns));
                    int __loop7_iteration = 0;
                    var __loop7_result =
                        (from __loop7_tmp_item___noname7 in EnumerableExtensions.Enumerate((ns).GetEnumerator())
                        from __loop7_tmp_item_Declarations in EnumerableExtensions.Enumerate((__loop7_tmp_item___noname7.Declarations).GetEnumerator())
                        from __loop7_tmp_item_endp in EnumerableExtensions.Enumerate((__loop7_tmp_item_Declarations).GetEnumerator()).OfType<Endpoint>()
                        select
                            new
                            {
                                __loop7_item___noname7 = __loop7_tmp_item___noname7,
                                __loop7_item_Declarations = __loop7_tmp_item_Declarations,
                                __loop7_item_endp = __loop7_tmp_item_endp,
                            }).ToArray();
                    foreach (var __loop7_item in __loop7_result)
                    {
                        var __noname7 = __loop7_item.__loop7_item___noname7;
                        var Declarations = __loop7_item.__loop7_item_Declarations;
                        var endp = __loop7_item.__loop7_item_endp;
                        ++__loop7_iteration;
                        Context.SetOutput(endp.Name + ".wsdl");
                        Context.Output(XsdWsdlGenerator.Generated_GenerateSingleWsdl(endp));
                    }
                }
                Context.CreateFolder(Properties.OutputDir + "/Oracle/" + Generated_GetProjectName() + "App/" + Generated_GetClientProjectName() + "/src/META-INF/policies");
                Context.SetOutputFolder(Properties.OutputDir + "/Oracle/" + Generated_GetProjectName() + "App/" + Generated_GetClientProjectName() + "/src/META-INF/policies");
                int __loop8_iteration = 0;
                var __loop8_result =
                    (from __loop8_tmp_item___noname8 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                    from __loop8_tmp_item_ns in EnumerableExtensions.Enumerate((__loop8_tmp_item___noname8).GetEnumerator()).OfType<Namespace>()
                    from __loop8_tmp_item_Declarations in EnumerableExtensions.Enumerate((__loop8_tmp_item_ns.Declarations).GetEnumerator())
                    from __loop8_tmp_item_binding in EnumerableExtensions.Enumerate((__loop8_tmp_item_Declarations).GetEnumerator()).OfType<Binding>()
                    select
                        new
                        {
                            __loop8_item___noname8 = __loop8_tmp_item___noname8,
                            __loop8_item_ns = __loop8_tmp_item_ns,
                            __loop8_item_Declarations = __loop8_tmp_item_Declarations,
                            __loop8_item_binding = __loop8_tmp_item_binding,
                        }).ToArray();
                foreach (var __loop8_item in __loop8_result)
                {
                    var __noname8 = __loop8_item.__loop8_item___noname8;
                    var ns = __loop8_item.__loop8_item_ns;
                    var Declarations = __loop8_item.__loop8_item_Declarations;
                    var binding = __loop8_item.__loop8_item_binding;
                    ++__loop8_iteration;
                    if (binding.HasPolicy())
                    {
                        Context.SetOutput(binding.Name + "_Policy.xml");
                        Context.Output(XsdWsdlGenerator.Generated_GeneratePolicy(ns, binding, true, true));
                    }
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
            
            public List<string> Generated_GenerateApplicationFile()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version = '1.0' encoding = 'UTF-8'?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<jws:workspace xmlns:jws=\"http://xmlns.oracle.com/ide/project\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   <value n=\"appTemplateId\" v=\"#genericApplicationTemplate\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   <hash n=\"component-versions\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.adf.share.dt.migration.jps.JaznCredStoreMigratorHelper\" v=\"11.1.1.1.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.adf.share.dt.migration.wsm.PolicyAttachmentMigratorHelper\" v=\"12.1.2.0.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.adfdtinternal.model.ide.security.extension.AdfSecurityMigrator\" v=\"11.1.1.1.0.13\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.ide.model.Project\" v=\"11.1.1.1.0;12.1.2.0.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jbo.dt.jdevx.deployment.JbdProjectMigrator\" v=\"11.1.2.0.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jdevimpl.appresources.ApplicationSrcDirMigrator\" v=\"11.1.2.0.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jdevimpl.deploy.migrators.JeeDeploymentMigrator\" v=\"12.1.2.0.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jdevimpl.xml.wl.WeblogicMigratorHelper\" v=\"11.1.1.1.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.mds.internal.dt.deploy.base.MarMigratorHelper\" v=\"11.1.1.1.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.mds.internal.dt.ide.migrator.MDSConfigMigratorHelper\" v=\"11.1.1.0.5313\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   <list n=\"contentSets\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <string v=\"oracle.mds.internal.dt.ide.appresources.MDSAppResourceCSProvider/MDSAppContentSet\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <string v=\"oracle.jdeveloper.model.PathsConfiguration/ADFContentSet\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <string v=\"oracle.jdeveloper.model.PathsConfiguration/ApplicationSrcContentSet\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <string v=\"oracle.jdeveloper.model.PathsConfiguration/ApplicationBuildToolContentSet\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <string v=\"oracle.jdeveloper.model.PathsConfiguration/ResourceBundlesContentSet\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   </list>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   <list n=\"listOfChildren\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <hash><url n=\"URL\" path=\"");
                    __printer.Write(Generated_GetProjectName());
                    __printer.WriteTemplateOutput("/");
                    __printer.Write(Generated_GetProjectName());
                    __printer.WriteTemplateOutput(".jpr\"/></hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <hash><url n=\"URL\" path=\"");
                    __printer.Write(Generated_GetClientProjectName());
                    __printer.WriteTemplateOutput("/");
                    __printer.Write(Generated_GetClientProjectName());
                    __printer.WriteTemplateOutput(".jpr\"/></hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   </list>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</jws:workspace>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateProjectFile()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version = '1.0' encoding = 'UTF-8'?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<jpr:project xmlns:jpr=\"http://xmlns.oracle.com/ide/project\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   <hash n=\"component-versions\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.adfdt.controller.migrate.TrinidadDatabindingsProjectMigrator\" v=\"11.1.2.0.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.adfdtinternal.dvt.datapresdt.migration.DVTDataMapMigrator\" v=\"11.1.1.1.0.3\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.adfdtinternal.model.ide.security.wizard.FormPageMigrator\" v=\"11.1.1.0.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.adfdtinternal.model.ide.security.wizard.JpsFilterMigrator\" v=\"11.1.1.1.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.adfdtinternal.view.common.migration.wizards.MigrationHelper\" v=\"11.1.1.1.0.3\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.adfdtinternal.view.rich.migration.ComponentIdNodeMigratorHelper\" v=\"11.1.1.1.0.01\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.adfdtinternal.view.rich.migration.FacesLibraryVersionMigrator\" v=\"11.1.1.1.0.1\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.ide.model.Project\" v=\"12.1.2.0.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.ide.model.ResourcePathsMigrator\" v=\"11.1.1.1.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.ideimpl.model.TechnologyScopeUpdateMigrator\" v=\"11.1.2.0.0.4\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jbo.dt.jdevx.deployment.JbdProjectMigrator\" v=\"11.1.2.0.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jbo.dt.jdevx.ui.appnav.APProjectMigrator\" v=\"11.1.1.0.1.5\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jbo.dt.migrate.ResourceBundlePathMigrator\" v=\"11.1.1.0.1.5\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jbo.dt.migration.ServiceInterfaceMigrator\" v=\"11.1.1.1.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jdeveloper.dbmodeler.Migration\" v=\"12.1.1.0.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jdeveloper.library.ProjectLibraryMigrator\" v=\"11.1.1.1.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jdeveloper.model.OutputDirectoryMigrator\" v=\"11.1.1.1.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jdevimpl.deploy.migrators.DeploymentMigrator\" v=\"12.1.2.0.1\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jdevimpl.jsp.JspMigrator\" v=\"11.1.1\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jdevimpl.offlinedb.migration.OfflineDBProjectMigrator\" v=\"12.1.1.0.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jdevimpl.webapp.jsp.libraries.JspLibraryMigrator\" v=\"11.1.1.1.4\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jdevimpl.webapp.jsp.taglibraries.trinidad.migration.TrinidadLibraryVersionMigrator\" v=\"11.1.1.1.0.1\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jdevimpl.webapp.WebAppContentSetNodeMigratorHelper\" v=\"11.1.1\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jdevimpl.webapp.WebAppProjectNodeMigratorHelper\" v=\"12.1.2.0.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jdevimpl.webservices.rest.migration.RestLibraryMigrator\" v=\"12.1.1.0.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jdevimpl.webservices.rest.migration.RestPathMigrator\" v=\"11.1.2.0.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jdevimpl.xml.wl.WeblogicMigratorHelper\" v=\"11.1.1.1.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.modeler.bmmigrate.management.Migration\" v=\"11.1.1.1.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.toplink.workbench.addin.migration.PersistenceProjectMigrator\" v=\"11.1.1.1.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.toplink.workbench.addin.migration.TopLinkProjectMigrator\" v=\"11.1.1.1.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   <list n=\"contentSets\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <string v=\"oracle.bm.commonIde.data.project.ModelerProjectSettings/modelersContentSet\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <string v=\"oracle.jdeveloper.model.J2eeSettings/webContentSet\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <string v=\"oracle.mds.internal.dt.ide.MDSLibraryCustCSProvider/mdsContentSet\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <string v=\"oracle.mds.internal.dt.ide.MDSADFLibVirtualNodeCSProvider/mdsLibVirtualNodeContentSet\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <string v=\"oracle.jdeveloper.model.PathsConfiguration/javaContentSet\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <string v=\"oracle.ide.model.ResourcePaths/resourcesContentSet\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <string v=\"oracle.jdeveloper.offlinedb.model.OfflineDBProjectSettings/offlineDBContentSet\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <string v=\"oracle.adfdtinternal.model.ide.settings.ADFMSettings/adfmContentSet\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <string v=\"oracle.toplink.workbench.addin/toplinkContentSet\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   </list>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   <value n=\"defaultPackage\" v=\"\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   <hash n=\"oracle.ide.model.TechnologyScopeConfiguration\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <list n=\"technologyScope\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <string v=\"Java\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <string v=\"WSDL\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <string v=\"WSPolicy\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <string v=\"WebServices\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <string v=\"XML\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <string v=\"XML_SCHEMA\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      </list>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   <hash n=\"oracle.jdeveloper.compiler.OjcConfiguration\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"internalEncoding\" v=\"Cp1252\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"webIANAEncoding\" v=\"windows-1252\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   <hash n=\"oracle.jdeveloper.deploy.dt.DeploymentProfiles\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <hash n=\"profileDefinitions\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <hash n=\"WebServices\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <hash n=\"appletArchives\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <hash n=\"appletFiles\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("               <value n=\"autoInclude\" v=\"true\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("               <list n=\"selectionFilters\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                  <string v=\"oracle.jdevimpl.deploy.common.JavaSelectionFilter\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("               </list>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <hash n=\"archiveOptions\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("               <value n=\"hasManifest\" v=\"false\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <url n=\"earURL\" path=\"deploy/");
                    __printer.Write(Generated_GetProjectName());
                    __printer.WriteTemplateOutput(".ear\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"enterpriseAppName\" v=\"");
                    __printer.Write(Generated_GetProjectName());
                    __printer.WriteTemplateOutput("\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <hash n=\"fileGroups\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("               <list n=\"groups\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                  <hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                     <list n=\"contributors\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                        <hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                           <value n=\"type\" v=\"5\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                        </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                     </list>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                     <value n=\"displayName\" v=\"Web Files\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                     <hash n=\"filters\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                        <list n=\"rules\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                           <hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                              <value n=\"pattern\" v=\"**\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                              <value n=\"type\" v=\"0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                           </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                        </list>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                     </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                     <value n=\"internalName\" v=\"web-files\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                     <value n=\"type\" v=\"1\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                  </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                  <hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                     <list n=\"contributors\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                        <hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                           <value n=\"type\" v=\"2\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                        </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                        <hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                           <value n=\"type\" v=\"7\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                        </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                     </list>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                     <value n=\"displayName\" v=\"WEB-INF/classes\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                     <hash n=\"filters\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                        <list n=\"rules\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                           <hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                              <value n=\"pattern\" v=\"**\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                              <value n=\"type\" v=\"0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                           </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                        </list>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                     </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                     <value n=\"internalName\" v=\"project-output\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                     <value n=\"targetWithinJar\" v=\"WEB-INF/classes\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                     <value n=\"type\" v=\"1\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                  </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                  <hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                     <value n=\"displayName\" v=\"WEB-INF/lib\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                     <hash n=\"filters\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                        <list n=\"rules\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                           <hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                              <value n=\"pattern\" v=\"**\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                              <value n=\"type\" v=\"0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                           </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                        </list>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                     </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                     <value n=\"internalName\" v=\"libraries\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                     <value n=\"targetWithinJar\" v=\"WEB-INF/lib\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                     <value n=\"type\" v=\"3\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("                  </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("               </list>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <url n=\"jarURL\" path=\"deploy/");
                    __printer.Write(Generated_GetProjectName());
                    __printer.WriteTemplateOutput(".war\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"platformType\" v=\"WEBLOGIC\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"platformVersion\" v=\"12.0.0.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"profileClass\" v=\"oracle.jdeveloper.deploy.war.WarProfile\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"profileName\" v=\"WebServices\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <list n=\"profileList\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <string v=\"WebServices\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      </list>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   <hash n=\"oracle.jdevimpl.config.JProjectLibraries\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <list n=\"exportedReferences\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"id\" v=\"JAX-WS Web Services\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"isJDK\" v=\"false\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      </list>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <list n=\"libraryReferences\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"id\" v=\"JAX-WS Web Services\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"isJDK\" v=\"false\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      </list>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   <hash n=\"oracle.jdevimpl.config.JProjectPaths\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <url n=\"outputDirectory\" path=\"classes/\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</jpr:project>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateClientProjectFile()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version = '1.0' encoding = 'UTF-8'?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<jpr:project xmlns:jpr=\"http://xmlns.oracle.com/ide/project\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   <hash n=\"component-versions\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.adfdt.controller.migrate.TrinidadDatabindingsProjectMigrator\" v=\"11.1.2.0.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.adfdtinternal.dvt.datapresdt.migration.DVTDataMapMigrator\" v=\"11.1.1.1.0.3\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.adfdtinternal.model.ide.security.wizard.FormPageMigrator\" v=\"11.1.1.0.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.adfdtinternal.model.ide.security.wizard.JpsFilterMigrator\" v=\"11.1.1.1.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.adfdtinternal.view.common.migration.wizards.MigrationHelper\" v=\"11.1.1.1.0.3\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.adfdtinternal.view.rich.migration.ComponentIdNodeMigratorHelper\" v=\"11.1.1.1.0.01\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.adfdtinternal.view.rich.migration.FacesLibraryVersionMigrator\" v=\"11.1.1.1.0.1\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.ide.model.Project\" v=\"12.1.2.0.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.ide.model.ResourcePathsMigrator\" v=\"11.1.1.1.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.ideimpl.model.TechnologyScopeUpdateMigrator\" v=\"11.1.2.0.0.4\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jbo.dt.jdevx.deployment.JbdProjectMigrator\" v=\"11.1.2.0.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jbo.dt.jdevx.ui.appnav.APProjectMigrator\" v=\"11.1.1.0.1.5\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jbo.dt.migrate.ResourceBundlePathMigrator\" v=\"11.1.1.0.1.5\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jbo.dt.migration.ServiceInterfaceMigrator\" v=\"11.1.1.1.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jdeveloper.dbmodeler.Migration\" v=\"12.1.1.0.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jdeveloper.ejb.EjbMigrator\" v=\"11.1.1.1.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jdeveloper.library.ProjectLibraryMigrator\" v=\"11.1.1.1.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jdeveloper.model.OutputDirectoryMigrator\" v=\"11.1.1.1.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jdevimpl.deploy.jps.JpsDataMigrator\" v=\"11.1.1.1.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jdevimpl.deploy.migrators.DeploymentMigrator\" v=\"12.1.2.0.1\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jdevimpl.jsp.JspMigrator\" v=\"11.1.1\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jdevimpl.offlinedb.migration.OfflineDBProjectMigrator\" v=\"12.1.1.0.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jdevimpl.resourcebundle.XliffAddin$XliffMigratorHelper\" v=\"11.1.1.1.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jdevimpl.webapp.jsp.libraries.JspLibraryMigrator\" v=\"11.1.1.1.4\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jdevimpl.webapp.jsp.taglibraries.trinidad.migration.TrinidadLibraryVersionMigrator\" v=\"11.1.1.1.0.1\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jdevimpl.webapp.WebAppContentSetNodeMigratorHelper\" v=\"11.1.1\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jdevimpl.webapp.WebAppProjectNodeMigratorHelper\" v=\"12.1.2.0.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jdevimpl.webservices.rest.migration.RestLibraryMigrator\" v=\"12.1.1.0.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jdevimpl.webservices.rest.migration.RestPathMigrator\" v=\"11.1.2.0.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jdevimpl.webservices.WebServicesMigratorHelper\" v=\"11.1.1.1.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.jdevimpl.xml.wl.WeblogicMigratorHelper\" v=\"11.1.1.1.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.modeler.bmmigrate.management.Migration\" v=\"11.1.1.1.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.toplink.workbench.addin.migration.PersistenceProjectMigrator\" v=\"11.1.1.1.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"oracle.toplink.workbench.addin.migration.TopLinkProjectMigrator\" v=\"11.1.1.1.0\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   <list n=\"contentSets\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <string v=\"oracle.bm.commonIde.data.project.ModelerProjectSettings/modelersContentSet\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <string v=\"oracle.jdeveloper.model.J2eeSettings/webContentSet\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <string v=\"oracle.mds.internal.dt.ide.MDSLibraryCustCSProvider/mdsContentSet\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <string v=\"oracle.mds.internal.dt.ide.MDSADFLibVirtualNodeCSProvider/mdsLibVirtualNodeContentSet\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <string v=\"oracle.jdeveloper.model.PathsConfiguration/javaContentSet\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <string v=\"oracle.ide.model.ResourcePaths/resourcesContentSet\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <string v=\"oracle.jdeveloper.offlinedb.model.OfflineDBProjectSettings/offlineDBContentSet\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <string v=\"oracle.adfdtinternal.model.ide.settings.ADFMSettings/adfmContentSet\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <string v=\"oracle.toplink.workbench.addin/toplinkContentSet\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   </list>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   <value n=\"defaultPackage\" v=\"\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   <hash n=\"oracle.ide.model.TechnologyScopeConfiguration\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <list n=\"technologyScope\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <string v=\"Database\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <string v=\"Java\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <string v=\"TopLink\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <string v=\"WSDL\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <string v=\"WSPolicy\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <string v=\"WebServices\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <string v=\"XML\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      </list>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   <hash n=\"oracle.jdeveloper.compiler.OjcConfiguration\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"internalEncoding\" v=\"Cp1252\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"webIANAEncoding\" v=\"windows-1252\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   <hash n=\"oracle.jdeveloper.model.J2eeSettings\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"j2eeWebAppName\" v=\"");
                    __printer.Write(Generated_GetProjectName());
                    __printer.WriteTemplateOutput("App-");
                    __printer.Write(Generated_GetClientProjectName());
                    __printer.WriteTemplateOutput("-webapp\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"j2eeWebContextRoot\" v=\"");
                    __printer.Write(Generated_GetProjectName());
                    __printer.WriteTemplateOutput("App-");
                    __printer.Write(Generated_GetClientProjectName());
                    __printer.WriteTemplateOutput("-context-root\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   <hash n=\"oracle.jdevimpl.config.JProjectLibraries\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <list n=\"exportedReferences\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"id\" v=\"JAX-WS Client\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"isJDK\" v=\"false\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"id\" v=\"JAX-WS Web Services\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"isJDK\" v=\"false\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"id\" v=\"TopLink\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"isJDK\" v=\"false\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      </list>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <list n=\"libraryReferences\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"id\" v=\"JAX-WS Client\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"isJDK\" v=\"false\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"id\" v=\"JAX-WS Web Services\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"isJDK\" v=\"false\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"id\" v=\"TopLink\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"isJDK\" v=\"false\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      </list>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   <hash n=\"oracle.jdevimpl.config.JProjectPaths\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <url n=\"outputDirectory\" path=\"classes/\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</jpr:project>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateJaxRpc(Endpoint endp)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version = '1.0' encoding = 'UTF-8'?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<jdevws:WebService xmlns:jdevws=\"http://xmlns.oracle.com/jdeveloper/10130/webservice\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   <list n=\"CHILD_NODES\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <url path=\"");
                    __printer.Write(endp.Namespace.GetRootDirForJava());
                    __printer.WriteTemplateOutput("/../public_html/WEB-INF/wsdl/");
                    __printer.Write(endp.Name);
                    __printer.WriteTemplateOutput(".wsdl\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <url path=\"");
                    __printer.Write(endp.Name);
                    __printer.WriteTemplateOutput(".java\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <url path=\"");
                    __printer.Write(endp.Interface.Name);
                    __printer.WriteTemplateOutput(".java\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   </list>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   <hash n=\"DATA_MODEL\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"ANNOTATE_ADDRESSING\" v=\"true\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"ATTACHMENT_ENCODING\" v=\"MIME\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <list n=\"BD_\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"BOUND_PORT_TYPE\" v=\"{");
                    __printer.Write(Generated_GetUri(endp.Namespace));
                    __printer.WriteTemplateOutput("}");
                    __printer.Write(endp.Interface.Name);
                    __printer.WriteTemplateOutput("\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"DEFAULT_NAME\" v=\"false\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"MODEL_CLASS\" v=\"oracle.jdeveloper.webservices.model.SOAPBinding\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"NAME\" v=\"{");
                    __printer.Write(Generated_GetUri(endp.Namespace));
                    __printer.WriteTemplateOutput("}");
                    __printer.Write(endp.Interface.Name);
                    __printer.WriteTemplateOutput("_");
                    __printer.Write(endp.Binding.Name);
                    __printer.WriteTemplateOutput("_Binding\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <hash n=\"PROPERTY_GROUPS\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      </list>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <list n=\"BINDING_FILES\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"GENERATE_TOP_DOWN\" v=\"TRUE\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"GENERATION_OPTION\" v=\"ANNOTATIONS_WSDL_SEI\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"LAST_GENERATION_BOTTOM_UP\" v=\"false\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"MODEL_CLASS\" v=\"oracle.jdeveloper.webservices.model.java.JavaWebService\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <list n=\"PO_\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"DEFAULT_NAME\" v=\"false\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"MODEL_CLASS\" v=\"oracle.jdeveloper.webservices.model.SOAPPort\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"NAME\" v=\"");
                    __printer.Write(endp.Interface.Name);
                    __printer.WriteTemplateOutput("_");
                    __printer.Write(endp.Binding.Name);
                    __printer.WriteTemplateOutput("_Port\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"PORT_BINDING_NAME\" v=\"{");
                    __printer.Write(Generated_GetUri(endp.Namespace));
                    __printer.WriteTemplateOutput("}");
                    __printer.Write(endp.Interface.Name);
                    __printer.WriteTemplateOutput("_");
                    __printer.Write(endp.Binding.Name);
                    __printer.WriteTemplateOutput("_Binding\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			");
                    if (endp.Binding.HasPolicy())
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("            <hash n=\"PROPERTY_GROUPS\">");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("               <hash n=\"WLS_POLICY_MODEL\">");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                  <value n=\"MODEL_CLASS\" v=\"oracle.jdeveloper.webservices.model.policy.WlsPolicyModel\"/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                  <list n=\"POLICY_LIST\">");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                     <string v=\"policy:");
                        __printer.Write(endp.Binding.Name);
                        __printer.WriteTemplateOutput("_Policy.xml\"/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                  </list>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                  <hash n=\"policy:");
                        __printer.Write(endp.Binding.Name);
                        __printer.WriteTemplateOutput("_Policy.xml\">");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                     <value n=\"ATTACH_TO_WSDL\" v=\"false\"/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                     <value n=\"CUSTOM_POLICY\" v=\"false\"/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                     <value n=\"DIRECTION\" v=\"Both\"/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                  </hash>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("               </hash>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("            </hash>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("			");
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      </list>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <hash n=\"PROPERTY_GROUPS\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <hash n=\"WSDL_TO_JAVA_PROPERTIES\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"MODEL_CLASS\" v=\"oracle.jdeveloper.webservices.model.java.WSDLToJavaProperties\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"WSDL2JAVA_DEFAULT_PACKAGE\" v=\"");
                    __printer.Write(Generated_GetPackage(endp.Namespace).ToLower());
                    __printer.WriteTemplateOutput("\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"WSDL2JAVA_ROOT_VALUE_PACKAGE\" v=\"");
                    __printer.Write(Generated_GetPackage(endp.Namespace).ToLower());
                    __printer.WriteTemplateOutput("\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <list n=\"PT_\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"AUTO_SERVICE_INTERFACE\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"DEFAULT_NAME\" v=\"false\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"MODEL_CLASS\" v=\"oracle.jdeveloper.webservices.model.java.JavaPortType\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"NAME\" v=\"{");
                    __printer.Write(Generated_GetUri(endp.Namespace));
                    __printer.WriteTemplateOutput("}");
                    __printer.Write(endp.Interface.Name);
                    __printer.WriteTemplateOutput("\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <hash n=\"PROPERTY_GROUPS\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"PT_CLASS\" v=\"");
                    __printer.Write(Generated_GetPackage(endp.Namespace).ToLower());
                    __printer.WriteTemplateOutput(".");
                    __printer.Write(endp.Name);
                    __printer.WriteTemplateOutput("\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"PT_SERVICE_INTERFACE\" v=\"");
                    __printer.Write(Generated_GetPackage(endp.Namespace).ToLower());
                    __printer.WriteTemplateOutput(".");
                    __printer.Write(endp.Interface.Name);
                    __printer.WriteTemplateOutput("\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"PT_TOP_DOWN_GENERATED_SEI_NAME\" v=\"");
                    __printer.Write(Generated_GetPackage(endp.Namespace).ToLower());
                    __printer.WriteTemplateOutput(".");
                    __printer.Write(endp.Interface.Name);
                    __printer.WriteTemplateOutput("\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      </list>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"SERVICE_ASYNC\" v=\"false\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <url n=\"TARGET_PROJECT\" path=\"../../");
                    __printer.Write(Generated_GetProjectName());
                    __printer.WriteTemplateOutput(".jpr\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"TOPDOWN_GENERATION_OPTION\" v=\"JAVA_SEI\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <list n=\"WS_DEPENDENT_VALUE_TYPES\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"WS_ORIG_GEN_DIRECTION\" v=\"BOTTOM_UP\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <url n=\"WS_REFERENCE_WSDL\" path=\"../../public_html/WEB-INF/wsdl/");
                    __printer.Write(endp.Name);
                    __printer.WriteTemplateOutput(".wsdl\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"WS_TARGET_NAMESPACE\" v=\"");
                    __printer.Write(Generated_GetUri(endp.Namespace));
                    __printer.WriteTemplateOutput("\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"WS_URI\" v=\"");
                    __printer.Write(endp.Name);
                    __printer.WriteTemplateOutput("\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <url n=\"WS_WSDL_FILE\" path=\"../../public_html/WEB-INF/wsdl/");
                    __printer.Write(endp.Name);
                    __printer.WriteTemplateOutput(".wsdl\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"WS_WSDL_LOC_ON_SEI\" v=\"false\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"WS_WSDL_LOC_VALUE\" v=\"WEB-INF/wsdl/");
                    __printer.Write(endp.Name);
                    __printer.WriteTemplateOutput(".wsdl\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</jdevws:WebService>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateJaxRpcProxy(Endpoint endp)
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version = '1.0' encoding = 'UTF-8'?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<jdevws:WebServiceProxy xmlns:jdevws=\"http://xmlns.oracle.com/jdeveloper/10130/webserviceproxy\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   <list n=\"CHILD_NODES\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <url path=\"");
                    __printer.Write(endp.Namespace.GetRootDirForJava());
                    __printer.WriteTemplateOutput("/META-INF/wsdl/");
                    __printer.Write(endp.Name);
                    __printer.WriteTemplateOutput(".wsdl\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <url path=\"");
                    __printer.Write(endp.Name);
                    __printer.WriteTemplateOutput("Service.java\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <url path=\"");
                    __printer.Write(endp.Interface.Name);
                    __printer.WriteTemplateOutput(".java\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   </list>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   <hash n=\"DATA_MODEL\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"ATTACHMENT_ENCODING\" v=\"MIME\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <list n=\"LOCAL_FILES\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"MODEL_CLASS\" v=\"oracle.jdeveloper.webservices.model.proxy.WebServiceProxy\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <hash n=\"PROPERTY_GROUPS\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <hash n=\"WSDL_TO_JAVA_PROPERTIES\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"MODEL_CLASS\" v=\"oracle.jdeveloper.webservices.model.java.WSDLToJavaProperties\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"WSDL2JAVA_DEFAULT_PACKAGE\" v=\"");
                    __printer.Write(Generated_GetPackage(endp.Namespace).ToLower());
                    __printer.WriteTemplateOutput("\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"WSDL2JAVA_ROOT_VALUE_PACKAGE\" v=\"");
                    __printer.Write(Generated_GetPackage(endp.Namespace).ToLower());
                    __printer.WriteTemplateOutput("\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"PROXY_ASYNC_METHODS\" v=\"FOR_ENABLED\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <list n=\"PROXY_CLIENTS\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <url path=\"");
                    __printer.Write(endp.Name);
                    __printer.WriteTemplateOutput("Service.java\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      </list>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"PROXY_MAPPING_URL\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <url n=\"PROXY_ORIGINAL_WSDL_URL\" path=\"");
                    __printer.Write(endp.Namespace.GetRootDirForJava());
                    __printer.WriteTemplateOutput("/META-INF/wsdl/");
                    __printer.Write(endp.Name);
                    __printer.WriteTemplateOutput(".wsdl\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <hash n=\"PROXY_PORTS\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <hash n=\"");
                    __printer.Write(endp.Interface.Name);
                    __printer.WriteTemplateOutput("_");
                    __printer.Write(endp.Binding.Name);
                    __printer.WriteTemplateOutput("_Port\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"ACCESSES_DOC_WRAPPPED\" v=\"true\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"ACCESSES_RPC_ENCODED\" v=\"false\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"CLASS_NAME\" v=\"");
                    __printer.Write(Generated_GetPackage(endp.Namespace).ToLower());
                    __printer.WriteTemplateOutput(".");
                    __printer.Write(endp.Interface.Name);
                    __printer.WriteTemplateOutput("\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"MODEL_CLASS\" v=\"oracle.jdeveloper.webservices.model.proxy.ProxyPort\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <value n=\"NAME\" v=\"");
                    __printer.Write(endp.Interface.Name);
                    __printer.WriteTemplateOutput("_");
                    __printer.Write(endp.Binding.Name);
                    __printer.WriteTemplateOutput("_Port\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("			");
                    if (endp.Binding.HasPolicy())
                    {
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("            <hash n=\"PROPERTY_GROUPS\">");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("               <hash n=\"WLS_POLICY_MODEL\">");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                  <value n=\"MODEL_CLASS\" v=\"oracle.jdeveloper.webservices.model.policy.WlsPolicyModel\"/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                  <list n=\"POLICY_LIST\">");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                     <string v=\"policy:");
                        __printer.Write(endp.Binding.Name);
                        __printer.WriteTemplateOutput("_Policy.xml\"/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                  </list>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                  <hash n=\"policy:");
                        __printer.Write(endp.Binding.Name);
                        __printer.WriteTemplateOutput("_Policy.xml\">");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                     <value n=\"ATTACH_TO_WSDL\" v=\"false\"/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                     <value n=\"CUSTOM_POLICY\" v=\"false\"/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                     <value n=\"DIRECTION\" v=\"Both\"/>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("                  </hash>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("               </hash>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("            </hash>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("			");
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <url n=\"PROXY_ORIGINAL_PORT_ENDPOINT\" protocol=\"http\" host=\"");
                    __printer.Write(endp.Address.Uri);
                    __printer.WriteTemplateOutput("\" path=\"\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <url n=\"PROXY_PORT_ENDPOINT\" protocol=\"http\" host=\"");
                    __printer.Write(endp.Address.Uri);
                    __printer.WriteTemplateOutput("\" path=\"\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"PROXY_SERVICE_NAME\" v=\"");
                    __printer.Write(endp.Name);
                    __printer.WriteTemplateOutput("\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"PROXY_SERVICE_NAMESPACE\" v=\"");
                    __printer.Write(Generated_GetUri(endp.Namespace));
                    __printer.WriteTemplateOutput("\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <url n=\"PROXY_TARGET_PROJECT\" path=\"");
                    __printer.Write(endp.Namespace.GetRootDirForJava());
                    __printer.WriteTemplateOutput("/../");
                    __printer.Write(Generated_GetClientProjectName());
                    __printer.WriteTemplateOutput(".jpr\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <url n=\"PROXY_WSDL_URL\" path=\"");
                    __printer.Write(endp.Namespace.GetRootDirForJava());
                    __printer.WriteTemplateOutput("/META-INF/wsdl/");
                    __printer.Write(endp.Name);
                    __printer.WriteTemplateOutput(".wsdl\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <hash n=\"REFERENCED_WEBSERVICE\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <list n=\"BD_\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("               <value n=\"BOUND_PORT_TYPE\" v=\"{");
                    __printer.Write(Generated_GetUri(endp.Namespace));
                    __printer.WriteTemplateOutput("}");
                    __printer.Write(endp.Interface.Name);
                    __printer.WriteTemplateOutput("\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("               <value n=\"DEFAULT_NAME\" v=\"false\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("               <value n=\"MODEL_CLASS\" v=\"oracle.jdeveloper.webservices.model.SOAPBinding\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("               <value n=\"NAME\" v=\"{");
                    __printer.Write(Generated_GetUri(endp.Namespace));
                    __printer.WriteTemplateOutput("}");
                    __printer.Write(endp.Interface.Name);
                    __printer.WriteTemplateOutput("_");
                    __printer.Write(endp.Binding.Name);
                    __printer.WriteTemplateOutput("_Binding\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("               <hash n=\"PROPERTY_GROUPS\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         </list>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <list n=\"BINDING_FILES\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <value n=\"MODEL_CLASS\" v=\"oracle.jdeveloper.webservices.model.proxy.WebServiceProxy$ProxyJavaWebService\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <list n=\"PO_\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("               <value n=\"DEFAULT_NAME\" v=\"false\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("               <value n=\"MODEL_CLASS\" v=\"oracle.jdeveloper.webservices.model.SOAPPort\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("               <value n=\"NAME\" v=\"");
                    __printer.Write(endp.Interface.Name);
                    __printer.WriteTemplateOutput("_");
                    __printer.Write(endp.Binding.Name);
                    __printer.WriteTemplateOutput("_Port\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("               <value n=\"PORT_BINDING_NAME\" v=\"{");
                    __printer.Write(Generated_GetUri(endp.Namespace));
                    __printer.WriteTemplateOutput("}");
                    __printer.Write(endp.Interface.Name);
                    __printer.WriteTemplateOutput("_");
                    __printer.Write(endp.Binding.Name);
                    __printer.WriteTemplateOutput("_Binding\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("               <hash n=\"PROPERTY_GROUPS\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("               <url n=\"URL\" protocol=\"http\" host=\"\" path=\"\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         </list>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <hash n=\"PROPERTY_GROUPS\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <hash n=\"WSDL_TO_JAVA_PROPERTIES\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("               <value n=\"MODEL_CLASS\" v=\"oracle.jdeveloper.webservices.model.java.WSDLToJavaProperties\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("               <value n=\"WSDL2JAVA_DEFAULT_PACKAGE\" v=\"");
                    __printer.Write(Generated_GetPackage(endp.Namespace).ToLower());
                    __printer.WriteTemplateOutput("\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("               <value n=\"WSDL2JAVA_ROOT_VALUE_PACKAGE\" v=\"");
                    __printer.Write(Generated_GetPackage(endp.Namespace).ToLower());
                    __printer.WriteTemplateOutput("\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <list n=\"PT_\">");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            <hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("               <value n=\"AUTO_SERVICE_INTERFACE\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("               <value n=\"DEFAULT_NAME\" v=\"false\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("               <value n=\"MODEL_CLASS\" v=\"oracle.jdeveloper.webservices.model.java.JavaPortType\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("               <value n=\"NAME\" v=\"{");
                    __printer.Write(Generated_GetUri(endp.Namespace));
                    __printer.WriteTemplateOutput("}");
                    __printer.Write(endp.Interface.Name);
                    __printer.WriteTemplateOutput("\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("               <hash n=\"PROPERTY_GROUPS\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("               <value n=\"PT_CLASS\" v=\"");
                    __printer.Write(Generated_GetPackage(endp.Namespace).ToLower());
                    __printer.WriteTemplateOutput(".");
                    __printer.Write(endp.Name);
                    __printer.WriteTemplateOutput("Service\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("               <value n=\"PT_SERVICE_INTERFACE\" v=\"");
                    __printer.Write(Generated_GetPackage(endp.Namespace).ToLower());
                    __printer.WriteTemplateOutput(".");
                    __printer.Write(endp.Interface.Name);
                    __printer.WriteTemplateOutput("\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("               <value n=\"PT_TOP_DOWN_GENERATED_SEI_NAME\" v=\"");
                    __printer.Write(Generated_GetPackage(endp.Namespace).ToLower());
                    __printer.WriteTemplateOutput(".");
                    __printer.Write(endp.Interface.Name);
                    __printer.WriteTemplateOutput("\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("            </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         </list>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <url n=\"TARGET_PROJECT\" path=\"");
                    __printer.Write(endp.Namespace.GetRootDirForJava());
                    __printer.WriteTemplateOutput("/../");
                    __printer.Write(Generated_GetClientProjectName());
                    __printer.WriteTemplateOutput(".jpr\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <value n=\"WS_ORIG_GEN_DIRECTION\" v=\"TOP_DOWN\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <value n=\"WS_TARGET_NAMESPACE\" v=\"");
                    __printer.Write(Generated_GetUri(endp.Namespace));
                    __printer.WriteTemplateOutput("\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <value n=\"WS_URI\" v=\"");
                    __printer.Write(endp.Name);
                    __printer.WriteTemplateOutput("\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         <url n=\"WS_WSDL_FILE\" path=\"");
                    __printer.Write(endp.Namespace.GetRootDirForJava());
                    __printer.WriteTemplateOutput("/META-INF/wsdl/");
                    __printer.Write(endp.Name);
                    __printer.WriteTemplateOutput(".wsdl\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"SERVICE_ASYNC\" v=\"false\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("      <value n=\"USE_WSDL_LOCATION\" v=\"true\"/>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("   </hash>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</jdevws:WebServiceProxy>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_GenerateWebXml()
            {
                List<string> __result = new List<string>();
                using(TemplatePrinter __printer = new TemplatePrinter(__result))
                {
                    __printer.WriteTemplateOutput("<?xml version = '1.0' encoding = 'windows-1252'?>");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("<web-app xmlns=\"http://java.sun.com/xml/ns/javaee\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         xsi:schemaLocation=\"http://java.sun.com/xml/ns/javaee http://java.sun.com/xml/ns/javaee/web-app_3_0.xsd\"");
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("         version=\"3.0\">");
                    __printer.WriteLine();
                    int __loop9_iteration = 0;
                    var __loop9_result =
                        (from __loop9_tmp_item___noname9 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                        from __loop9_tmp_item_endp in EnumerableExtensions.Enumerate((__loop9_tmp_item___noname9).GetEnumerator()).OfType<Endpoint>()
                        select
                            new
                            {
                                __loop9_item___noname9 = __loop9_tmp_item___noname9,
                                __loop9_item_endp = __loop9_tmp_item_endp,
                            }).ToArray();
                    foreach (var __loop9_item in __loop9_result)
                    {
                        var __noname9 = __loop9_item.__loop9_item___noname9;
                        var endp = __loop9_item.__loop9_item_endp;
                        ++__loop9_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("  <servlet>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    <servlet-name>");
                        __printer.Write(endp.Interface.Name);
                        __printer.WriteTemplateOutput("_");
                        __printer.Write(endp.Name);
                        __printer.WriteTemplateOutput("_Port</servlet-name>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    <servlet-class>");
                        __printer.Write(Generated_GetPackage(endp.Namespace).ToLower());
                        __printer.WriteTemplateOutput(".");
                        __printer.Write(endp.Name);
                        __printer.WriteTemplateOutput("</servlet-class>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    <load-on-startup>1</load-on-startup>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("  </servlet>");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    int __loop10_iteration = 0;
                    var __loop10_result =
                        (from __loop10_tmp_item___noname10 in EnumerableExtensions.Enumerate((Instances).GetEnumerator())
                        from __loop10_tmp_item_endp in EnumerableExtensions.Enumerate((__loop10_tmp_item___noname10).GetEnumerator()).OfType<Endpoint>()
                        select
                            new
                            {
                                __loop10_item___noname10 = __loop10_tmp_item___noname10,
                                __loop10_item_endp = __loop10_tmp_item_endp,
                            }).ToArray();
                    foreach (var __loop10_item in __loop10_result)
                    {
                        var __noname10 = __loop10_item.__loop10_item___noname10;
                        var endp = __loop10_item.__loop10_item_endp;
                        ++__loop10_iteration;
                        __printer.TrimLine();
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("  <servlet-mapping>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    <servlet-name>");
                        __printer.Write(endp.Interface.Name);
                        __printer.WriteTemplateOutput("_");
                        __printer.Write(endp.Name);
                        __printer.WriteTemplateOutput("_Port</servlet-name>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("    <url-pattern>/services/");
                        __printer.Write(endp.Name);
                        __printer.WriteTemplateOutput("</url-pattern>");
                        __printer.WriteLine();
                        __printer.WriteTemplateOutput("  </servlet-mapping>");
                        __printer.WriteLine();
                    }
                    __printer.TrimLine();
                    __printer.WriteLine();
                    __printer.WriteTemplateOutput("</web-app>");
                    __printer.WriteLine();
                }
                return __result;
            }
            
            public List<string> Generated_Generate_weblogic_script()
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
        
