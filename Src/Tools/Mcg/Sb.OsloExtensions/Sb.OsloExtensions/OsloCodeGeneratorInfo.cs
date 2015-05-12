using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Dataflow;
using System.Dynamic;

namespace OsloExtensions
{
    public delegate void ProgressDelegate(uint current, uint total);

    public class OsloCodeGeneratorInfo
    {
        public string FileName { get; private set; }
        public bool IgnoreIncludes { get; private set; }
        public Language CodeParser { get; private set; }
        public Language TemplateParser { get; private set; }
        public ErrorReporter ErrorReporter { get; private set; }

        public SortedSet<string> References { get; private set; }
        public SortedSet<string> Usings { get; private set; }
        public SortedSet<string> Imports { get; private set; }
        public List<OsloCodeGeneratorInfo> Includes { get; private set; }
        public bool HasMainFunction { get; internal set; }
        public uint FunctionCount { get; internal set; }

        private ProgressDelegate progress;
        public dynamic Program { get; private set; }
        public TemplatePrinter CodePrinter { get; private set; }

        public string NamespaceName { get; set; }
        public string ClassName { get; set; }
        public string InstancesType { get; set; }
        public string ContextType { get; set; }
        public string PropertiesName { get; set; }
        public bool UseMcgLineNumbers { get; set; }

        public OsloCodeGeneratorInfo(string fileName, TextReader fileContent, bool ignoreIncludes, ErrorReporter errorReporter)
        {
            this.CodeParser = OsloCodeGeneratorLanguages.GetOsloCodeGeneratorParser();
            this.TemplateParser = OsloCodeGeneratorLanguages.GetOsloCodeGeneratorTemplateParser();
            this.CodePrinter = null;
            this.ErrorReporter = errorReporter;

            this.References = new SortedSet<string>();
            this.Usings = new SortedSet<string>();
            this.Imports = new SortedSet<string>();
            this.Includes = new List<OsloCodeGeneratorInfo>();

            //this.IgnoreIncludes = ignoreIncludes;
            this.IgnoreIncludes = false;
            if (fileName != null)
            {
                this.FileName = Path.GetFullPath(fileName);
                if (!File.Exists(this.FileName))
                {
                    this.ErrorReporter.Error("File not found: {0}", fileName);
                }
            }
            this.Program = this.CodeParser.Parse(fileContent, this.ErrorReporter);

            this.Usings.Add("System");
            this.Usings.Add("System.Collections.Generic");
            this.Usings.Add("System.Linq");
            this.Usings.Add("System.Text");
            this.Usings.Add("OsloExtensions");
            this.Usings.Add("OsloExtensions.Extensions");
            this.Includes.Add(this);

            this.FunctionCount = 0;
            new OsloCodeGeneratorUsingProcessor(this, this).Process(this.Program);
        }

        internal OsloCodeGeneratorInfo(OsloCodeGeneratorInfo root, string fileName)
        {
            this.CodeParser = root.CodeParser;
            this.TemplateParser = root.TemplateParser;
            this.CodePrinter = root.CodePrinter;
            this.ErrorReporter = root.ErrorReporter;

            this.References = new SortedSet<string>();
            this.Usings = new SortedSet<string>();
            this.Imports = new SortedSet<string>();
            this.Includes = new List<OsloCodeGeneratorInfo>();

            this.IgnoreIncludes = root.IgnoreIncludes;

            this.FileName = Path.GetFullPath(fileName);
            if (!File.Exists(this.FileName))
            {
                this.ErrorReporter.Error("File not found: {0}", fileName);
            }
            this.Program = this.CodeParser.Parse(new StreamReader(this.FileName), this.ErrorReporter);
        }

