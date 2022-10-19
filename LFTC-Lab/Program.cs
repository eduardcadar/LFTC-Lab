namespace LFTC_Lab
{
    internal class Program
    {
        static void Main()
        {
            string filePath = @"C:\facultate\Semestrul 5\LFTC\LFTC-Lab\LFTC-Lab\inputCode.txt";
            string[] code = File.ReadAllLines(filePath);
            
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

            } catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}