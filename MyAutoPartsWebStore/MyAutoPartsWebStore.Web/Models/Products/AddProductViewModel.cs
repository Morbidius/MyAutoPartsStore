using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyAutoPartsWebStore.Web.Models.Products
{
    public class AddProductViewModel
    {
        public string Name { get; set; }
       
        public string Description { get; set; }

        public decimal Price { get; set; }

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
