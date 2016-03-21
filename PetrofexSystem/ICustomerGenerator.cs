using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PumpLibrary;

namespace PetrofexSystem
{
    public interface ICustomerGenerator
    {
        event CustomerGenerator.CustomerReadyHandler CustomerReady;
        event CustomerGenerator.PumpProgressHandler PumpProgress;
        event CustomerGenerator.PumpingFinishedHandler PumpingFinished;
        void ActivatePump();
    }
}
