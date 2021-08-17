namespace MyAutoPartsStore.Models.ServiceModels.Orders
{
    using MyAutoPartsStore.Data.Models;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DealerOrderFormServiceModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        [Required]
        [Phone]
        [Display (Name = "Phone number")]
        public string BuyerPhone { get; set; }

        [Required]
        [EmailAddress]
        [Display (Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string BuyerAddress { get; set; }

        public string Note { get; set; }

        public int Quantity { get; set; }

        public string ProductName { get; set; }

        public DateTime OrderedOn { get; set; } = DateTime.UtcNow;

        public bool IsCompleted { get; set; }
    }
}
