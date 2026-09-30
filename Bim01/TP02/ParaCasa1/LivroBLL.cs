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
        public static void inicializa()
        {
            LivroDAL.conecta();
            if (!Erro.getErro())
            {
                LivroDAL.populaDR();
            }
        }

        public static void finaliza()
        {
            LivroDAL.desconecta();
        }

        public static Livro getProximo()
        {
            return LivroDAL.getProximo();
        }

    }
}

