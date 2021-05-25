using System;

#nullable disable

namespace BankMatilda.ViewModels
{
    public class TransactionViewModel
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public string Operation { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public string Bank { get; set; }
        public string Symbol { get; set; }

    }
}
