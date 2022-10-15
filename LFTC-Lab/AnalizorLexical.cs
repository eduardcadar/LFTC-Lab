using System.Text.RegularExpressions;
using System.Text;

namespace LFTC_Lab
{
    public class AnalizorLexical
    {
        public static List<Atom> SeparateAtoms(string code)
        {
            code = Regex.Replace(code, @"\s+", " ").Replace(System.Environment.NewLine, " ");
            List<Atom> atoms = new();
            StringBuilder sb = new();
            bool inQuote = false;

            foreach (char c in code)
            {
                if (c.IsDelimiter())
                {
                    if (inQuote)
                    {
                        sb.Append(c);
                        if (c.IsQuoteMark())
                        {
                            inQuote = false;
                            atoms.Add(new Atom(sb.ToString()));
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
                                atoms.Add(new Atom(sb.ToString()));
                                sb.Clear();
                            }
                            if (!c.IsSpace())
                                atoms.Add(new Atom(c.ToString()));
                        }
                    }
                }
                else
                    sb.Append(c);
            }
            if (sb.Length > 0)
                atoms.Add(new Atom(sb.ToString()));

            foreach (Atom atom in atoms)
                atom.AtomType = IdentifyAtomType(atom);

            return atoms;
        }

        public static AtomType IdentifyAtomType(Atom atom)
        {
            if (atom.AtomText.IsDelimiter())
                return AtomType.DELIMITER;
            if (atom.AtomText.IsKeyWord())
                return AtomType.KEYWORD;
            if (atom.AtomText.IsID())
                return AtomType.ID;
            if (atom.AtomText.IsConstNumber())
                return AtomType.CONSTNUMBER;
            if (atom.AtomText.IsConstText())
                return AtomType.CONSTTEXT;
            if (atom.AtomText.IsOperator())
                return AtomType.OPERATOR;
            return AtomType.INVALID;
        }
    }
}
