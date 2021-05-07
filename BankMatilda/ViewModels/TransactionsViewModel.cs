using System.Collections.Generic;

#nullable disable

namespace BankMatilda.Models
{
    public class TransactionsViewModel
    {
        public int CustomerId { get; set; }
        public int AccountId { get; set; }
        public List<TransactionViewModel> Transactions { get; set; } = new List<TransactionViewModel>();

    }
}
