namespace MyAutoPartsStore.Models.ServiceModels.Orders
{
    using MyAutoPartsStore.Models.ServiceModels.Products;
    using System;
    using System.Collections.Generic;

    public class OrderServiceInformation
    {
        public string BuyerPhone { get; set; }

        public string UserEmail { get; set; }

        public string UserFullName { get; set; }

        public string BuyerAddress { get; set; }

        public string Note { get; set; }

        public DateTime OrderedOn { get; set; }

        public IEnumerable<OrderProductsServiceModel> Products { get; set; }
    }
}