        public void GenerateTemporaryCode(TemplatePrinter output, ProgressDelegate progress = null)
        {
            this.CodePrinter = output;
            this.progress = progress;

            if (this.NamespaceName == null) this.NamespaceName = "TemporaryNamespace";
            if (this.ClassName == null) this.ClassName = "TemporaryGenerator";
            if (this.InstancesType == null) this.InstancesType = "List<object>";
            if (this.ContextType == null) this.ContextType = "GeneratorContext";

            foreach (string usingName in this.Usings)
            {
                this.CodePrinter.WriteLine("using {0};", usingName);
            }
            this.CodePrinter.ForcedWriteLine();
            this.CodePrinter.WriteLine("namespace {0}", this.NamespaceName);
            this.CodePrinter.WriteLine("{");
            this.CodePrinter.AppendIndent("    ");
            if (this.HasMainFunction)
            {
                this.CodePrinter.WriteLine("// The main file of the generator.");
                this.CodePrinter.WriteLine("public partial class {0} : Generator<{1}, {2}>", this.ClassName, this.InstancesType, this.ContextType);
                this.CodePrinter.WriteLine("{");
                this.CodePrinter.AppendIndent("    ");
                foreach (string import in this.Imports)
                {
                    this.CodePrinter.Write("public {0} {0}", import);
                    this.CodePrinter.WriteLine(" { get; private set; }");
                }
                this.CodePrinter.ForcedWriteLine();
                this.CodePrinter.WriteLine("public {0}({1} instances, {2} context)", this.ClassName, this.InstancesType, this.ContextType);
                this.CodePrinter.WriteLine("    : base(instances, context)");
                this.CodePrinter.WriteLine("{");
                this.CodePrinter.AppendIndent("    ");
                if (this.PropertiesName != null)
                {
                    this.CodePrinter.WriteLine("this.{0} = new PropertyGroup_{0}();", this.PropertiesName);
                }
                foreach (string import in this.Imports)
                {
                    this.CodePrinter.WriteLine("this.{0} = new {0}(instances, context);", import);
                }
                this.CodePrinter.ResetIndent();
                this.CodePrinter.WriteLine("}");
                this.CodePrinter.ForcedWriteLine();
            }
            else
            {
                this.CodePrinter.WriteLine("// Inheritace from 'Generator<{0}, {1}>' and constructor is only generated into the main file.", this.InstancesType, this.ContextType);
                this.CodePrinter.WriteLine("public partial class {0}", this.ClassName, this.InstancesType, this.ContextType);
                this.CodePrinter.WriteLine("{");
                this.CodePrinter.AppendIndent("    ");
            }
            this.CodePrinter.WriteLine();
            foreach (OsloCodeGeneratorInfo codeGenerator in this.Includes)
            {
                this.CodePrinter.AppendIndent("    ");
                this.CodePrinter.WriteLine("#region functions from \"{0}\"", codeGenerator.FileName);
                this.CodePrinter.WriteLine();
                OsloCodeGeneratorPrintProcessor ocgpp = new OsloCodeGeneratorPrintProcessor(this, codeGenerator);
                ocgpp.Process(codeGenerator.Program);
                /*if (codeGenerator == this && !this.IgnoreIncludes && !this.HasMainFunction)
                {
                    this.ErrorReporter.Error("Missing 'void Main()' function in {0}.", this.FileName);
                }*/
                this.CodePrinter.WriteLine();
                this.CodePrinter.WriteLine("#endregion");
            }
            this.CodePrinter.ResetIndent();
            this.CodePrinter.WriteLine("}");
            this.CodePrinter.ResetIndent();
            this.CodePrinter.WriteLine("}");

        }

        internal void Progress(uint current, uint total)
        {
            if (this.progress != null)
            {
                this.progress(current, total);
            }
        }
    }

    internal class OsloCodeGeneratorUsingProcessor : LanguageProcessor
    {
        private OsloCodeGeneratorInfo rootCodeGenerator;
        private OsloCodeGeneratorInfo codeGenerator;

        public OsloCodeGeneratorUsingProcessor(OsloCodeGeneratorInfo rootCodeGenerator, OsloCodeGeneratorInfo codeGenerator)
        {
            this.rootCodeGenerator = rootCodeGenerator;
            this.codeGenerator = codeGenerator;
        }

        public void Process_Reference(dynamic node)
        {
            string assembly = node.Assembly;
            this.rootCodeGenerator.References.Add(assembly);
        }

        public void Process_Using(dynamic node)
        {
            List<string> namespaceNames = new List<string>();
            foreach (var ns in node.Namespace)
            {
                namespaceNames.Add(ns.ToString());
            }
            string namespaceName = namespaceNames.ConcatWithDelimiter(".");
            this.rootCodeGenerator.Usings.Add(namespaceName);
        }

        public void Process_Import(dynamic node)
        {
            this.rootCodeGenerator.Imports.Add(node.GeneratorName);
        }

        public void Process_Include(dynamic node)
        {
            if (this.codeGenerator.IgnoreIncludes) return;
            string includeFileName = node.FileName;
            if (includeFileName.Length < 2)
            {
                this.rootCodeGenerator.ErrorReporter.Error("Invalid include '{0}' in '{1}'.", includeFileName, this.codeGenerator.FileName);
            }
            includeFileName = includeFileName.Substring(1, includeFileName.Length - 2);
            string includeFilePath = Path.Combine(Path.GetDirectoryName(this.codeGenerator.FileName), includeFileName);
            bool includedAlready = this.rootCodeGenerator.Includes.Where(imp => imp.FileName == includeFilePath).Count() > 0;
            if (!includedAlready)
            {
                OsloCodeGeneratorInfo include = new OsloCodeGeneratorInfo(this.rootCodeGenerator, includeFilePath);
                this.rootCodeGenerator.Includes.Add(include);
                new OsloCodeGeneratorUsingProcessor(this.rootCodeGenerator, include).Process(include.Program);
            }
        }

        public void Process_ConfigurationContent(dynamic node)
        {
            if (node.ConfigurationProperties != null)
            {
                this.Process(node.ConfigurationProperties);
            }
            if (node.UserProperties != null)
            {
                this.codeGenerator.PropertiesName = node.UserProperties.Name;
            }
        }

        public void Process_ConfigurationProperty(dynamic node)
        {
            string propertyName = node.Name;
            StringBuilder sb = new StringBuilder();
            string propertyValue;
            bool boolValue;
            switch (propertyName)
            {
                case "NamespaceName":
                    propertyValue = node.Default.ToString();
                    propertyValue = propertyValue.Substring(1, propertyValue.Length - 2);
                    this.codeGenerator.NamespaceName = propertyValue;
                    break;
                case "ClassName":
                    propertyValue = node.Default.ToString();
                    propertyValue = propertyValue.Substring(1, propertyValue.Length - 2);
                    this.codeGenerator.ClassName = propertyValue;
                    break;
                case "InstancesType":
                    this.TypeToString(node.Default, sb);
                    propertyValue = sb.ToString();
                    this.codeGenerator.InstancesType = propertyValue;
                    break;
                case "ContextType":
                    this.TypeToString(node.Default, sb);
                    propertyValue = sb.ToString();
                    this.codeGenerator.ContextType = propertyValue;
                    break;
                case "UseMcgLineNumbers":
                    if (bool.TryParse(node.Default, out boolValue))
                    {
                        this.codeGenerator.UseMcgLineNumbers = boolValue;
                    }
                    break;
                default:
                    break;
            }
        }

