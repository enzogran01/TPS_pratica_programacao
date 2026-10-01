using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace ServidorRecebe
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket socketreceber = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
            EndPoint endereco = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9060);
            byte[] data = new byte[1024];
            int qtdbytes;

            socketreceber.Bind(endereco);

            qtdbytes = socketreceber.ReceiveFrom(data, ref endereco); // recebe chave
            int key = int.Parse(Encoding.ASCII.GetString(data, 0, qtdbytes));

            qtdbytes = socketreceber.ReceiveFrom(data, ref endereco); // recebe nome
            String filename = Encoding.ASCII.GetString(data, 0, qtdbytes);

            FileStream outfile = new FileStream(filename, FileMode.Create, FileAccess.Write);

            List<byte> cifrado = new List<byte>();
            List<byte> decifrado = new List<byte>();

            do
            {
                qtdbytes = socketreceber.ReceiveFrom(data, ref endereco); // recebe até 10 bytes
                for (int i = 0; i < qtdbytes; ++i)
                {
                    byte original = (byte)(data[i] - 18 - key); // descriptografa
                    cifrado.Add(data[i]);
                    decifrado.Add(original);
                    outfile.WriteByte(original);
                }
            } while (qtdbytes == 10); // pacote menor que 10 = fim

            outfile.Close();
            socketreceber.Close();

            Console.WriteLine("Chave: " + key);
            Console.WriteLine();
            Console.WriteLine("criptografado: ");
            Console.WriteLine(BitConverter.ToString(cifrado.ToArray()));
            Console.WriteLine();
            Console.WriteLine("descriptografado: ");
            Console.WriteLine(Encoding.UTF8.GetString(decifrado.ToArray()));

            Console.ReadKey();
        }
    }
}