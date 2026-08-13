using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace ParaCasa1
{
    class LivroDAL
    {
        private static String strConexao = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BDLivros.mdb";
        private static OleDbConnection conn = new OleDbConnection(strConexao);
        private static OleDbCommand strSQL;
        private static OleDbDataReader result;
        
        public static void conecta()
        {
            try
            {
                conn.Open();
            }
            catch (Exception)
            {
                Erro.setMsg("Banco da Dados não localizado - contacte o suporte.");
            }
        }

        public static void desconecta()
        {
            conn.Close();
        }

        
        public static void populaDR()
        {
            String aux = "select * from TabLivro";

            strSQL = new OleDbCommand(aux, conn);
            result = strSQL.ExecuteReader();
        }

        public static Livro getProximo()
        {
            Livro umlivro = new Livro();

            Erro.setErro(false);
            if (result.Read())
            {
                umlivro.setCodigo(result.GetString(0));
                umlivro.setTitulo(result.GetString(1));
                umlivro.setAutor(result.GetString(2));
                umlivro.setEditora(result.GetString(3));
                umlivro.setAno(result.GetString(4));
            }
            else
                Erro.setErro(true);
            return umlivro;
        }
        


    }
}
