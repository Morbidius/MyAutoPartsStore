namespace MyAutoPartsStore.Models.ServiceModels.Orders
{
    using System;

    public class BasicOrderInformation
    {
        public int OrderId { get; set; }

        public string OrderUserEmail { get; set; }

        public DateTime OrderOrderedOn { get; set; }

        // Override to split the orders for each user
        public override bool Equals(object obj)
        {
            var item = obj as BasicOrderInformation;

            if (item == null)
            {
                return false;
            }

            return this.OrderId.Equals(item.OrderId);
        }

        public override int GetHashCode()
        {
            return this.OrderId.GetHashCode();
        }
    }
}
