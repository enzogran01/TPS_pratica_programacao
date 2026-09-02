using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Primos
{
    class Program
    {
        static int k = 0;
        public static bool isPrimo(int x)
        {
            int f = x/2 + 1;
            for (int i=2;i<f;++i)
                if (x%i == 0) return false;
            return true;
        }
        static void Main(string[] args)
        {
            DateTime inicio = DateTime.Now;
            Parallel.Invoke(
                () => processo(1, 700000),
                () => processo(700001, 1000000)
            );
            DateTime fim = DateTime.Now;
            Console.WriteLine("Foram encontrados " + k + " numeros primos.");
            Console.WriteLine("Tempo = " + (fim - inicio));
            Console.ReadKey();
        }
        public static void processo(int _inicio, int _fim)
        {
            for (int i = _inicio; i < _fim; ++i)
                if (isPrimo(i)) ++k;
        }
    }
}
