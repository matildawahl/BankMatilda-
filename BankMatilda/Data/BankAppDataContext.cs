using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BankMatilda.Data
{
    public partial class BankAppDataContext : IdentityDbContext
    {
        public BankAppDataContext(DbContextOptions<BankAppDataContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Disposition> Dispositions { get; set; }
        public virtual DbSet<Loan> Loans { get; set; }
        public virtual DbSet<PermenentOrder> PermenentOrders { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
