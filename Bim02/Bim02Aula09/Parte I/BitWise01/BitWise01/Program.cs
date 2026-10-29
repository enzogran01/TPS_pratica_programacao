using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitWise01
{
    class Program
    {
        static void Main(string[] args)
        {
            char x = 'A'; // A => 0100 0001 = 41h = 65

            Console.WriteLine("x = " + x + " ou " + (int)x);

            /*
             * x = 0100 0001 
             * 1 = 0000 0001
             *     0000 0001
            */
            if ((x & 1) != 0)
                Console.WriteLine("bit 1 = 1");
            else
                Console.WriteLine("bit 1 = 0");
            /*
             * x = 0100 0001 
             * 2 = 0000 0010
             *     0000 0000
            */
            if ((x & 2) != 0)
                Console.WriteLine("bit 2 = 1");
            else
                Console.WriteLine("bit 2 = 0");
            /*
             * x = 0100 0001 
             * 1 = 0000 0100
             *     0000 0000
            */
            if ((x & 4) != 0)
                Console.WriteLine("bit 3 = 1");
            else
                Console.WriteLine("bit 3 = 0");
            if ((x & 8) != 0)
                Console.WriteLine("bit 4 = 1");
            else
                Console.WriteLine("bit 4 = 0");
            if ((x & 16) != 0)
                Console.WriteLine("bit 5 = 1");
            else
                Console.WriteLine("bit 5 = 0");
            if ((x & 32) != 0)
                Console.WriteLine("bit 6 = 1");
            else
                Console.WriteLine("bit 6 = 0");
            if ((x & 64) != 0)
                Console.WriteLine("bit 7 = 1");
            else
                Console.WriteLine("bit 7 = 0");
            if ((x & 128) != 0)
                Console.WriteLine("bit 8 = 1");
            else
                Console.WriteLine("bit 8 = 0");
            Console.ReadKey();
        }
    }
}
