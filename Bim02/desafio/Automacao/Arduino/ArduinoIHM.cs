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

namespace Arduino
{
    public partial class ArduinoIHM : Form
    {
        private SerialPort serial;
        public ArduinoIHM()
        {
            InitializeComponent();
            serial = new SerialPort("COM5", 9600);
            serial.NewLine = "\n";
        }

        private void tratarBotoes(object sender, EventArgs e)
        {
            Button botao = (Button)sender;
            int dispositivo = int.Parse(botao.Tag.ToString());

            bool vaiLigar = botao.Text == "Ligar";
            string acao = vaiLigar ? "Ligar" : "Desligar";
            string comando = dispositivo + "," + (vaiLigar ? "1" : "0") + "\n";

            try
            {
                if (!serial.IsOpen)
                    serial.Open();

                serial.Write(comando);

                if (vaiLigar)
                    ArduinoBLL.ligaDispositivo(dispositivo.ToString());
                else
                    ArduinoBLL.desligaDispositivo(dispositivo.ToString());

                byte estado = (byte)ArduinoBLL.getDisplay();
                serial.Write(new byte[] { estado }, 0, 1);

                botao.Text = vaiLigar ? "Desligar" : "Ligar";
                textBox1.Text = ArduinoBLL.mostraBits(ArduinoBLL.getDisplay());

                RegistroDAL.Registrar(dispositivo, acao);
            }
            catch (Exception erro)
            {
                MessageBox.Show(
                    "Não foi possível enviar o comando ou registrar no banco.\n\n" +
                    erro.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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