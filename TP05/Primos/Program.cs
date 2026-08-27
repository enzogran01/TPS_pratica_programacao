using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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
            Thread thread01 = new Thread(new ThreadStart(()=>processo(1,700000)));
            Thread thread02 = new Thread(new ThreadStart(()=>processo(700001,1000000)));
            thread01.Start();
            thread02.Start();
            thread01.Join();
            thread02.Join();
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
