using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using PetrofexSystem.Messaging;

namespace PetrofexSystem
{
    internal class PumpMessagingClient
    {
        private readonly string _id;
        private byte[] _sharedKey;

        public PumpMessagingClient(string id)
        {
            this._id = id;
        }

        public void Connect(Action<byte[]> connectionEstablishedCallback)
        {
            var serverPublicKey = this.GetServerPublicKey();
            var clientPublicKey = new DESCryptoServiceProvider().Key;
            var encryptedSharedKey = this.GetSharedSymmetricKey(clientPublicKey);
            var encryption = new MessageEncryption();
            this._sharedKey = Encoding.UTF8.GetBytes(encryption.Decrypt(encryptedSharedKey, clientPublicKey));
            var encryptedId = encryption.Encrypt(this._id, this._sharedKey);
            var connectedMessage = new Message(MessageType.Connected, encryptedId);
            this.SendToServer(connectedMessage, message =>
            {
                connectionEstablishedCallback(message.Payload);
            });
        }

        private byte[] GetServerPublicKey()
        {
            var encodedId = Encoding.UTF8.GetBytes(this._id);
            var helloMessage = new Message(MessageType.Hello, encodedId);
            byte[] publicKey = null;
            this.SendToServer(helloMessage, message => publicKey = message.Payload);
            return publicKey;
        }

        private byte[] GetSharedSymmetricKey(byte[] clientKey)
        {
            var encodedId = Encoding.UTF8.GetBytes(this._id);
            var message = new Message(MessageType.Start, encodedId.Concat(clientKey).ToArray());
            byte[] key = null;
            this.SendToServer(message, resultMessage => key = message.Payload);
            return key;
        }

        private void SendToServer(Message message, Action<Message> onReceiveResult)
        {
            throw new NotImplementedException();
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
