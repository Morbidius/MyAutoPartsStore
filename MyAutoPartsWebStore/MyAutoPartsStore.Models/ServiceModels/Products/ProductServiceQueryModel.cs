namespace MyAutoPartsStore.Models.ServiceModels.Products
{
    using MyAutoPartsStore.Models.BaseModels;
    using MyAutoPartsWebStore.Web.Models.Products;

    public class ProductServiceQueryModel : ProductsSearchQueryModel, INameModel, IIsAllowed
    {
        public string Name { get; set; }

        public bool IsAllowed { get; set; }
    }
}
