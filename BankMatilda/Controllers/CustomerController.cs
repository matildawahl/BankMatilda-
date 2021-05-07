using System;
using BankMatilda.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using BankMatilda.Data;
using BankMatilda.Services;
using Microsoft.AspNetCore.Authorization;

namespace BankMatilda.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IRepository _repository;
        public CustomerController(IRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index(int id)
        {
            var viewModel = new CustomerIndexViewModel();
            viewModel.Customers = _repository.GetCustomers().Take(10)
                .Select(person => new CustomerIndexViewModel.CustomerViewModel()
                {
                    Givenname = person.Givenname,
                    CustomerId = person.CustomerId,
                    City = person.City,
                    Streetaddress = person.Streetaddress,
                    NationalId = person.NationalId,
                    Surname = person.Surname,
                    Zipcode = person.Zipcode

                }).ToList();

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Details(int id)
        {
            var viewModel = new CustomerDetailsViewModel();

            viewModel.Accounts = _repository.GetAccounts(id)
                .Select(accounts => new CustomerDetailsViewModel.AccountViewModel()
                {
                   AccountId = accounts.AccountId,
                   Balance = accounts.Balance,
                   Created = accounts.Created,
                   Frequency = accounts.Frequency

                }).ToList();

            viewModel.Customer = _repository.GetCustomer(id).Select(customers =>
                new CustomerDetailsViewModel.CustomerViewModel()
                {
                    CustomerId = customers.CustomerId,
                    City = customers.City,
                    Givenname = customers.Givenname,
                    Birthday = (DateTime)customers.Birthday,
                    Surname = customers.Surname,
                    Zipcode = customers.Zipcode,
                    NationalId = customers.NationalId,
                    Country = customers.Country,
                    CountryCode = customers.CountryCode,
                    Emailaddress = customers.Emailaddress,
                    Gender = customers.Gender,
                    Streetaddress = customers.Streetaddress,
                    Telephonecountrycode = customers.Telephonecountrycode,
                    Telephonenumber = customers.Telephonenumber
                }).ToList();


            return View(viewModel);
        }

        [Authorize(Roles = "Admin,Cashier")]
        public IActionResult EditCustomer(int id)
        {
            var viewModel = new CustomerEditViewModel();

            //var customer = _context.Customers.First(r => r.CustomerId == id);

            //viewModel.Emailaddress = customer.Emailaddress;
            //viewModel.Givenname = customer.Givenname;
            //viewModel.Streetaddress = customer.Streetaddress;
            ////Fält =----> viewModel.Streetaddress = customer.Streetaddress;
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult EditCustomer(int id, CustomerEditViewModel viewModel)
        {
            //if (ModelState.IsValid)
            //{
            //    var customer = _context.Customers.First(r => r.CustomerId == id);
            //    customer.Givenname = viewModel.Givenname;
            //    customer.Emailaddress = viewModel.Emailaddress;
            //    customer.Streetaddress = viewModel.Streetaddress;
            //    _context.SaveChanges();
            //    //TODO ändra redrict
            //    return RedirectToAction("Index");
            //}

            return View(viewModel);
        }
        
        //Todo paginering = ladda in med js/ajax. Ta bort Take(100) sen
        [Authorize(Roles = "Admin, Cashier")]
        public IActionResult AccountTransactions(int customerId,int accountId)
        {
            var viewModel = new TransactionsViewModel();
            //viewModel.Transactions = _repository.GetTransactions(customerId, accountId)
            //    {
                    
                    
                   
            //    }).OrderByDescending(x=>x.Date).ToList();

            //viewModel.AccountId = accountId;
            //viewModel.CustomerId = customerId;

            return View(viewModel);
        }
    }
}

