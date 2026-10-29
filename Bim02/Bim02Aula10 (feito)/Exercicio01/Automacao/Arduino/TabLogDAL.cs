using System;
using System.Data;
using System.Data.OleDb;

namespace Arduino
{
    class TabLogDAL
    {
        private static String strConexao = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=banco.accdb;";
        private static OleDbConnection conn = new OleDbConnection(strConexao);

        public static void conecta()
        {
            Erro.setErro(false);
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
            }
            catch (Exception ex)
            {
                Erro.setErro(ex.Message);
            }
        }

        public static void desconecta()
        {
            Erro.setErro(false);
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Erro.setErro(ex.Message);
            }
        }

        public static void insere(TabLog _log)
        {
            Erro.setErro(false);
            conecta();
            if (Erro.getErro()) return;

            try
            {
                String strSQL = "INSERT INTO TabLog (dispositivo, operacao, datahora) VALUES (?, ?, ?)";
                OleDbCommand cmd = new OleDbCommand(strSQL, conn);
                cmd.Parameters.Add("@dispositivo", OleDbType.VarChar).Value = _log.getDispositivo();
                cmd.Parameters.Add("@operacao", OleDbType.VarChar).Value = _log.getOperacao();
                cmd.Parameters.Add("@datahora", OleDbType.VarChar).Value = _log.getDatahora();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Erro.setErro(ex.Message);
            }
            finally
            {
                desconecta();
            }
        }
    }
}