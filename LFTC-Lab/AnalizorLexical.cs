using System.Text;
using LFTC_Lab.AtomThings;

namespace LFTC_Lab
{
    public class AnalizorLexical
    {
        public readonly List<string> LexicalAtoms = new()
        {
            "ID", "CONST", "double", "string", "struct", "Console.WriteLine", "Console.ReadLine",
            "+", "-", "*", "/", "=", "<", ">", "<=", ">=", "==", "!=", "true", "false", "||", "&&",
            "if", "while", ";", ",", "(", ")", "{", "}"
        };

        private readonly List<AtomType> Symbols = new()
        {
            AtomType.CONST, AtomType.ID
        };

        public List<Atom> SeparateAtoms(string[] code)
        {
            List<Atom> atoms = new();
            StringBuilder sb = new();
            bool inQuote = false;
            int lineNumber = 0;

            foreach (string line in code)
            {
                int column = 0;
                foreach (char c in line)
                {
                    // daca nu e delimitator (toti delimitatorii sunt formati dintr-un singur caracter)
                    if (!c.IsDelimiter())
                        sb.Append(c);
                    else
                    {
                        if (inQuote)
                        {
                            sb.Append(c);
                            if (c.IsQuoteMark())
                            {
                                inQuote = false;
                                var atomText = sb.ToString();
                                var atom = new Atom(atomText,
                                    IdentifyAtomType(atomText, lineNumber, column), lineNumber, column);
                                if (atom.AtomType.Equals(AtomType.ID))
                                    CheckIDLength(atomText, lineNumber, column);
                                atoms.Add(atom);
                                column += sb.Length;
                                sb.Clear();
                            }
                        }
                        else
                        {
                            if (c.IsQuoteMark())
                            {
                                inQuote = true;
                                sb.Append(c);
                            }
                            else
                            {
                                if (sb.Length > 0)
                                {
                                    var atomText = sb.ToString();
                                    var atom = new Atom(atomText,
                                        IdentifyAtomType(atomText, lineNumber, column), lineNumber, column);
                                    if (atom.AtomType.Equals(AtomType.ID))
                                        CheckIDLength(atomText, lineNumber, column);
                                    atoms.Add(atom);
                                    column += sb.Length;
                                    sb.Clear();
                                }
                                if (c.IsSpace())
                                {
                                    column++;
                                }
                                else
                                {
                                    sb.Append(c);
                                    var atomText = sb.ToString();
                                    var atom = new Atom(atomText,
                                        IdentifyAtomType(atomText, lineNumber, column), lineNumber, column);
                                    if (atom.AtomType.Equals(AtomType.ID))
                                        CheckIDLength(atomText, lineNumber, column);
                                    atoms.Add(atom);
                                    column += sb.Length;
                                    sb.Clear();
                                }
                            }
                        }
                    }
                }
                if (sb.Length > 0)
                {
                    var atomText = sb.ToString();
                    var atom = new Atom(atomText,
                        IdentifyAtomType(atomText, lineNumber, column), lineNumber, column);
                    if (atom.AtomType.Equals(AtomType.ID))
                        CheckIDLength(atomText, lineNumber, column);
                    atoms.Add(atom);
                    sb.Clear();
                }
                lineNumber++;
            }
            if (sb.Length > 0)
                atoms.Add(new Atom(sb.ToString()));

            return atoms;
        }

        private static void CheckIDLength(string id, int line, int column)
        {
            if (id.Length > 250)
                throw new ArgumentOutOfRangeException(
                    id, "ID too long on line " + line + ", column " + column);
        }

        public List<ItemFIP> GetFIP(List<Atom> atoms, BinarySearchTree TS)
        {
            List<ItemFIP> items = new();
            foreach (Atom atom in atoms)
            {
                ItemFIP item = new();
                if (Symbols.Contains(atom.AtomType))
                {
                    item.TypeID = LexicalAtoms.IndexOf(atom.AtomType.ToString());
                    int idx = TS.GetIndex(atom.AtomText);
                    if (idx < 0)
                        TS.Add(atom.AtomText);
                    //item.SymbolID = 0;
                }
                else
                {
                    item.TypeID = LexicalAtoms.IndexOf(atom.AtomText);
                }
                items.Add(item);
            }
            for (int i = 0; i < atoms.Count; i++)
                if (Symbols.Contains(atoms[i].AtomType))
                    items[i].SymbolID = TS.GetIndex(atoms[i].AtomText);

            return items;
        }

        private static AtomType IdentifyAtomType(string atomText, int line, int column)
        {
            if (atomText.IsDelimiter()) return AtomType.DELIMITER;
            if (atomText.IsKeyWord()) return AtomType.KEYWORD;
            if (atomText.IsID()) return AtomType.ID;
            if (atomText.IsConst()) return AtomType.CONST;
            if (atomText.IsOperator()) return AtomType.OPERATOR;

            throw new ArgumentOutOfRangeException(
                atomText, "Invalid atom on line " + line + ", column " + column);
        }
    }
}
