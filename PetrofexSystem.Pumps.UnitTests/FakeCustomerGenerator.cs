using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PumpLibrary;

namespace PetrofexSystem.Pumps.UnitTests
{
    class FakeCustomerGenerator : ICustomerGenerator
    {
        internal bool IsPumpActive { get; private set; }
        public event CustomerGenerator.CustomerReadyHandler CustomerReady;
        public event CustomerGenerator.PumpProgressHandler PumpProgress;
        public event CustomerGenerator.PumpingFinishedHandler PumpingFinished;
        public void ActivatePump()
        {
            this.IsPumpActive = true;
        }

        public void InvokeCustomerReady(CustomerReadyEventArgs eventArgs)
        {
            this.CustomerReady(this, eventArgs);
        }

        public void InvokePumpProgress(PumpProgressEventArgs eventArgs)
        {
            this.PumpProgress(this, eventArgs);
        }

        public void InvokePumpingFinished(EventArgs e)
        {
            this.PumpingFinished(this, e);
        }
    }
}
