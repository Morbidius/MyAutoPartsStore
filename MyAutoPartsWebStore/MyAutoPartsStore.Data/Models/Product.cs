namespace MyAutoPartsStore.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(DataConstants.Product.ProductNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(20, 4)")]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(DataConstants.Product.ProductSizeCapacityMaxLength)]
        public string SizeCapacity { get; set; }

        [Required]
        public float Weight { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public string ImageUrl { get; set; }
    }
}
