using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitWise04
{
    class Program
    {
        public static void mostraBits(int _x)
        {
            int aux = 1;

            for (int i = 0; i < 8; ++i)
            {
                if ((_x & aux) != 0)
                    Console.WriteLine("bit " + (i + 1) + " = 1");
                else
                    Console.WriteLine("bit " + (i + 1) + " = 0");
                aux = aux << 1;
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Argumento 65");
            mostraBits(65);
            Console.WriteLine("Argumento not(65) ou ~65");
            mostraBits(~65);
            Console.ReadKey();
        }
    }
}
