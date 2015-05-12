using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Package;
using Microsoft.VisualStudio.TextManager.Interop;
using System.Dataflow;
using OsloExtensions;

namespace OsloExtensions.VisualStudio
{
    enum OsloCodeGeneratorScannerState
    {
        None = 0,
        InTemplateCommand = 1,
        InTemplateOutput = 2,
        InComment = 4
    }

    public class OsloCodeGeneratorScanner : IScanner
    {
        private static string[] keywords =
            new string[] 
            {
                "template", "function", "end", "return", "if", "else", "loop", "where", 
                "true", "false", "null", "void", "object",
                "bool", "byte", "int", "long", "float", "double", "string",
                "typeof", "orderby", "descending", "dynamic", 
                "reference", "using", "include", "import", "configuration", "properties", 
            };

        private int currentOffset;
        private string currentLine;
        private int bracketCount;
        private OsloCodeGeneratorScannerState currentState;
        private bool enterTemplateOutputMode;

        public OsloCodeGeneratorScanner()
        {
            this.currentState = OsloCodeGeneratorScannerState.None;
        }

        #region IScanner Members

        public bool ScanTokenAndProvideInfoAboutIt(TokenInfo tokenInfo, ref int state)
        {
            if (this.currentLine == null) return false;
            this.currentState = (OsloCodeGeneratorScannerState)state;
            if (this.currentOffset >= this.currentLine.Length)
            {
                if (this.currentState == OsloCodeGeneratorScannerState.InTemplateCommand)
                {
                    this.currentState = OsloCodeGeneratorScannerState.InTemplateOutput;
                }
                if (this.currentState == OsloCodeGeneratorScannerState.None && this.enterTemplateOutputMode)
                {
                    this.currentState = OsloCodeGeneratorScannerState.InTemplateOutput;
                    this.enterTemplateOutputMode = false;
                }
                state = (int)this.currentState;
                return false;
            }

            this.currentState = (OsloCodeGeneratorScannerState)state;

            if (this.currentOffset == 0)
            {
                if (this.currentState == OsloCodeGeneratorScannerState.InTemplateCommand)
                {
                    this.currentState = OsloCodeGeneratorScannerState.InTemplateOutput;
                }
            }
            int oldOffset = this.currentOffset;
            if (tokenInfo != null)
            {
                tokenInfo.StartIndex = this.currentOffset;
            }

            if (this.currentState == OsloCodeGeneratorScannerState.None ||
                this.currentState == OsloCodeGeneratorScannerState.InComment)
            {
                if (this.ProcessComment(tokenInfo))
                {
                    state = (int)this.currentState;
                    return true;
                }
            }
            if (this.currentState == OsloCodeGeneratorScannerState.None)
            {
                if (this.ProcessLineComment(tokenInfo))
                {
                    state = (int)this.currentState;
                    return true;
                }
            }
            if (this.currentState == OsloCodeGeneratorScannerState.None ||
                this.currentState == OsloCodeGeneratorScannerState.InTemplateCommand)
            {
                if (this.ProcessKeyword(tokenInfo))
                {
                    state = (int)this.currentState;
                    return true;
                }
                if (this.ProcessStringLiteral(tokenInfo))
                {
                    state = (int)this.currentState;
                    return true;
                }
                if (this.ProcessIdentifier(tokenInfo))
                {
                    state = (int)this.currentState;
                    return true;
                }
                if (this.ProcessNumber(tokenInfo))
                {
                    state = (int)this.currentState;
                    return true;
                }
                if (this.ProcessDelimiter(tokenInfo))
                {
                    state = (int)this.currentState;
                    return true;
                }
            }
            if (this.ProcessWhitespace(tokenInfo))
            {
                state = (int)this.currentState;
                return true;
            }
            if (this.currentState == OsloCodeGeneratorScannerState.InTemplateOutput)
            {
                if (this.ProcessTemplateOutput(tokenInfo))
                {
                    state = (int)this.currentState;
                    return true;
                }
            }

            if (this.currentOffset == oldOffset)
            {
                System.Windows.Forms.MessageBox.Show("Error!");
            }

            return false;
        }

        public void SetSource(string source, int offset)
        {
            this.currentOffset = offset;
            this.currentLine = source;
            this.bracketCount = 0;
            this.enterTemplateOutputMode = false;
        }

        #endregion

        private bool ProcessWhitespace(TokenInfo tokenInfo)
        {
            int length = 0;
            string text = this.currentLine.Substring(this.currentOffset);
            while (length < text.Length && char.IsWhiteSpace(text[length]))
            {
                ++length;
            }
            if (length > 0)
            {
                this.currentOffset += length;
                if (tokenInfo != null)
                {
                    tokenInfo.EndIndex = this.currentOffset - 1;
                    tokenInfo.Color = (TokenColor)OsloTokenColor.Text;
                    tokenInfo.Type = TokenType.WhiteSpace;
                }
                return true;
            }
            return false;
        }

