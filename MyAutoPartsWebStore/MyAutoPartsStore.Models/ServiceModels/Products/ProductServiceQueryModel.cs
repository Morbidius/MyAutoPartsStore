namespace MyAutoPartsStore.Models.ServiceModels.Products
{
    using MyAutoPartsStore.Models.BaseModels;
    using MyAutoPartsWebStore.Web.Models.Products;

    public class ProductServiceQueryModel : ProductsSearchQueryModel, INameModel, IIsAllowed
    {
        public int TotalProducts { get; init; }

        public int CurrentPage { get; init; }

        public int ProductsPerPage { get; init; }

        public string Name { get; set; }

        public bool IsAllowed { get; set; }
    }
}
