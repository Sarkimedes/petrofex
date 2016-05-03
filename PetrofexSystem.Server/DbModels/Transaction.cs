using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetrofexSystem.Server.DbModels
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string PumpId { get; set; }
        public string FuelType { get; set; }

        public double LitresPumped { get; set; }
        public double Amount { get; set; }
    }
}
