using System;
using System.Collections.Generic;
using BankMatilda.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using BankMatilda.Data;
using BankMatilda.Services;
using BankMatilda.ViewModels;
using JW;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

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

            var customer = _repository.GetAllAccountFromCustomer(id);

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

            viewModel.Account = customer.Dispositions.Select(d => new CustomerDetailsViewModel.AccountViewModel
            {
                AccountId = account.AccountId,
                Balance = account.Balance,
                Created = account.Created,
                Frequency = account.Frequency
                
            }).ToList();

            viewModel.TotalCustAccount = viewModel.Account.Sum(a => a.Balance);

            return View(viewModel);
        }


       

        [Authorize(Roles = "Cashier")]
        public IActionResult EditCustomer(int id)
        {
            var viewModel = new CustomerEditViewModel();
            viewModel.Genders = GetGenderListItems();

            var customer = _repository.GetCustomer(id);

            viewModel.Givenname = customer.Givenname;
            viewModel.Surname = customer.Surname;
            viewModel.SelectedGender = customer.Gender;
            viewModel.Birthday = customer.Birthday;
            viewModel.Telephonecountrycode = customer.Telephonecountrycode;
            viewModel.Telephonenumber = customer.Telephonenumber;
            viewModel.Emailaddress = customer.Emailaddress;
            viewModel.Streetaddress = customer.Streetaddress;
            viewModel.Zipcode = customer.Zipcode;
            viewModel.City = customer.City;
            viewModel.CountryCode = customer.CountryCode;
            viewModel.Country = customer.Country;
           
       
            return View(viewModel);
        }


        [HttpPost]
        public IActionResult EditCustomer(int id, CustomerEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var customer = _repository.GetCustomer(id);
                customer.Givenname = viewModel.Givenname;
                customer.Surname = viewModel.Surname;
                customer.Gender = viewModel.SelectedGender;
                customer.Telephonecountrycode = viewModel.Telephonecountrycode;
                customer.Telephonenumber = viewModel.Telephonenumber;
                customer.Emailaddress = viewModel.Emailaddress;
                customer.Streetaddress = viewModel.Streetaddress;
                customer.Zipcode = viewModel.Zipcode;
                customer.City = viewModel.City;
                customer.CountryCode = viewModel.CountryCode;
                customer.Country = viewModel.Country;

                _repository.UpdateCustomer(customer);
                return RedirectToAction("Index");
            }
            viewModel.Genders = GetGenderListItems();
            return View(viewModel);
        }

        public IActionResult NewCustomer()
        {
            var viewModel = new CustomerNewViewModel();
            viewModel.Genders = GetGenderListItems();

            return View(viewModel);
        }


        [HttpPost]
        public IActionResult NewCustomer(CustomerNewViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer();
                customer.Givenname = viewModel.Givenname;
                customer.Surname = viewModel.Surname;
                customer.Gender = viewModel.SelectedGender;
                customer.Telephonecountrycode = viewModel.Telephonecountrycode;
                customer.Telephonenumber = viewModel.Telephonenumber;
                customer.Emailaddress = viewModel.Emailaddress;
                customer.Streetaddress = viewModel.Streetaddress;
                customer.Zipcode = viewModel.Zipcode;
                customer.City = viewModel.City;
                customer.CountryCode = viewModel.CountryCode;
                customer.Country = viewModel.Country;

               
                _repository.SaveCustomer(customer);
                return RedirectToAction("Index");
            }

            viewModel.Genders = GetGenderListItems();
            return View(viewModel);

        }

        private List<SelectListItem> GetGenderListItems()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "0", Text = "Select Gender" });

            list.Add(new SelectListItem { Value = "Female", Text = "Female" });
            list.Add(new SelectListItem { Value = "Male", Text = "Male" });
            return list;
        }

    }



}








