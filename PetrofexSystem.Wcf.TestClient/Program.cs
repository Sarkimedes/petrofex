using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using PetrofexSystem.Wcf.TestClient.PetrofexSystem.Messaging.Wcf.TestClient.Service;

namespace PetrofexSystem.Wcf.TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new MessagingServiceClient();
            ChannelFactory<IMessagingService> cf = new ChannelFactory<IMessagingService>();
            cf.CreateChannel();
            cf.
        }
    }
}
