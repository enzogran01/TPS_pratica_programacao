using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Primos
{
    class Program
    {
        public static bool isPrimo(int x)
        {
            int f = x/2 + 1;
            for (int i=2;i<f;++i)
                if (x%i == 0) return false;
            return true;
        }

        static void Main(string[] args)
        {
            DateTime inicio, fim;
            int k=0;

            inicio = DateTime.Now;
            for (int i = 1; i < 100000; ++i)
                if (isPrimo(i)) ++k;
            fim = DateTime.Now;
            
            Console.WriteLine("Foram encontrados "+k+" numeros primos.");
            Console.WriteLine("Tempo = " + (fim-inicio));
            Console.ReadKey();
        }

    }
}
