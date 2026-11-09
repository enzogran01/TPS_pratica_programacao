using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arduino
{
    public partial class ArduinoIHM : Form
    {
        public ArduinoIHM()
        {
            InitializeComponent();
        }

        private void tratarBotoes(object sender, EventArgs e)
        {
            Button generico = (Button)sender;

            if (generico.Text == "Ligar")
            {
                generico.Text = "Desligar";
                ArduinoBLL.ligaDispositivo(generico.Tag.ToString());
                textBox1.Text = ArduinoBLL.mostraBits(ArduinoBLL.getDisplay());
            }
            else
            {
                generico.Text = "Ligar";
                ArduinoBLL.desligaDispositivo(generico.Tag.ToString());
                textBox1.Text = ArduinoBLL.mostraBits(ArduinoBLL.getDisplay());
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ArduinoBLL.setDisplay(0);
            textBox1.Text = ArduinoBLL.mostraBits(ArduinoBLL.getDisplay());
            button1.Text = "Ligar";
            button2.Text = "Ligar";
            button3.Text = "Ligar";
            button4.Text = "Ligar";
            button5.Text = "Ligar";
            button6.Text = "Ligar";
            button7.Text = "Ligar";
            button8.Text = "Ligar";
        }

        private async void btnAlarme_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                ArduinoBLL.setDisplay(255);
                textBox1.Text = ArduinoBLL.mostraBits(ArduinoBLL.getDisplay());
                await Task.Delay(2000);

                ArduinoBLL.setDisplay(0);
                textBox1.Text = ArduinoBLL.mostraBits(ArduinoBLL.getDisplay());
                await Task.Delay(2000);
            }
        }

        private async void btnSequencial_Click(object sender, EventArgs e)
        {
            ArduinoBLL.setDisplay(0);
            for (int i = 1; i <= 8; i++)
            {
                ArduinoBLL.ligaDispositivo(i.ToString());
                textBox1.Text = ArduinoBLL.mostraBits(ArduinoBLL.getDisplay());
                await Task.Delay(2000);
            }
        }
    }
}