namespace MyAutoPartsStore.Services.ProductServices
{
    public class ProductServiceDetailsModel : ProductServiceModel
    {
        public int CategoryId { get; init; }

        public string DealerName { get; init; }

        public string UserId { get; init; }
    }
}
