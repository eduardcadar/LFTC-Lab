namespace LFTC_Lab
{
    public class Atom
    {
        public string AtomText { get; set; }
        public AtomType AtomType { get; set; }

        public Atom(string atomText)
        {
            AtomText = atomText;
        }

        public Atom(string atomText, AtomType atomType)
        {
            AtomText = atomText;
            AtomType = atomType;
        }

        public override string ToString()
        {
            return AtomType.ToString() + " -> " + AtomText;
        }
    }
}
