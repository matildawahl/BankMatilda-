using BankMatilda.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using BankMatilda.Data;
using Microsoft.AspNetCore.Authorization;

namespace BankMatilda.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly BankAppDataContext _context;
        public TransactionsController(ILogger<AdminController> logger, BankAppDataContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var viewModel = new TransactionsViewModel();
            viewModel.Transactions = _context.Transactions.Take(100)
                .Select(transaction => new TransactionViewModel()
                {
                    AccountId = transaction.AccountId,
                    Date = transaction.Date,
                    Operation = transaction.Operation,
                    Amount = transaction.Amount,
                    TransactionId = transaction.TransactionId
                }).OrderByDescending(x => x.Date).ToList();

            return View(viewModel);
        }
    }
}
