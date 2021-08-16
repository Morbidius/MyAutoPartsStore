namespace MyAutoPartsStore.Models.ViewModels.Products
{
    using MyAutoPartsStore.Models.ServiceModels.Products;
    using System.Collections.Generic;

    public class AllProductsQueryModel
    {
        public const int ProductsPerPage = 20;

        public string Name { get; init; }

        public string SearchTerm { get; init; }

        public ProductSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalProducts { get; set; }

        public IEnumerable<ProductServiceModel> Products { get; set; }
    }
}
