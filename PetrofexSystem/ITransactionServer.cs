using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetrofexSystem.Common;

namespace PetrofexSystem
{
    public interface ITransactionServer
    {
        void SendFuelTransaction(Transaction transaction);
    }
}
