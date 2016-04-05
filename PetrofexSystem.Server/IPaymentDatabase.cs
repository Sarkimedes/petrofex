using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetrofexSystem.Common;

namespace PetrofexSystem.Server
{
    public interface IPaymentDatabase
    {
        void SaveTransaction(Transaction transaction);
    }
}