namespace MyAutoPartsStore.Models.ServiceModels.Products
{
    using System.Collections.Generic;

    public class ProductQueryServiceModel
    {
        public int TotalProducts { get; init; }

        public IEnumerable<ProductServiceModel> Products { get; init; }
    }
}
