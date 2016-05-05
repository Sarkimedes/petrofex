using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetrofexSystem.PosTerminals.UnitTests
{
    public class MockPumpFactory : IPumpFactory
    {
        public Pump AddedPump { get; private set; }

        public void AddPump(Pump pump)
        {
            this.AddedPump = pump;
        }

        public Pump GetPumpById(string id)
        {
            return this.AddedPump ?? new Pump(id, new FakePaymentServer(), this);
        }
    }
}
