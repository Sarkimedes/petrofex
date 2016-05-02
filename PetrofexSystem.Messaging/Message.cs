using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace PetrofexSystem.Messaging
{
    [Serializable]
    [DataContract]
    public class Message
    {
        public Message(MessageType messageType, byte[] payload)
        {
            this.MessageType = messageType;
            this.Payload = payload;
        }

        [DataMember]
        public MessageType MessageType { get; private set; }

        [DataMember]
        public byte[] Payload { get; private set; }

        public byte[] ToByteArray()
        {
            var start = new byte[] { 0, 0, 0 };
            var messageType = (byte) this.MessageType;
            var length = Convert.ToInt16(this.Payload.Length);
            // Convert length to bytes
            var lengthBytes = BitConverter.GetBytes(length);


            return start.Concat(new[] {messageType}).Concat(lengthBytes).Concat(this.Payload).ToArray();
        }
    }
}
