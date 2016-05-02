using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels.Tcp;
using System.Security.Cryptography;
using System.Text;
using PetrofexSystem.Messaging;

namespace PetrofexSystem
{
    internal class PumpMessagingClient : IMessagingClient
    {
        private readonly string _id;
        private byte[] _sharedKey;

        public PumpMessagingClient(string id)
        {
            this._id = id;
        }

        public void Connect(Func<byte[]> connectionEstablishedCallback)
        {
            var serverPublicKey = this.GetServerPublicKey();
            var clientPublicKey = new DESCryptoServiceProvider().Key;
            var encryptedSharedKey = this.GetSharedSymmetricKey(clientPublicKey);
            var encrytor = new MessageEncryption();
            this._sharedKey = Encoding.UTF8.GetBytes(encrytor.Decrypt(encryptedSharedKey, clientPublicKey));
            var encryptedId = encrytor.Encrypt(this._id, this._sharedKey);
            var connectedMessage = new Message(MessageType.Connected, encryptedId);
            GetResponseFromServer(connectedMessage);
        }

        private byte[] GetServerPublicKey()
        {
            var encodedId = Encoding.UTF8.GetBytes(this._id);
            var helloMessage = new Message(MessageType.Hello, encodedId);
            return this.GetResponseFromServer(helloMessage).Payload;
        }

        private byte[] GetSharedSymmetricKey(byte[] clientKey)
        {
            var encodedId = Encoding.UTF8.GetBytes(this._id);
            var message = new Message(MessageType.Start, encodedId.Concat(clientKey).ToArray());
            var response = this.GetResponseFromServer(message);
            return response.Payload;
        }

        private Message GetResponseFromServer(Message message)
        {
            var client = new MessagingService.MessagingServiceClient();
            var response = client.SendMessage(message.ToByteArray());
            return new MessageConverter().FromByteArray(response);
        }
        

        public void SendMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }
    }
}
