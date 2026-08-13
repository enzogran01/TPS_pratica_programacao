using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace ParaCasa1
{
    public partial class CadLivrosUIL : Form
    {
        Livro umlivro = new Livro();
        public CadLivrosUIL()
        {
            InitializeComponent();
        }

        private void CadLivrosUIL_Load(object sender, EventArgs e)
        {
            LivroBLL.inicializa();
            if (Erro.getErro())
            {
                MessageBox.Show(Erro.getMsg());
                Application.Exit();
            }
        }

        private void CadLivrosUIL_FormClosed(object sender, FormClosedEventArgs e)
        {
            LivroBLL.finaliza();
        }

         private void button1_Click(object sender, EventArgs e)
        {
            List<Livro> livros = new List<Livro>();
            TextWriter arquivo = new StreamWriter("livros.json");
            JsonSerializer obj = new JsonSerializer();
            int i = 0;

            umlivro = LivroBLL.getProximo();
            while (!Erro.getErro())
            {
                listBox1.Items.Add(umlivro.getTitulo());
                livros.Add(new Livro());
                livros[i] = umlivro;
                ++i;
                umlivro = LivroBLL.getProximo();
            }
            obj.Serialize(arquivo, livros);
            arquivo.Close();
        }

    }
}
