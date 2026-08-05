using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova
{
    class Cliente
    {
        private static String cnpj;
        private static String nome;

        public void setCNPJ(String _cnpj) { cnpj = _cnpj; }
        public void setNome(String _nome) { nome = _nome; }
        public String getCNPJ() { return cnpj; }
        public String getNome() { return nome; }
    }
}
