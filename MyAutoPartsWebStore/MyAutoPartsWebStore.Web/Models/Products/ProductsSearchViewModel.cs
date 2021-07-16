namespace MyAutoPartsWebStore.Web.Models.Products
{
    using MyAutoPartsWebStore.Web.Models.Products.Enums;
    using System.Collections.Generic;

    public class ProductsSearchViewModel
    {
        public string Name { get; init; }

        public IEnumerable<string> Names { get; init; }

        public string SearchTerm { get; init; }

        public ProductSorting Sorting { get; init; }

        public IEnumerable<ProductListingViewModel> Products { get; init; }
    }
}
