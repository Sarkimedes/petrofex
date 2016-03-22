using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PumpLibrary;

namespace PetrofexSystem
{
    class CustomerGeneratorAdapter : ICustomerGenerator
    {
        private readonly CustomerGenerator _customerGenerator;

        public CustomerGeneratorAdapter(CustomerGenerator customerGenerator)
        {
            this._customerGenerator = customerGenerator;
        }

        public event CustomerGenerator.CustomerReadyHandler CustomerReady
        {
            add { this._customerGenerator.CustomerReady += value; }
            remove { this._customerGenerator.CustomerReady -= value; }
        }
        
        public event CustomerGenerator.PumpProgressHandler PumpProgress
        {
            add { this._customerGenerator.PumpProgress += value; }
            remove { this._customerGenerator.PumpProgress -= value; }
        }

        public event CustomerGenerator.PumpingFinishedHandler PumpingFinished
        {
            add { this._customerGenerator.PumpingFinished += value; }
            remove { this._customerGenerator.PumpingFinished -= value; }
        }

        public void ActivatePump()
        {
            this._customerGenerator.ActivatePump();
        }
    }
}
