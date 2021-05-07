using BankMatilda.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using BankMatilda.Data;
using Microsoft.AspNetCore.Authorization;

namespace BankMatilda.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly BankAppDataContext _context;
        public AdminController(ILogger<AdminController> logger, BankAppDataContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var viewModel = new AdminViewModel();
            viewModel.Transactions = _context.Transactions.Take(10)
                .Select(transaction => new TransactionViewModel()
                {
                    AccountId = transaction.AccountId,
                    Date = transaction.Date,
                    Operation = transaction.Operation,
                    Amount = transaction.Amount,
                    TransactionId = transaction.TransactionId
                }).ToList();
            return View(viewModel);
        }
        

        //TODO: FIXa lista på konton
    
    }
}
