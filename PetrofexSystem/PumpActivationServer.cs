using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetrofexSystem.Messaging;
using PetrofexSystem.Server;

namespace PetrofexSystem
{
    public class PumpActivationServer : IPumpActivationServer
    {
        private readonly IMessagingClient _client;
        public PumpActivationServer(IMessagingClient client)
        {
            this._client = client;
        }

        public void RequestActivation(string pumpId, Action successCallback)
        {
            var message = new Message(MessageType.ActivationRequest, Encoding.UTF8.GetBytes(pumpId));
            this._client.SendMessageEncrypted(message, m => { successCallback(); });
        }

        public void RequestDeactivation(string pumpId, Action successCallback)
        {
            var message = new Message(MessageType.DeactivationRequest, Encoding.UTF8.GetBytes(pumpId));
            this._client.SendMessageEncrypted(message, m => { successCallback(); });
        }
    }
}
