namespace CRMSystem.Web.ViewModels.Organizations
{
    using CRMSystem.Data.Models.Enums;
    using CRMSystem.Web.ViewModels.Addresses;
    using System.ComponentModel.DataAnnotations;

    public class OrganizationCreateInputModel
    {
        public string UserId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Company name should be maximum 50 letters")]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000, ErrorMessage = "Company description should be maximum 1000 letters")]
        [MinLength(80, ErrorMessage = "Company description should be minimum 80 letters")]
        public string Description { get; set; }

        public bool IsPublic { get; set; }

        [Required]
        public IndustryType Industry { get; set; }

        public AddressCreateInputModel Address { get; set; }
    }
}
