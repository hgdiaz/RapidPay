using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapidPayAPI.Domain
{
    public class Card
    {
        public Card()
        {
            this.Payments = new HashSet<Payment>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(15), MinLength(15)]
        public string Number { get; set; }

        [Required]
        [MaxLength(100)]
        public string CardHolderName { get; set; }

        [Required]
        public int ExpirationMonth  { get; set; }

        [Required]
        public int ExpirationtYear { get; set; }
        
        [Required]
        [MaxLength(3), MinLength(3)]
        public string CVC { get; set; }

        [Precision(18,2)]
        public double Balance { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }

        [NotMapped]
        public string Expiration => $"{ExpirationMonth}/{ExpirationtYear}";

    }
}
