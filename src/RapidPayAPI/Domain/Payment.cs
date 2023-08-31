using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace RapidPayAPI.Domain
{
    public class Payment
    {
        public Payment()
        {
            TransactionDate = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int CardId { get; set; }
        public virtual Card Card { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        [Precision(12, 4)]
        public double Fee { get; set; }

        [Required]
        [Precision(18, 2)]
        public double Amount { get; set; }

        [NotMapped]
        public double TotalAmount { get => Amount + Fee; }
    }
}
