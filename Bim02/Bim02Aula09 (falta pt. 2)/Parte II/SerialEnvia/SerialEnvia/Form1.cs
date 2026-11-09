using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace SerialEnvia
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            listBox1.Items.Add("Foram identificadas " + SerialPort.GetPortNames().Length.ToString() + " portas seriais no sistema.");
            listBox1.Items.Add("São elas:");
            foreach (String porta in SerialPort.GetPortNames())
            {
                listBox1.Items.Add(porta);
            }
            try
            {
                serialPort1.Open();
                listBox1.Items.Add("Porta serial COM5 conectada com sucesso.");
            }
            catch(Exception erro)
            {
                listBox1.Items.Add("Erro ao se conectar a porta COM5 (" + erro.Message + ")");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.WriteLine(textBox1.Text);
                textBox1.Clear();
            }
            catch(Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
