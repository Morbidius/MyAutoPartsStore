namespace MyAutoPartsWebStore.Web.Models.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MyAutoPartsStore.Services.ProductServices;

    public class ProductsSearchQueryModel
    {
        [Required]
        [Display(Name = "Search word")]
        [StringLength(50, ErrorMessage = "{0} should be between {2} and {1} symbols long", MinimumLength = 1)]
        public string SearchTerm { get; set; }

        public string Category { get; set; }

        public IEnumerable<ProductListingViewModel> Products { get; set; }

        public IEnumerable<ProductServiceCategoryModel> Categories { get; set; }
    }
}