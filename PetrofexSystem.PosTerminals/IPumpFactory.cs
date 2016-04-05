using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetrofexSystem.PosTerminals
{
    public interface IPumpFactory
    {
        Pump GetPumpById(string id);
    }
}
