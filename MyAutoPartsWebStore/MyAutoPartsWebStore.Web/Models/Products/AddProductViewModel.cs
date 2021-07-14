namespace MyAutoPartsWebStore.Web.Models.Products
{
    using MyAutoPartsStore.Data;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AddProductViewModel
    {
        [Required]
        [StringLength(DataConstants.Product.ProductNameMaxLength, MinimumLength = DataConstants.Product.ProductNameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(int.MaxValue,
            MinimumLength = DataConstants.Product.ProductDescriptionMinLength,
            ErrorMessage = "The field Description must be with a minimum length of {2} characters.")]
        public string Description { get; set; }

        [Range(
            typeof(decimal),
            "0.01",
            "79228162514264337593543950335",
            ErrorMessage = "The product must have a price.")]
        public decimal Price { get; set; }

        [Range(
            typeof(float),
            "0.01",
            "79228162514264337593543950335",
            ErrorMessage = "The product must have a weight.")]
        public float Weight { get; set; }

        [Display(Name = "Image URL")]
        [Required]
        [Url]
        public string ImageUrl { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<ProductCategoryViewModel> Categories { get; set; }
    }
}