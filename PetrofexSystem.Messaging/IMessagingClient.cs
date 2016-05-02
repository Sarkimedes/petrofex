using System;

namespace PetrofexSystem.Messaging
{
    public interface IMessagingClient
    {
        void Connect(string host, int port, Action<string> connectionEstablishedCallback);
        void SendTransaction(Message message);
        void Disconnect();
    }
}
