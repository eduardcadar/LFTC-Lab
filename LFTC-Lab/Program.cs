namespace LFTC_Lab
{
    internal class Program
    {
        static void Main()
        {
            var code = @"double a, b, a3, =<, 3., 0.2, 3.5, -4.3, -0.2, -2, -0;
string c;
struct Point {
    double x, y;
}
a = 2 + 3;
Console.ReadLine(c);
if (c == ""a"") {
    Console.WriteLine(""you wrote a"");
}
else {
    Console.WriteLine(""you wrote "" + c);
}
while (a > 0) {
    Console.WriteLine(a + ""\n"");
    a = a - 1;
}";
            Console.WriteLine(code);
            Console.WriteLine();
            var atoms = AnalizorLexical.SeparateAtoms(code);
            foreach (var atom in atoms)
                Console.WriteLine(atom);
        }

        public static void Perimetru_arie_cerc()
        {
            double r, P, A;
            Console.Write("raza = ");
            r = Convert.ToInt32(Console.ReadLine());
            P = 2 * 3.14 * r;
            A = 3.14 * r * r;
        }

        public static void Cmmdc()
        {
            double a, b, cmmdc;
            Console.Write("a = ");
            a = Convert.ToInt32(Console.ReadLine());
            Console.Write("b = ");
            b = Convert.ToInt32(Console.ReadLine());
            while (a != b)
            {
                if (a > b)
                {
                    a = a - b;
                }
                else
                {
                    b = b - a;
                }
            }
            cmmdc = a;
        }

        public static void Suma()
        {
            double a, n, s;
            Console.Write("n = ");
            n = Convert.ToInt32(Console.ReadLine());
            s = 0;
            while (n > 0)
            {
                a = Convert.ToInt32(Console.ReadLine());
                s = s + a;
                n = n - 1;
            }
        }

        public static void FaraErori()
        {
            double a = 15;
            Console.WriteLine(a);
            a--;
        }
    }
}