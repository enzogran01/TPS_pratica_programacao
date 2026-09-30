using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prova
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BLL.conecta();
            if (Erro.getErro())
                MessageBox.Show(Erro.getMsg());
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            BLL.desconecta();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int totalTon = 0;
            float totalVal = 0;

            Cliente.setCNPJ(textBox1.Text);

            BLL.validaCNPJ();

            if (Erro.getErro())
            {
                MessageBox.Show(Erro.getMsg());
            }
            else
            {
                textBox2.Text = Cliente.getNome();
            }


            if (radioButton1.Checked)
            {
                BLL.getProximo("ton");
                while (!Erro.getErro())
                {
                    //listBox1.Items.Add(VendaCliente.getData());
                    //listBox2.Items.Add(VendaCliente.getToneladas());
                    //listBox3.Items.Add(VendaCliente.getValor());

                    chart1.Series.Add(VendaCliente.getToneladas());

                    totalTon += int.Parse(VendaCliente.getToneladas());
                    BLL.getProximo("ton");
                }
                textBox3.Text = totalTon.ToString();
            } else
            {
                BLL.getProximo("val");
                while (!Erro.getErro())
                {
                    //listBox1.Items.Add(VendaCliente.getData());
                    //listBox2.Items.Add(VendaCliente.getToneladas());
                    //listBox3.Items.Add(VendaCliente.getValor());
                    totalVal += float.Parse(VendaCliente.getValor());
                    BLL.getProximo("val");
                }
                textBox3.Text = totalVal.ToString();
            }
        }
    }
}
