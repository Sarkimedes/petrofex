using System;
using PetrofexSystem.Messaging;

namespace PetrofexSystem.Messaging
{
    public interface IMessagingClient
    {
        void Connect(Action<Message> connectionEstablishedCallback);
        void SendMessage(Message message, Action<Message> onCompleteCallback);
        void SendMessageEncrypted(Message message, Action<Message> onCompleteCallback);
        void Disconnect(Action<Message> onCompleteCallback);
    }
}
