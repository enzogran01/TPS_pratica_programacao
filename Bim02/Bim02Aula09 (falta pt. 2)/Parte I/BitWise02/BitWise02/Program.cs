using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitWise02
{
    class Program
    {
        static void Main(string[] args)
        {
            char x = 'A';
            byte aux;

            Console.WriteLine("x = " + x + " ou " + (int)x);

            for (int i = 0; i < 8; ++i)
            {
                aux = (byte)(Math.Pow(2, i));
                if ((x & aux) != 0)
                    Console.WriteLine("bit " + (i + 1) + " = 1");
                else
                    Console.WriteLine("bit " + (i + 1) + " = 0");
            }
            Console.ReadKey();
        }
    }
}
