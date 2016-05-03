using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace PetrofexSystem.Server.DbModels
{
    public class PaymentsContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }


    }
}
