using System;
using System.Collections.Generic;
using System.Linq;
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
            String filename;
            // ===== INCLUI =====
            int chave;
            String recebido = "";
            String descriptografado = "";
            // =====

            socketreceber.Bind(endereco);
            qtdbytes = socketreceber.ReceiveFrom(data, ref endereco);
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, qtdbytes));
            filename = Encoding.ASCII.GetString(data, 0, qtdbytes);

            // ===== INCLUI =====
            qtdbytes = socketreceber.ReceiveFrom(data, ref endereco);
            chave = int.Parse(Encoding.ASCII.GetString(data, 0, qtdbytes));
            // =====

            FileStream outfile;

            outfile = new System.IO.FileStream(filename,
                                               System.IO.FileMode.Create,
                                               System.IO.FileAccess.Write);

            // ===== INCLUI =====
            do
            {
                // =====
                qtdbytes = socketreceber.ReceiveFrom(data, ref endereco);
                // ===== INCLUI =====
                recebido += Encoding.GetEncoding("iso-8859-1").GetString(data, 0, qtdbytes);
                // =====

                for (int i = 0; i < qtdbytes; ++i)
                {
                    // ===== INCLUI =====
                    outfile.WriteByte((byte)(data[i] - (18 + chave)));
                    descriptografado += (char)(data[i] - (18 + chave));
                    // =====
                }
                // ===== INCLUI =====
            } while (qtdbytes == 10);

            Console.WriteLine("Conteúdo recebido: " + recebido);
            Console.WriteLine("Chave: " + chave);
            Console.WriteLine("Conteúdo descriptografado: " + descriptografado);
            // =====
            outfile.Close();
            socketreceber.Close();
            // ===== INCLUI =====
            Console.ReadKey();
            // =====
        }
    }
}