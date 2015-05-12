using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OsloExtensions
{
    internal enum TemplatePrinterState
    {
        LineBegin,
        LineContent
    }


    public class NameScope
    {
        public SortedSet<string> Names { get; private set; }

        public NameScope()
        {
            this.Names = new SortedSet<string>();
        }

        public void Add(string name)
        {
            if (!this.Names.Add(name))
            {
                throw new OsloCodeGeneratorException("Name redeclared: " + name);
            }
        }

        public bool Contains(string name)
        {
            return this.Names.Contains(name);
        }
    }

    internal class LoopScope : NameScope
    {
        public string LoopVariable { get; private set; }
        public string MemberNamePattern { get; set; }
        public dynamic LoopExpression { get; set; }

        public LoopScope(string loopVariable)
        {
            this.LoopVariable = loopVariable;
            this.MemberNamePattern = "{0}_item_{1}";
            this.LoopExpression = null;
        }

        public string GetMemberName(string memberName)
        {
            if (this.Contains(memberName))
            {
                return string.Format(this.MemberNamePattern, this.LoopVariable, memberName);
            }
            else
            {
                return memberName;
            }
        }
    }

    internal class IfScope : NameScope
    {
    }
    
    public class TemplatePrinter : TextWriter
    {
        private TextWriter outputWriter;
        private List<string> outputList;

        private TemplatePrinterState state;
        private bool afterTemplateOutput;
        private bool noNewLine;
        private StringWriter currentLine;
        
        private Stack<string> indentStack;

        public string Indent { get; protected set; }

        public TemplatePrinter(TextWriter output)
        {
            this.outputWriter = output;
            this.outputList = null;
            this.state = TemplatePrinterState.LineBegin;
            this.afterTemplateOutput = false;
            this.noNewLine = false;
            this.currentLine = null;
            this.indentStack = new Stack<string>();
            this.Indent = "";
        }

        public TemplatePrinter(List<string> output)
        {
            this.outputWriter = null;
            this.outputList = output;
            this.state = TemplatePrinterState.LineBegin;
            this.afterTemplateOutput = false;
            this.currentLine = null;
            this.indentStack = new Stack<string>();
            this.Indent = "";
        }

        protected void BeginLine()
        {
            if (state == TemplatePrinterState.LineBegin)
            {
                this.currentLine = new StringWriter();
                this.currentLine.Write(this.Indent);
                this.state = TemplatePrinterState.LineContent;
                this.afterTemplateOutput = false;
            }
            this.noNewLine = false;
        }

        protected void EndLine()
        {
            if (this.noNewLine) return;
            if (state == TemplatePrinterState.LineContent)
            {
                this.currentLine.Flush();
                string line = this.currentLine.ToString();
                if (this.afterTemplateOutput && line.EndsWith("\\"))
                {
                    line = line.Substring(0, line.Length - 1);
                    this.currentLine = new StringWriter();
                    this.currentLine.Write(line);
                    this.afterTemplateOutput = false;
                    this.noNewLine = true;
                }
                else if (this.afterTemplateOutput && line.EndsWith("^"))
                {
                    line = line.Substring(0, line.Length - 1);
                    if (this.outputWriter != null)
                    {
                        this.outputWriter.WriteLine(line);
                    }
                    else if (this.outputList != null)
                    {
                        this.outputList.Add(line);
                    }
                    this.currentLine = null;
                    this.state = TemplatePrinterState.LineBegin;
                    this.afterTemplateOutput = false;
                }
                else if (!string.IsNullOrWhiteSpace(line))
                {
                    if (this.outputWriter != null)
                    {
                        this.outputWriter.WriteLine(line);
                    }
                    else if (this.outputList != null)
                    {
                        this.outputList.Add(line);
                    }
                    this.currentLine = null;
                    this.state = TemplatePrinterState.LineBegin;
                    this.afterTemplateOutput = false;
                }
            }
        }

        public override void Flush()
        {
            base.Flush();
            if (this.outputWriter != null)
            {
                this.outputWriter.Flush();
            }
        }

        public override void Close()
        {
            if (this.currentLine != null)
            {
                this.currentLine.Flush();
                string line = this.currentLine.ToString();
                if (this.afterTemplateOutput && line.EndsWith("\\"))
                {
                    line = line.Substring(0, line.Length - 1);
                    this.currentLine = new StringWriter();
                    this.currentLine.Write(line);
                    this.afterTemplateOutput = false;
                }
                this.EndLine();
            }
            base.Close();
            if (this.outputWriter != null)
            {
                this.outputWriter.Close();
            }
        }

        public void SetIndent(string newIndent)
        {
            this.indentStack.Push(this.Indent);
            this.Indent = newIndent;
        }

        public void AppendIndent(string append)
        {
            this.indentStack.Push(this.Indent);
            this.Indent += append;
        }

        public void ResetIndent()
        {
            if (this.indentStack.Count > 0)
            {
                this.Indent = this.indentStack.Pop();
            }
            else
            {
                this.Indent = "";
            }
        }

        public string CurrentIndent
        {
            get { return this.Indent; }
        }

        public string CurrentLine
        {
            get 
            {
                this.currentLine.Flush();
                return this.currentLine.ToString(); 
            }
        }

        public virtual void WriteTemplateOutput(string text)
        {
            string trimmed = text.TrimNewLineCharacters();
            this.BeginLine();
            this.currentLine.Write(trimmed);
            this.afterTemplateOutput = true;
            if (trimmed.Length < text.Length)
            {
                this.EndLine();
            }
        }

        public virtual void ForcedWriteLine()
        {
            this.WriteLine();
            this.WriteTemplateOutput("^");
            this.WriteLine();
        }

        public virtual void TrimLine()
        {
            if (this.noNewLine) return;
            this.BeginLine();
            this.currentLine.Flush();
            string line = this.currentLine.ToString();
            this.currentLine = new StringWriter();
            this.currentLine.Write(line.Trim());
        }

        public override void Write(bool value)
        {
            this.BeginLine();
            this.currentLine.Write(value);
            this.afterTemplateOutput = false;
        }

        public override void Write(char value)
        {
            this.BeginLine();
            this.currentLine.Write(value);
            this.afterTemplateOutput = false;
        }

        public override void Write(char[] buffer)
        {
            this.BeginLine();
            this.currentLine.Write(buffer);
            this.afterTemplateOutput = false;
        }

        public override void Write(decimal value)
        {
            this.BeginLine();
            this.currentLine.Write(value);
            this.afterTemplateOutput = false;
        }

        public override void Write(double value)
        {
            this.BeginLine();
            this.currentLine.Write(value);
            this.afterTemplateOutput = false;
        }

        public override void Write(float value)
        {
            this.BeginLine();
            this.currentLine.Write(value);
            this.afterTemplateOutput = false;
        }

        public override void Write(int value)
        {
            this.BeginLine();
            this.currentLine.Write(value);
            this.afterTemplateOutput = false;
        }

        public override void Write(long value)
        {
            this.BeginLine();
            this.currentLine.Write(value);
            this.afterTemplateOutput = false;
        }

        public override void Write(object value)
        {
            if (value == null) return;
            if (value is string)
            {
                this.Write((string)value);
                return;
            }
            if (value is IEnumerable<string>)
            {
                this.Write((IEnumerable<string>)value);
                return;
            }
            if (value is IEnumerable<object>)
            {
                this.Write((IEnumerable<object>)value);
                return;
            }
            this.BeginLine();
            this.currentLine.Write(value);
            this.afterTemplateOutput = false;
        }

        public override void Write(string value)
        {
            this.BeginLine();
            this.currentLine.Write(value);
            this.afterTemplateOutput = false;
        }

        public override void Write(uint value)
        {
            this.BeginLine();
            this.currentLine.Write(value);
            this.afterTemplateOutput = false;
        }

        public override void Write(ulong value)
        {
            this.BeginLine();
            this.currentLine.Write(value);
            this.afterTemplateOutput = false;
        }

        public override void Write(string format, object arg0)
        {
            this.BeginLine();
            this.currentLine.Write(format, arg0);
            this.afterTemplateOutput = false;
        }

        public override void Write(string format, params object[] arg)
        {
            this.BeginLine();
            this.currentLine.Write(format, arg);
            this.afterTemplateOutput = false;
        }

        public override void Write(char[] buffer, int index, int count)
        {
            this.BeginLine();
            this.currentLine.Write(buffer, index, count);
            this.afterTemplateOutput = false;
        }

        public override void Write(string format, object arg0, object arg1)
        {
            this.BeginLine();
            this.currentLine.Write(format, arg0, arg1);
            this.afterTemplateOutput = false;
        }

        public override void Write(string format, object arg0, object arg1, object arg2)
        {
            this.BeginLine();
            this.currentLine.Write(format, arg0, arg1, arg2);
            this.afterTemplateOutput = false;
        }

        public virtual void Write(IEnumerable<string> lines)
        {
            if (lines == null) return;
            this.BeginLine();
            this.currentLine.Flush();
            this.SetIndent(this.currentLine.ToString());

            bool insertNewLine = false;
            foreach (var line in lines)
            {
                if (insertNewLine)
                {
                    this.EndLine();
                    this.BeginLine();
                }
                if (string.IsNullOrWhiteSpace(line))
                {
                    this.WriteTemplateOutput("^");
                    this.EndLine();
                }
                else
                {
                    this.currentLine.Write(line);
                }
                insertNewLine = true;
            }

            this.ResetIndent();
            this.afterTemplateOutput = false;
        }

        public virtual void Write(IEnumerable<object> lines)
        {
            if (lines == null) return;
            this.BeginLine();
            this.currentLine.Flush();
            this.SetIndent(this.currentLine.ToString());

            bool insertNewLine = false;
            foreach (var line in lines)
            {
                if (insertNewLine)
                {
                    this.EndLine();
                    this.BeginLine();
                }
                if (line is string)
                {
                    string stringLine = (string)line;
                    if (string.IsNullOrWhiteSpace(stringLine))
                    {
                        this.WriteTemplateOutput("^");
                        this.EndLine();
                    }
                    else
                    {
                        this.currentLine.Write(stringLine);
                    }
                }
                else if (line is IEnumerable<string>)
                {
                    this.Write((IEnumerable<string>)line);
                }
                else if (line is IEnumerable<object>)
                {
                    this.Write((IEnumerable<object>)line);
                }
                insertNewLine = true;
            }

            this.ResetIndent();
            this.afterTemplateOutput = false;
        }

        public override void WriteLine()
        {
            this.EndLine();
        }

        public override void WriteLine(bool value)
        {
            this.Write(value);
            this.WriteLine();
        }

        public override void WriteLine(char value)
        {
            this.Write(value);
            this.WriteLine();
        }

        public override void WriteLine(char[] buffer)
        {
            this.Write(buffer);
            this.WriteLine();
        }

        public override void WriteLine(decimal value)
        {
            this.Write(value);
            this.WriteLine();
        }

        public override void WriteLine(double value)
        {
            this.Write(value);
            this.WriteLine();
        }

        public override void WriteLine(float value)
        {
            this.Write(value);
            this.WriteLine();
        }

        public override void WriteLine(int value)
        {
            this.Write(value);
            this.WriteLine();
        }

        public override void WriteLine(long value)
        {
            this.Write(value);
            this.WriteLine();
        }

        public override void WriteLine(object value)
        {
            this.Write(value);
            this.WriteLine();
        }

        public override void WriteLine(string value)
        {
            this.Write(value);
            this.WriteLine();
        }

        public override void WriteLine(uint value)
        {
            this.Write(value);
            this.WriteLine();
        }

        public override void WriteLine(ulong value)
        {
            this.Write(value);
            this.WriteLine();
        }

        public override void WriteLine(string format, object arg0)
        {
            this.Write(format, arg0);
            this.WriteLine();
        }

        public override void WriteLine(string format, params object[] arg)
        {
            this.Write(format, arg);
            this.WriteLine();
        }

        public override void WriteLine(char[] buffer, int index, int count)
        {
            this.Write(buffer, index, count);
            this.WriteLine();
        }

        public override void WriteLine(string format, object arg0, object arg1)
        {
            this.Write(format, arg0, arg1);
            this.WriteLine();
        }

        public override void WriteLine(string format, object arg0, object arg1, object arg2)
        {
            this.Write(format, arg0, arg1, arg2);
            this.WriteLine();
        }

        public virtual void WriteLine(IEnumerable<string> lines)
        {
            this.Write(lines);
            this.WriteLine();
        }

        public virtual void WriteLine(IEnumerable<object> lines)
        {
            this.Write(lines);
            this.WriteLine();
        }

        public override Encoding Encoding
        {
            get 
            {
                if (this.outputWriter != null) return this.outputWriter.Encoding;
                else return Encoding.UTF8;
            }
        }
    }
}
