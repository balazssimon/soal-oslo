using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OsloExtensions;
using System.Collections;
using System.Dataflow;

namespace OsloExtensionsTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                OsloCodeGeneratorCompiler<ArrayList> compiler = new OsloCodeGeneratorCompiler<ArrayList>(@"..\..\test.mcg");
                OsloErrorReporter oer = new OsloErrorReporter();
                compiler.Parse(oer);
                Console.WriteLine(compiler.GetTemporaryCSharpCode());
                Generator<ArrayList, GeneratorContext> generator = compiler.CreateGenerator(new ArrayList());
                Console.WriteLine("There are {0} errors.", oer.Errors.Count);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
