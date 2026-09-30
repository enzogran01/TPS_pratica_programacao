const WebSocket = require('ws');

const PORT = process.env.PORT || 9070;
const wss = new WebSocket.Server({ port: PORT });

console.log(`Servidor WebSocket rodando na porta ${PORT}`);

wss.on('connection', (ws, req) => {
    const ip = req.socket.remoteAddress;
    console.log(`Novo cliente conectado: ${ip}`);

    ws.on('message', (data) => {
        let chatMessage;
        try {
            chatMessage = JSON.parse(data.toString());
        } catch (err) {
            console.error('Mensagem inválida recebida:', data.toString());
            return;
        }

        console.log(`[${chatMessage.username}]: ${chatMessage.message}`);

        // Reenvia para TODOS os clientes conectados, inclusive quem enviou.
        // Assim o cliente não precisa de lógica separada para "minha própria mensagem",
        // ele só compara username == ele mesmo, igual você já faz no listBox1.
        const payload = JSON.stringify(chatMessage);
        wss.clients.forEach((client) => {
            if (client.readyState === WebSocket.OPEN) {
                client.send(payload);
            }
        });
    });

    ws.on('close', () => {
        console.log(`Cliente desconectado: ${ip} | ${chatMessage.username}`);
    });

    ws.on('error', (err) => {
        console.error('Erro no socket:', err.message);
    });
});
