using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitWise05
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
            String bit;
            int numero = 65;
            int aux = 1;

            Console.WriteLine("Argumento 65");
            mostraBits(numero);

            Console.WriteLine("Qual o bit você deseja setar em 1?");
            bit = Console.ReadLine();

            aux = aux << (int.Parse(bit) - 1);
            numero = numero | aux;

            // numero = 0100 0001
            // aux    = 0000 1000
            //          0100 1001

            Console.WriteLine("Valor final...");
            mostraBits(numero);

            aux = 1;
            Console.WriteLine("E agora, qual o bit você deseja desligar?");
            bit = Console.ReadLine();

            aux = aux << (int.Parse(bit) - 1);
            numero = numero & ~aux;

            Console.WriteLine("Valor final...");
            mostraBits(numero);

            Console.ReadKey();
        }
    }
}
