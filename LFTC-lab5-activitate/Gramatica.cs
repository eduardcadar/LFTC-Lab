using System.Text;
using System.Linq;

namespace LFTC_lab5_activitate
{
    public class Gramatica
    {
        public char[] Terminali { get; private set; }
        public char[] Neterminali { get; private set; }
        public Dictionary<char, List<string>> Rules { get; private set; }

        public Gramatica(string inputFile)
        {
            Rules = new();
            GenerateFromFile(inputFile);
        }

        private void GenerateFromFile(string inputFile)
        {
            string[] lines = File.ReadAllLines(inputFile);
            HashSet<char> allChars = new();
            foreach (string line in lines)
            {
                if (line.Length < 3 || line[1] != ':')
                    throw new Exception("Wrong line format: " + line);
                if (!Rules.ContainsKey(line[0]))
                    Rules.Add(line[0], new List<string>());
                allChars.Add(line[0]);
                foreach (char ch in line)
                    allChars.Add(ch);
                Rules[line[0]].Add(line.Substring(2));
            }
            allChars.Remove(':');
            Neterminali = Rules.Keys.ToArray();
            Terminali = allChars.Except(Neterminali).ToArray();
        }

        public string[] ReguliRecursive()
        {
            List<string> reguli = new();
            StringBuilder sb = new();
            foreach (char ch in Rules.Keys)
            {
                foreach (string rule in Rules[ch])
                    if (rule.Contains(ch))
                        sb.Append(ch).Append(':').Append(rule);
                reguli.Add(sb.ToString());
                sb.Clear();
            }

            return reguli.ToArray();
        }

        public override string ToString()
        {
            StringBuilder sb = new();

            sb.Append("Neterminali: ");
            foreach (char ch in Neterminali)
                sb.Append(ch);
            sb.AppendLine();
            
            sb.Append("Terminali: ");
            foreach (char ch in Terminali)
                sb.Append(ch);
            sb.AppendLine().AppendLine();

            sb.Append("Reguli de productie").AppendLine();
            foreach (char character in Rules.Keys)
                foreach (string rule in Rules[character])
                    sb.Append(character).Append(':').Append(rule).AppendLine();

            return sb.ToString();
        }
    }
}
