using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using PetrofexSystem.Server;

namespace PetrofexSystem
{
    public interface IPumpServiceLocator
    {
        IPumpActivationServer PumpActivationServer { get; }
        ICustomerGenerator CustomerGenerator { get; }
        IFuelPricesServer FuelPricesServer { get; }
        ITransactionServer TransactionServer { get; }
    }
}
