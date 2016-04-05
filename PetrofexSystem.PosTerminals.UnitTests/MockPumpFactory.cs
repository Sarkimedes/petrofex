using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetrofexSystem.PosTerminals.UnitTests
{
    class MockPumpFactory : IPumpFactory
    {
        public List<Pump> pumpsCreated = new List<Pump>(); 

        public Pump GetPumpById(string id)
        {
            var pump = new Pump(id, new FakePaymentServer());
            pumpsCreated.Add(pump);
            return pump;
        }
    }
}
