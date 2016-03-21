using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PetrofexSystem
{
    /// <summary>
    /// Defines an object which gets fuel prices from a central server.
    /// </summary>
    interface IFuelPricesServer
    {
        /// <summary>
        /// Gets a list of fuel prices from the server, showing type of fuel and cost per unit.
        /// </summary>
        /// <returns></returns>
        IEnumerable<FuelPrice> GetFuelPrices();
    }
}
