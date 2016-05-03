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
            this.SendHelloMessage(message =>
            {
                var clientPublicKey = new DESCryptoServiceProvider().Key;
                this.SendStartMessage(clientPublicKey, encryptedSharedKeyMessage =>
                {
                    var encryptedSharedKey = encryptedSharedKeyMessage.Payload;
                    var encryption = new MessageEncryption();
                    this._sharedKey = Encoding.UTF8.GetBytes(encryption.Decrypt(encryptedSharedKey, clientPublicKey));
                    var encryptedId = encryption.Encrypt(this._id, this._sharedKey);
                    var connectedMessage = new Message(MessageType.Connected, encryptedId);
                    this.SendToServer(connectedMessage, connectionAcknowledgedMessage =>
                    {
                        connectionEstablishedCallback(connectionAcknowledgedMessage.Payload);
                    });
                });

            });
        }

        private void SendHelloMessage(Action<Message> doneCallback)
        {
            var encodedId = Encoding.UTF8.GetBytes(this._id);
            var helloMessage = new Message(MessageType.Hello, encodedId);
            this.SendToServer(helloMessage, doneCallback);
        }

        private void SendStartMessage(byte[] clientKey, Action<Message> callback)
        {
            var encodedId = Encoding.UTF8.GetBytes(this._id);
            var message = new Message(MessageType.Start, encodedId.Concat(clientKey).ToArray());
            this.SendToServer(message, callback);
        }

        private void SendToServer(Message message, Action<Message> onReceiveResult)
        {
            var client = new TcpMessagingClient(IPAddress.Loopback.ToString(), 5000);
            client.SendMessage(message, onReceiveResult);
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
