using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankMatilda.Data;
using BankMatilda.Models;
using BankMatilda.Services;
using JW;
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

        public IActionResult Index(int page=1)
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



            return View(viewModel);

            viewModel.TotalPageCount = paging.TotalPages;
            viewModel.DisplayPages = paging.Pages;
            viewModel.CurrentPage = paging.CurrentPage;
        }

        private int CalculateHowManyAccountsToSkip(int page, int pageSize)
        {
            return (page - 1) * pageSize;
        }

    }
}

