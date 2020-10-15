namespace CRMSystem.Web.ViewModels.Organizations
{
    using System.ComponentModel.DataAnnotations;

    using CRMSystem.Data.Models;
    using CRMSystem.Services.Mapping;
    using CRMSystem.Web.ViewModels.Addresses;

    public class OrganizationCreateInputModel : IMapFrom<Organization>
    {
        public string UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public AddressCreateInputModel Address { get; set; }
    }
}
