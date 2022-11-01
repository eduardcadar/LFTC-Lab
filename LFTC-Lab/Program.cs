namespace LFTC_Lab
{
    public class Program
    {
        public static void Main()
        {
            string inputCodeFilePath = @"C:\facultate\Semestrul 5\LFTC\LFTC-Lab\LFTC-Lab\textFiles\inputCode.txt";
            string FIPPath = @"C:\facultate\Semestrul 5\LFTC\LFTC-Lab\LFTC-Lab\textFiles\FIP.txt";
            string TSPath = @"C:\facultate\Semestrul 5\LFTC\LFTC-Lab\LFTC-Lab\textFiles\TS.txt";
            string[] code = File.ReadAllLines(inputCodeFilePath);
            
            AnalizorLexical analizorLexical = new();
            try
            {
                var atoms = analizorLexical.SeparateAtoms(code);
                foreach (var atom in atoms)
                    Console.WriteLine(atom);

                var TS = new BinarySearchTree();
                var FIP = analizorLexical.GetFIP(atoms, TS);

                foreach (var fipItem in FIP)
                    Console.WriteLine(fipItem);

                var nodes = TS.GetAllNodes();
                for (int i = 0; i < nodes.Length; i++)
                    if (nodes[i] != null)
                        Console.WriteLine(i + " " + nodes[i]);

                WriteFIPToFile(FIPPath, FIP);
                File.WriteAllText(TSPath, TS.ToString());
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void WriteFIPToFile(string filePath, List<ItemFIP> FIP)
        {
            File.WriteAllText(filePath, "");
            foreach (ItemFIP item in FIP)
                File.AppendAllText(filePath, item.ToString() + Environment.NewLine);
        }
    }
}