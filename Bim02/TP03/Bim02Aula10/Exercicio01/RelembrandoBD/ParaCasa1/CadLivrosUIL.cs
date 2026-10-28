using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ParaCasa1
{
    public partial class CadLivrosUIL : Form
    {
        public CadLivrosUIL()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Livro.setCodigo(textBox1.Text);
            Livro.setTitulo(textBox2.Text);
            Livro.setAutor(textBox3.Text);
            Livro.setEditora(textBox4.Text);
            Livro.setAno(textBox5.Text);

            LivroBLL.validaDados();

            if (Erro.getErro())
                MessageBox.Show(Erro.getMsg());
            else
                MessageBox.Show("Dados inseridos com sucesso!");
        }

        private void CadLivrosUIL_Load(object sender, EventArgs e)
        {
            LivroBLL.conecta();
            if (Erro.getErro())
                MessageBox.Show(Erro.getMsg());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Livro.setCodigo(textBox1.Text);
            LivroBLL.validaCodigo();
            if (Erro.getErro())
                MessageBox.Show(Erro.getMsg());
            else
            {
                textBox1.Text = Livro.getCodigo();
                textBox2.Text = Livro.getTitulo();
                textBox3.Text = Livro.getAutor();
                textBox4.Text = Livro.getEditora();
                textBox5.Text = Livro.getAno();
            }
        }

        private void CadLivrosUIL_FormClosing(object sender, FormClosingEventArgs e)
        {
            LivroBLL.desconecta();
        }
    }
}
