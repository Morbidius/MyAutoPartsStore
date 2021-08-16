namespace MyAutoPartsStore.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ShoppingCart
    {
        public int Id { get; init; }

        public string UserId { get; set; }

        public User User { get; set; }

        public IEnumerable<Product> Products { get; init; } = new List<Product>();
    }
}
