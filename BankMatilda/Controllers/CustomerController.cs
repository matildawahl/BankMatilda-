using System;
using BankMatilda.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using BankMatilda.Data;
using BankMatilda.Services;
using JW;
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

        public IActionResult Index(int page = 1)
        {
            var viewModel = new CustomerIndexViewModel();
            var totalCustomers = _repository.GetCustomers().Count();
            var paging = new Pager(totalCustomers, page, 50, 10);
            var skip = CalculateHowManyCustomersToSkip(page, 50);


            viewModel.Customers = _repository.GetCustomers().Skip(skip).Take(50).Select(person =>
                new CustomerIndexViewModel.CustomerViewModel()
                {
                    Givenname = person.Givenname,
                    CustomerId = person.CustomerId,
                    City = person.City,
                    Streetaddress = person.Streetaddress,
                    NationalId = person.NationalId,
                    Surname = person.Surname,
                    Zipcode = person.Zipcode

                }).ToList();

            viewModel.TotalPageCount = paging.TotalPages;
            viewModel.DisplayPages = paging.Pages;
            viewModel.CurrentPage = paging.CurrentPage;


            return View(viewModel);
        }

        private int CalculateHowManyCustomersToSkip(int page, int pageSize)
        {
            return (page - 1) * pageSize;
        }


        public IActionResult Details(int id)
        {
            var viewModel = new CustomerDetailsViewModel();
            var account = _repository.GetAccountById(id);
            viewModel.Account = new CustomerDetailsViewModel.AccountViewModel
            {
                    AccountId = account.AccountId,
                    Balance = account.Balance,
                    Created = account.Created,
                    Frequency = account.Frequency
            };

            var customer = _repository.GetCustomer(id);
            viewModel.Customer = new CustomerDetailsViewModel.CustomerViewModel()
            {
                CustomerId = customer.CustomerId,
                City = customer.City,
                Givenname = customer.Givenname,
                Birthday = customer.Birthday,
                Surname = customer.Surname,
                Zipcode = customer.Zipcode,
                NationalId = customer.NationalId,
                Country = customer.Country,
                CountryCode = customer.CountryCode,
                Emailaddress = customer.Emailaddress,
                Gender = customer.Gender,
                Streetaddress = customer.Streetaddress,
                Telephonecountrycode = customer.Telephonecountrycode,
                Telephonenumber = customer.Telephonenumber
            };


            return View(viewModel);
        }
    }
}

//[Authorize(Roles = "Admin,Cashier")]
//public IActionResult EditCustomer(int id)
//{
//var viewModel = new CustomerEditViewModel();

//var customer = _context.Customers.First(r => r.CustomerId == id);

//viewModel.Emailaddress = customer.Emailaddress;
//viewModel.Givenname = customer.Givenname;
//viewModel.Streetaddress = customer.Streetaddress;

//return View(viewModel);
//}



//[HttpPost]
//public IActionResult EditCustomer(int id, CustomerEditViewModel viewModel)
//{
//    //if (ModelState.IsValid)
//    //{
//    //    var customer = _context.Customers.First(r => r.CustomerId == id);
//    //    customer.Givenname = viewModel.Givenname;
//    //    customer.Emailaddress = viewModel.Emailaddress;
//    //    customer.Streetaddress = viewModel.Streetaddress;
//    //    _context.SaveChanges();
//    //    //TODO ändra redrict
//    //    return RedirectToAction("Index");
//    //}

//    return View(viewModel);
//}








