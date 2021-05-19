﻿using System;
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
        public IQueryable<Transaction> GetTransactions(int customerId, int accountId);
        public IEnumerable<Account> GetAllAccounts();
        public IQueryable<Transaction> GetAllTransactions();
        public void CreateTransaction(string id, decimal amount);
        public IEnumerable<Data.Disposition> GetAll();
        public Account UpdateAccount(Account account);





    }
}
