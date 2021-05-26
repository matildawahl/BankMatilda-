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
        public List<Account> GetCustomerAccounts(int customerId);
        public Customer GetCustomer(int id);
        public IQueryable<Transaction> GetTransactions(int accountId);
        public IEnumerable<Account> GetAllAccounts();
        public IQueryable<Transaction> GetAllTransactions();
        public void Transfer(int fromAccountId, int toAccountId, decimal amount);
        public IEnumerable<Data.Disposition> GetAll();
        public Customer GetAllAccountFromCustomer(int id);
        public Account UpdateAccount(Account account);
        public Customer UpdateCustomer(Customer customer);
        public Customer SaveCustomer(Customer customer);
        public bool CheckIfSufficientBalance(decimal amount, decimal balance);





    }
}
