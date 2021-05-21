using BankMatilda.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BankMatilda.Services
{
    public class Repository : IRepository
    {
        private readonly BankAppDataContext _context;

        public Repository(BankAppDataContext Context)
        {
            _context = Context;
        }

        public Account GetAccountById(int id)
        {
            return _context.Accounts.FirstOrDefault(a => a.AccountId == id);
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers;
        }

        public Customer GetCustomer(int id)
        {
            return _context.Customers.FirstOrDefault(c => c.CustomerId == id);
        }

        public IQueryable<Transaction> GetTransactions(int accountId)
        {
            return _context.Transactions.Where(t=> t.AccountId == accountId).OrderByDescending(d => d.Date).ThenByDescending(d => d.TransactionId);
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return _context.Accounts;
        }

        public IQueryable<Transaction> GetAllTransactions()
        {
            return _context.Transactions.OrderByDescending(d => d.Date).ThenByDescending(d => d.TransactionId);

        }

       

        public IEnumerable<Disposition> GetAll()
        {
            return _context.Dispositions.Include(a => a.Account).Include(c => c.Customer);

        }

        public Account UpdateAccount(Account account)
        {
            _context.Update(account);
            _context.SaveChanges();
            return account;
        }

       
    }
}
