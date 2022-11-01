using LFTC_Automat;

namespace LFTC_Lab
{
    public class Automate
    {
        public Automat AutomatConstNumber { get; }
        public Automat AutomatID { get; }

        public Automate(string automatNumberFilePath, string automatIDFilePath)
        {
            AutomatConstNumber = new();
            string[] automatNrDesc = File.ReadAllLines(automatNumberFilePath);
            AutomatConstNumber.InitializeWithListOfTransitions(automatNrDesc);
            AutomatID = new();
            string[] automatIDDesc = File.ReadAllLines(automatIDFilePath);
            AutomatID.InitializeWithListOfTransitions(automatIDDesc);
        }
    }
}
