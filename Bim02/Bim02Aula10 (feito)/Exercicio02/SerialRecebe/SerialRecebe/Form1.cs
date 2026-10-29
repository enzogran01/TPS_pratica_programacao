using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.Data.OleDb;

namespace SerialRecebe
{
    public partial class Form1 : Form
    {
        OleDbConnection conexao = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Banco.accdb");
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            serialPort1.Open();
            //conexao.Open();
            listBox1.Items.Add("Porta serial conectada");

            while (true)
            {
                string valor = serialPort1.ReadLine().Trim();

                listBox1.Items.Add(valor);

                chart1.Series[0].Points.AddY(Convert.ToDouble(valor));

                //string sql = "INSERT INTO TabAmostras (Valor, DataHora) VALUES (@v, @dh)";
                //OleDbCommand cmd = new OleDbCommand(sql, conexao);
                //cmd.Parameters.AddWithValue("@v", valor);
                //cmd.Parameters.AddWithValue("@dh", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //cmd.ExecuteNonQuery();

                Refresh();
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
            }
        }
    }
}