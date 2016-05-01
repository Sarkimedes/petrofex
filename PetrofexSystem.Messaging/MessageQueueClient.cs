using System;
using System.Net.Http;

namespace PetrofexSystem.Messaging
{
    class MessageQueueClient
    {
        private readonly Uri _queueUri;

        public MessageQueueClient(Uri queueUri)
        {
            if (queueUri == null)
                throw new ArgumentNullException("queueUri");
            this._queueUri = queueUri;
        }

        public void Send(Message message)
        {
            using (var client = new HttpClient())
            {
                
            }
        }
    }
}
