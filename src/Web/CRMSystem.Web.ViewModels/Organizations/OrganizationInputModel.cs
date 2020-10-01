namespace CRMSystem.Web.ViewModels.Organizations
{
    using System.ComponentModel.DataAnnotations;

    using CRMSystem.Data.Models;
    using CRMSystem.Services.Mapping;

    public class OrganizationInputModel : IMapFrom<Organization>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public int AddressId { get; set; }

        public Address Address { get; set; }

    }
}
