using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arduino
{
    class ArduinoBLL
    {
        private static int display = 0;

        public static void setDisplay(int _display)
        {
            display = _display;
            if (_display == 0)
            {
                TabLog log = new TabLog();
                log.setDispositivo("Todos");
                log.setOperacao("Desligar");
                log.setDatahora(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                TabLogDAL.insere(log);
            }
        }

        public static int getDisplay() { return display; }

        public static String mostraBits(int _x)
        {
            String retorno = "";
            int aux = 128;

            for (int i = 0; i < 8; ++i)
            {
                if ((_x & aux) != 0)
                    retorno += "1";
                else
                    retorno += "0";
                aux = aux >> 1;
            }
            return retorno;
        }

        public static void ligaDispositivo(String _n)
        {
            int aux = 1 << (int.Parse(_n) - 1);
            display = display | aux;

            TabLog log = new TabLog();
            log.setDispositivo(_n);
            log.setOperacao("Ligar");
            log.setDatahora(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            TabLogDAL.insere(log);
        }

        public static void desligaDispositivo(String _n)
        {
            int aux = 1 << (int.Parse(_n) - 1);
            display = display & ~aux;

            TabLog log = new TabLog();
            log.setDispositivo(_n);
            log.setOperacao("Desligar");
            log.setDatahora(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            TabLogDAL.insere(log);
        }
    }
}