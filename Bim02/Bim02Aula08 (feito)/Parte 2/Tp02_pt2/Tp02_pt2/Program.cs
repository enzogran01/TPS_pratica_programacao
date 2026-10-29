using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ExercicioPontoXML
{
    [Serializable]
    public class OcorrenciaPonto
    {
        public string Prontuario { get; set; }
        public string Data { get; set; }
        public string Hora { get; set; }
        public OcorrenciaPonto()
        {
        }
        public OcorrenciaPonto(string prontuario, string data, string hora)
        {
            Prontuario = prontuario;
            Data = data;
            Hora = hora;
        }
        public override string ToString()
        {
            return $"Prontuário: {Prontuario} | Data: {Data} | Hora: {Hora}";
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string nomeArquivoTxt = "Abril 2017.txt";
            string nomeArquivoXml = "ocorrencias.xml";
            List<OcorrenciaPonto> ocorrencias = new List<OcorrenciaPonto>();
            using (StreamReader file = new StreamReader(nomeArquivoTxt))
            {
                string linha;
                while ((linha = file.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(linha)) continue;
                    string prontuario = linha.Substring(0, 15);
                    string data = linha.Substring(15, 6);
                    string hora = linha.Substring(21, 4);
                    OcorrenciaPonto ponto = new OcorrenciaPonto
                    {
                        Prontuario = prontuario,
                        Data = data,
                        Hora = hora
                    };
                    Console.WriteLine(ponto);
                    ocorrencias.Add(ponto);
                }
            }
            Console.WriteLine("\nTotal de ocorrências processadas: " + ocorrencias.Count);
            Console.WriteLine("\nGravando dados no arquivo XML...");
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<OcorrenciaPonto>));

                using (StreamWriter writer = new StreamWriter(nomeArquivoXml))
                {
                    serializer.Serialize(writer, ocorrencias);
                }

                Console.WriteLine($"Sucesso! Arquivo '{nomeArquivoXml}' gerado com êxito.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao serializar para XML: " + ex.Message);
            }
            Console.WriteLine("\nPressione qualquer tecla para finalizar...");
            Console.ReadKey();
        }
    }
}