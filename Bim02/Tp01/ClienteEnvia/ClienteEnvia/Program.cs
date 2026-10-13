using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace ClienteEnvia
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream infile;
            int tam;
            String buffer = "";
            String filename = "testeXML.xml";
            // ===== INCLUI =====
            String aux = "";
            int chave = 5;
            // =====

            infile = new System.IO.FileStream(filename,
                                   System.IO.FileMode.Open,
                                   System.IO.FileAccess.Read);

            tam = (int)infile.Length;
            for (int i = 0; i < tam; ++i)
            {
                buffer += (char)infile.ReadByte();
            }
            infile.Close();

            // ===== INCLUI =====
            for (int i = 0; i < tam; ++i)
            {
                aux += (char)(buffer[i] + (18 + chave));
            }

            Console.WriteLine("Conteúdo do arquivo: " + buffer);
            Console.WriteLine("Chave: " + chave);
            Console.WriteLine("Conteúdo enviado: " + aux);
            buffer = "";
            // =====

            Socket socketenviar = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
            IPEndPoint endereco = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9060);

            Console.ReadKey();
            socketenviar.SendTo(Encoding.ASCII.GetBytes(filename), endereco);
            // ===== INCLUI =====
            socketenviar.SendTo(Encoding.ASCII.GetBytes(chave.ToString()), endereco);

            for (int i = 0, j = 1; i < tam; ++i, ++j)
            {
                buffer += aux[i];
                if (j == 10)
                {
                    socketenviar.SendTo(Encoding.GetEncoding("iso-8859-1").GetBytes(buffer), endereco);
                    j = 0;
                    buffer = "";
                }
            }
            socketenviar.SendTo(Encoding.GetEncoding("iso-8859-1").GetBytes(buffer), endereco);
            // =====
            socketenviar.Close();
        }
    }
}