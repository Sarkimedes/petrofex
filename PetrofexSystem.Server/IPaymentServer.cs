using System;
using PetrofexSystem.Common;

namespace PetrofexSystem.Server
{
    public interface IPaymentServer
    {
        void SendForProcessing(Transaction transaction, Action callback = null);
    }
}