        private bool ProcessKeyword(TokenInfo tokenInfo)
        {
            int length = 0;
            string text = this.currentLine.Substring(this.currentOffset);
            foreach (var keyword in OsloCodeGeneratorScanner.keywords)
            {
                if (text.StartsWith(keyword) && ((text.Length == keyword.Length) || (text.Length > keyword.Length && !this.IsIdentifierCharacter(text[keyword.Length]))))
                {
                    length = keyword.Length;
                    break;
                }
            }
            if (length > 0)
            {
                if (this.currentOffset == 0 && text.StartsWith("template"))
                {
                    this.enterTemplateOutputMode = true;
                }
                this.currentOffset += length;
                if (tokenInfo != null)
                {
                    tokenInfo.EndIndex = this.currentOffset - 1;
                    tokenInfo.Color = (TokenColor)OsloTokenColor.Keyword;
                    tokenInfo.Type = TokenType.Keyword;
                }
                return true;
            }
            return false;
        }

        private bool ProcessLineComment(TokenInfo tokenInfo)
        {
            string text = this.currentLine.Substring(this.currentOffset);
            if (text.StartsWith("//"))
            {
                this.currentOffset += text.Length;
                if (tokenInfo != null)
                {
                    tokenInfo.EndIndex = this.currentOffset - 1;
                    tokenInfo.Color = (TokenColor)OsloTokenColor.Comment;
                    tokenInfo.Type = TokenType.LineComment;
                }
                return true;
            }
            return false;
        }

        private bool ProcessComment(TokenInfo tokenInfo)
        {
            int length = 0;
            string text = this.currentLine.Substring(this.currentOffset);
            if (this.currentState == OsloCodeGeneratorScannerState.InComment)
            {
                int pos = text.IndexOf("*/");
                if (pos >= 0)
                {
                    length = pos + 2;
                    this.currentState = OsloCodeGeneratorScannerState.None;
                }
                else
                {
                    length = text.Length;
                    this.currentState = OsloCodeGeneratorScannerState.InComment;
                }
            }
            else
            {
                if (text.StartsWith("/*"))
                {
                    int pos = text.Substring(2).IndexOf("*/");
                    if (pos >= 0)
                    {
                        length = pos + 4;
                    }
                    else
                    {
                        length = text.Length;
                        this.currentState = OsloCodeGeneratorScannerState.InComment;
                    }
                }
            }
            if (length > 0)
            {
                this.currentOffset += length;
                if (tokenInfo != null)
                {
                    tokenInfo.EndIndex = this.currentOffset - 1;
                    tokenInfo.Color = (TokenColor)OsloTokenColor.Comment;
                    tokenInfo.Type = TokenType.Comment;
                }
                return true;
            }
            return false;
        }

        private bool ProcessStringLiteral(TokenInfo tokenInfo)
        {
            int length = 0;
            string text = this.currentLine.Substring(this.currentOffset);
            if (text.StartsWith("@\""))
            {
                length = 2;
                while (length < text.Length)
                {
                    if (text[length] == '"' && (length+1 < text.Length && text[length+1] == '"'))
                    {
                        length += 2;
                    }
                    else if (text[length] == '"')
                    {
                        length += 1;
                        break;
                    }
                    else
                    {
                        ++length;
                    }
                }
            }
            else if (text.StartsWith("\""))
            {
                length = 1;
                while (length < text.Length)
                {
                    if (text[length] == '\\')
                    {
                        length += 2;
                    }
                    else if (text[length] == '"')
                    {
                        length += 1;
                        break;
                    }
                    else
                    {
                        ++length;
                    }
                }
            }
            else if (text.StartsWith("'"))
            {
                length = 1;
                while (length < text.Length)
                {
                    if (text[length] == '\\')
                    {
                        length += 2;
                    }
                    else if (text[length] == '\'')
                    {
                        length += 1;
                        break;
                    }
                    else
                    {
                        ++length;
                    }
                }
            }
            if (length > text.Length) length = text.Length;
            if (length > 0)
            {
                this.currentOffset += length;
                if (tokenInfo != null)
                {
                    tokenInfo.EndIndex = this.currentOffset - 1;
                    tokenInfo.Color = (TokenColor)OsloTokenColor.String;
                    tokenInfo.Type = TokenType.String;
                }
                return true;
            }
            return false;
        }

        private bool ProcessIdentifier(TokenInfo tokenInfo)
        {
            int length = 0;
            string text = this.currentLine.Substring(this.currentOffset);
            if (text.Length > 0)
            {
                if (this.IsIdentifierBegin(text[0]))
                {
                    length = 1;
                    while (length < text.Length && this.IsIdentifierCharacter(text[length]))
                    {
                        ++length;
                    }
                }
            }
            if (length > 0)
            {
                this.currentOffset += length;
                if (tokenInfo != null)
                {
                    tokenInfo.EndIndex = this.currentOffset - 1;
                    tokenInfo.Color = (TokenColor)OsloTokenColor.Identifier;
                    tokenInfo.Type = TokenType.Identifier;
                }
                return true;
            }
            return false;
        }

