using System.Text.RegularExpressions;

namespace LFTC_Lab
{
    public static class AtomUtils
    {
        public static List<char> Delimiters = new()
        {
            ';', ' ', '"', '{', '}', '(', ')', ','
        };

        public static List<string> Keywords = new()
        {
            "double", "string", "struct", "if", "else", "while",
            "Console.ReadLine", "Console.WriteLine"
        };

        public static List<string> Operators = new()
        {
            "+", "-", "*", "/", "<", ">", "=",
            "<=", ">=", "==", "!=", "||", "&&"
        };

        public static bool IsDelimiter(this char c)
        {
            return Delimiters.Contains(c);
        }

        public static bool IsSpace(this char c)
        {
            return c == ' ';
        }

        public static bool IsQuoteMark(this char c)
        {
            return c == '"';
        }

        public static bool IsDelimiter(this string text)
        {
            return text.Length == 1 && text[0].IsDelimiter();
        }

        public static bool IsKeyWord(this string text)
        {
            return Keywords.Contains(text);
        }

        public static bool IsID(this string text)
        {
            return Regex.IsMatch(text, @"^[a-zA-Z]+$");
        }

        public static bool IsConstNumber(this string text)
        {
            return Regex.IsMatch(text, @"^[+-]?(0|([1-9][0-9]*))(\.[0-9]+)?$");
        }

        public static bool IsConstText(this string text)
        {
            return Regex.IsMatch(text, @"^""[^""]*""$");
        }

        public static bool IsOperator(this string text)
        {
            return Operators.Contains(text);
        }

        public static bool IsValidAtom(this string atom)
        {
            return true;
        }
    }
}
