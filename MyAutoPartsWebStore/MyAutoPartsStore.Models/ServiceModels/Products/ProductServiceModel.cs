namespace MyAutoPartsStore.Models.ServiceModels.Products
{
    using MyAutoPartsWebStore.Web.Base;

    public class ProductServiceModel : BaseCategoryModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string Description { get; init; }

        public decimal Price { get; init; }

        public string SizeCapacity { get; init; }

        public float Weight { get; init; }

        public string ImageUrl { get; init; }

        public int DealerId { get; init; }

        public bool IsAllowed { get; init; }
    }
}
