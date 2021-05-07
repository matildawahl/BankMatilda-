using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankMatilda.Data;
using BankMatilda.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace BankMatilda.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly BankAppDataContext _context;
        public AccountController(ILogger<AccountController> logger, BankAppDataContext context)
        {
            _logger = logger;
            _context = context;
        }
        
        public IActionResult Account()
        {
            var viewModel = new AccountViewModel();
            viewModel.AccountList = _context.Accounts.Take(1000)
                .Select(person => new AccountViewModel.Accounts()
                {
                    AccountId = person.AccountId,
                    Balance = person.Balance,
                    Frequency = person.Frequency,
                    Created = person.Created
                }).ToList();

            return View(viewModel); 
        }
    }
}
