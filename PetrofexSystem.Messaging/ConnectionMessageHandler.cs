using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace PetrofexSystem.Messaging
{
    public class ConnectionMessageHandler
    {
        private readonly Connection _connection;

        public ConnectionMessageHandler(Connection connection)
        {
            this._connection = connection;
        }

        public void HandleMessage(Message message)
        {
            var messageType = message.MessageType;
            var payload = message.Payload;
            switch (messageType)
            {
                case MessageType.Ack:
                    break;
                case MessageType.Disconnect:
                    // If server has not already made a disconnection request
                    if (!this._connection.IsDisconnecting)
                    {
                        var disconnectionMessage = new Message(MessageType.Disconnect, new byte[0]);
                        this._connection.SendEncryptedMessage(disconnectionMessage, m => { });
                    }
                    else
                    {
                        // If the server initiated the disconnection
                        this._connection.Disconnect();
                    }
                    break;
                case MessageType.Error:
                    break;
            }
        }
    }
}
