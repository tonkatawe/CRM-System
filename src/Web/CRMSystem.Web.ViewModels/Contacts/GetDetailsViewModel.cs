namespace CRMSystem.Web.ViewModels.Contacts
{
    using System.Collections.Generic;

    using CRMSystem.Data.Models;
    using CRMSystem.Data.Models.Enums;
    using CRMSystem.Services.Mapping;
    using CRMSystem.Web.ViewModels.Organizations;

    public class GetDetailsViewModel : IMapFrom<Contact>
    {
        public int Id { get; set; }

        public Title Title { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string JobTitle { get; set; }

        public IndustryType Industry { get; set; }

        public OrganizationViewModel Organization { get; set; }

        public string AdditionalInfo { get; set; }

        public Address Address { get; set; }

        public ICollection<PhoneNumber> PhoneNumbers { get; set; }

        public ICollection<EmailAddress> EmailAddresses { get; set; }

        public ICollection<SocialNetwork> SocialNetworks { get; set; }
    }
}
