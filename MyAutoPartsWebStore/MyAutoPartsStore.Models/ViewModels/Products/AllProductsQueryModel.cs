namespace MyAutoPartsStore.Models.ViewModels.Products
{
    using MyAutoPartsStore.Models.BaseModels;
    using MyAutoPartsStore.Models.ServiceModels.Products;
    using System.Collections.Generic;

    public class AllProductsQueryModel : IPagingModel
    {
        public string SearchTerm { get; init; }

        public ProductSorting Sorting { get; init; }

        public IEnumerable<ProductServiceModel> Products { get; set; }
        public int CurrentPage { get; set; }
        public int? NextPage { get; set; }
        public int? PreviousPage { get; set; }
        public int MaxPages { get; set; }
    }
}
