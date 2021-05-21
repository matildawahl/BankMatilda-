using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BankMatilda.ViewModels
{
    public class TransactionWithdrawViewModel
    {
        [Remote("CheckAccountId", "Transactions")]
        public int AccountId { get; set; }

        [Range(1, 100000)]
        public decimal AmountToWithdraw { get; set; }
    }
}
