using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AppDbContext : DbContext
    {
        #region DbSets

        public DbSet<User> Users { get; set; } // 
        public DbSet<Category> Categories { get; set; } //
        public DbSet<Customer> Customers { get; set; } // 
        public DbSet<Product> Products { get; set; } // 
        public DbSet<Seller> Sellers { get; set; } //
        public DbSet<IncomeAndExpense> IncomeAndExpenses { get; set; }
        public DbSet<Rol> Rols { get; set; } //
        public DbSet<Sales> Sales { get; set; } // 
        public DbSet<Stock> Stocks { get; set; } //
        public DbSet<Contact> Contacts { get; set; } //
        public DbSet<Billing> Billings { get; set; }
        #endregion


        #region Connection
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=100.42.184.64;Initial Catalog=trainingDB;User Id=trainingSA;Password=7u94v9Kw!;MultipleActiveResultSets=true;TrustServerCertificate=True;");

            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Turkish_CI_AS");
        }
        #endregion
    }
}
