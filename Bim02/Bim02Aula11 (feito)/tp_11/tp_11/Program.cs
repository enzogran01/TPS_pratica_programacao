using System;
using System.IO;
using System.Text;

namespace compres
{
    class Program
    {
        const int TAM_CAB1 = 14;

        const string ARQUIVO = @"C:\Users\unisanta\Desktop\Figura.bmp";
        const string COMPRIMIDO = @"C:\Users\unisanta\Desktop\Figura.cmp";
        const string RESTAURADO = @"C:\Users\unisanta\Desktop\Figura_restaurada.bmp";

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n===== BMP - Compressão RLE =====");
                Console.WriteLine("Arquivo: " + ARQUIVO);
                Console.WriteLine("1 - Ler CAB1 e exibir dados");
                Console.WriteLine("2 - Comprimir");
                Console.WriteLine("3 - Descomprimir");
                Console.WriteLine("0 - Sair");
                Console.Write("Opção: ");

                string op = Console.ReadLine();
                if (op == null) break;
                op = op.Trim();
                if (op == "0") break;

                try
                {
                    if (op == "1") Ex1(ARQUIVO);
                    else if (op == "2") Ex2(ARQUIVO, COMPRIMIDO);
                    else if (op == "3") Ex3(COMPRIMIDO, RESTAURADO);
                    else Console.WriteLine("Opção inválida.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
            }

            Console.WriteLine("Encerrando... pressione uma tecla.");
            Console.ReadKey();
        }

        static void Ex1(string caminho)
        {
            using (FileStream fs = new FileStream(caminho, FileMode.Open, FileAccess.Read))
            using (BinaryReader br = new BinaryReader(fs))
            {
                if (fs.Length < TAM_CAB1) throw new Exception("Arquivo menor que 14 bytes.");

                byte[] assinatura = br.ReadBytes(2);
                uint tamanho = br.ReadUInt32();
                ushort res1 = br.ReadUInt16();
                ushort res2 = br.ReadUInt16();
                uint desloc = br.ReadUInt32();

                Console.WriteLine("\n--- CAB1 (14 bytes) ---");
                Console.WriteLine("Assinatura            : " + Encoding.ASCII.GetString(assinatura));
                Console.WriteLine("Tamanho do arquivo    : " + tamanho + " bytes (em disco: " + fs.Length + ")");
                Console.WriteLine("Campo reservado 1     : " + res1);
                Console.WriteLine("Campo reservado 2     : " + res2);
                Console.WriteLine("Deslocamento de dados : " + desloc + " bytes");
            }
        }

        static void Ex2(string entrada, string saida)
        {
            byte[] arq = File.ReadAllBytes(entrada);
            if (arq.Length < TAM_CAB1 || arq[0] != 'B' || arq[1] != 'M')
                throw new Exception("Não é um BMP válido.");

            uint desloc = BitConverter.ToUInt32(arq, 10);
            if (desloc > arq.Length) throw new Exception("Deslocamento inválido.");

            long tamSaida;
            using (FileStream fs = new FileStream(saida, FileMode.Create, FileAccess.Write))
            using (BinaryWriter bw = new BinaryWriter(fs))
            {
                bw.Write(arq, 0, (int)desloc);   

                int i = (int)desloc;
                while (i < arq.Length)
                {
                    byte valor = arq[i];
                    int qtd = 1;
                    while (i + qtd < arq.Length && arq[i + qtd] == valor) qtd++;
                    bw.Write(qtd);                       
                    bw.Write(valor);                     
                    i += qtd;
                }
                bw.Flush();
                tamSaida = fs.Length;
            }

            Console.WriteLine("Original  : " + arq.Length + " bytes");
            Console.WriteLine("Comprimido: " + tamSaida + " bytes (" + (100.0 * tamSaida / arq.Length).ToString("F1") + "%)");
            Console.WriteLine("Gerado em : " + saida);
        }

        static void Ex3(string entrada, string saida)
        {
            using (FileStream fin = new FileStream(entrada, FileMode.Open, FileAccess.Read))
            using (BinaryReader br = new BinaryReader(fin))
            using (FileStream fout = new FileStream(saida, FileMode.Create, FileAccess.Write))
            using (BinaryWriter bw = new BinaryWriter(fout))
            {
                byte[] cab1 = br.ReadBytes(TAM_CAB1);
                if (cab1.Length < TAM_CAB1 || cab1[0] != 'B' || cab1[1] != 'M')
                    throw new Exception("Arquivo comprimido inválido.");

                uint desloc = BitConverter.ToUInt32(cab1, 10);
                byte[] resto = br.ReadBytes((int)desloc - TAM_CAB1);  

                bw.Write(cab1);
                bw.Write(resto);

                while (fin.Position < fin.Length)
                {
                    int qtd = br.ReadInt32();
                    byte valor = br.ReadByte();
                    for (int k = 0; k < qtd; k++) bw.Write(valor);
                }
                bw.Flush();
                Console.WriteLine("Restaurado: " + fout.Length + " bytes em " + RESTAURADO);
            }
        }
    }
}
