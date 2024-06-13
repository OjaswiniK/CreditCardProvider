using CreditCardProvider.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CreditCardProvider.Data
{
    public class CCDBContext : DbContext
    {
        public CCDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<CardType> CardTypes { get; set; }

        public DbSet<CCCustomer> CCCustomers { get; set; }

        public DbSet<CCTransactions> CCTransactions { get; set; }

    }
}
