using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BankMatilda.Data
{
    public partial class Disposition
    {
        public Disposition()
        {
            Cards = new HashSet<Card>();
        }

        [Key]
        public int DispositionId { get; set; }
        public int CustomerId { get; set; }
        public int AccountId { get; set; }
        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        [ForeignKey(nameof(AccountId))]
        [InverseProperty("Dispositions")]
        public virtual Account Account { get; set; }
        [ForeignKey(nameof(CustomerId))]
        [InverseProperty("Dispositions")]
        public virtual Customer Customer { get; set; }
        [InverseProperty(nameof(Card.Disposition))]
        public virtual ICollection<Card> Cards { get; set; }
    }
}
