using System.Collections.Generic;
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
            var viewModel = new CustomerIndexViewModel();

            viewModel.Customers = _repository.GetCustomers().Where(x => x.City.ToLower() == q.ToLower() || x.Givenname.ToLower() == q.ToLower()).Select(customer => new CustomerIndexViewModel.CustomerViewModel()
            {
                City = customer.City,
                Givenname = customer.Givenname,
                CustomerId = customer.CustomerId,
                Streetaddress = customer.Streetaddress,
                NationalId = customer.NationalId,
                Surname = customer.Surname,
                Zipcode = customer.Zipcode
                
            }).ToList();

            return View(viewModel);
        }
    }
}
