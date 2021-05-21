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

        public void Transfer(int fromAccountId, int toAccountId, decimal amount)
        {
            var fromAccount = _context.Accounts.First(a => a.AccountId == fromAccountId);
            var toAccount = _context.Accounts.First(a => a.AccountId == toAccountId);
            var transaction = new Transaction
            {
                AccountId = fromAccountId,
                Date = DateTime.Now,
                Type = "Debit",
                Operation = "Transfer to Another account",
                Balance = fromAccount.Balance - amount,
                Amount = -amount,
                Account = toAccount.ToString()
            };
            var transactionTo = new Transaction
            {
                AccountId = toAccountId,
                Date = DateTime.Now,
                Type = "Credit",
                Operation = "Collection from Another account",
                Balance = toAccount.Balance + amount,
                Amount = amount,
                Account = fromAccountId.ToString()
            };
            fromAccount.Balance -= amount;
            toAccount.Balance += amount;

            fromAccount.Transactions.Add(transaction);
            toAccount.Transactions.Add(transaction);
            _context.SaveChanges();
        }


    }
}
