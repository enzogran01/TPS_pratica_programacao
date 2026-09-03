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
        static void Main(string[] args)
        {
            DateTime inicio = DateTime.Now;
            Parallel.Invoke(
                () => processo(1, 5000),
                () => processo(2, 4000),
                () => processo(3, 3000)
            );
            processo(6, 1000);
            DateTime fim = DateTime.Now;
            Console.WriteLine("Tempo = " + (fim - inicio));
            Console.ReadKey();
        }
        public static void processo(int _n, int _tempo)
        {
            Console.WriteLine("Iniciando Processo " + _n);
            Thread.Sleep(_tempo);
            Console.WriteLine("Finalizando Processo " + _n);
            if (_n == 1) processo(4, 3000);
            if (_n == 2) processo(5, 5000);
        }
    }
}
