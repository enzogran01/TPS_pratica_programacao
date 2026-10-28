using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * Todos os campos são de preenchimento obrigatorio
 * Ano de edição deverá ser numérico e positivo
*/

namespace ParaCasa1
{
    class LivroBLL
    {
        public static void conecta()
        {
            LivroDAL.conecta();
        }

        public static void desconecta()
        {
            LivroDAL.desconecta();
        }

        public static void validaCodigo()
        {
            Erro.setErro(false);
            if (Livro.getCodigo().Equals(""))
            {
                Erro.setMsg("O código é de preenchimento obrigatório!");
                return;
            }
            LivroDAL.consultaUmLivro();
        }

        public static void validaDados()
        {
            Erro.setErro(false);
            if (Livro.getCodigo().Equals(""))
            {
                Erro.setMsg("O código é de preenchimento obrigatório!");
                return;
            }
            if (Livro.getTitulo().Equals(""))
            {
                Erro.setMsg("O título é de preenchimento obrigatório!");
                return;
            }
            if (Livro.getAutor().Equals(""))
            {
                Erro.setMsg("O autor é de preenchimento obrigatório!");
                return;
            }
            if (Livro.getEditora().Equals(""))
            {
                Erro.setMsg("A Editora é de preenchimento obrigatório!");
                return;
            }
            if (Livro.getAno().Equals(""))
            {
                Erro.setMsg("O ano é de preenchimento obrigatório!");
                return;
            }


            try
            {
                int.Parse(Livro.getAno());
            }
            catch (Exception)
            {
                Erro.setMsg("O valor do ano deve ser numérico!");
                return;
            }


            if (int.Parse(Livro.getAno()) <= 0)
            {
                Erro.setMsg("O valor do Ano deve ser numérico e positivo!");
                return;
            }
            LivroDAL.inseriUmLivro();

        }

    }
}

