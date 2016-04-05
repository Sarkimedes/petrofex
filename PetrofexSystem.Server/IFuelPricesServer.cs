using System.Collections.Generic;
using PumpLibrary;

namespace PetrofexSystem
{
    /// <summary>
    /// Defines an object which gets fuel prices from a central server.
    /// </summary>
    public interface IFuelPricesServer
    {
        /// <summary>
        /// Gets a list of fuel prices from the server, showing type of fuel and cost per unit.
        /// </summary>
        /// <returns></returns>
        IDictionary<FuelType, double> GetFuelPrices();
    }
}
