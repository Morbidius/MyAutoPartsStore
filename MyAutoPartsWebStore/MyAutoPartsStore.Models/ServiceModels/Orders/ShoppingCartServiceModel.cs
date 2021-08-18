namespace MyAutoPartsStore.Models.ServiceModels.Orders
{
    public class ShoppingCartServiceModel
    {
        public int ProductId { get; init; }

        public string ProductName { get; set; }

        public decimal ProductPrice { get; init; }

        public string ProductSizeCapacity { get; init; }

        public float ProductWeight { get; init; }

        public string ProductImageUrl { get; init; }

        public int Quantity { get; set; }

        public string ProductDealerName { get; init; }
    }
}
