namespace MyAutoPartsStore.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {
        [MaxLength(DataConstants.User.FullNameMaxLength)]
        public string FullName { get; set; }
    }
}
