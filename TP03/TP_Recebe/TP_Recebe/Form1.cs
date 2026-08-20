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

namespace TP_Recebe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Socket socketreceber = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
            EndPoint endereco = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9060);
            byte[] data = new byte[1024];
            int qtdbytes;

            socketreceber.Bind(endereco);

            while (true)
            {
                qtdbytes = socketreceber.ReceiveFrom(data, ref endereco);
                listBox1.Items.Add(Encoding.ASCII.GetString(data, 0, qtdbytes));
                Refresh();
            }
            socketreceber.Close();

        }
    }
}
