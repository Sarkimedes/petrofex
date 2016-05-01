using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PetrofexSystem.Messaging.UnitTests
{
    [TestClass]
    public class MessageSerializerTests
    {
        [TestMethod]
        public void Serialize_WithValidMessage_SerializesMessage()
        {
            Message message = new Message(MessageType.Hello, "test");
            MessageSerializer serializer = new MessageSerializer();

            var result = serializer.Serialize(message);
            var deserialized = serializer.Deserialize(result);

            Assert.AreEqual(MessageType.Hello, deserialized.MessageType);
            Assert.AreEqual(deserialized.Timestamp.ToLongTimeString(), message.Timestamp.ToLongTimeString());
            Assert.AreEqual("test", deserialized.Payload);
        }
    }
}
