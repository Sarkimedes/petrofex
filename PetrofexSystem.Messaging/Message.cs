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
        }

        public MessageType MessageType { get; private set; }
        public string Payload { get; private set; }

        public byte[] ToByteArray()
        {
            var start = new byte[] { 0, 0, 0 };
            var messageType = (byte) this.MessageType;
            var length = Convert.ToInt16(this.Payload.Length);
            // Convert length to bytes
            var lengthBytes = BitConverter.GetBytes(length);

            var payloadBytes = Encoding.UTF8.GetBytes(this.Payload);

            return start.Concat(new[] {messageType}).Concat(lengthBytes).Concat(payloadBytes).ToArray();
        }
    }
}
