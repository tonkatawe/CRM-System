namespace CRMSystem.Web.ViewModels.Contacts
{
    using CRMSystem.Data.Models;
    using CRMSystem.Data.Models.Enums;
    using CRMSystem.Services.Mapping;
    using CRMSystem.Web.ViewModels.Organizations;

    public class ContactViewModel : IMapFrom<Contact>
    {
        public Title Title { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string JobTitle { get; set; }

        public IndustryType Industry { get; set; }

        public OrganizationInputModel Organization { get; set; }

        public string AdditionalInfo { get; set; }

        public Address Address { get; set; }
    }
}
