namespace MyAutoPartsStore.Models.ServiceModels.Orders
{
    using System.ComponentModel.DataAnnotations;

    public class DealerOrderFormServiceModel
    {
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
    }
}
