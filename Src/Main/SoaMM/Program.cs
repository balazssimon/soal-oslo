using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OsloExtensions;
using System.IO;
using Microsoft.M;
using System.Reflection;
using System.Dataflow;
using SoaMetaModel;
using Sb.Meta;

namespace SoaMM
{
    public class Program
    {
        private class Options
        {
            public bool GenerateXsdWsdl { get; private set; }
            public bool GenerateNetbeans { get; private set; }
            public bool GenerateJBoss { get; private set; }
            public bool GenerateTomcat { get; private set; }
            public bool GenerateIbm { get; private set; }
            public bool GenerateOracle { get; private set; }
            public bool GenerateVS { get; private set; }
            public bool GenerateNoImplementationDelegates { get; set; }
            public bool GenerateNoUnimplementedException { get; set; }
            public bool GenerateImplementationBase { get; set; }

            public string NetbeansProject { get; private set; }
            public string JBossProject { get; private set; }
            public string TomcatProject { get; private set; }
            public string IbmProject { get; private set; }
            public string OracleProject { get; private set; }
            public string VSProject { get; private set; }


            public string OutputDir { get; set; }
            public string[] InputFiles { get; private set; }

            public Options()
            {
                GenerateXsdWsdl = true;
                GenerateNetbeans = false;
                GenerateJBoss = false;
                GenerateTomcat = false;
                GenerateIbm = false;
                GenerateOracle = false;
                GenerateVS = false;
                GenerateNoImplementationDelegates = false;
                GenerateNoUnimplementedException = false;
                GenerateImplementationBase = false;

                NetbeansProject = null;
                JBossProject = null;
                TomcatProject = null;
                IbmProject = null;
                OracleProject = null;
                VSProject = null;

                OutputDir = "Output";
            }

