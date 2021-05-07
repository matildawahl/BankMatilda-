using BankMatilda.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using BankMatilda.Data;
using Microsoft.AspNetCore.Authorization;

namespace BankMatilda.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly BankAppDataContext _context;
        public CustomerController(ILogger<CustomerController> logger, BankAppDataContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index(int id)
        {
            var viewModel = new CustomersViewModel();
            viewModel.Customer = _context.Customers.Take(10)
                .Select(person => new CustomerViewModel()
                {
                    Givenname = person.Givenname,
                    CustomerId = person.CustomerId,
                    Birthday = person.Birthday,
                    City = person.City,
                    Emailaddress = person.Emailaddress,
                    Gender = person.Gender,
                    Streetaddress = person.Streetaddress,
                    Telephonenumber = person.Telephonenumber

                }).ToList();

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ViewCustomer(int id)
        {
            var viewModel = new CustomerViewModel();
            var customer = _context.Customers.First(r => r.CustomerId == id);
            var accounts = _context.Dispositions.Where(y => y.CustomerId == customer.CustomerId).Select(xx => xx.Account).ToList();

            viewModel.Emailaddress = customer.Emailaddress;
            viewModel.Givenname = customer.Givenname;
            viewModel.Streetaddress = customer.Streetaddress;
            viewModel.CustomerId = customer.CustomerId;
            viewModel.Dispositions = customer.Dispositions;
            viewModel.Accounts = accounts;
            viewModel.Birthday = customer.Birthday;
            viewModel.City = customer.City;

            return View(viewModel);
        }

        [Authorize(Roles = "Admin,Cashier")]
        public IActionResult EditCustomer(int id)
        {
            var viewModel = new CustomerEditViewModel();

            var customer = _context.Customers.First(r => r.CustomerId == id);

            viewModel.Emailaddress = customer.Emailaddress;
            viewModel.Givenname = customer.Givenname;
            viewModel.Streetaddress = customer.Streetaddress;
            //Fält =----> viewModel.Streetaddress = customer.Streetaddress;
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult EditCustomer(int id, CustomerEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var customer = _context.Customers.First(r => r.CustomerId == id);
                customer.Givenname = viewModel.Givenname;
                customer.Emailaddress = viewModel.Emailaddress;
                customer.Streetaddress = viewModel.Streetaddress;
                _context.SaveChanges();
                //TODO ändra redrict
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }
        
        //Todo paginering = ladda in med js/ajax. Ta bort Take(100) sen
        [Authorize(Roles = "Admin, Cashier")]
        public IActionResult AccountTransactions(int customerId,int accountId)
        {
            var viewModel = new TransactionsViewModel();
            viewModel.Transactions = _context.Transactions.Where(x =>x.AccountId == accountId)
                .Select(transaction => new TransactionViewModel()
                {
                    AccountId = transaction.AccountId,
                    Date = transaction.Date,
                    Operation = transaction.Operation,
                    Amount = transaction.Amount,
                    TransactionId = transaction.TransactionId,
                    Balance = transaction.Balance,
                    Symbol = transaction.Symbol,
                    Bank = transaction.Bank
                }).OrderByDescending(x=>x.Date).ToList();

            viewModel.AccountId = accountId;
            viewModel.CustomerId = customerId;

            return View(viewModel);
        }
    }
}
