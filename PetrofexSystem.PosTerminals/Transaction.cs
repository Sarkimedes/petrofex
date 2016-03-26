using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PumpLibrary;

namespace PetrofexSystem.PosTerminals
{
    public struct Transaction
    {
        public FuelType FuelType { get; set; }
        public double LitresPumped { get; set; }
        public double TotalAmount { get; set; }
        public bool IsPaid { get; set; }
    }
}
