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
        /// Gets or sets the pump identifier.
        /// </summary>
        /// <value>
        /// The pump identifier.
        /// </value>
        public string PumpId { get; set; }

        /// <summary>
        /// The type of fuel dispensed in the transaction.
        /// </summary>
        /// <value>
        /// The type of the fuel.
        /// </value>
        public FuelType FuelType { get; set; }

        public double LitresPumped { get; set; }

        /// <summary>
        /// The total amount of money spent on this transaction.
        /// </summary>
        public double Total { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Pump Id: {3} | Fuel type: {0} | Litres pumped: {1} | Total Spent: {2}", this.FuelType, this.LitresPumped, this.Total, this.PumpId);
        }
    }
}
