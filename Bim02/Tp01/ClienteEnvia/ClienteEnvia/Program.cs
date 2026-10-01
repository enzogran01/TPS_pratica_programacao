using System;
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
            String filename = "testeXML.xml";
            Random rndNumber = new Random();
            int key = rndNumber.Next(1, 10000);

            Socket socketenviar = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
            IPEndPoint endereco = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9060);

            socketenviar.SendTo(Encoding.ASCII.GetBytes(key.ToString()), endereco); // envia chave
            socketenviar.SendTo(Encoding.ASCII.GetBytes(filename), endereco);       // envia nome

            FileStream infile = new FileStream(filename, FileMode.Open, FileAccess.Read);

            byte[] buffer = new byte[10];
            int pos = 0;
            int b;

            while ((b = infile.ReadByte()) != -1)
            {
                buffer[pos++] = (byte)(b + 18 + key); // criptografa
                if (pos == 10)
                {
                    socketenviar.SendTo(buffer, 0, pos, SocketFlags.None, endereco); // envia 10 bytes
                    pos = 0;
                }
            }

            // resto (menos de 10 bytes, pode ser 0): serve de sinal de fim
            socketenviar.SendTo(buffer, 0, pos, SocketFlags.None, endereco);

            infile.Close();
            Console.ReadKey();
            socketenviar.Close();
        }
    }
}