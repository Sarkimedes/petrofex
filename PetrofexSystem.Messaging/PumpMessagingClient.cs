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
    public class PumpMessagingClient
    {
        private readonly string _id;
        private TcpMessagingClient _client;

        public PumpMessagingClient(string id)
        {
            this._id = id;
            this._client = new TcpMessagingClient(IPAddress.Loopback.ToString(), 5000);
        }

        public void Connect(Action<byte[]> connectionEstablishedCallback)
        {
            this.SendHelloMessage(message =>
            {
                var clientPublicKey = new DESCryptoServiceProvider().Key;
                this.SendStartMessage(clientPublicKey, encryptedSharedKeyMessage =>
                {
                    Debug.WriteLine(string.Format("Client key on client: {0}", WriteByteArray(clientPublicKey)));
                    var encryptedSharedKey = encryptedSharedKeyMessage.Payload;
                    Debug.WriteLine(string.Format("Received shared key: {0}", WriteByteArray(encryptedSharedKey)));
                    var encryption = new MessageEncryption();
                    var sharedKey = encryption.DecryptBytes(encryptedSharedKey, clientPublicKey);
                    var encryptedId = encryption.Encrypt(this._id, sharedKey);
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
            var idLength = (short)encodedId.Length;
            var idLengthBytes = BitConverter.GetBytes(idLength);
            var message = new Message(MessageType.Start, idLengthBytes.Concat(encodedId).Concat(clientKey).ToArray());
            Debug.WriteLine(string.Format("Client payload: {0}", WriteByteArray(message.Payload)));
            this.SendToServer(message, callback);
        }

        private void SendToServer(Message message, Action<Message> onReceiveResult)
        {
            
            this._client.SendMessage(message, onReceiveResult);
        }
        

        public void SendMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
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
