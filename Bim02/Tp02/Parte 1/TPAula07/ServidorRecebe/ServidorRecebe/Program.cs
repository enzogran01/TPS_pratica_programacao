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
            int qtdbytes, qtd;
            String filename;

            socketreceber.Bind(endereco);

            qtdbytes = socketreceber.ReceiveFrom(data, ref endereco);
            filename = Encoding.ASCII.GetString(data, 0, qtdbytes);
            Console.WriteLine("FileName: " + filename);

            qtdbytes = socketreceber.ReceiveFrom(data, ref endereco);
            qtd = int.Parse(Encoding.ASCII.GetString(data, 0, qtdbytes));
            Console.WriteLine("QtdDataGramas: " + qtd);

            FileStream outfile;

            outfile = new System.IO.FileStream(filename,
                                               System.IO.FileMode.Create,
                                               System.IO.FileAccess.Write);

            int checksumCalculado, checksumRecebido;
            byte[] dataCheck = new byte[1024];
            int bytesCheck;

            for (int i = 0; i < qtd; i++)
            {
                qtdbytes = socketreceber.ReceiveFrom(data, ref endereco);
                Console.WriteLine("Pacote recebido: " + Encoding.ASCII.GetString(data, 0, qtdbytes));

                checksumCalculado = 0;
                for (int j = 0; j < qtdbytes; ++j)
                {
                    checksumCalculado += (int)data[j];
                }

                bytesCheck = socketreceber.ReceiveFrom(dataCheck, ref endereco);
                checksumRecebido = int.Parse(Encoding.ASCII.GetString(dataCheck, 0, bytesCheck));

                Console.WriteLine("CheckSum Calculado: " + checksumCalculado + " | CheckSum Recebido: " + checksumRecebido);

                if (checksumCalculado != checksumRecebido)
                {
                    Console.WriteLine("\n[ERRO] Pacote corrompido! CheckSum incompatível no pacote " + (i + 1));
                    outfile.Close();
                    socketreceber.Close();
                    Console.ReadKey();
                    return;
                }
                for (int j = 0; j < qtdbytes; ++j)
                {
                    outfile.WriteByte((byte)data[j]);
                }
            }

            outfile.Close();
            socketreceber.Close();

            Console.WriteLine("\nArquivo recebido e validado com sucesso!");
            Console.ReadKey();
        }
    }
}