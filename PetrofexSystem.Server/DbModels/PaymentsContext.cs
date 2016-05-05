using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PetrofexSystem.Server.DbModels
{
    public class PaymentsContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }

        static PaymentsContext()
        {
            Database.SetInitializer(new SqliteInitializer());
        }

       
    }

    public class SqliteInitializer : IDatabaseInitializer<PaymentsContext>
    {
        private const string TableTransactions = "Transactions";

        public void InitializeDatabase(PaymentsContext context)
        {
            context.Database.ExecuteSqlCommand("DROP TABLE IF EXISTS " + TableTransactions);

            const string CreateTransactionsTable = "CREATE TABLE " + TableTransactions +
                                                   "(" +
                                                   "TransactionId INTEGER PRIMARY KEY," +
                                                   "PumpId TEXT," +
                                                   "FuelType TEXT," +
                                                   "LitresPumped DOUBLE," +
                                                   "Amount DOUBLE" +
                                                   ")";

            context.Database.ExecuteSqlCommand(CreateTransactionsTable);
        }
    }
}
