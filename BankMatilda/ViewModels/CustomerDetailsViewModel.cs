using System;
using System.Collections.Generic;

#nullable disable

namespace BankMatilda.Models
{
    public class CustomerDetailsViewModel
    {
        public CustomerViewModel Customer { get; set; }
        public List<AccountViewModel> Account { get; set; }
        public decimal TotalCustAccount { get; set; }

        public class CustomerViewModel
        {
            public int CustomerId { get; set; }
            public string Gender { get; set; }
            public string Givenname { get; set; }
            public string Surname { get; set; }
            public string Streetaddress { get; set; }
            public string City { get; set; }
            public string Zipcode { get; set; }
            public string Country { get; set; }
            public string CountryCode { get; set; }
            public DateTime Birthday { get; set; }
            public string NationalId { get; set; }
            public string Telephonecountrycode { get; set; }
            public string Telephonenumber { get; set; }
            public string Emailaddress { get; set; }



        }

        public class AccountViewModel
        {
            public int AccountId { get; set; }
            public string Frequency { get; set; }
            public DateTime Created { get; set; }
            public decimal Balance { get; set; }
        }
    }
}
