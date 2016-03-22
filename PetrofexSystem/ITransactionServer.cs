using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetrofexSystem
{
    public interface ITransactionServer
    {
        void SendFuelTransaction(FuelTransaction transaction);
    }
}
