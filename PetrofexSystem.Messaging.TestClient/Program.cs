using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace PetrofexSystem.Messaging.TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey();
                var message = new Message(MessageType.Hello, new byte[0]);
                SendData(message, null);
                //var client = new TcpClient(IPAddress.Loopback.ToString(), 5000);
                //var stream = client.GetStream();

                //;
                //stream.Write(message.ToByteArray(), 0, message.ToByteArray().Length);

                //byte[] buffer = new byte[1024];
                //client.Client.BeginReceive(buffer, 0, 1024, SocketFlags.None, (result =>
                //{
                //    var socket = (Socket)result.AsyncState;
                //    socket.EndReceive(result);
                //    Console.WriteLine("Received data");
                //    stream.Close();
                //}), client.Client);

            } while (key.Key != ConsoleKey.Escape);
        }

        private static void SendData(Message message, Action<Message> callback)
        {
            var client = new TcpClient(IPAddress.Loopback.ToString(), 5000);
            var stream = client.GetStream();
            stream.Write(message.ToByteArray(), 0, message.ToByteArray().Length);

            var buffer = new byte[1024];
            client.Client.BeginReceive(buffer, 0, 1024, SocketFlags.None, (result =>
            {
                var socket = (Socket)result.AsyncState;
                var read = socket.EndReceive(result);
                if (read > 0)
                {
                    var data = new MessageConverter().FromByteArray(buffer.Take(read).ToArray());
                    Console.WriteLine(data);
                }
            }), client.Client);
        }
    }
}