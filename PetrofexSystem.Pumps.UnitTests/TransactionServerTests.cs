using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetrofexSystem.Common;
using PumpLibrary;

namespace PetrofexSystem.Pumps.UnitTests
{
    [TestClass]
    public class TransactionServerTests
    {
        [TestMethod]
        public void Transaction_WhenSent_IsSerializedOutToJson()
        {
            var messagingClient = new FakeMessagingClient();
            var transaction = new TransactionServer(messagingClient);
            transaction.SendFuelTransaction(new Transaction()
            {
                FuelType = FuelType.Diesel,
                LitresPumped = 5, 
                PumpId = "Test",
                TotalAmount = 5
            });

            var messageBody = messagingClient.LastMessageSent.Payload;

            var serializer = new DataContractJsonSerializer(typeof (Transaction));
            using (var stream = new MemoryStream(messageBody))
            {
                var deserializedTransaction = (Transaction)serializer.ReadObject(stream);
                Assert.IsTrue(deserializedTransaction.FuelType == FuelType.Diesel);
            }
        }
    }
}
