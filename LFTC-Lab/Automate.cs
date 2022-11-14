using LFTC_Automat;

namespace LFTC_Lab
{
    public class Automate
    {
        public Automat AutomatIntNumber { get; }
        public Automat AutomatRealNumber { get; }
        public Automat AutomatID { get; }

        public Automate(string automatIntNumberFilePath, string automatRealNumberFilePath, string automatIDFilePath)
        {
            AutomatIntNumber = new();
            string[] automatIntNrDesc = File.ReadAllLines(automatIntNumberFilePath);
            AutomatIntNumber.InitializeWithListOfTransitions(automatIntNrDesc);
            AutomatRealNumber = new();
            string[] automatRealNrDesc = File.ReadAllLines(automatRealNumberFilePath);
            AutomatRealNumber.InitializeWithListOfTransitions(automatRealNrDesc);
            AutomatID = new();
            string[] automatIDDesc = File.ReadAllLines(automatIDFilePath);
            AutomatID.InitializeWithListOfTransitions(automatIDDesc);
        }
    }
}
