using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Json;
using System.Text;
using PetrofexSystem.Common;

namespace PetrofexSystem.Messaging
{
    public class ConnectionMessageHandler
    {
        private readonly Connection _connection;

        public event Action<string> PumpActivated;
        public event Action<string> PumpDeactivated;
        public event Action<Transaction> PumpProgress;

        public ConnectionMessageHandler(Connection connection)
        {
            this._connection = connection;
        }

        private byte[] GetDecryptedBody(byte[] encryptedBody)
        {
            var encryption = new MessageEncryption();
            return encryption.DecryptBytes(encryptedBody, this._connection.SymmetricKey);
        }

        public void HandleMessage(Message message)
        {
            var messageType = message.MessageType;
            var payload = message.Payload;
            switch (messageType)
            {
                case MessageType.Disconnect:
                    // If server has not already made a disconnection request
                    if (!this._connection.IsDisconnecting)
                    {
                        var disconnectionMessage = new Message(MessageType.Disconnect, new byte[0]);
                        this._connection.SendEncryptedMessage(disconnectionMessage, this.HandleMessage);
                    }
                    else
                    {
                        // If the server initiated the disconnection
                        this._connection.Disconnect();
                    }
                    break;
                case MessageType.Error:
                    // Stop here
                    break;
                case MessageType.ActivationRequest:
                    var decryptedPumpId = GetDecryptedBody(payload);
                    var onPumpActivated = this.PumpActivated;
                    if (onPumpActivated != null) onPumpActivated(Encoding.UTF8.GetString(decryptedPumpId));
                    var activationAcknowledgement = new Message(MessageType.Ack, new byte[0]);
                    this._connection.SendEncryptedMessage(activationAcknowledgement, this.HandleMessage);
                    break;
                case MessageType.DeactivationRequest:
                    var decryptedPumpId2 = GetDecryptedBody(payload);
                    var onPumpDeactivated = this.PumpDeactivated;
                    if (onPumpDeactivated != null) onPumpDeactivated(Encoding.UTF8.GetString(decryptedPumpId2));
                    var deactivationAcknowledgement = new Message(MessageType.Ack, new byte[0]);
                    this._connection.SendEncryptedMessage(deactivationAcknowledgement, this.HandleMessage);
                    break;
                case MessageType.Transaction:
                    var decryptedTransactionBody = GetDecryptedBody(payload);
                    var serializer = new DataContractJsonSerializer(typeof (Transaction));
                    using (var memoryStream = new MemoryStream(decryptedTransactionBody))
                    {
                        var transaction = (Transaction) serializer.ReadObject(memoryStream);
                        var onPumpProgress = this.PumpProgress;
                        if (onPumpProgress != null) onPumpProgress(transaction);
                    }

                    this._connection.SendEncryptedMessage(new Message(MessageType.Ack, new byte[0]), this.HandleMessage);
                    break;
                default:
                    break;
            }
        }
    }
}
