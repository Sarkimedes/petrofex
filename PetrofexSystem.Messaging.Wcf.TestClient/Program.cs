using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetrofexSystem.Messaging.Wcf.TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new TestClient.ServiceReference1.MessagingServiceClient();
            var response = client.SendMessage(new byte[] {1, 2, 3, 4, 5});
            Console.WriteLine(response);
            Console.ReadKey();
        }
    }
}
