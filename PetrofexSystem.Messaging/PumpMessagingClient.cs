using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using PetrofexSystem.Messaging;

namespace PetrofexSystem
{
    public class PumpMessagingClient : IMessagingClient
    {
        private readonly string _id;
        private readonly TcpMessagingClient _client;
        private byte[] _sharedKey;

        public PumpMessagingClient(string id)
        {
            this._id = id;
            this._client = new TcpMessagingClient(IPAddress.Loopback.ToString(), 5000);
        }

        public void Connect(Action<Message> connectionEstablishedCallback)
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
                    this._sharedKey = encryption.DecryptBytes(encryptedSharedKey, clientPublicKey);
                    var encryptedId = encryption.Encrypt(this._id, this._sharedKey);
                    var connectedMessage = new Message(MessageType.Connected, encryptedId);
                    this.SendToServer(connectedMessage, connectionEstablishedCallback);
                });

            });
        }

        public void SendMessage(Message message, Action<Message> onCompleteCallback)
        {
            this._client.SendMessage(message, onCompleteCallback);
        }

        public void SendMessageEncrypted(Message message, Action<Message> onCompleteCallback)
        {
            var body = message.Payload;
            var encryptor = new MessageEncryption();
            var encrypted = encryptor.EncryptBytes(body, this._sharedKey);
            this._client.SendMessage(new Message(message.MessageType, encrypted), onCompleteCallback);
        }

        public void Disconnect(Action<Message> onCompleteCallback)
        {
            var disconnectMessage = new Message(MessageType.Disconnect, new byte[0]);
            this._client.SendMessage(disconnectMessage, onCompleteCallback);
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
