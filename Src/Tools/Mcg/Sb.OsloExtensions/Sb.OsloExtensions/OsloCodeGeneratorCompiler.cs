using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Dataflow;
using System.Reflection;

namespace OsloExtensions
{
    public class OsloCodeGeneratorCompiler<TInstances, TContext>
        where TContext : GeneratorContext
    {
        private string fileName;
        private ErrorReporter errorReporter;
        private OsloCodeGeneratorInfo codeGeneratorInfo;
        private string temporaryCsharpCode;
        private Assembly assembly;

        public OsloCodeGeneratorCompiler(string fileName)
        {
            this.fileName = fileName;
            this.errorReporter = null;
            this.codeGeneratorInfo = null;
            this.temporaryCsharpCode = null;
            this.assembly = null;
        }

        public virtual void Parse(ErrorReporter errorReporter = null)
        {
            if (this.codeGeneratorInfo != null) return;
            if (errorReporter == null)
            {
                this.errorReporter = new OsloErrorReporter();
            }
            else
            {
                this.errorReporter = errorReporter;
            }
            this.codeGeneratorInfo = new OsloCodeGeneratorInfo(this.fileName, new StreamReader(this.fileName), false, this.errorReporter);
        }

        public virtual string GetTemporaryCSharpCode()
        {
            if (this.temporaryCsharpCode != null) return this.temporaryCsharpCode;
            this.Parse();
            StringWriter csharpCodeWriter = new StringWriter();
            using (TemplatePrinter output = new TemplatePrinter(csharpCodeWriter))
            {
                if (codeGeneratorInfo.InstancesType == null) codeGeneratorInfo.InstancesType = typeof(TInstances).FullName;
                if (codeGeneratorInfo.ContextType == null) codeGeneratorInfo.ContextType = typeof(TContext).FullName;
                codeGeneratorInfo.GenerateTemporaryCode(output);
            }
            this.temporaryCsharpCode = csharpCodeWriter.ToString();
            return this.temporaryCsharpCode;
        }

        public virtual Assembly Compile(IEnumerable<string> referencedAssemblies = null)
        {
            if (this.assembly != null) return this.assembly;
            this.GetTemporaryCSharpCode();
            if (this.temporaryCsharpCode == null || this.errorReporter.ErrorCount > 0)
            {
                return null;
            }
            SortedSet<string> referencedAssemblyList = new SortedSet<string>();
            referencedAssemblyList.Add("System.dll");
            referencedAssemblyList.Add("System.Core.dll");
            referencedAssemblyList.Add("System.Data.dll");
            referencedAssemblyList.Add("System.Data.DataSetExtensions.dll");
            referencedAssemblyList.Add("System.Xml.dll");
            referencedAssemblyList.Add("System.Xml.Linq.dll");
            referencedAssemblyList.Add("OsloExtensions.dll");
            if (referencedAssemblies != null)
            {
                foreach (var referencedAssembly in referencedAssemblies)
                {
                    referencedAssemblyList.Add(referencedAssembly);
                }
            }
            foreach (var referencedAssembly in this.codeGeneratorInfo.References)
            {
                referencedAssemblyList.Add(referencedAssembly);
            }
            this.assembly = CSharpCompiler.CompileAssembly(this.temporaryCsharpCode, errorReporter, referencedAssemblyList.ToArray());
            return this.assembly;
        }

        public virtual Generator<TInstances, TContext> CreateGenerator(TInstances instances, TContext context)
        {
            if (this.assembly == null)
            {
                this.Compile();
            }
            if (this.assembly != null)
            {
                Type generatorType = this.assembly.GetType(this.codeGeneratorInfo.NamespaceName + "." + this.codeGeneratorInfo.ClassName);
                if (generatorType != null)
                {
                    object generatorObject = Activator.CreateInstance(generatorType);
                    Generator<TInstances, TContext> generator = generatorObject as Generator<TInstances, TContext>;
                    return generator;
                }
            }
            return null;
        }

        public virtual TGenerator CreateGenerator<TGenerator>(TInstances instances, TContext context)
            where TGenerator : Generator<TInstances, TContext>
        {
            Generator<TInstances, TContext> generator = this.CreateGenerator(instances, context);
            return generator as TGenerator;
        }
    }

    public class OsloCodeGeneratorCompiler<TInstances> : OsloCodeGeneratorCompiler<TInstances, GeneratorContext>
    {
        public OsloCodeGeneratorCompiler(string fileName) : base(fileName)
        {
        }

        public override Generator<TInstances, GeneratorContext> CreateGenerator(TInstances instances, GeneratorContext context = null)
        {
            if (context == null)
            {
                context = new GeneratorContext();
            }
            return base.CreateGenerator(instances, context);
        }

        public override TGenerator CreateGenerator<TGenerator>(TInstances instances, GeneratorContext context = null)
        {
            if (context == null)
            {
                context = new GeneratorContext();
            }
            return base.CreateGenerator<TGenerator>(instances, context);
        }
    }

    public class OsloCodeGeneratorCompiler : OsloCodeGeneratorCompiler<List<object>, GeneratorContext>
    {
        public OsloCodeGeneratorCompiler(string fileName)
            : base(fileName)
        {
        }
    }
}
