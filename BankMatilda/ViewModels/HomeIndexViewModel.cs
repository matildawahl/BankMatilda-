using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankMatilda.Data;

namespace BankMatilda.Models
{
    public class HomeIndexViewModel
    {
        public int AccountId { get; set; }
        public int CustomerId { get; set; }
        public decimal Balance { get; set; }
        public decimal LargestAccount { get; set; }

    }
}
