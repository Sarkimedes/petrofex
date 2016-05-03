using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using PetrofexSystem.Messaging;

namespace PetrofexSystem.TestServer
{
    class Program
    {
        private static ManualResetEvent tcpClientConnected = new ManualResetEvent(false);
        static void Main(string[] args)
        {
            var listener = new TcpListener(IPAddress.Loopback, 5000);
            listener.Start();
            while (true)
            {
                tcpClientConnected.Reset();

                listener.BeginAcceptTcpClient(AcceptSocket, listener);

                tcpClientConnected.WaitOne();
            }
        }

        private static void AcceptSocket(IAsyncResult result)
        {
            var tcpListener = (TcpListener)result.AsyncState;
            var client = tcpListener.EndAcceptTcpClient(result);
            using (var stream = client.GetStream())
            {
                var reader = new StreamReader(client.GetStream());

                var buffer = new char[1024];
                reader.Read(buffer, 0, 1024);
                var messageConverter = new MessageConverter();
                Console.WriteLine(messageConverter.FromByteArray(Encoding.UTF8.GetBytes(buffer)).ToString());
                var messageBytes = new Message(MessageType.PubKey, new byte[] {1, 2, 3, 4, 5}).ToByteArray();
                stream.Write(messageBytes, 0, messageBytes.Length);
                reader.Close();
                tcpClientConnected.Set();
            }
        }
    }
}