        private void TypeToString(dynamic type, StringBuilder result)
        {
            string brand = type.GetBrand().ToString();
            switch (brand)
            {
                case "Literal":
                    string name = type[0];
                    result.Append(name);
                    break;
                case "SimpleType":
                    result.Append(type.Name);
                    if (type.IsNullable)
                    {
                        result.Append("?");
                    }
                    break;
                case "ArrayType":
                    this.TypeToString(type.ItemType, result);
                    result.Append("[]");
                    break;
                case "TemplateType":
                    result.Append(type.Name);
                    result.Append("<");
                    string delim = "";
                    foreach (dynamic param in type.Params)
                    {
                        result.Append(delim);
                        this.TypeToString(param, result);
                        delim = ", ";
                    }
                    result.Append(">");
                    break;
                default:
                    break;
            }

        }

        public void Process_Function(dynamic function)
        {
            if (function.Name == "Main" && function.Parameters == null)
            {
                this.rootCodeGenerator.HasMainFunction = true;
            }
            ++this.rootCodeGenerator.FunctionCount;
        }

        public void Process_Template(dynamic template)
        {
            ++this.rootCodeGenerator.FunctionCount;
        }
    }

    internal class OsloCodeGeneratorPrintProcessor : LanguageProcessor
    {
        private OsloCodeGeneratorInfo rootCodeGenerator;
        private OsloCodeGeneratorInfo codeGenerator;
        private TemplatePrinter CodePrinter;

        private int loopCounter = 0;
        private int variableCounter = 0;
        private uint functionCounter = 0;
        private int baseLineNumber = 0;
        private string fileName;

        private Stack<NameScope> nameStack;
        private Stack<LoopScope> loopStack;
        private NameScope functionNames;

        public OsloCodeGeneratorPrintProcessor(OsloCodeGeneratorInfo rootCodeGenerator, OsloCodeGeneratorInfo codeGenerator)
        {
            this.rootCodeGenerator = rootCodeGenerator;
            this.codeGenerator = codeGenerator;
            this.CodePrinter = this.rootCodeGenerator.CodePrinter;
            if (this.codeGenerator != null && this.codeGenerator.FileName != null)
            {
                this.fileName = Path.GetFileName(this.codeGenerator.FileName);
            }

            this.loopStack = new Stack<LoopScope>();
            this.nameStack = new Stack<NameScope>();
            this.functionNames = new NameScope();

            this.functionCounter = 0;
        }

        public void BeginLoop()
        {
            ++this.loopCounter;
            LoopScope scope = new LoopScope("__loop" + this.loopCounter);
            this.loopStack.Push(scope);
            this.nameStack.Push(scope);
        }

        public LoopScope EndLoop()
        {
            if (this.nameStack.Count > 0)
            {
                this.nameStack.Pop();
            }
            if (this.loopStack.Count > 0)
            {
                return this.loopStack.Pop();
            }
            return null;
        }

        public void BeginIf()
        {
            this.nameStack.Push(new IfScope());
        }

        public void EndIf()
        {
            if (this.nameStack.Count > 0)
            {
                this.nameStack.Pop();
            }
        }

        public void BeginScope()
        {
            NameScope scope = new NameScope();
            this.nameStack.Push(scope);
        }

        public void EndScope()
        {
            if (this.nameStack.Count > 0)
            {
                this.nameStack.Pop();
            }
        }

        public string GetNextNonameVariable()
        {
            ++this.variableCounter;
            return "__noname" + this.variableCounter;
        }

        public bool VariableExists(string variableName)
        {
            foreach (var scope in this.nameStack)
            {
                if (scope.Contains(variableName))
                {
                    return true;
                }
            }
            return false;
        }

        public NameScope CurrentScope
        {
            get
            {
                if (this.nameStack.Count > 0) return this.nameStack.Peek();
                else return null;
            }
        }

        public LoopScope CurrentLoop
        {
            get
            {
                if (this.loopStack.Count > 0) return this.loopStack.Peek();
                else return null;
            }
        }

        public string RenameVariable(string variableName)
        {
            foreach (var loop in this.loopStack)
            {
                if (loop.Contains(variableName))
                {
                    return loop.GetMemberName(variableName);
                }
            }
            return variableName;
        }

        public void PrintLineNumber(dynamic node)
        {
            if (this.codeGenerator != null && this.codeGenerator.UseMcgLineNumbers)
            {
                this.CodePrinter.WriteLine();
                int lineNumber = node.GetSourceSpan().Start.Line;
                this.CodePrinter.WriteLine("#line {0} \"{1}\"", this.baseLineNumber + lineNumber, this.fileName);
            }
        }

        public void Process_ConfigurationContent(dynamic node)
        {
            if (node.UserProperties != null)
            {
                this.Process(node.UserProperties);
            }
        }