        private bool ProcessNumber(TokenInfo tokenInfo)
        {
            int length = 0;
            string text = this.currentLine.Substring(this.currentOffset);
            if (text.Length > 0)
            {
                if (text.StartsWith("0x"))
                {
                    length = 2;
                    while (length < text.Length && this.IsHexa(text[length]))
                    {
                        ++length;
                    }
                }
                else if (char.IsDigit(text[0]))
                {
                    while (length < text.Length && char.IsDigit(text[length]))
                    {
                        ++length;
                    }
                    if (length < text.Length && text[length] == '.') ++length;
                    while (length < text.Length && char.IsDigit(text[length]))
                    {
                        ++length;
                    }
                    if (length < text.Length && (text[length] == 'e' || text[length] == 'E'))
                    {
                        ++length;
                        if (length < text.Length && (text[length] == '-' || text[length] == '+'))
                        {
                            ++length;
                        }
                    }
                    while (length < text.Length && char.IsDigit(text[length]))
                    {
                        ++length;
                    }
                }
            }
            if (length > 0)
            {
                this.currentOffset += length;
                if (tokenInfo != null)
                {
                    tokenInfo.EndIndex = this.currentOffset - 1;
                    tokenInfo.Color = (TokenColor)OsloTokenColor.Number;
                    tokenInfo.Type = TokenType.Literal;
                }
                return true;
            }
            return false;
        }

        private bool ProcessDelimiter(TokenInfo tokenInfo)
        {
            int length = 0;
            string text = this.currentLine.Substring(this.currentOffset);
            if (text.Length > 0)
            {
                if (text[0] == '[')
                {
                    ++this.bracketCount;
                }
                if (text[0] == ']')
                {
                    --this.bracketCount;
                    if (this.bracketCount < 0 && this.currentState == OsloCodeGeneratorScannerState.InTemplateCommand)
                    {
                        this.currentState = OsloCodeGeneratorScannerState.InTemplateOutput;
                    }
                }
                length = 1;
            }
            if (length > 0)
            {
                this.currentOffset += length;
                if (tokenInfo != null)
                {
                    tokenInfo.EndIndex = this.currentOffset - 1;
                    if (this.currentState == OsloCodeGeneratorScannerState.None ||
                        this.currentState == OsloCodeGeneratorScannerState.InTemplateCommand)
                    {
                        tokenInfo.Color = (TokenColor)OsloTokenColor.Text;
                        tokenInfo.Type = TokenType.Delimiter;
                    }
                    else
                    {
                        tokenInfo.Color = (TokenColor)OsloTokenColor.TemplateText;
                        tokenInfo.Type = TokenType.Text;
                    }
                }
                return true;
            }
            return false;
        }

        private bool ProcessTemplateOutput(TokenInfo tokenInfo)
        {
            if (this.currentLine == "end template")
            {
                this.currentState = OsloCodeGeneratorScannerState.None;
                this.currentOffset += this.currentLine.Length;
                if (tokenInfo != null)
                {
                    tokenInfo.EndIndex = this.currentOffset - 1;
                    tokenInfo.Color = (TokenColor)OsloTokenColor.Keyword;
                    tokenInfo.Type = TokenType.Keyword;
                }
                return true;
            }
            int length = 0;
            string text = this.currentLine.Substring(this.currentOffset);
            if (text.Length > 0)
            {
                int pos = text.IndexOf('[');
                if (pos >= 0)
                {
                    length = pos + 1;
                    this.currentState = OsloCodeGeneratorScannerState.InTemplateCommand;
                    this.bracketCount = 0;
                }
                else
                {
                    length = text.Length;
                }
            }
            if (length > 0)
            {
                this.currentOffset += length;
                if (tokenInfo != null)
                {
                    tokenInfo.EndIndex = this.currentOffset - 1;
                    tokenInfo.Color = (TokenColor)OsloTokenColor.TemplateText;
                    tokenInfo.Type = TokenType.Text;
                }
                return true;
            }
            return false;
        }

        private bool IsHexa(char ch)
        {
            return (ch >= '0' && ch <= '9') || (ch >= 'a' && ch <= 'f') || (ch >= 'A' && ch <= 'F');
        }

        private bool IsIdentifierBegin(char ch)
        {
            return (ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || (ch == '_');
        }

        private bool IsIdentifierCharacter(char ch)
        {
            return (ch >= '0' && ch <= '9') || (ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || (ch == '_');
        }

        internal void ScanLine(ref int state)
        {
            this.ScanTokenAndProvideInfoAboutIt(null, ref state);
        }

        class Token
        {
            public Token(int position, string text, OsloTokenColor color, TokenType type)
            {
                this.Position = position;
                this.Text = text;
                this.Color = (TokenColor)color;
                this.Type = type;
            }

            public int Position { get; private set; }
            public string Text { get; private set; }
            public TokenColor Color { get; private set; }
            public TokenType Type { get; private set; }
        }
    }
}
