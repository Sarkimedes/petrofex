using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PetrofexSystem.Messaging.UnitTests
{
    [TestClass]
    public class MessageTests
    {
        [TestMethod]
        public void ToByteArray_WithValidMessage_AddsThreeEmptyBytes()
        {
            Message message = new Message(MessageType.Hello, new byte[0]);

            var bytes = message.ToByteArray();

            Assert.IsTrue(new byte[] {0, 0, 0}.SequenceEqual(bytes.Take(3).ToArray()));
        }

        [TestMethod]
        public void ToByteArray_WithMessageBody_SetsLengthBytesCorrectly()
        {
            Message message = new Message(MessageType.Hello, Encoding.UTF8.GetBytes("Test"));

            var bytes = message.ToByteArray();

            Assert.AreEqual(bytes[4], 4);
            Assert.AreEqual(bytes[5], 0);
        }

        [TestMethod]
        public void ToByteArray_WithMessageBodyLongerThan255Characters_SetsLengthBytesCorrectly()
        {
            Message message = new Message(MessageType.Hello, Encoding.UTF8.GetBytes(new string('a', 258)));

            var bytes = message.ToByteArray();

            // 258 should be represented as 0x0102
            Assert.AreEqual(bytes[4], 2);
            Assert.AreEqual(bytes[5], 1);

            Message message2 = new Message(MessageType.Hello, Encoding.UTF8.GetBytes(new string('a', 513)));

            var bytes2 = message2.ToByteArray();

            // 513 should be represented as 0x0201
            Assert.AreEqual(bytes2[4], 1);
            Assert.AreEqual(bytes2[5], 2);
        }

        [TestMethod]
        public void ToByteArray_ReturnsMessageTypeAsFourthByte()
        {
            Message message = new Message(MessageType.Hello, new byte[0]);

            var bytes = message.ToByteArray();

            Assert.AreEqual(1, bytes[3]);
        }

        [TestMethod]
        public void ToByteArray_WithMessageBody_EncodesMessageBody()
        {
            var payload = Encoding.UTF8.GetBytes("This is a test message");
            var message = new Message(MessageType.Hello, payload);

            var bytes = message.ToByteArray();

            Assert.IsTrue(payload.SequenceEqual(bytes.Skip(6).ToArray()));
        }

        [TestMethod]
        public void Test1()
        {
            var client = new TcpClient();
            var wrapper = new ClientWrapper(client);

            Assert.IsTrue(client == wrapper.Client);
        }

        public class ClientWrapper
        {
            public ClientWrapper(TcpClient client)
            {
                this.Client = client;
            }

            public TcpClient Client { get; private set; }
        }
    }
}