        public void Process_UserPropertyGroup(dynamic node)
        {
            this.PrintLineNumber(node);
            this.CodePrinter.Write("public PropertyGroup_{0} {0}", node.Name);
            this.CodePrinter.WriteLine(" { get; private set; }");
            this.CodePrinter.ForcedWriteLine();
            this.PrintLineNumber(node);
            this.CodePrinter.WriteLine("public class PropertyGroup_{0}", node.Name);
            this.CodePrinter.WriteLine("{");
            this.CodePrinter.AppendIndent("    ");
            this.PrintLineNumber(node);
            this.CodePrinter.WriteLine("public PropertyGroup_{0}()", node.Name);
            this.CodePrinter.WriteLine("{");
            this.CodePrinter.AppendIndent("    ");
            foreach (var property in node.Properties)
            {
                if (property.GetBrand() == "UserProperty")
                {
                    if (property.Default != null)
                    {
                        this.PrintLineNumber(property);
                        this.CodePrinter.Write("this.{0} = ", property.Name);
                        this.Process(property.Default);
                        this.CodePrinter.WriteLine(";");
                    }
                }
                else if (property.GetBrand() == "UserPropertyGroup")
                {
                    this.PrintLineNumber(property);
                    this.CodePrinter.WriteLine("this.{0} = new PropertyGroup_{0}();", property.Name);
                }
            }
            this.CodePrinter.ResetIndent();
            this.CodePrinter.WriteLine("}");
            this.CodePrinter.ForcedWriteLine();
            this.Process(node.Properties);
            this.CodePrinter.ResetIndent();
            this.CodePrinter.WriteLine("}");
            this.CodePrinter.ForcedWriteLine();
        }

        public void Process_UserProperty(dynamic node)
        {
            this.PrintLineNumber(node);
            this.CodePrinter.Write("public ");
            this.Process(node.Type);
            this.CodePrinter.Write(" {0}", node.Name);
            this.CodePrinter.WriteLine(" { get; set; }");
        }

        public void Process_Function(dynamic function)
        {
            if (function == null) return;
            this.PrintLineNumber(function);
            this.CodePrinter.Write("public ");
            if (function.Name == "Main" && function.Parameters == null)
            {
                this.CodePrinter.Write("override ");
            }
            this.Process(function.ReturnType);
            this.PrintLineNumber(function);
            this.CodePrinter.Write(" Generated_{0}(", function.Name);
            if (function.Parameters != null)
            {
                string delim = "";
                foreach (dynamic param in function.Parameters)
                {
                    this.CodePrinter.Write(delim);
                    this.Process(param);
                    delim = ", ";
                }
            }
            this.CodePrinter.WriteLine(")");
            this.CodePrinter.WriteLine("{");
            if (function.Statements != null)
            {
                this.BeginScope();
                if (function.Parameters != null)
                {
                    foreach (dynamic param in function.Parameters)
                    {
                        this.CurrentScope.Add(param.Name);
                    }
                }
                this.CodePrinter.AppendIndent("    ");
                foreach (dynamic statement in function.Statements)
                {
                    this.Process(statement);
                }
                this.CodePrinter.ResetIndent();
                this.EndScope();
            }
            this.CodePrinter.WriteLine("}");
            this.CodePrinter.ForcedWriteLine();
            ++this.functionCounter;
            this.rootCodeGenerator.Progress(this.functionCounter, this.rootCodeGenerator.FunctionCount);
        }

        public void Process_Template(dynamic template)
        {
            if (template == null) return;
            try
            {
                this.PrintLineNumber(template);
                this.CodePrinter.Write("public List<string> Generated_{0}(", template.Name);
                if (template.Parameters != null)
                {
                    string delim = "";
                    foreach (dynamic param in template.Parameters)
                    {
                        this.CodePrinter.Write(delim);
                        this.Process(param);
                        delim = ", ";
                    }
                }
                this.CodePrinter.WriteLine(")");
                this.CodePrinter.WriteLine("{");
                if (template.Content != null)
                {
                    this.BeginScope();
                    if (template.Parameters != null)
                    {
                        foreach (dynamic param in template.Parameters)
                        {
                            this.CurrentScope.Add(param.Name);
                        }
                    }
                    this.CodePrinter.AppendIndent("    ");
                    this.CodePrinter.WriteLine("List<string> __result = new List<string>();");
                    this.CodePrinter.WriteLine("using(TemplatePrinter __printer = new TemplatePrinter(__result))");
                    this.CodePrinter.WriteLine("{");
                    this.CodePrinter.AppendIndent("    ");
                    int lineNumber = template.GetSourceSpan().Start.Line;
                    foreach (dynamic line in template.Content)
                    {
                        ++lineNumber;
                        this.baseLineNumber = lineNumber - 1;
                        string text = line.ToString();
                        OsloErrorReporter templateLineErrorReporter = new OsloErrorReporter();
                        dynamic program = this.rootCodeGenerator.TemplateParser.Parse(new StringReader(text), templateLineErrorReporter);
                        foreach (var error in templateLineErrorReporter.Errors)
                        {
                            this.rootCodeGenerator.ErrorReporter.Report(
                                new SourceLocation(
                                    new SourceSpan(
                                        new SourcePoint(lineNumber, error.Location.Span.Start.Column),
                                        new SourcePoint(lineNumber, error.Location.Span.End.Column)),
                                    error.Location.FileName),
                                error);
                        }
                        if (program != null)
                        {
                            bool hasStatement = false;
                            foreach (dynamic content in program.Content)
                            {
                                if (content.GetBrand() == "TemplateControl" && content.Statement != null)
                                {
                                    hasStatement = true;
                                }
                                this.Process(content);
                            }
                            if (hasStatement)
                            {
                                this.CodePrinter.WriteLine("__printer.TrimLine();");
                            }
                            this.CodePrinter.WriteLine("__printer.WriteLine();");
                        }
                        else if (templateLineErrorReporter.ErrorCount == 0)
                        {
                            this.rootCodeGenerator.ErrorReporter.Error(
                                new SourceLocation(
                                    new SourceSpan(
                                        new SourcePoint(lineNumber, 0),
                                        new SourcePoint(lineNumber, text.Length)),
                                    this.codeGenerator.FileName),
                                "Could not process line in template {1}.", template.Name);
                        }
                    }
                    this.CodePrinter.ResetIndent();
                    this.CodePrinter.WriteLine("}");
                    this.CodePrinter.WriteLine("return __result;");
                    this.CodePrinter.ResetIndent();
                    this.EndScope();
                }
                this.CodePrinter.WriteLine("}");
                this.CodePrinter.ForcedWriteLine();
                ++this.functionCounter;
                this.rootCodeGenerator.Progress(this.functionCounter, this.rootCodeGenerator.FunctionCount);
            }
            finally
            {
                this.baseLineNumber = 0;
            }
        }

