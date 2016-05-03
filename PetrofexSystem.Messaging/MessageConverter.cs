using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetrofexSystem.Messaging
{
    public class MessageConverter
    {
        public Message FromByteArray(byte[] messageBytes)
        {
            // First three bytes are always 0, so strip them off
            var trimmedMessage = messageBytes.Skip(3).ToArray();
            MessageType messageType;
            try
            {
                messageType = (MessageType) trimmedMessage.First();
            }
            catch (InvalidCastException e)
            {
                messageType = MessageType.Error;
            }
            var lengthBytes = trimmedMessage.Skip(1).Take(2).ToArray();
            var bodyLength = BitConverter.IsLittleEndian
                ? BitConverter.ToInt16(lengthBytes, 0)
                : BitConverter.ToInt16(lengthBytes.Reverse().ToArray(), 0);
            // Message body is the message with headers trimmed off
            var messageBody = trimmedMessage.Skip(3).Take(bodyLength).ToArray();
            return new Message(messageType, messageBody);
        }

        public byte[] ToByteArray(Message message)
        {
            return message.ToByteArray();
        }
    }
}
