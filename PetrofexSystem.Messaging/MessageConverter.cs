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
           
            // Message body is the message with headers trimmed off
            var messageBody = trimmedMessage.Skip(3).ToArray();
            return new Message(messageType, messageBody);
        }

        public byte[] ToByteArray(Message message)
        {
            return message.ToByteArray();
        }
    }
}
