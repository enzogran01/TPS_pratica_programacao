using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace Prova
{
    class DAL
    {
        private static String strConexao = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BDFarinha.mdb";
        private static OleDbConnection conn = new OleDbConnection(strConexao);
        private static OleDbCommand strSQL;
        private static OleDbDataReader result;
        private static OleDbCommand strSQL1;
        private static OleDbDataReader result1;



        public static void conecta()
        {
            try
            {
                conn.Open();
            }
            catch (Exception)
            {
                Erro.setMsg("Problemas ao se conectar ao Banco de Dados");
            }

        }

        public static void desconecta()
        {
            conn.Close();
        }

        public static void consultaUmCliente()
        {
            String aux = "SELECT * FROM TabClientes WHERE cnpj = '" + Cliente.getCNPJ() + "'";

            strSQL = new OleDbCommand(aux, conn);

            result = strSQL.ExecuteReader();
            if (result.Read())
            {
                Cliente.setCNPJ(result.GetString(0));
                Cliente.setNome(result.GetString(1));
                popula();
            }
            else
            {
                Erro.setErro(true);
            }
        }

        public static void popula()
        {
            String aux = "SELECT * FROM TabVendasCliente WHERE cnpj = '" + Cliente.getCNPJ() + "'";

            strSQL1 = new OleDbCommand(aux, conn);

            result1 = strSQL1.ExecuteReader();
        }

        public static void getProximo(String type)
        {
            Erro.setErro(false);
            if (result1.Read())
            {
                VendaCliente.setData(result1.GetString(2));
                VendaCliente.setToneladas(result1.GetString(3));
                VendaCliente.setvalor(result1.GetString(4));
            }
            else
            {
                Erro.setErro(true);
            }
        }
    }
}