        public void Process_TemplateOutput(dynamic output)
        {
            this.PrintLineNumber(output);
            string text = ((string)output.Output).TrimNewLineCharacters().EscapeString();
            if (!string.IsNullOrEmpty(text))
            {
                this.CodePrinter.WriteLine("__printer.WriteTemplateOutput(\"{0}\");", text);
            }
        }

        public void Process_TemplateControl(dynamic output)
        {
            if (output.Expression != null)
            {
                this.PrintLineNumber(output);
                this.CodePrinter.Write("__printer.Write(");
                this.Process(output.Expression);
                this.CodePrinter.WriteLine(");");
            }
            else if (output.Statement != null)
            {
                this.Process(output.Statement);
            }
        }

        public void Process_SimpleType(dynamic type)
        {
            this.PrintLineNumber(type);
            this.CodePrinter.Write(type.Name);
            if (type.IsNullable)
            {
                this.CodePrinter.Write("?");
            }
        }

        public void Process_ArrayType(dynamic type)
        {
            this.PrintLineNumber(type);
            this.Process(type.ItemType);
            this.CodePrinter.Write("[]");
        }

        public void Process_TemplateType(dynamic type)
        {
            this.PrintLineNumber(type);
            this.CodePrinter.Write(type.Name);
            this.CodePrinter.Write("<");
            string delim = "";
            foreach (dynamic param in type.Params)
            {
                this.CodePrinter.Write(delim);
                this.Process(param);
                delim = ", ";
            }
            this.CodePrinter.Write(">");
        }

        public void Process_Literal(dynamic node)
        {
            this.PrintLineNumber(node);
            this.CodePrinter.Write(node[0]);
        }

        public void Process_Parameter(dynamic node)
        {
            this.PrintLineNumber(node);
            this.Process(node.Type);
            this.CodePrinter.Write(" {0}", node.Name);
        }

        public void Process_BracketExpression(dynamic node)
        {
            this.PrintLineNumber(node);
            this.CodePrinter.Write("(");
            this.Process(node.Expression);
            this.CodePrinter.Write(")");
        }

        public void Process_TypeCastExpression(dynamic node)
        {
            this.PrintLineNumber(node);
            this.CodePrinter.Write("(");
            this.Process(node.Type);
            this.CodePrinter.Write(")");
            this.Process(node.Expression);
        }

        public void Process_VariableExpression(dynamic node)
        {
            this.PrintLineNumber(node);
            this.CodePrinter.Write(this.RenameVariable(node.Name));
        }

        public void Process_PropertyAccessExpression(dynamic node)
        {
            this.PrintLineNumber(node);
            this.Process(node.Object);
            this.CodePrinter.Write(".");
            this.PrintLineNumber(node);
            this.CodePrinter.Write(node.PropertyName);
        }

        public void Process_MethodCallExpression(dynamic node)
        {
            if (node.Object.GetBrand() == "VariableExpression")
            {
                string name = node.Object.Name;
                if (this.codeGenerator.Imports.Contains(name))
                {
                    this.Process(node.Object);
                    this.PrintLineNumber(node);
                    this.CodePrinter.Write(".");
                    this.Process_FunctionCallExpression(node.FunctionCall);
                    return;
                }
            }
            this.Process(node.Object);
            this.PrintLineNumber(node);
            this.CodePrinter.Write(".");
            this.Process(node.FunctionCall);
        }

        public void Process_MemberFunctionCallExpression(dynamic node)
        {
            this.PrintLineNumber(node);
            this.CodePrinter.Write(node.Name);
            if (node.TemplateParams != null)
            {
                this.CodePrinter.Write("<");
                string delim = "";
                foreach (dynamic param in node.TemplateParams)
                {
                    this.CodePrinter.Write(delim);
                    this.Process(param);
                    delim = ", ";
                }
                this.CodePrinter.Write(">");
            }
            this.CodePrinter.Write("(");
            if (node.Params != null)
            {
                string delim = "";
                foreach (dynamic param in node.Params)
                {
                    this.CodePrinter.Write(delim);
                    this.Process(param);
                    delim = ", ";
                }
            }
            this.CodePrinter.Write(")");
        }

