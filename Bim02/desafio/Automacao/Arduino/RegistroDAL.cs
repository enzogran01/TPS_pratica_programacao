using System;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace Arduino
{
    class RegistroDAL
    {
        private static readonly string caminhoBanco = "C:\\Users\\Administrador\\OneDrive\\Desktop\\TPS_pratica_programacao\\Bim02\\desafio\\Automacao\\Arduino\\bin\\Debug\\automacao.db";

        public static void Registrar(int dispositivo, string acao)
        {
            string conexao = "Data Source=" + caminhoBanco + ";Version=3;";

            using (var connection = new SQLiteConnection(conexao))
            using (var command = new SQLiteCommand(
                @"INSERT INTO Registro (DataHora, Dispositivo, Acao)
                  VALUES (@dataHora, @dispositivo, @acao)", connection))
            {
                command.Parameters.AddWithValue("@dataHora", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@dispositivo", dispositivo);
                command.Parameters.AddWithValue("@acao", acao);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}