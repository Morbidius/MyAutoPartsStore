namespace MyAutoPartsWebStore.Web.Models.Products
{
    using MyAutoPartsStore.Data;
    using MyAutoPartsStore.Models.ServiceModels.Products;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ProductFormModel
    {
        [Required]
        [StringLength(DataConstants.Product.NameMaxLength, MinimumLength = DataConstants.Product.NameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(int.MaxValue,
            MinimumLength = DataConstants.Product.DescriptionMinLength,
            ErrorMessage = "The field Description must be with a minimum length of {2} characters.")]
        public string Description { get; set; }

        [Range(
            typeof(decimal),
            "0.01",
            "79228162514264337593543950335",
            ErrorMessage = "The product must have a valid price.")]
        public decimal Price { get; set; }

        [Display(Name = "Size or Capacity")]
        [StringLength(int.MaxValue,
            MinimumLength = DataConstants.Product.SizeCapacityMinLength,
            ErrorMessage = "The product must have a valid size or capacity.")]
        public string SizeCapacity { get; set; }

        [Range(1, 1000,
            ErrorMessage = "The product must have a valid weight.")]
        public float Weight { get; set; }

        [Display(Name = "Image URL")]
        [Required]
        [Url]
        public string ImageUrl { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<ProductServiceCategoryModel> Categories { get; set; }
    }
}