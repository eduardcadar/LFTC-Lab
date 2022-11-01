using System.Text;

namespace LFTC_Automat
{
    public class Automat
    {
        public List<string> Alphabet { get; set; }
        public List<string> States { get; set; }
        public string InitialState { get; set; }
        public Dictionary<string, bool> IsFinish { get; set; }
        public Dictionary<string, Dictionary<string, string>> Transitions { get; set; }

        public Automat()
        {
            Transitions = new();
            IsFinish = new();
            InitialState = string.Empty;
            States = new();
            Alphabet = new();
        }

        public void InitializeWithListOfTransitions(string[] automatDescription)
        {
            string[] states = automatDescription[0].Split(' ');
            InitialState = states[0];
            for (int i = 1; i < states.Length; i++)
            {
                IsFinish[states[i]] = true;
                States.Add(states[i]);
            }
            for (int i = 1; i < automatDescription.Length; i++)
            {
                string[] transition = automatDescription[i].Split(' ');
                string src = transition[0], element = transition[1], dest = transition[2];
                if (transition.Length != 3)
                    throw new ArgumentException("Invalid transition: " + automatDescription[i]);
                if (!IsFinish.ContainsKey(src))
                {
                    States.Add(src);
                    IsFinish[src] = false;
                }
                if (!Alphabet.Contains(element))
                    Alphabet.Add(element);
                if (!IsFinish.ContainsKey(dest))
                {
                    States.Add(dest);
                    IsFinish[dest] = false;
                }
                if (!Transitions.ContainsKey(src))
                    Transitions[src] = new();
                if (Transitions[src].ContainsKey(element))
                    throw new ArgumentException("Not determinist");
                Transitions[src][element] = dest;
            }
        }

        public void InitializeWithTable(string[] automatDescription)
        {
            Alphabet.AddRange(automatDescription[0].Split());
            InitialState = string.Empty;
            for (int i = 1; i < automatDescription.Length; i++)
            {
                string[] elements = automatDescription[i].Split(' ');
                string state = elements[0];
                States.Add(state);
                if (i == 1)
                    InitialState = state;
                IsFinish[state] = elements.Last() == "1";
                for (int j = 1; j < elements.Length - 1; j++)
                {
                    if (!Transitions.ContainsKey(state))
                        Transitions[state] = new Dictionary<string, string>();
                    Transitions[state][Alphabet[j - 1]] = elements[j];
                }
            }
        }

        public bool Process(string sequence)
        {
            string state = InitialState;
            foreach (char c in sequence)
            {
                if (!Alphabet.Contains(c.ToString()))
                    return false;
                    //throw new ArgumentException($"{c} not in alphabet");
                if (!Transitions[state].ContainsKey(c.ToString()))
                    return false;
                state = Transitions[state][c.ToString()];
            }
            return IsFinish[state];
        }

        public string LongestValidPrefix(string sequence)
        {
            string state = InitialState;
            int length = -1;
            for (int i = 0; i < sequence.Length; i++)
            {
                if (!Transitions[state].ContainsKey(sequence[i].ToString()))
                    break;
                state = Transitions[state][sequence[i].ToString()];
                if (IsFinish[state])
                    length = i + 1;
            }
            if (length == -1)
                return string.Empty;
            return sequence.Substring(0, length);
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.Append("States: ");
            foreach (string s in States)
                sb.Append(s + " ");

            sb.Append(Environment.NewLine + "Alphabet: ");
            foreach (string s in Alphabet)
                sb.Append(s + " ");

            sb.AppendLine(Environment.NewLine + "Transitions:");
            foreach (var kv in Transitions)
                foreach (var v in kv.Value)
                    sb.AppendLine(kv.Key + ", " + v.Key + " -> " + v.Value);

            sb.Append(Environment.NewLine + "Final states: ");
            foreach (var kv in IsFinish)
                if (kv.Value)
                    sb.Append(kv.Key + " ");

            return sb.ToString();
        }
    }
}
