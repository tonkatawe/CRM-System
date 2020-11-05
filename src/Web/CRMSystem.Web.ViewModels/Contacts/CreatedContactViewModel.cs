namespace CRMSystem.Web.ViewModels.Contacts
{
    using CRMSystem.Data.Models;
    using CRMSystem.Services.Mapping;

    public class CreatedContactViewModel : IMapFrom<Customer>
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public int OrganizationId { get; set; }
    }
}
