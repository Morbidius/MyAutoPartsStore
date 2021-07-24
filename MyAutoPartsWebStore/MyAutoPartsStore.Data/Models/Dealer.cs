using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyAutoPartsStore.Data.Models
{
    public class Dealer
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(DataConstants.Dealer.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DataConstants.Dealer.CompanyNameMaxLength)]
        public string CompanyName { get; set; }

        [Required]
        [MaxLength(DataConstants.Dealer.PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserId { get; set; }

        public IEnumerable<Product> Products { get; init; } = new List<Product>();
    }
}
