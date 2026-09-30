using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
// CLIENTE UNISANTA — versão via servidor WebSocket
namespace TP_Envia
{
    public partial class Form1 : Form
    {
        private ClientWebSocket _webSocket;
        private CancellationTokenSource _cts;

        // Troque pelo endereço/porta onde o server.js está rodando.
        // Se o servidor estiver na nuvem (ex: uma VPS), use o IP público dela.
        // Se estiver na mesma rede, pode usar o IP local. Não precisa mais de VPN.
        private const string SERVER_URI = "ws://127.0.0.1:9070";

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            _webSocket = new ClientWebSocket();
            _cts = new CancellationTokenSource();

            try
            {
                await _webSocket.ConnectAsync(new Uri(SERVER_URI), _cts.Token);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Não foi possível conectar ao servidor: {ex.Message}",
                    "Erro de conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Dispara o loop de recebimento em background (substitui a antiga Thread + ReceiveFrom)
            _ = ReceberMensagensAsync();
        }

        private async Task ReceberMensagensAsync()
        {
            var buffer = new byte[4096];

            try
            {
                while (_webSocket.State == WebSocketState.Open)
                {
                    WebSocketReceiveResult resultado =
                        await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), _cts.Token);

                    if (resultado.MessageType == WebSocketMessageType.Close)
                    {
                        await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure,
                            "Fechado pelo servidor", CancellationToken.None);
                        break;
                    }

                    string jsonRecebido = Encoding.UTF8.GetString(buffer, 0, resultado.Count);
                    ChatMessage msgRecebida = JsonConvert.DeserializeObject<ChatMessage>(jsonRecebido);

                    // ReceiveAsync não roda na thread da UI, então ainda precisa de Invoke
                    if (listBox1.InvokeRequired)
                    {
                        listBox1.Invoke((Action)delegate ()
                        {
                            AdicionarMensagem(msgRecebida);
                        });
                    }
                    else
                    {
                        AdicionarMensagem(msgRecebida);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // encerramento normal, ex: form fechando
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Conexão perdida: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AdicionarMensagem(ChatMessage msg)
        {
            listBox1.Items.Add($"{(msg.username == textBox2.Text ? "Você" : msg.username)}: {msg.message}");
            listBox1.TopIndex = listBox1.Items.Count - 1;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Digite um nome de usuário antes de enviar uma mensagem.",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBox1.Text == "") return;

            if (_webSocket == null || _webSocket.State != WebSocketState.Open)
            {
                MessageBox.Show("Você não está conectado ao servidor.", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ChatMessage chatmessage = new ChatMessage(textBox2.Text, textBox1.Text);
            string jsonString = JsonConvert.SerializeObject(chatmessage);
            byte[] bytes = Encoding.UTF8.GetBytes(jsonString);

            textBox1.Clear();
            textBox1.Focus();

            try
            {
                // Note que não adicionamos a mensagem no listBox aqui: o servidor
                // faz broadcast de volta pra todo mundo, inclusive pra quem enviou,
                // e AdicionarMensagem já trata "Você" comparando o username.
                await _webSocket.SendAsync(new ArraySegment<byte>(bytes),
                    WebSocketMessageType.Text, true, _cts.Token);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Falha ao enviar mensagem: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _cts?.Cancel();

            if (_webSocket != null && _webSocket.State == WebSocketState.Open)
            {
                try
                {
                    await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure,
                        "Fechando aplicação", CancellationToken.None);
                }
                catch
                {
                    // ignora erro ao fechar durante o shutdown
                }
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            textBox2.Enabled = false;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (!string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    button1_Click(this, new EventArgs());
                }
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (!string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    textBox2.Enabled = false;
                    textBox1.Focus();
                }
            }
        }
    }
}