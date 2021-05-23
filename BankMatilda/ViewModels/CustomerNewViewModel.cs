using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankMatilda.ViewModels
{
    public class CustomerNewViewModel
    {
        public int CustomerId { get; set; }
        public List<SelectListItem> Genders { get; set; }
        public string SelectedGender { get; set; }

        [Required]
        [MaxLength(100)]
        public string Givenname { get; set; }
        public string Surname { get; set; }
        [MaxLength(100)]
        public string Streetaddress { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public DateTime? Birthday { get; set; }
        public string NationalId { get; set; }
        public string Telephonecountrycode { get; set; }
        public string Telephonenumber { get; set; }

        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Emailaddress { get; set; }
    }
}
