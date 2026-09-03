using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Newtonsoft.Json;
// CLIENTE UNISANTA 26.158.195.53
namespace TP_Envia
{
    public partial class Form1 : Form
    {
        Thread minhaThread;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Digite um nome de usuário antes de enviar uma mensagem.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBox1.Text == "") return;
            ChatMessage chatmessage = new ChatMessage(textBox2.Text, textBox1.Text);
            String jsonString = JsonConvert.SerializeObject(chatmessage);

            Socket socketenviar = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
            IPEndPoint endereco = new IPEndPoint(IPAddress.Parse("26.207.103.30"), 9070);

            listBox1.Items.Add($"Você: {chatmessage.message}");
            textBox1.Clear();
            listBox1.TopIndex = listBox1.Items.Count - 1;
            textBox1.Focus();

            socketenviar.SendTo(Encoding.ASCII.GetBytes(jsonString), endereco);
            socketenviar.Close();
        }

        private void processo()
        {
            Socket socketreceber = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
            EndPoint endereco = new IPEndPoint(IPAddress.Any, 9060);
            byte[] data = new byte[1024];
            socketreceber.Bind(endereco);
            int qtdbytes;

            while (true)
            {
                qtdbytes = socketreceber.ReceiveFrom(data, ref endereco);

                string jsonRecebido = Encoding.ASCII.GetString(data, 0, qtdbytes);
                ChatMessage msgRecebida = JsonConvert.DeserializeObject<ChatMessage>(jsonRecebido);

                listBox1.Invoke((Action)delegate ()
                {
                    listBox1.Items.Add($"{(msgRecebida.username == textBox2.Text ? "Você" : msgRecebida.username)}: {msgRecebida.message}");
                    listBox1.TopIndex = listBox1.Items.Count - 1;
                });
            }
            socketreceber.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            minhaThread = new Thread(new ThreadStart(this.processo));
            minhaThread.Start();
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            textBox2.Enabled = false;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (!string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    button1_Click(this, new EventArgs());
                    textBox1.Clear();
                }
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (!string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    textBox2.Enabled = false;
                    textBox1.Focus();
                }
            }
        }
    }
}
