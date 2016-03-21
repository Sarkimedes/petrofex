using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PumpLibrary;

namespace PetrofexSystem
{
    public struct FuelTransaction
    {
        /// <summary>
        /// The type of fuel dispensed in the transaction.
        /// </summary>
        public FuelType FuelType { get; set; }

        /// <summary>
        /// The total amount of money spent on this transaction.
        /// </summary>
        public double Total { get; set; }
    }
}
