namespace MyAutoPartsStore.Models.ServiceModels.Orders
{
    using MyAutoPartsStore.Data.Models;
    using System;

    public class DealerOrderFormServiceModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public string BuyerEmail { get; set; }

        public string BuyerPhone { get; set; }

        public string BuyerAddress { get; set; }

        public string Note { get; set; }

        public DateTime OrderedOn { get; set; }
    }
}
