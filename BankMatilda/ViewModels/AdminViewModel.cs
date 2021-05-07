using System;
using System.Collections.Generic;

namespace BankMatilda.Models
{
    public class AdminViewModel
    {
        public List<TransactionViewModel> Transactions { get; set; } = new List<TransactionViewModel>();
        public List<AccountViewModel> Accounts { get; set; } = new List<AccountViewModel>();
        public List<CustomerViewModel> Customer { get; set; } = new List<CustomerViewModel>();
    }
}
