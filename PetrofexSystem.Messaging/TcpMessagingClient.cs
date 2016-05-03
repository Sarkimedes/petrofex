using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;

namespace PetrofexSystem.Messaging
{
    public class TcpMessagingClient
    {
        private readonly string _host;
        private readonly int _port;
        private readonly TcpClient _client;

        public TcpMessagingClient(string host, int port)
        {
            this._host = host;
            this._port = port;
            this._client = new TcpClient(this._host, this._port);
        }

        public void SendMessage(Message message, Action<Message> responseCallback)
        {            
            var stream = this._client.GetStream();
            stream.Write(message.ToByteArray(), 0, message.ToByteArray().Length);
            Debug.WriteLine(string.Format("TCP client bytes: {0}", WriteByteArray(message.ToByteArray())));
            const int bufferSize = 65535;
            var buffer = new byte[bufferSize];
            this._client.Client.BeginReceive(buffer, 0, bufferSize, SocketFlags.None, (result =>
            {
                var socket = (Socket)result.AsyncState;
                var read = socket.EndReceive(result);
                if (read > 0)
                {
                    var data = new MessageConverter().FromByteArray(buffer.Take(read).ToArray());
                    responseCallback(data);
                }
            }), this._client.Client);
        }

        private static string WriteByteArray(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (var b in bytes)
            {
                sb.Append(b + " ");
            }
            return sb.ToString();
        }
    }
}
