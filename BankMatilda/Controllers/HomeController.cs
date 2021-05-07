using BankMatilda.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using BankMatilda.Data;

namespace BankMatilda.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BankAppDataContext _context;

        public HomeController(ILogger<HomeController> logger, BankAppDataContext context)
        {
            _logger = logger;
            _context = context;
        }

        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Client, NoStore = true)]
        public IActionResult Index()
        {
            var viewModel = new HomeIndexViewModel();
            viewModel.CustomerId = _context.Customers.Select(c => new HomeIndexViewModel
            {
                CustomerId = c.CustomerId
            }).Count();

            viewModel.AccountId = _context.Accounts.Select(c => new HomeIndexViewModel
            {
                AccountId = c.AccountId
            }).Count();

            viewModel.Balance = _context.Accounts.Sum(c => c.Balance);
           
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
