using System;
using System.CodeDom.Compiler;
using System.Reflection;
using Microsoft.CSharp;
using System.Collections.Specialized;
using System.Dataflow;

namespace OsloExtensions
{
    public class CompileErrorException : Exception
    {
        public CompileErrorException(CompilerError error) : base(error.ToString()) { }
    }
    
    public class CSharpCompiler
    {
        private CSharpCompiler() { }

        private static CompilerResults DoCompile(string fullSource, params string[] referencedAssemblies)
        {
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CompilerParameters cp = new CompilerParameters();
            cp.GenerateExecutable = false;
            cp.GenerateInMemory = true;
            cp.TreatWarningsAsErrors = false;
            cp.ReferencedAssemblies.AddRange(referencedAssemblies);
            return provider.CompileAssemblyFromSource(cp, fullSource);
        }

        public static Assembly CompileAssembly(string fullSource, params string[] referencedAssemblies)
        {
            CompilerResults cr = DoCompile(fullSource, referencedAssemblies);
            if (cr.Errors.HasErrors)
                throw new CompileErrorException(cr.Errors[0]);  //throw with the first error in the list
            return cr.CompiledAssembly;
        }

        public static Assembly CompileAssembly(string fullSource, ErrorReporter errorReporter, params string[] referencedAssemblies)
        {
            CompilerResults cr = DoCompile(fullSource, referencedAssemblies);
            foreach (CompilerError item in cr.Errors)
            {
                if (item.IsWarning)
                {
                    errorReporter.Warning(item.ToString());
                }
                else 
                {
                    errorReporter.Error(item.ToString());
                }
            }
            return cr.CompiledAssembly;
        }

    }
}