        public void Process_FunctionCallExpression(dynamic node)
        {
            bool rename = true;
            if (node.Name == "typeof")
            {
                rename = false;
            }
            this.PrintLineNumber(node);
            if (rename)
            {
                this.CodePrinter.Write("Generated_" + node.Name);
            }
            else
            {
                this.CodePrinter.Write(node.Name);
            }
            if (node.TemplateParams != null)
            {
                this.CodePrinter.Write("<");
                string delim = "";
                foreach (dynamic param in node.TemplateParams)
                {
                    this.CodePrinter.Write(delim);
                    this.Process(param);
                    delim = ", ";
                }
                this.CodePrinter.Write(">");
            }
            this.CodePrinter.Write("(");
            if (node.Params != null)
            {
                string delim = "";
                foreach (dynamic param in node.Params)
                {
                    this.CodePrinter.Write(delim);
                    this.Process(param);
                    delim = ", ";
                }
            }
            this.CodePrinter.Write(")");
        }


        public void Process_NewExpression(dynamic node)
        {
            this.PrintLineNumber(node);
            this.CodePrinter.Write("new ");
            this.CodePrinter.Write(node.Name);
            if (node.TemplateParams != null)
            {
                this.CodePrinter.Write("<");
                string delim = "";
                foreach (dynamic param in node.TemplateParams)
                {
                    this.CodePrinter.Write(delim);
                    this.Process(param);
                    delim = ", ";
                }
                this.CodePrinter.Write(">");
            }
            this.CodePrinter.Write("(");
            if (node.Params != null)
            {
                string delim = "";
                foreach (dynamic param in node.Params)
                {
                    this.CodePrinter.Write(delim);
                    this.Process(param);
                    delim = ", ";
                }
            }
            this.CodePrinter.Write(")");
        }

        public void Process_InfixExpression(dynamic node)
        {
            this.Process(node.Left);
            this.PrintLineNumber(node);
            this.CodePrinter.Write(" {0} ", node.Operator);
            this.Process(node.Right);
        }

        public void Process_PrefixExpression(dynamic node)
        {
            this.PrintLineNumber(node);
            this.CodePrinter.Write("{0}", node.Operator);
            this.Process(node.Expression);
        }

        public void Process_VariableDeclarationStatement(dynamic node)
        {
            this.Process(node.Type);
            this.PrintLineNumber(node);
            if (node.Default == null)
            {
                this.CodePrinter.Write(" {0}", node.Name);
            }
            else
            {
                this.CodePrinter.Write(" {0} = ", node.Name);
                this.Process(node.Default);
            }
            this.CodePrinter.WriteLine(";");
        }

        public void Process_ReturnStatement(dynamic node)
        {
            this.PrintLineNumber(node);
            this.CodePrinter.Write("return");
            if (node.Result != null)
            {
                this.CodePrinter.Write(" ");
                this.Process(node.Result);
            }
            this.CodePrinter.WriteLine(";");
        }

        public void Process_AssignmentStatement(dynamic node)
        {
            this.Process(node.Left);
            this.PrintLineNumber(node);
            this.CodePrinter.Write(" = ");
            this.Process(node.Right);
            this.CodePrinter.WriteLine(";");
        }

        public void Process_CallStatement(dynamic node)
        {
            this.Process(node.Call);
            this.CodePrinter.WriteLine(";");
        }

        public void Process_IfStatement(dynamic node)
        {
            this.Process(node.Begin);
            if (node.ThenStatements != null)
            {
                foreach (dynamic statement in node.ThenStatements)
                {
                    this.Process(statement);
                }
            }
            if (node.ElseIfs != null)
            {
                foreach (dynamic elseIf in node.ElseIfs)
                {
                    this.Process(elseIf);
                }
            }
            if (node.Else != null)
            {
                this.Process(node.Else);
                if (node.ElseStatements != null)
                {
                    foreach (dynamic statement in node.ElseStatements)
                    {
                        this.Process(statement);
                    }
                }
            }
            this.Process(node.End);
        }

        public void Process_IfStatementBegin(dynamic node)
        {
            this.PrintLineNumber(node);
            this.CodePrinter.Write("if (");
            this.Process(node.Condition);
            this.CodePrinter.WriteLine(")");
            this.CodePrinter.WriteLine("{");
            this.CodePrinter.AppendIndent("    ");
            this.BeginIf();
        }

        public void Process_ElseStatement(dynamic node)
        {
            NameScope currentScope = this.CurrentScope;
            if (currentScope is IfScope)
            {
                this.CodePrinter.ResetIndent();
                this.PrintLineNumber(node);
                this.CodePrinter.WriteLine("}");
                this.PrintLineNumber(node);
                this.CodePrinter.WriteLine("else");
                this.PrintLineNumber(node);
                this.CodePrinter.WriteLine("{");
                this.CodePrinter.AppendIndent("    ");
            }
            else if (currentScope is LoopScope)
            {
                this.CodePrinter.ResetIndent();
                this.PrintLineNumber(node);
                this.CodePrinter.WriteLine("}");
                this.PrintLineNumber(node);
                this.CodePrinter.WriteLine("if ({0}_iteration == 0)", this.CurrentLoop.LoopVariable);
                this.PrintLineNumber(node);
                this.CodePrinter.WriteLine("{");
                this.CodePrinter.AppendIndent("    ");
            }
        }

        public void Process_ElseIfStatementBegin(dynamic node)
        {
            this.CodePrinter.ResetIndent();
            this.CodePrinter.WriteLine("}");
            this.PrintLineNumber(node);
            this.CodePrinter.Write("else if (");
            this.Process(node.Condition);
            this.CodePrinter.WriteLine(")");
            this.CodePrinter.WriteLine("{");
            this.CodePrinter.AppendIndent("    ");
        }

