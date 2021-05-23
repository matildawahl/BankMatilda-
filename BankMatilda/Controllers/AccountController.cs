using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankMatilda.Data;
using BankMatilda.Models;
using BankMatilda.Services;
using BankMatilda.ViewModels;
using JW;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.Extensions.Logging;

namespace BankMatilda.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRepository _repository;
        public AccountController(IRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index(int page = 1)
        {
            var viewModel = new AccountViewModel();
            var totalAccounts = _repository.GetAllAccounts().Count();
            var paging = new Pager(totalAccounts, page, 50, 10);
            var skip = CalculateHowManyAccountsToSkip(page, 50);

            viewModel.AccountList = _repository.GetAllAccounts().Skip(skip).Take(50).Select(account => new AccountViewModel.Accounts()
            {
                AccountId = account.AccountId,
                Balance = account.Balance,
                Frequency = account.Frequency,
                Created = account.Created
            }).ToList();


            viewModel.TotalPageCount = paging.TotalPages;
            viewModel.DisplayPages = paging.Pages;
            viewModel.CurrentPage = paging.CurrentPage;

            return View(viewModel);
        }

        public IActionResult Details(int id){

            var account = _repository.GetAccountById(id);
            var viewModel = new AccountDetailsViewModel
            {
                AccountId = account.AccountId,
                Balance = account.Balance,
                Frequency = account.Frequency
            };

            var query = _repository.GetTransactions(id);

            viewModel.Transactions =query.Skip(0).Take(20).Select(t =>
                new AccountDetailsViewModel.TransactionItem
                {
                    Transaction = t.Operation,
                    Date = t.Date,
                    Amount = t.Amount,
                    SaldoLeft = t.Balance,
                }).ToList();

            return View(viewModel);
        }

        public IActionResult GetTransactions(int id, int skip)
        {
            var viewModel = new AccountDetailsViewModel.GetTransactions();
            var query = _repository.GetTransactions(id);

            viewModel.Transactions = query.Skip(skip).Take(20).Select(t =>
                new AccountDetailsViewModel.TransactionItem()
                {
                    Transaction = t.Operation,
                    Date = t.Date,
                    Amount = t.Amount,
                    SaldoLeft = t.Balance,
                }).ToList();



            //viewModel.Transactions = query.Skip(skip).Take(20).Select(dbTransact => new CustomerTransactionsViewModel.TransactionsViewModel
            //{
            //    AccountId = dbTransact.AccountId,
            //    TransactionId = dbTransact.TransactionId,
            //    Account = dbTransact.Account,
            //    Amount = dbTransact.Amount,
            //    Balance = dbTransact.Balance,
            //    Date = dbTransact.Date,
            //    Bank = dbTransact.Bank,
            //    Operation = dbTransact.Operation,
            //    Symbol = dbTransact.Symbol,
            //    Type = dbTransact.Type
            //}).ToList();

          

            return View(viewModel);
        }



        private int CalculateHowManyAccountsToSkip(int page, int pageSize)
        {
            return (page - 1) * pageSize;
        }

    }
}

