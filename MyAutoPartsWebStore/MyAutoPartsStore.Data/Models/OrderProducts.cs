namespace MyAutoPartsStore.Data.Models
{
    public class OrderProducts
    {
        public Order Order { get; set; }

        public int OrderId { get; set; }

        public Product Product { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public bool IsCompleted { get; set; } = false;
    }
}