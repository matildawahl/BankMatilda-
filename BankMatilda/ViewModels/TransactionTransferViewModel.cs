using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BankMatilda.ViewModels
{
    public class TransactionTransferViewModel
    {
        [Range(1, 100000)]
        public decimal AmountToTransfer { get; set; }

        [Remote("CheckAccountId", "Transactions")]
        public int AccountId { get; set; }

        [Remote("CheckAccountId2", "Transactions")]
        public int ToAccountId { get; set; }

    }
}
