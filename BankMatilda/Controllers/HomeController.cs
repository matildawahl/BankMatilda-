using System.Collections.Generic;
using BankMatilda.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using BankMatilda.Data;
using BankMatilda.Services;

namespace BankMatilda.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;
        public HomeController(IRepository repository)
        {
            _repository = repository;
        }

        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            var viewModel = new HomeIndexViewModel();
            viewModel.CustomerId = _repository.GetCustomers().Select(c => new HomeIndexViewModel
            {
                CustomerId = c.CustomerId
            }).Count();

            viewModel.AccountId = _repository.GetAllAccounts().Select(c => new HomeIndexViewModel
            {
                AccountId = c.AccountId
            }).Count();

            var query = _repository.GetAllAccounts();
            viewModel.Balance = query.Sum(c => c.Balance);

            
            viewModel.LargestAccount = _repository.GetAll().OrderByDescending(b => b.Account.Balance).Select(t => t.Account.Balance).FirstOrDefault();


            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
