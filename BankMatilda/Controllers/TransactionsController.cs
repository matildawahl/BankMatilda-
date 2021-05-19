using System;
using BankMatilda.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using BankMatilda.Data;
using BankMatilda.Services;
using BankMatilda.ViewModels;
using JW;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BankMatilda.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly IRepository _repository;

        public TransactionsController(IRepository repository)
        {
            _repository = repository;
        }

        [Authorize(Roles = "Admin,Cashier")]
        public IActionResult Index(int page = 1)
        {
            var viewModel = new TransactionsViewModel();
            var totalTransactions = _repository.GetAllTransactions().Count();
            var paging = new Pager(totalTransactions, page, 50, 10);
            var skip = CalculateHowManyAccountsToSkip(page, 50);

            viewModel.Transactions = _repository.GetAllTransactions().Skip(skip).Take(50).Select(transaction =>
                new TransactionViewModel()
                {
                    AccountId = transaction.AccountId,
                    Balance = transaction.Balance,
                    Amount = transaction.Amount,
                    Date = transaction.Date
                }).ToList();


            viewModel.TotalPageCount = paging.TotalPages;
            viewModel.DisplayPages = paging.Pages;
            viewModel.CurrentPage = paging.CurrentPage;

            return View(viewModel);
        }

        private int CalculateHowManyAccountsToSkip(int page, int pageSize)
        {
            return (page - 1) * pageSize;
        }

        public IActionResult Transfer(int accountId, decimal amount)
        {
            var viewModel = new TransactionTransferViewModel();
            return View(viewModel);
        }

        public IActionResult Withdraw(int accountId, decimal amount)
        {
            var viewModel = new TransactionWithdrawViewModel();
            return View(viewModel);
        }

        public IActionResult Deposit()
        {
            var viewModel = new TransactionDepositViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Deposit(TransactionDepositViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var trans = new Transaction();
                var account = _repository.GetAccountById(viewModel.AccountId);

                trans.Balance = account.Balance + viewModel.AmountToDeposit;
                trans.AccountId = account.AccountId;
                trans.Date = DateTime.Now.Date;
                trans.Operation = "Credit in Cash";
                trans.Type = "Credit";
                trans.Symbol = "";
                trans.Amount = viewModel.AmountToDeposit;
                trans.AccountNavigation = account;

                account.Balance += viewModel.AmountToDeposit;
                account.Transactions.Add(trans);

                _repository.UpdateAccount(account);

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        public JsonResult CheckAccountId(int AccountId)
        {
                var account = _repository.GetAccountById(AccountId);
            if (account != null)
            {
                return Json(true);
            }

            return Json("Invalid AccountId");
        }

    }

}

