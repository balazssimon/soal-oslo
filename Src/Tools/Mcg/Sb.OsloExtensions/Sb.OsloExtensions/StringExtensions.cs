using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OsloExtensions
{
    public static class StringExtensions
    {
        public static string ConcatWithDelimiter(this IEnumerable<string> values, string delimiter)
        {
            StringBuilder sb = new StringBuilder();
            bool first = true;
            foreach (var value in values)
            {
                if (!first) sb.Append(delimiter);
                sb.Append(value);
                first = false;
            }
            return sb.ToString();
        }

        public static string ToCamelCase(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return value.Substring(0, 1).ToLower() + value.Substring(1);
            }
            else
            {
                return value;
            }
        }

        public static string ToPascalCase(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return value.Substring(0, 1).ToUpper() + value.Substring(1);
            }
            else
            {
                return value;
            }
        }

        public static string EscapeString(this string text)
        {
            StringBuilder sb = new StringBuilder(text.Length * 2);
            foreach (char ch in text)
            {
                if (ch == '\\' || ch == '"')
                {
                    sb.Append('\\');
                }
                sb.Append(ch);
            }
            return sb.ToString();
        }

        public static string TrimNewLineCharacters(this string text)
        {
            int length = text.Length;
            while (length > 0 && StringExtensions.IsNewLineChar(text[length - 1])) --length;
            return text.Substring(0, length);
        }

        public static bool IsNewLineChar(char ch)
        {
            return ch == '\u000A' || ch == '\u000D' || ch == '\u0085' || ch == '\u2028' || ch == '\u2029';
        }

    }
}
