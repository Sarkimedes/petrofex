using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace PetrofexSystem.Messaging
{
    [Serializable]
    public class Message
    {
        public Message(MessageType messageType, string payload)
        {
            this.MessageType = messageType;
            this.Payload = payload;
            this.Timestamp = DateTime.Now;
        }

        public MessageType MessageType { get; private set; }
        public string Payload { get; private set; }
        internal DateTime Timestamp { get; private set; }
    }
}
