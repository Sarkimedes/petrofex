using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using PetrofexSystem.Common;
using PetrofexSystem.Messaging;

namespace PetrofexSystem.PosTerminals.UI
{
    class PosTerminal
    {
        private string _id;
        private MessagingClient _client;

        private bool _connected;
        private byte[] _sharedKey;

        public PosTerminal()
        {
            this._id = "POSTERMINAL" + Guid.NewGuid();
            this._client = new MessagingClient(this._id);
            this._client.Connect(m =>
            {
                
                this._client.ListenForEncryptedMessage(this.StartListeningForEncrypted);
            });
        }

        public void StartListeningForEncrypted(Message message)
        {
            this._sharedKey = this._client.SharedKey;

            if (message.MessageType != MessageType.PumpActivated)
            {
                StartListeningForEncrypted(message);
            }
            else
            {
                var body = message.Payload;
                var decryptor = new MessageEncryption();
                var decrypted = decryptor.DecryptBytes(body, this._sharedKey);
                HandleMessage(new Message(message.MessageType, decrypted));
            }
        }
        public void HandleMessage(Message message) 
        {
            switch (message.MessageType)
            {
                case MessageType.PumpActivated:
                    HandlePumpActivationRequested(Encoding.UTF8.GetString(message.Payload));                  
                    break;
                default:
                    break;
            }
        }

        public ICollection<PumpVm> KnownPumps { get; set; }

        public void HandlePumpActivationRequested(string pumpId)
        {
            if (this.KnownPumps.Select(x => x.Id).Contains(pumpId))
            {
                Debug.WriteLine("Pump activated");
                var pump = this.KnownPumps.First(x => x.Id == pumpId);
                pump.IsCustomerWaiting = true;
            }
            else
            {
                this.KnownPumps.Add(new PumpVm()
                {
                    Id = pumpId,
                    IsCustomerWaiting = true,
                    IsTransactionPending = false
                });
            }
        }

        public void HandlePumpProgress(Transaction transaction)
        {
            var pump = this.KnownPumps.FirstOrDefault(x => x.Id == transaction.PumpId);
            if (pump != null)
            {
                pump.CurrenTransaction = transaction;
            }
        }
    }
}
