using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using PetrofexSystem.Common;
using PetrofexSystem.Messaging;

namespace PetrofexSystem
{
    public class TransactionServer : ITransactionServer
    {
        private readonly IMessagingClient _client;
        public TransactionServer(IMessagingClient client)
        {
            this._client = client;
        }

        public void SendFuelTransaction(Transaction transaction)
        {
            var serialiser = new DataContractJsonSerializer(typeof(Transaction));
            using (var stream = new MemoryStream())
            {
                serialiser.WriteObject(stream, transaction);
                this._client.SendMessageEncrypted(new Message(MessageType.Transaction, stream.ToArray()), message => { });
            }
            
        }
    }
}
