using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccsessLayer
{
    public class _DbContext : DbContext
    {
        

        #region TABLES
        public DbSet<User> Users { get; set; } // 
        public DbSet<Category> Categories { get; set; } //
        public DbSet<Customer> Customers { get; set; } // 
        public DbSet<Product> Products { get; set; } // 
        public DbSet<Seller> Sellers { get; set; } //
        public DbSet<IncomeAndExpense> IncomeAndExpenses { get; set; } // 
        public DbSet<Rol> Rols { get; set; } //
        public DbSet<Sales> Sales { get; set; } // 
        public DbSet<Stock> Stocks { get; set; } //
        public DbSet<Contact> Contacts { get; set; } //
        public DbSet<Billing> Billings { get; set; }

        #endregion


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=100.42.184.64;Initial Catalog=trainingDB;User Id=trainingSA;Password=7u94v9Kw!;MultipleActiveResultSets=true;TrustServerCertificate=True;");
                
            }
        }


    }
}
