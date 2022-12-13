using LFTC_lab5_activitate;

string inputFile = @"C:\facultate\Semestrul 5\LFTC\LFTC-Lab\LFTC-lab5-activitate\gramatica.txt";
Gramatica gramatica = new(inputFile);

Console.WriteLine(gramatica);

Console.WriteLine("Reguli recursive");
foreach (string rule in gramatica.ReguliRecursive())
    Console.WriteLine(rule);
