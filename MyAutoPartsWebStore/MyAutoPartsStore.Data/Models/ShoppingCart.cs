namespace MyAutoPartsStore.Data.Models
{
    public class ShoppingCart
    {
        public Product Product { get; set; }

        public int ProductId { get; set; }

        public User User { get; set; }

        public string UserId { get; set; }

        public int Quantity { get; set; }
    }
}