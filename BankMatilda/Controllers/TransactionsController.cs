using BankMatilda.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using BankMatilda.Data;
using BankMatilda.Services;
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

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var viewModel = new TransactionsViewModel();
            //viewModel.Transactions = _repository.GetAllAccounts()
            //    .Select(transaction => new TransactionViewModel()
            //    {
            //        AccountId = transaction.AccountId,
            //        Date = transaction.Date,
            //        Operation = transaction.Operation,
            //        Amount = transaction.Amount,
            //        TransactionId = transaction.TransactionId
            //    }).OrderByDescending(x => x.Date).ToList();

            return View(viewModel);
        }
    }
}
