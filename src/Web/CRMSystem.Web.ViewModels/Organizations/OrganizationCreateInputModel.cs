using CRMSystem.Data.Models.Enums;

namespace CRMSystem.Web.ViewModels.Organizations
{
    using System.ComponentModel.DataAnnotations;

    using CRMSystem.Data.Models;
    using CRMSystem.Services.Mapping;
    using CRMSystem.Web.ViewModels.Addresses;

    public class OrganizationCreateInputModel
    {
        public string UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        [MinLength(80)]
        public string Description { get; set; }

        public bool IsPublic { get; set; }

        [Required]
        public IndustryType Industry { get; set; }

        public AddressCreateInputModel Address { get; set; }
    }
}