        public void Process_IfStatementEnd(dynamic node)
        {
            this.EndIf();
            this.CodePrinter.ResetIndent();
            this.PrintLineNumber(node);
            this.CodePrinter.WriteLine("}");
        }

        public void Process_LoopStatement(dynamic node)
        {
            this.Process(node.Begin);
            if (node.Statements != null)
            {
                foreach (dynamic statement in node.Statements)
                {
                    this.Process(statement);
                }
            }
            if (node.Else != null)
            {
                this.Process(node.Else);
            }
            if (node.ElseStatements != null)
            {
                foreach (dynamic statement in node.ElseStatements)
                {
                    this.Process(statement);
                }
            }
            this.Process(node.End);
        }

        public void Process_LoopStatementBegin(dynamic node)
        {
            this.Process(node.Loop);
        }

        public void Process_LoopStatementEnd(dynamic node)
        {
            /*if (this.CurrentLoop.LoopExpression.OrderBy == null)
            {
                if (this.CurrentLoop.LoopExpression.Where != null)
                {
                    this.CodePrinter.ResetIndent();
                    this.CodePrinter.WriteLine("}");
                }
                foreach (dynamic loopItem in this.CurrentLoop.LoopExpression.LoopChain)
                {
                    this.CodePrinter.ResetIndent();
                    this.CodePrinter.WriteLine("}");
                }
            }
            else
            {*/
            this.CodePrinter.ResetIndent();
            this.PrintLineNumber(node);
            this.CodePrinter.WriteLine("}");
            //}
            this.EndLoop();
        }

