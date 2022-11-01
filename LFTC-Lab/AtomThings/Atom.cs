namespace LFTC_Lab.AtomThings
{
    public class Atom
    {
        public string AtomText { get; set; }
        public AtomType AtomType { get; set; }
        public int Line { get; set; }
        public int Column { get; set; }

        public Atom(string atomText)
        {
            AtomText = atomText;
        }

        public Atom(string atomText, AtomType atomType, int line, int column)
        {
            AtomText = atomText;
            AtomType = atomType;
            Line = line;
            Column = column;
        }

        public override string ToString()
        {
            return AtomText + " -> " + AtomType + " on line " + Line + ", column " + Column;
        }
    }
}
