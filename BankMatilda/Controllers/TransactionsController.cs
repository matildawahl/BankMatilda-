using BankMatilda.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using BankMatilda.Data;
using BankMatilda.Services;
using JW;
using Microsoft.AspNetCore.Authorization;

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

        public IActionResult CreateTransaction(string accountId,  decimal amount)
        {

            _repository.CreateTransaction(accountId, 99.00M);

            return null;
        }
    }
}

