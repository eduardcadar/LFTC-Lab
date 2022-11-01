namespace LFTC_Automat
{
    public class Program
    {
        public static bool ReadFromFile = true;
        public static bool ReadTable = false;
        public const string FilePathTable = @"C:\facultate\Semestrul 5\LFTC\LFTC-Lab\LFTC-Automat\automatTabel.txt";
        public const string FilePathList = @"C:\facultate\Semestrul 5\LFTC\LFTC-Lab\LFTC-Automat\automatLista.txt";
        public const string IntCppFilePathList = @"C:\facultate\Semestrul 5\LFTC\LFTC-Lab\LFTC-Automat\automatListaIntCpp.txt";
        public const string LabPathList = @"C:\facultate\Semestrul 5\LFTC\LFTC-Lab\LFTC-Automat\automatLab.txt";
        public static readonly List<string> Options = new()
        {
            "1", "2", "3", "4", "5", "6", "7"
        };

        public static void Main(string[] args)
        {
            string[] automatDescription;
            if (ReadFromFile)
                try
                {
                    //automatDescription = File.ReadAllLines(FilePathTable);
                    //automatDescription = File.ReadAllLines(FilePathList);
                    automatDescription = File.ReadAllLines(IntCppFilePathList);
                    //automatDescription = File.ReadAllLines(LabPathList);
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
                while (true)
                {
                    Console.Write(@"1. Show automatum
2. Verify sequence
3. Longest valid prefix" + Environment.NewLine);
                    Console.Write("Write your option: ");
                    string? optionString = Console.ReadLine();
                    if (optionString == null)
                    {
                        Console.WriteLine("Wrong input");
                        continue;
                    }
                    string? sequence;
                    switch (optionString)
                    {
                        case "1":
                            Console.WriteLine(automat);
                            break;
                        case "2":
                            Console.Write("Sequence: ");
                            sequence = Console.ReadLine();
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
                                }
                            }
                            catch (ArgumentException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            finally { Console.WriteLine(); }
                            break;
                        case "3":
                            Console.Write("Sequence: ");
                            sequence = Console.ReadLine();
                            try
                            {
                                if (sequence == null)
                                    throw new ArgumentException("Invalid sequence");
                                string longestValidPrefix = automat.LongestValidPrefix(sequence);
                                if (longestValidPrefix.Length > 0)
                                    Console.WriteLine("Longest valid prefix is: " + longestValidPrefix);
                                else
                                    Console.WriteLine("No valid prefix");
                            }
                            catch (ArgumentException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            finally { Console.WriteLine(); }
                            break;
                        default:
                            Console.WriteLine("Wrong input");
                            break;
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