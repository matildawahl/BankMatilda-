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
    public class SearchController : Controller
    {
        private readonly IRepository _repository;
        public SearchController(IRepository repository)
        {
            _repository = repository;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index(string q)
        {
            var viewModel = new CustomerIndexViewModel.CustomerViewModel();
            var customer = _repository.GetCustomers().First(x => x.City == q || x.Givenname == q);

            viewModel.City = customer.City;
            viewModel.Givenname = customer.Givenname;

            return View(viewModel);
        }
    }
}
