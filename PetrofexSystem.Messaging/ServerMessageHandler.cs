using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace PetrofexSystem.Messaging
{
    public class ServerMessageHandler
    {
        private readonly byte[] _serverKey;
        private readonly ConnectionPool _connectionPool;

        public ServerMessageHandler()
        {
            this._serverKey = new DESCryptoServiceProvider().Key;
            this._connectionPool = new ConnectionPool();
        }

        public void RespondToMessage(Message message, TcpClient client)
        {
            switch (message.MessageType)
            {
                case MessageType.Hello:
                    var pubKeyMessage = this.CreatePublicKeyMessage();
                    this.SendMessage(pubKeyMessage, client);
                    break;
                case MessageType.Start:
                    // The first two bytes of the message body are the identifier length
                    // Small byte is placed first
                    Debug.WriteLine(string.Format("Server payload: {0}", WriteByteArray(message.Payload)));
                    var lengthBytes = message.Payload.Take(2);
                    var identifierLength = BitConverter.IsLittleEndian
                        ? BitConverter.ToInt16(lengthBytes.ToArray(), 0)
                        : BitConverter.ToInt16(lengthBytes.Reverse().ToArray(), 0);
                    // Following that is the identifier
                    var identifierBytes = message.Payload.Skip(2).Take(identifierLength);
                    // The rest of the body is the key
                    var clientKeyBytes = message.Payload.Skip(2 + identifierLength);
                    // Add a new connection to the pool. Any further data sending will be done via this
                    var connection =
                        this._connectionPool.GetConnection(Encoding.UTF8.GetString(identifierBytes.ToArray()), client);
                    var startOkMessage = this.CreateStartOkMessage(connection.SymmetricKey, clientKeyBytes.ToArray());
                    Debug.WriteLine(string.Format("Sent key: {0}", WriteByteArray(startOkMessage.Payload)));
                    this.SendMessage(startOkMessage, client);
                    break;
                case MessageType.Connected:
                    var connectedConnection = this._connectionPool.GetConnection(client);
                    connectedConnection.SendEncryptedMessage(new Message(MessageType.Ack, Encoding.UTF8.GetBytes(connectedConnection.ClientId)));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }            
        }

        private void SendMessage(Message message, TcpClient client)
        {
            client.GetStream().Write(message.ToByteArray(), 0, message.ToByteArray().Length);
        }

        private Message CreateStartOkMessage(byte[] symmetricKey, byte[] clientKey)
        {
            var encryptor = new MessageEncryption();
            Debug.WriteLine(string.Format("Client key on server: {0}", WriteByteArray(clientKey)));
            var body = encryptor.EncryptBytes(symmetricKey, clientKey);
            return new Message(MessageType.StartOk, body);
        }

        private Message CreatePublicKeyMessage()
        {
            return new Message(MessageType.PubKey, this._serverKey);
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
