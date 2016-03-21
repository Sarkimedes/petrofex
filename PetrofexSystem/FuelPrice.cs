using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PumpLibrary;

namespace PetrofexSystem
{
    /// <summary>
    /// Data object for fuel prices
    /// </summary>
    class FuelPrice
    {
        public FuelType FuelType { get; set; }
        public double Price { get; set; }
    }
}
