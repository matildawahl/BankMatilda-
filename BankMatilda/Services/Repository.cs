using BankMatilda.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankMatilda.Services
{
    public class Repository : IRepository
    {
        private readonly BankAppDataContext _context;

        public Repository(BankAppDataContext Context)
        {
            _context = Context;
        }

        public IEnumerable<Account> GetAccounts(int id)
        {
            return _context.Accounts.Where(a => a.AccountId == id);
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers;
        }

        public IEnumerable<Customer> GetCustomer(int id)
        {
            return _context.Customers.Where(c => c.CustomerId == id); 
        }

        public IEnumerable<Transaction> GetTransactions(int customerId, int accountId)
        {
            return _context.Transactions;
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return _context.Accounts;
        }


    }
}
