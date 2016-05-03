using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetrofexSystem.Messaging;
using PetrofexSystem.Server;

namespace PetrofexSystem
{
    class PumpActivationServer : IPumpActivationServer
    {
        private readonly IMessagingClient _client;
        public PumpActivationServer(IMessagingClient client)
        {
            this._client = client;
        }

        public void RequestActivation(string pumpId)
        {
            var message = new Message(MessageType.ActivationRequest, Encoding.UTF8.GetBytes(pumpId));
            SendMessage(message);
        }

        public void RequestDeactivation(string pumpId)
        {
            var message = new Message(MessageType.DeactivationRequest, Encoding.UTF8.GetBytes(pumpId));
            SendMessage(message);
        }

        private void SendMessage(Message message)
        {
            this._client.SendMessageEncrypted(message, m => { });
        }
    }
}
