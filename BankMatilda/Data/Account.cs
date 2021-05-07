using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BankMatilda.Data
{
    public partial class Account
    {
        public Account()
        {
            Dispositions = new HashSet<Disposition>();
            Loans = new HashSet<Loan>();
            PermenentOrders = new HashSet<PermenentOrder>();
            Transactions = new HashSet<Transaction>();
        }

        [Key]
        public int AccountId { get; set; }
        [Required]
        [StringLength(50)]
        public string Frequency { get; set; }
        [Column(TypeName = "date")]
        public DateTime Created { get; set; }
        [Column(TypeName = "decimal(13, 2)")]
        public decimal Balance { get; set; }

        [InverseProperty(nameof(Disposition.Account))]
        public virtual ICollection<Disposition> Dispositions { get; set; }
        [InverseProperty(nameof(Loan.Account))]
        public virtual ICollection<Loan> Loans { get; set; }
        [InverseProperty(nameof(PermenentOrder.Account))]
        public virtual ICollection<PermenentOrder> PermenentOrders { get; set; }
        [InverseProperty(nameof(Transaction.AccountNavigation))]
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
