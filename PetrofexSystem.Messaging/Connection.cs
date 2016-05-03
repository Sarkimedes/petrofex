using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace PetrofexSystem.Messaging
{
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

        public void SendEncryptedMessage(Message message)
        {
            var encryptor = new MessageEncryption();
            var encryptedBody = encryptor.EncryptBytes(message.Payload, this.SymmetricKey);
            var sentMessage = new Message(message.MessageType, encryptedBody);
            this.Client.GetStream().Write(sentMessage.ToByteArray(), 0, sentMessage.ToByteArray().Length);
        }
    }
}