            public Options(string[] args)
                : this()
            {
                List<string> input = new List<string>();
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i].StartsWith("--"))
                    {
                        switch (args[i])
                        {
                            case "--no-generate":
                                GenerateXsdWsdl = false;
                                GenerateNetbeans = false;
                                GenerateVS = false;
                                break;
                            case "--no-wsdl":
                                GenerateXsdWsdl = false;
                                break;
                            case "--generate-implementation-base":
                                GenerateImplementationBase = true;
                                break;
                            case "--netbeans-project":
                                NetbeansProject = args[++i];
                                GenerateNetbeans = true;
                                break;
                            case "--jboss-project":
                                JBossProject = args[++i];
                                GenerateJBoss = true;
                                break;
                            case "--tomcat-project":
                                TomcatProject = args[++i];
                                GenerateTomcat = true;
                                break;
                            case "--rad-project":
                                IbmProject = args[++i];
                                GenerateIbm = true;
                                break;
                            case "--oracle-project":
                                OracleProject = args[++i];
                                GenerateOracle = true;
                                break;
                            case "--visualstudio-project":
                                VSProject = args[++i];
                                GenerateVS = true;
                                break;
                            case "--output":
                                OutputDir = args[++i];
                                break;
                            case "--no-implementation-delegates":
                                GenerateNoImplementationDelegates = true;
                                break;
                            case "--no-unimplemented-exception":
                                GenerateNoUnimplementedException = true;
                                break;
                            default:
                                throw new Exception("Invalid option: " + args[i]);
                        }
                    }
                    else
                    {
                        input.Add(args[i]);
                    }
                }
                if (input.Count > 0)
                {
                    InputFiles = input.ToArray();
                }
            }
        }

        private static void Parse(SoaModel model, string input, ErrorReporter er)
        {
            SoaLanguageContext lc = new SoaLanguageContext(new FileInfo(input).Name);

            dynamic root = SoaLanguage.Load().Parse(new StreamReader(input), er);

            new DeclarationParser(er, lc).Parse(root);
            new MemberParser(er, lc).Parse(root);
            new ExpressionParser(er, lc).Parse(root);

            new NameValidator(er, lc).Validate(model);
            new BindingValidator(er, lc).Validate(model);
            new ExpressionValidator(er, lc).Validate(model);
        }

        private static void Parse(SoaModel model, Options options, ErrorReporter er)
        {
            foreach (string input in options.InputFiles)
            {
                Parse(model, input, er);
            }
        }

        private static void Generate(SoaModel model, Options options)
        {
            options.OutputDir = Path.GetFullPath(options.OutputDir);
            IEnumerable<SoaObject> visible = model.Instances.Where(obj => !obj.HasMetaInfo<SoaMetaModel.MetaInfo.HiddenInfo>());

            if (options.GenerateXsdWsdl)
            {
                XsdWsdlGenerator xsdWsdlGenerator = new XsdWsdlGenerator(visible, new GeneratorContext());
                xsdWsdlGenerator.Properties.OutputDir = options.OutputDir+"/common";
                xsdWsdlGenerator.Execute();
                xsdWsdlGenerator = new XsdWsdlGenerator(visible, new GeneratorContext());
                xsdWsdlGenerator.Properties.OutputDir = options.OutputDir + "/common/single";
                xsdWsdlGenerator.Properties.GenerateSingleWsdl = true;
                xsdWsdlGenerator.Properties.GenerateSeparateXsdWsdlFolder = false;
                xsdWsdlGenerator.Execute();
            }
            if (options.GenerateNetbeans)
            {
                NetbeansGenerator netbeansGenerator = new NetbeansGenerator(visible, new GeneratorContext());
                netbeansGenerator.Properties.OutputDir = options.OutputDir;
                netbeansGenerator.Properties.ProjectName = options.NetbeansProject;
                netbeansGenerator.Properties.NoImplementationDelegates = options.GenerateNoImplementationDelegates;
                netbeansGenerator.Properties.ThrowNotImplementedException = !options.GenerateNoUnimplementedException;
                netbeansGenerator.Properties.GenerateImplementationBase = options.GenerateImplementationBase;
                netbeansGenerator.Execute();
            }
            if (options.GenerateJBoss)
            {
                JBossCxfGenerator jbossGenerator = new JBossCxfGenerator(visible, new GeneratorContext());
                jbossGenerator.Properties.OutputDir = options.OutputDir;
                jbossGenerator.Properties.ProjectName = options.JBossProject;
                jbossGenerator.Properties.NoImplementationDelegates = options.GenerateNoImplementationDelegates;
                jbossGenerator.Properties.ThrowNotImplementedException = !options.GenerateNoUnimplementedException;
                jbossGenerator.Properties.GenerateImplementationBase = options.GenerateImplementationBase;
                jbossGenerator.Execute();
            }
            if (options.GenerateTomcat)
            {
                TomcatCxfGenerator tomcatGenerator = new TomcatCxfGenerator(visible, new GeneratorContext());
                tomcatGenerator.Properties.OutputDir = options.OutputDir;
                tomcatGenerator.Properties.ProjectName = options.TomcatProject;
                tomcatGenerator.Properties.NoImplementationDelegates = options.GenerateNoImplementationDelegates;
                tomcatGenerator.Properties.ThrowNotImplementedException = !options.GenerateNoUnimplementedException;
                tomcatGenerator.Properties.GenerateImplementationBase = options.GenerateImplementationBase;
                tomcatGenerator.Execute();
            }
            if (options.GenerateIbm)
            {
                IbmRadGenerator ibmGenerator = new IbmRadGenerator(visible, new GeneratorContext());
                ibmGenerator.Properties.OutputDir = options.OutputDir;
                ibmGenerator.Properties.ProjectName = options.JBossProject;
                ibmGenerator.Properties.NoImplementationDelegates = options.GenerateNoImplementationDelegates;
                ibmGenerator.Properties.ThrowNotImplementedException = !options.GenerateNoUnimplementedException;
                ibmGenerator.Properties.GenerateImplementationBase = options.GenerateImplementationBase;
                ibmGenerator.Execute();
            }
            if (options.GenerateOracle)
            {
                OracleGenerator oracleGenerator = new OracleGenerator(visible, new GeneratorContext());
                oracleGenerator.Properties.OutputDir = options.OutputDir;
                oracleGenerator.Properties.ProjectName = options.OracleProject;
                oracleGenerator.Properties.NoImplementationDelegates = options.GenerateNoImplementationDelegates;
                oracleGenerator.Properties.ThrowNotImplementedException = !options.GenerateNoUnimplementedException;
                oracleGenerator.Properties.GenerateImplementationBase = options.GenerateImplementationBase;
                oracleGenerator.Execute();
            }
            if (options.GenerateVS)
            {
                VSGenerator vsGenerator = new VSGenerator(visible, new GeneratorContext());
                vsGenerator.Properties.OutputDir = options.OutputDir;
                vsGenerator.Properties.ProjectName = options.VSProject;
                vsGenerator.Properties.NoImplementationDelegates = options.GenerateNoImplementationDelegates;
                vsGenerator.Properties.ThrowNotImplementedException = !options.GenerateNoUnimplementedException;
                vsGenerator.Properties.GenerateImplementationBase = options.GenerateImplementationBase;
                vsGenerator.Execute();
            }
        }

        private static void Refactor(SoaModel model, string input, ErrorReporter er)
        {
            //XsdWsdlRefactor refactor = new XsdWsdlRefactor();
            //refactor.Run();
        }

        private static void Refactor(SoaModel model, Options options, ErrorReporter er)
        {
            foreach (string input in options.InputFiles)
            {
                Refactor(model, input, er);
            }
        }

        public static void Main(string[] args)
        {
            try
            {
                /*
                string soalFile = @"..\..\soal\Hello.soal";
                string projectName = @"HelloWeb";
                //*/
                /*
                string soalFile = @"..\..\soal\WsPerfTest.soal";
                string projectName = @"WsPerfTest";
                //*/
                /*
                string soalFile = @"..\..\soal\WsPerfTestAdv.soal";
                string projectName = @"WsPerfTestAdv";
                //*/
                /*
                string soalFile = @"..\..\soal\WsInteropTest.soal";
                string projectName = @"WsInteropTest";
                //*/
                /*
                string soalFile = @"..\..\soal\Bank.soal";
                string projectName = @"Bank";
                //*/
                /*
                string soalFile = @"..\..\soal\Calculator.soal";
                string projectName = @"Calculator";
                //*/
                /*
                string soalFile = @"..\..\soal\TopDown.soal";
                string projectName = @"TopDown";
                //*/
                /*
                string soalFile = @"..\..\soal\WineShop.soal";
                string projectName = @"WineShop";
                //*/
                /*
                string soalFile = @"..\..\soal\SoiSamples1.soal";
                string projectName = @"SoiSamples1";
                //*/
                /*
                string soalFile = @"..\..\soal\OrderProcess.soal";
                string projectName = @"OrderProcess";
                //*/
                /*
                string soalFile = @"..\..\soal\SoiXmlWsApi.soal";
                string projectName = @"SoiXmlWsApi";
                //*/
                /*
                string soalFile = @"..\..\soal\SeatReservation.soal";
                string projectName = @"SeatReservation";
                //*/
                /*
                string soalFile = @"..\..\soal\BpelCinemaReservation.soal";
                string projectName = @"CinemaReservation";
                //*/
                //string outputDir = @"..\..\soal\Output";
                //args = new string[] { soalFile, "--no-implementation-delegates", /*"--generate-implementation-base",*/ "--output", outputDir, "--netbeans-project", projectName, "--jboss-project", projectName, "--tomcat-project", projectName, "--rad-project", projectName, "--oracle-project", projectName, "--visualstudio-project", projectName };
                Options options = new Options(args);
                if (options.InputFiles == null || options.InputFiles.Length == 0)
                {
                    Console.WriteLine("Usage:");
                    Console.WriteLine("SoaMM.exe input.soal [--output OutputDir] [--visualstudio-project VSProjectName] [--netbeans-project NBProjectName] [--jboss-project JBossProjectName] [--tomcat-project TomcatProjectName] [--rad-project IbmRadProjectName] [--oracle-project OracleProjectName]");
                    return;
                }
                ErrorReporter er = new TextWriterReporter();
                SoaModel model = new SoaModel();
                using (new ModelContextScope<SoaModel>(model))
                {
                    Parse(model, options, er);
//                    Refactor(model, options, er);
                    Generate(model, options);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
