namespace LFTC_Automat
{
    public class Program
    {
        public static bool ReadFromFile = true;
        public static bool ReadTable = false;
        public const string FilePathTable = @"C:\facultate\Semestrul 5\LFTC\LFTC-Lab\LFTC-Automat\automatTabel.txt";
        public const string FilePathList = @"C:\facultate\Semestrul 5\LFTC\LFTC-Lab\LFTC-Automat\automatLista.txt";
        public const string IntCppFilePathList = @"C:\facultate\Semestrul 5\LFTC\LFTC-Lab\LFTC-Automat\automatListaIntCpp.txt";

        public static void Main(string[] args)
        {
            string[] automatDescription;
            if (ReadFromFile)
                try
                {
                    //automatDescription = File.ReadAllLines(FilePathTable);
                    //automatDescription = File.ReadAllLines(FilePathList);
                    automatDescription = File.ReadAllLines(IntCppFilePathList);
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
            else
                automatDescription = ReadAutomatDescriptionFromConsole();

            Automat automat = new();
            try
            {
                if (ReadTable)
                    automat.InitializeWithTable(automatDescription);
                else
                    automat.InitializeWithListOfTransitions(automatDescription);
                Console.WriteLine(automat);
                while (true)
                {
                    Console.Write("Sequence: ");
                    string? sequence = Console.ReadLine();
                    try
                    {
                        if (sequence == null)
                            throw new ArgumentException("Invalid sequence");
                        bool valid = automat.Process(sequence);
                        if (valid)
                            Console.WriteLine("Valid sequence");
                        else
                        {
                            Console.WriteLine("Invalid sequence");
                            string longestValidPrefix = automat.LongestValidPrefix(sequence);
                            if (longestValidPrefix.Length > 0)
                                Console.WriteLine("Longest valid prefix is: " + longestValidPrefix);
                        }
                    } catch (ArgumentException e)
                    {
                        Console.WriteLine(e.Message);
                    } finally
                    {
                        Console.WriteLine();
                    }
                }
            } catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static string[] ReadAutomatDescriptionFromConsole()
        {
            string[] automatDescription;
            Console.Write("Number of lines: ");
            string? input = Console.ReadLine();
            int noLines;
            if (input != null)
                noLines = int.Parse(input);
            else
                throw new ArgumentException("Invalid number of lines");
            automatDescription = new string[noLines];
            for (int i = 0; i < noLines; i++)
            {
                input = Console.ReadLine();
                if (input != null)
                    automatDescription[i] = input;
                else
                    throw new ArgumentException("Invalid input");
            }
            return automatDescription;
        }
    }
}