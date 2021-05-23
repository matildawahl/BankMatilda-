using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BankMatilda.ViewModels
{
    public class AccountDetailsViewModel
    {
        public class TransactionItem
        {
            public string Transaction { get; set; }
            public DateTime Date { get; set; }

            public Decimal Amount { get; set; }
            
            public Decimal SaldoLeft { get; set; }
        }
        [Key]
        public int AccountId { get; set; }
        [Required]
        [StringLength(50)]
        public string Frequency { get; set; }
        [Column(TypeName = "date")]
        
        public decimal Balance { get; set; }

        public List<TransactionItem> Transactions { get; set; } = new List<TransactionItem>();
    public class GetTransactions
    {
        public int AccountId { get; set; }
        public List<TransactionItem> Transactions { get; set; } = new List<TransactionItem>();
    }
    }

}
