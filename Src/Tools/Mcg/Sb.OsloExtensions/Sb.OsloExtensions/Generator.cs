using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Dynamic;
using System.Collections;

namespace OsloExtensions
{
    public abstract class Generator<TInstances, TContext>
        where TContext : GeneratorContext
    {
        public TContext Context { get; private set; }
        public TInstances Instances { get; private set; }

        public Generator(TInstances instances, TContext context)
        {
            this.Instances = instances;
            this.Context = context;
        }

        public void Execute()
        {
            this.Context.BeginCodeGeneration(this);
            this.Generated_Main();
            this.Context.EndCodeGeneration();
        }

        public abstract void Generated_Main();
    }

    public class GeneratorContext
    {
        private string OldOutputDirectory;

        public object Generator { get; protected set; }

        public string OutputDirectory { get; protected set; }
        public string CurrentFileName { get; protected set; }
        public TemplatePrinter CurrentTemplatePrinter { get; protected set; }
        public LanguageProcessor DynamicProcessor { get; protected set; }

        public GeneratorContext()
        {
            this.Generator = null;
            this.OutputDirectory = Directory.GetCurrentDirectory();
            this.CurrentFileName = null;
            this.CurrentTemplatePrinter = new TemplatePrinter(Console.Out);
            this.DynamicProcessor = null;
        }

        public virtual void BeginCodeGeneration(object generator)
        {
            this.Generator = generator;
            this.OldOutputDirectory = Directory.GetCurrentDirectory();
        }

        public virtual void EndCodeGeneration()
        {
            if (this.CurrentFileName != null)
            {
                this.CurrentTemplatePrinter.Close();
            }
            Directory.SetCurrentDirectory(this.OldOutputDirectory);
        }

        public virtual void CreateFolder(string folderName)
        {
            Directory.CreateDirectory(folderName);
        }

        public virtual void SetOutputFolder(string folderName)
        {
            this.OutputDirectory = Path.GetFullPath(folderName);
            this.CreateFolder(this.OutputDirectory);            
            Directory.SetCurrentDirectory(this.OutputDirectory);
        }

        public virtual void SetOutput(string fileName)
        {
            if (this.CurrentFileName != fileName)
            {
                if (this.CurrentFileName != null)
                {
                    this.CurrentTemplatePrinter.Close();
                }
                this.CurrentFileName = fileName;
                if (this.CurrentFileName == null)
                {
                    this.CurrentTemplatePrinter = new TemplatePrinter(Console.Out);
                }
                else
                {
                    this.CurrentTemplatePrinter = new TemplatePrinter(new StreamWriter(this.CurrentFileName));
                }
            }
        }

        public virtual void Output(IEnumerable<string> lines)
        {
            if (this.CurrentTemplatePrinter != null)
            {
                this.CurrentTemplatePrinter.WriteLine(lines);
            }
        }

        public virtual void Output(string text)
        {
            if (this.CurrentTemplatePrinter != null)
            {
                this.CurrentTemplatePrinter.Write(text);
            }
        }

        public virtual void SetDynamicProcessor(string namePattern)
        {
            if (namePattern == null)
            {
                this.DynamicProcessor = null;
            }
            else
            {
                this.DynamicProcessor = new LanguageProcessor(this.Generator, "Generated_" + namePattern);
            }
        }

        public virtual dynamic ProcessDynamic(dynamic node)
        {
            if (this.DynamicProcessor != null)
            {
                return this.DynamicProcessor.Process(node);
            }
            else
            {
                throw new OsloCodeGeneratorException("No Context.DynamicProcessor specified. Use Context.SetDynamicProcessor().");
            }
        }

        public virtual dynamic ProcessDynamicChildren(dynamic node)
        {
            if (this.DynamicProcessor != null)
            {
                return this.DynamicProcessor.ProcessChildren(node);
            }
            else
            {
                throw new OsloCodeGeneratorException("No Context.DynamicProcessor specified. Use Context.SetDynamicProcessor().");
            }
        }

        public virtual void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
