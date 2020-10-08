namespace CRMSystem.Web.ViewModels.Organizations
{
    using CRMSystem.Data.Models;
    using CRMSystem.Services.Mapping;

    public class OrganizationViewModel : IMapFrom<Organization>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int AddressId { get; set; }

        public Address Address { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
