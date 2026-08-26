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
            Socket socketenviar = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
            IPEndPoint endereco = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9060);

            socketenviar.SendTo(Encoding.ASCII.GetBytes(textBox1.Text), endereco);
            socketenviar.Close();
        }

        private void processo()
        {
            Socket socketreceber = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
            EndPoint endereco = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9060);
            byte[] data = new byte[1024];
            socketreceber.Bind(endereco);
            int qtdbytes;

            while (true)
            {
                qtdbytes = socketreceber.ReceiveFrom(data, ref endereco);
                listBox1.Invoke((Action)delegate ()
                {
                    listBox1.Items.Add(Encoding.ASCII.GetString(data, 0, qtdbytes));
                });

            }
            socketreceber.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            minhaThread = new Thread(new ThreadStart(this.processo));
            minhaThread.Start();
        }
    }
}
