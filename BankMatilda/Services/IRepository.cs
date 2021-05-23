using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankMatilda.Data;
using BankMatilda.Models;

namespace BankMatilda.Services
{
    public interface IRepository
    {
        public IEnumerable<Customer> GetCustomers();
        public Account GetAccountById(int id);
        public Customer GetCustomer(int id);
        public IQueryable<Transaction> GetTransactions(int accountId);
        public IEnumerable<Account> GetAllAccounts();
        public IQueryable<Transaction> GetAllTransactions();
        public void Transfer(int fromAccountId, int toAccountId, decimal amount);
        public IEnumerable<Data.Disposition> GetAll();
        public Account UpdateAccount(Account account);
        public Customer UpdateCustomer(Customer customer);
        public Customer SaveCustomer(Customer customer);





    }
}
