using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetrofexSystem.Common;

namespace PetrofexSystem.PosTerminals
{
    public interface IPaymentServer
    {
        void SendForProcessing(Transaction transaction);
    }
}
