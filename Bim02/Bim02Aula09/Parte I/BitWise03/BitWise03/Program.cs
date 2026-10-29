using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitWise03
{
    class Program
    {
        static void Main(string[] args)
        {
            char x = 'A';
            int aux = 1;    // 1 = 0000 0001

            // aux = 0000 0001
            // aux = 0000 0010
            // aux = 0000 0100
            // peteleco = shift left
            // shift left em C# é <<

            Console.WriteLine("x = " + x + " ou " + (int)x);

            for (int i = 0; i < 8; ++i)
            {
                if ((x & aux) != 0)
                    Console.WriteLine("bit " + (i + 1) + " = 1");
                else
                    Console.WriteLine("bit " + (i + 1) + " = 0");
                aux = aux << 1;
            }
            Console.ReadKey();
        }
    }
}
