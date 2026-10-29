using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arduino
{
    class TabLog
    {
        private String dispositivo;
        private String operacao;
        private String datahora;

        public void setDispositivo(String _dispositivo) { dispositivo = _dispositivo; }
        public String getDispositivo() { return dispositivo; }

        public void setOperacao(String _operacao) { operacao = _operacao; }
        public String getOperacao() { return operacao; }

        public void setDatahora(String _datahora) { datahora = _datahora; }
        public String getDatahora() { return datahora; }
    }
}
