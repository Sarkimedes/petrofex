using System;

namespace PetrofexSystem.Messaging
{
    interface IMessageListener
    {
        void Start(Action<Message> onMessageReceived);
    }
}
