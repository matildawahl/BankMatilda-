using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankMatilda.Data;
using BankMatilda.Models;
using BankMatilda.Services;
using Microsoft.AspNetCore.Authorization;
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

        public IActionResult Index()
        {
            var viewModel = new AccountViewModel();
            viewModel.AccountList = _repository.GetAllAccounts()
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
