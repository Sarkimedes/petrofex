using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetrofexSystem.Server
{
    public interface INotifyPumpActivated
    {
        event Action<string> PumpActivationRequested;
        event Action<string> PumpDeactivationRequested;
    }
}
