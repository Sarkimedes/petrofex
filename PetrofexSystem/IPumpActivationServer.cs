using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetrofexSystem
{
    public interface IPumpActivationServer
    {
        void RequestActivation(string pumpId);
    }
}
