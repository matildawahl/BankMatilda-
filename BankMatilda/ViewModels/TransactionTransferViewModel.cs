using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankMatilda.ViewModels
{
    public class TransactionTransferViewModel
    {
        public decimal AmountToTransfer { get; set; }
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }

    }
}
