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
            Console.WriteLine("Client");
            do
            {
                key = Console.ReadKey();
                var client = new PumpMessagingClient("Test");
                Console.WriteLine("Attempting to connect");
                client.Connect(_ => Console.WriteLine("Successfully connected"));

            } while (key.Key != ConsoleKey.Escape);
        }
    }
}