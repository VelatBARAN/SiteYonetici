using SiteYonetici.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteYonetici.DataAccessLayer.EntityFramework
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Safe> Safe { get; set; }
        public DbSet<Apartment> Apartment { get; set; }
        public DbSet<ApartmentType> ApartmentType { get; set; }
        public DbSet<Block> Block { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Expense> Expense { get; set; }
        public DbSet<ExpenseAccount> ExpenseAccount { get; set; }
        public DbSet<Income> Income { get; set; }
        public DbSet<IncomeAccount> IncomeAccount { get; set; }
        public DbSet<Managers> Managers { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Dues> Dues { get; set; }
        public DbSet<DuesPayment> DuesPayment { get; set; }
        public DbSet<DuesPaymentDetails> DuesPaymentDetails { get; set; }
        public DbSet<DuesPeriod> DuesPeriod { get; set; }

        public DatabaseContext()
        {
            Database.SetInitializer(new MyInitializer());
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DuesPayment>()
                .HasMany(m => m.DuesPaymentDetails)
                .WithRequired(n => n.DuesPayment)
                .WillCascadeOnDelete(true);
        }
    }
}
