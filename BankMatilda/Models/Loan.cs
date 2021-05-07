using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BankMatilda.Models
{
    public partial class Loan
    {
        [Key]
        public int LoanId { get; set; }
        public int AccountId { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        [Column(TypeName = "decimal(13, 2)")]
        public decimal Amount { get; set; }
        public int Duration { get; set; }
        [Column(TypeName = "decimal(13, 2)")]
        public decimal Payments { get; set; }
        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        [ForeignKey(nameof(AccountId))]
        [InverseProperty("Loans")]
        public virtual AccountViewModel Account { get; set; }
    }
}
