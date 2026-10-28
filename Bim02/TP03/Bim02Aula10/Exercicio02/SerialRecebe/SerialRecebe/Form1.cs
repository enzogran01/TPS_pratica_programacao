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

namespace SerialRecebe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Open();
                listBox1.Items.Add("Porta serial COM5 conectada com sucesso.");
            }
            catch (Exception erro)
            {
                listBox1.Items.Add("Erro ao se conectar a porta COM5 (" + erro.Message + ")");
            }
            while(true)
            {
                listBox1.Items.Add(serialPort1.ReadLine());
                Refresh();
                listBox1.SendToBack();
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
            }
        }
    }
}
