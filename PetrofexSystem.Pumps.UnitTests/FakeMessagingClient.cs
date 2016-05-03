using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetrofexSystem.Messaging;

namespace PetrofexSystem.Pumps.UnitTests
{
    class FakeMessagingClient : IMessagingClient
    {
        public Message LastMessageSent { get; private set; }

        public void Connect(Action<Message> connectionEstablishedCallback)
        {
            connectionEstablishedCallback(new Message(MessageType.Ack, new byte[0]));
        }

        public void SendMessage(Message message, Action<Message> onCompleteCallback)
        {
            this.LastMessageSent = message;
            onCompleteCallback(message);
        }

        public void SendMessageEncrypted(Message message, Action<Message> onCompleteCallback)
        {
            this.LastMessageSent = message;
            onCompleteCallback(message);
        }

        public void Disconnect(Action<Message> onCompleteCallback)
        {
            onCompleteCallback(new Message(MessageType.Ack, new byte[0]));
        }
    }
}
