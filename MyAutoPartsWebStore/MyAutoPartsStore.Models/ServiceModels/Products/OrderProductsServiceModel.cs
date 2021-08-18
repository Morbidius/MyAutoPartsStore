namespace MyAutoPartsStore.Models.ServiceModels.Products
{
    public class OrderProductsServiceModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public int Quantity { get; set; }

        public string ProductSizeCapacity { get; set; }
    }
}
