using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Envia
{
    internal class ChatMessage
    {
        public String username { get; set; }
        public String message { get; set; }

        public ChatMessage(String _username, String _message)
        {
            username = _username;
            message = _message;
        }
    }
}
