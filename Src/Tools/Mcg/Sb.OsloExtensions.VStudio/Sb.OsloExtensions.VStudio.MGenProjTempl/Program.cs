using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OsloExtensions;

namespace $safeprojectname$
{
	public class Program
	{
        public static void Main(string[] args)
        {
            try 
	        {
                List<object> instances = new List<object>();
                GeneratorContext context = new GeneratorContext();
                Generator generator = new Generator(instances, context);
                generator.Execute();
	        }
	        catch (Exception ex)
	        {
                Console.WriteLine(ex);
	        }
        }
	}
}
