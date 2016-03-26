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
        /// Gets or sets the litres pumped in this transaction.
        /// </summary>
        /// <value>
        /// The litres pumped.
        /// </value>
        public double LitresPumped { get; set; }

        /// <summary>
        /// The total amount of money spent on this transaction.
        /// </summary>
        public double Total { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is the last transaction for a particular customer.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is last transaction; otherwise, <c>false</c>.
        /// </value>
        public bool IsLastTransaction { get; set; }

        public override string ToString()
        {
            return string.Format("Fuel type: {0} | Total Spent: {1}", this.FuelType, this.Total);
        }
    }
}