        public void Process_LoopExpression(dynamic node)
        {
            List<KeyValuePair<string, dynamic>> items = new List<KeyValuePair<string, dynamic>>();
            foreach (dynamic loopItem in node.LoopChain)
            {
                string alias = loopItem.Alias;
                if (loopItem.Item is string)
                {
                    if (alias == null)
                    {
                        alias = loopItem.Item;
                    }
                    items.Add(new KeyValuePair<string, dynamic>(alias, loopItem.Item));
                }
                else
                {
                    if (alias == null)
                    {
                        alias = this.GetNextNonameVariable();
                    }
                    items.Add(new KeyValuePair<string, dynamic>(alias, loopItem.Item));
                }
            }
            this.BeginLoop();
            this.CurrentLoop.LoopExpression = node;
            string loopName = this.CurrentLoop.LoopVariable;
            /*            if (node.OrderBy == null)
                        {
                            this.CurrentLoop.MemberNamePattern = "{1}";
                            bool first = true;
                            string parentName = null;
                            string currentName = null;
                            if (node.Runs != null && node.Runs.Count >= 2)
                            {
                                this.CodePrinter.WriteLine("int {0}_iteration = 0;", loopName);
                            }
                            if (node.Runs != null && node.Runs.Count >= 1)
                            {
                                foreach (dynamic runStatement in node.Runs[0])
                                {
                                    this.Process(runStatement);
                                }
                            }
                            foreach (var item in items)
                            {
                                this.CurrentLoop.Add(item.Key);
                                currentName = this.CurrentLoop.GetMemberName(item.Key);
                                if (item.Value is DynamicObject && item.Value.GetBrand() == "MemberFunctionCallExpression" && item.Value.Name == "typeof")
                                {
                                    this.Process(item.Value.Params);
                                    this.CodePrinter.Write(" {0} = {1} as ", currentName, parentName);
                                    this.Process(item.Value.Params);
                                    this.CodePrinter.WriteLine(";");
                                    this.CodePrinter.WriteLine("if ({0} != null)", currentName);
                                }
                                else if (item.Value is DynamicObject && item.Value.GetBrand() == "MemberFunctionCallExpression" && item.Value.Name == "brandof")
                                {
                                    this.CodePrinter.WriteLine("dynamic {0} = {1};", currentName, parentName);
                                    this.CodePrinter.Write("if ({0}.GetBrand() == \"", currentName);
                                    this.Process(item.Value.Params);
                                    this.CodePrinter.WriteLine("\")");
                                }
                                else
                                {
                                    if (first)
                                    {
                                        this.CodePrinter.Write("foreach (var {0} in EnumerableExtensions.Enumerate((", currentName);
                                    }
                                    else
                                    {
                                        this.CodePrinter.Write("foreach (var {0} in EnumerableExtensions.Enumerate(({1}.", currentName, parentName);
                                    }
                                    if (item.Value is string)
                                    {
                                        this.CodePrinter.Write(item.Value);
                                    }
                                    else
                                    {
                                        this.Process(item.Value);
                                    }
                                    this.CodePrinter.WriteLine(").GetEnumerator()))");
                                }
                                this.CodePrinter.WriteLine("{");
                                this.CodePrinter.AppendIndent("    ");
                                parentName = currentName;
                                first = false;
                            }
                            if (node.Where != null)
                            {
                                this.CodePrinter.Write("if (");
                                this.Process(node.Where);
                                this.CodePrinter.WriteLine(")");
                                this.CodePrinter.WriteLine("{");
                                this.CodePrinter.AppendIndent("    ");
                            }
                            if (node.Runs != null && node.Runs.Count >= 2)
                            {
                                this.CodePrinter.WriteLine("++{0}_iteration;", loopName);
                                for (int i = 1; i < node.Runs.Count; ++i)
                                {
                                    this.CodePrinter.WriteLine("if ({0}_iteration == {1})", loopName, i+1);
                                    this.CodePrinter.WriteLine("{");
                                    this.CodePrinter.AppendIndent("    ");
                                    foreach (dynamic runStatement in node.Runs[i])
                                    {
                                        this.Process(runStatement);
                                    }
                                    this.CodePrinter.ResetIndent();
                                    this.CodePrinter.WriteLine("}");
                                }
                            }
                        }
                        else
                        {*/
            this.CodePrinter.WriteLine("int {0}_iteration = 0;", loopName);
            if (node.Runs != null && node.Runs.Count >= 1)
            {
                foreach (dynamic runStatement in node.Runs[0])
                {
                    this.Process(runStatement);
                }
            }
            this.CurrentLoop.MemberNamePattern = "{0}_tmp_item_{1}";
            this.CodePrinter.WriteLine("var {0}_result =", loopName);
            this.CodePrinter.AppendIndent("    ");
            bool first = true;
            string parentName = null;
            string currentName = null;
            foreach (var item in items)
            {
                this.CurrentLoop.Add(item.Key);
                currentName = this.CurrentLoop.GetMemberName(item.Key);
                if (item.Value is DynamicObject && item.Value.GetBrand() == "MemberFunctionCallExpression" && item.Value.Name == "typeof")
                {
                    this.PrintLineNumber(item);
                    this.CodePrinter.Write("from {0} in EnumerableExtensions.Enumerate(({1}).GetEnumerator()).OfType<", currentName, parentName);
                    this.Process(item.Value.Params);
                    this.PrintLineNumber(item);
                    this.CodePrinter.WriteLine(">()");
                }
                else
                {
                    this.PrintLineNumber(item);
                    if (first)
                    {
                        this.CodePrinter.Write("(from {0} in EnumerableExtensions.Enumerate((", currentName);
                        first = false;
                    }
                    else
                    {
                        this.CodePrinter.Write("from {0} in EnumerableExtensions.Enumerate(({1}.", currentName, parentName);
                    }
                    if (item.Value is string)
                    {
                        this.CodePrinter.Write(item.Value);
                    }
                    else
                    {
                        this.Process(item.Value);
                    }
                    this.PrintLineNumber(item);
                    this.CodePrinter.WriteLine(").GetEnumerator())");
                }
                parentName = currentName;
            }
            if (node.Where != null)
            {
                this.PrintLineNumber(node.Where);
                this.CodePrinter.Write("where ");
                this.Process(node.Where);
                this.CodePrinter.WriteLine();
            }
            if (node.OrderBy != null)
            {
                this.PrintLineNumber(node.OrderBy);
                this.CodePrinter.Write("orderby ");
                string obdelim = "";
                foreach (dynamic orderBy in node.OrderBy)
                {
                    this.CodePrinter.Write(obdelim);
                    this.Process(orderBy.Expression);
                    if (orderBy.Descending)
                    {
                        this.PrintLineNumber(orderBy);
                        this.CodePrinter.Write(" descending");
                    }
                    obdelim = ", ";
                }
                this.CodePrinter.WriteLine();
            }
            this.PrintLineNumber(node);
            this.CodePrinter.WriteLine("select");
            this.CodePrinter.AppendIndent("    ");
            this.CodePrinter.WriteLine("new");
            this.CodePrinter.WriteLine("{");
            this.CodePrinter.AppendIndent("    ");
            foreach (var item in items)
            {
                currentName = this.CurrentLoop.GetMemberName(item.Key);
                string selectedName = loopName + "_item_" + item.Key;
                this.PrintLineNumber(item);
                this.CodePrinter.WriteLine("{0} = {1},", selectedName, currentName);
            }
            this.CodePrinter.ResetIndent();
            this.CodePrinter.WriteLine("}).ToArray();");
            this.CodePrinter.ResetIndent();
            this.CodePrinter.ResetIndent();

            this.CurrentLoop.MemberNamePattern = "{0}_item_{1}";
            this.CodePrinter.WriteLine("foreach (var {0}_item in {0}_result)", loopName);
            this.CodePrinter.WriteLine("{");
            this.CodePrinter.AppendIndent("    ");
            foreach (var item in items)
            {
                currentName = this.CurrentLoop.GetMemberName(item.Key);
                string variableName = item.Key;
                this.PrintLineNumber(item);
                this.CodePrinter.WriteLine("var {0} = {1}_item.{2};", variableName, loopName, currentName);
            }
            this.CodePrinter.WriteLine();
            this.CodePrinter.WriteLine("++{0}_iteration;", loopName);
            if (node.Runs != null && node.Runs.Count >= 2)
            {
                for (int i = 1; i < node.Runs.Count; ++i)
                {
                    if (i < node.Runs.Count - 1)
                    {
                        this.CodePrinter.WriteLine("if ({0}_iteration == {1})", loopName, i + 1);
                    }
                    else
                    {
                        this.CodePrinter.WriteLine("if ({0}_iteration >= {1})", loopName, i + 1);
                    }
                    this.CodePrinter.WriteLine("{");
                    this.CodePrinter.AppendIndent("    ");
                    foreach (dynamic runStatement in node.Runs[i])
                    {
                        this.Process(runStatement);
                    }
                    this.CodePrinter.ResetIndent();
                    this.CodePrinter.WriteLine("}");
                }
            }
            //            }
            this.CurrentLoop.MemberNamePattern = "{1}";
        }
    }
}
