using System;

namespace PetrofexSystem.Messaging
{
    public interface IMessagingClient
    {
        void Connect(Func<byte[]> connectionEstablishedCallback);
        void SendMessage(Message message);
        void Disconnect();
    }
}
