using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace PetrofexSystem.Messaging
{
    /// <summary>
    /// Manages a connection to the server.
    /// </summary>
    public class Connection
    {      
        internal Connection(TcpClient client, string clientId)
        {
            this.Client = client;
            this.ClientId = clientId;
            this.SymmetricKey = new DESCryptoServiceProvider().Key;
        }

        public TcpClient Client { get; private set; }

        public EndPoint Endpoint { get { return this.Client.Client.LocalEndPoint; }}

        public byte[] SymmetricKey { get; private set; }

        public string ClientId { get; private set; }

        public bool IsDisconnecting { get; private set; }

        public void SendEncryptedMessage(Message message, Action<Message> onReceivedCallback)
        {
            var encryptor = new MessageEncryption();
            var encryptedBody = encryptor.EncryptBytes(message.Payload, this.SymmetricKey);
            var sentMessage = new Message(message.MessageType, encryptedBody);
            this.Client.GetStream().Write(sentMessage.ToByteArray(), 0, sentMessage.ToByteArray().Length);

            // Wait for a response
            const int bufferSize = 65535;
            var buffer = new byte[bufferSize];
            this.Client.Client.BeginReceive(buffer, 0, bufferSize, SocketFlags.None, (result =>
            {
                var socket = (Socket)result.AsyncState;
                var read = socket.EndReceive(result);
                if (read > 0)
                {
                    var data = new MessageConverter().FromByteArray(buffer.Take(read).ToArray());
                    onReceivedCallback(data);
                }
            }), this.Client.Client);          
        }

        public void Disconnect()
        {
            if (this.Client.Connected)
            {
                this.Client.Close();
            }
        }
    }
}
