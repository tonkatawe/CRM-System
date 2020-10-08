namespace CRMSystem.Web.ViewModels.Contacts
{
    using CRMSystem.Data.Models;
    using CRMSystem.Services.Mapping;

    public class OrganizationDropDownViewModel : IMapFrom<Organization>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
