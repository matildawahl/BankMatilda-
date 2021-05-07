using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BankMatilda.Data
{
    public partial class Card
    {
        [Key]
        public int CardId { get; set; }
        public int DispositionId { get; set; }
        [Required]
        [StringLength(50)]
        public string Type { get; set; }
        [Column(TypeName = "date")]
        public DateTime Issued { get; set; }
        [Required]
        [Column("CCType")]
        [StringLength(50)]
        public string Cctype { get; set; }
        [Required]
        [Column("CCNumber")]
        [StringLength(50)]
        public string Ccnumber { get; set; }
        [Required]
        [Column("CVV2")]
        [StringLength(10)]
        public string Cvv2 { get; set; }
        public int ExpM { get; set; }
        public int ExpY { get; set; }

        [ForeignKey(nameof(DispositionId))]
        [InverseProperty("Cards")]
        public virtual Disposition Disposition { get; set; }
    }
}
