using System;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BankMatilda.Models
{
    public class CustomerEditViewModel
    {
        public int CustomerId { get; set; }
        public string Gender { get; set; }

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
