using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankMatilda.Data;

namespace BankMatilda.Services
{
    public interface IRepository
    {
        public IEnumerable<Customer> GetCustomers();
        public IEnumerable<Account> GetAccounts(int id);
        public IEnumerable<Customer> GetCustomer(int id);
        public IEnumerable<Transaction> GetTransactions(int customerId, int accountId); 
    }
}
