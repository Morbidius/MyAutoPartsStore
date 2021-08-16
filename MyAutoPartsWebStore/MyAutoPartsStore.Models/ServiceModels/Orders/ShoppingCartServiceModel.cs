namespace MyAutoPartsStore.Models.ServiceModels.Orders
{
    public class ShoppingCartServiceModel
    {
        public int Id { get; init; }

        public string Name { get; set; }

        public decimal Price { get; init; }

        public string SizeCapacity { get; init; }

        public float Weight { get; init; }

        public string ImageUrl { get; init; }

        public int Quantity { get; set; }

        public int DealerId { get; init; }

        public string DealerName { get; init; }
    }
}
