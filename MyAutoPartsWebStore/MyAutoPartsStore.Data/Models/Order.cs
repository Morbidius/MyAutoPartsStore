namespace MyAutoPartsStore.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Order
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public string BuyerPhone { get; set; }

        public string BuyerAddress { get; set; }

        public string Note { get; set; }

        public DateTime OrderedOn { get; set; } = DateTime.UtcNow;

        public bool IsCompleted { get; set; } = false;

        public IEnumerable<OrderProducts> Products { get; init; } = new List<OrderProducts>();
    }
}