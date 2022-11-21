using System.Text.RegularExpressions;

namespace LFTC_Lab.AtomThings
{
    public static class AtomUtils
    {
        public readonly static string AutomatIDFilePath =
            @"C:\facultate\Semestrul 5\LFTC\LFTC-Lab\LFTC-Lab\textFiles\automatID.txt";
        public readonly static string AutomatIntNumberFilePath =
            @"C:\facultate\Semestrul 5\LFTC\LFTC-Lab\LFTC-Lab\textFiles\automatIntNumbers.txt";
        public readonly static string AutomatRealNumberFilePath =
            @"C:\facultate\Semestrul 5\LFTC\LFTC-Lab\LFTC-Lab\textFiles\automatRealNumbers.txt";
        public readonly static Automate Automate = new(AutomatIntNumberFilePath, AutomatRealNumberFilePath, AutomatIDFilePath);

        public static readonly List<char> Delimiters = new()
        {
            ';', ' ', '"', '{', '}', '(', ')', ','
        };

        public static readonly List<char> OperatorParts = new()
        {
            '+', '-', '*', '/', '<', '>', '=', '!', '|', '&'
        };

        public static readonly List<string> Keywords = new()
        {
            "int", "double", "string", "struct", "if", "else", "while",
            "Console.ReadLine", "Console.WriteLine"
        };

        public static readonly List<string> Operators = new()
        {
            "+", "-", "*", "/", "<", ">", "=",
            "<=", ">=", "==", "!=", "||", "&&"
        };

        public static bool IsDelimiter(this char c) => Delimiters.Contains(c);

        public static bool IsOperatorPart(this char c) => OperatorParts.Contains(c);

        public static bool IsSpace(this char c) => c == ' ';

        public static bool IsQuoteMark(this char c) => c == '"';

        public static bool IsDelimiter(this string text) => text.Length == 1 && text[0].IsDelimiter();

        public static bool IsKeyWord(this string text) => Keywords.Contains(text);

        public static bool IsID(this string text) => Automate.AutomatID.Process(text);

        public static bool IsConst(this string text) => text.IsConstNumber() || text.IsConstText();

        public static bool IsConstNumber(this string text) => text.IsIntNumber() || text.IsRealNumber();

        public static bool IsIntNumber(this string text) => Automate.AutomatIntNumber.Process(text);

        public static bool IsRealNumber(this string text) => Automate.AutomatRealNumber.Process(text);

        public static bool IsConstText(this string text)
        {
            if (text[0] != '"' || text[text.Length - 1] != '"')
                return false;
            for (int i = 1; i < text.Length - 1; i++)
                if (text[i] == '"')
                    return false;
            return true;
        }

        public static bool IsOperator(this string text) => Operators.Contains(text);
    }
}
