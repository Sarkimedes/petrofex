using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PetrofexSystem.Messaging
{
    public class TcpMessagingClient
    {
        private readonly string _host;
        private readonly int _port;

        public TcpMessagingClient(string host, int port)
        {
            this._host = host;
            this._port = port;
        }

        public void SendMessage(Message message, Action<Message> responseCallback)
        {
            var client = new TcpClient(this._host, this._port);
            var stream = client.GetStream();
            stream.Write(message.ToByteArray(), 0, message.ToByteArray().Length);

            var buffer = new byte[1024];
            client.Client.BeginReceive(buffer, 0, 1024, SocketFlags.None, (result =>
            {
                var socket = (Socket)result.AsyncState;
                var read = socket.EndReceive(result);
                if (read > 0)
                {
                    var data = new MessageConverter().FromByteArray(buffer.Take(read).ToArray());
                    responseCallback(data);
                }
            }), client.Client);
        }
    }
}
