using BankMatilda.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using BankMatilda.Data;
using Microsoft.AspNetCore.Authorization;

namespace BankMatilda.Controllers
{
    public class SearchController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly BankAppDataContext _context;
        public SearchController(ILogger<AdminController> logger, BankAppDataContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index(string q)
        {
            var viewModel = new CustomerViewModel();
            var customer = _context.Customers.First(x => x.City == q || x.Givenname == q);

            viewModel.City = customer.City;
            viewModel.Givenname = customer.Givenname;

            return View(viewModel);
        }
    }
}
