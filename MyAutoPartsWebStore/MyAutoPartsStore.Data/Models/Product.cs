namespace MyAutoPartsStore.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(DataConstants.Product.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(20, 4)")]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(DataConstants.Product.SizeCapacityMaxLength)]
        public string SizeCapacity { get; set; }

        [Required]
        public float Weight { get; set; }

        public bool IsAllowed{ get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; init; }

        public string ImageUrl { get; set; }

        public int DealerId { get; set; }

        public Dealer Dealer { get; init; }

        public ICollection<OrderProducts> Orders { get; set; } = new HashSet<OrderProducts>();
    }
}
