using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using PumpLibrary;

namespace PetrofexSystem.Common
{
    [Serializable]
    [DataContract]
    public struct Transaction
    {
        /// <summary>
        /// Gets or sets the pump identifier.
        /// </summary>
        /// <value>
        /// The pump identifier.
        /// </value>
        [DataMember]
        public string PumpId { get; set; }


        /// <summary>
        /// Gets or sets the type of the fuel.
        /// </summary>
        /// <value>
        /// The type of the fuel.
        /// </value>
        [DataMember]
        public FuelType FuelType { get; set; }


        /// <summary>
        /// Gets or sets the litres pumped.
        /// </summary>
        /// <value>
        /// The litres pumped.
        /// </value>
        [DataMember]
        public double LitresPumped { get; set; }

        /// <summary>
        /// Gets or sets the total amount.
        /// </summary>
        /// <value>
        /// The total amount.
        /// </value>
        [DataMember]
        public double TotalAmount { get; set; }


        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Pump Id: {3} | Fuel type: {0} | Litres pumped: {1} | Total Spent: {2}", this.FuelType, this.LitresPumped, this.TotalAmount, this.PumpId);
        }
    }
}
