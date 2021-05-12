using System;
using System.Collections.Generic;

#nullable disable

namespace BankMatilda.Models
{
    public class CustomerIndexViewModel
    {
        public List<CustomerViewModel> Customers { get; set; } = new List<CustomerViewModel>();
        public int TotalPageCount { get; set; }
        public IEnumerable<int> DisplayPages { get; set; }
        public int CurrentPage { get; set; }

        public class CustomerViewModel
        {
            public int CustomerId { get; set; }
            public string Givenname { get; set; }
            public string Surname { get; set; }
            public string Streetaddress { get; set; }
            public string City { get; set; }
            public string Zipcode { get; set; }
            public string NationalId { get; set; }
            
        }
     
      
    }
}
