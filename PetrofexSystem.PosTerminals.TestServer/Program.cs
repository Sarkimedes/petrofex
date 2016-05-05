using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using PetrofexSystem.Messaging;

namespace PetrofexSystem.PosTerminals.TestServer
{
    class Program
    {
        private static readonly ManualResetEvent TcpClientConnected = new ManualResetEvent(false);

        static void Main(string[] args)
        {

            var listener = new TcpListener(IPAddress.Loopback, 5000);
            listener.Start();
            while (true)
            {
                TcpClientConnected.Reset();

                listener.BeginAcceptTcpClient(AcceptSocket, listener);

                TcpClientConnected.WaitOne();
            }
        }

        private static void AcceptSocket(IAsyncResult result)
        {
            var tcpListener = (TcpListener)result.AsyncState;
            var client = tcpListener.EndAcceptTcpClient(result);
            RespondToMessage(client);
            TcpClientConnected.Set();
        }

        private static void RespondToMessage(TcpClient client)
        {
            var buffer = new byte[65535];
            var stream = client.GetStream();
            stream.Read(buffer, 0, buffer.Length);
            var messageConverter = new MessageConverter();
            var message = messageConverter.FromByteArray(buffer);
            var timeout = new TimeSpan(0, 0, 2, 0);
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            if (message.MessageType != MessageType.Connected)
            {
                RespondToMessage(client);
            }
        }
    }
}
