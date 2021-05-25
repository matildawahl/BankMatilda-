using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankMatilda.ViewModels;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BankMatilda.Models
{
    public partial class AccountViewModel
    {
        public int TotalPageCount { get; set; }
        public IEnumerable<int> DisplayPages { get; set; }
        public int CurrentPage { get; set; }
        public List<Accounts> AccountList { get; set; } = new List<Accounts>();

        public class Accounts
        {
            public int AccountId { get; set; }
            public string Frequency { get; set; }
            public DateTime Created { get; set; }
            public decimal Balance { get; set; }

            public virtual ICollection<Disposition> Dispositions { get; set; }
            public virtual ICollection<Loan> Loans { get; set; }
            public virtual ICollection<PermenentOrder> PermenentOrders { get; set; }
            public virtual ICollection<TransactionViewModel> Transactions { get; set; }

        }
       
    }
}
