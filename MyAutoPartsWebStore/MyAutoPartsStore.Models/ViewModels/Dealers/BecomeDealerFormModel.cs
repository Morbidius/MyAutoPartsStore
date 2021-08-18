namespace MyAutoPartsWebStore.Web.Models.Dealers
{
    using MyAutoPartsStore.Data;
    using System.ComponentModel.DataAnnotations;

    public class BecomeDealerFormModel
    {
        [Required]
        [StringLength(
            DataConstants.Dealer.NameMaxLength,
            MinimumLength = DataConstants.Dealer.NameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(
            DataConstants.Dealer.CompanyNameMaxLength,
            MinimumLength = DataConstants.Dealer.CompanyNameMinLength)]
        [RegularExpression(DataConstants.Dealer.PhoneNumberRegex)]
        [Display(Name = "Phone Number")]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(
            DataConstants.Dealer.PhoneNumberMaxLength,
            MinimumLength = DataConstants.Dealer.PhoneNumberMinLength)]
        [Display(Name = "Company Name")]
        public string PhoneNumber { get; set; }
    }
}
