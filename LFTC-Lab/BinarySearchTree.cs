namespace LFTC_Lab
{
    public class Node
    {
        public string Elem { get; set; }

        public Node(string elem)
        {
            Elem = elem;
        }

        public override string ToString()
        {
            return Elem;
        }
    }

    public class BinarySearchTree
    {
        private Node[] Nodes { get; set; }
        public int Length { get; set; }
        private int Size { get; set; }

        public BinarySearchTree()
        {
            Length = 0;
            Size = 4;
            Nodes = new Node[Size];
        }

        public Node Get(int index)
        {
            return Nodes[index];
        }

        public int GetIndex(string elem)
        {
            int i = 0;
            while (i < Size && Nodes[i] != null && !Nodes[i].Elem.Equals(elem))
            {
                if (elem.CompareTo(Nodes[i].Elem) < 0)
                    i = i * 2 + 1;
                else
                    i = i * 2 + 2;
            }
            if (i > Size || Nodes[i] == null)
                return -1;
            return i;
        }

        public void Add(string elem)
        {
            Node node = new(elem);
            int i = 0;
            while (i < Size && Nodes[i] != null)
            {
                if (node.Elem.CompareTo(Nodes[i].Elem) < 0)
                    i = i * 2 + 1;
                else
                    i = i * 2 + 2;
                if (i >= Size)
                    IncreaseSize();
            }
            if (i >= Size)
                IncreaseSize();
            Nodes[i] = node;
            Length++;
        }

        private void IncreaseSize()
        {
            Node[] newNodes = new Node[2 * Size];
            for (int i = 0; i < Size; i++)
                newNodes[i] = Nodes[i];
            Nodes = newNodes;
            Size *= 2;
        }

        public Node[] GetAllNodes()
        {
            return Nodes;
        }
    }
}
