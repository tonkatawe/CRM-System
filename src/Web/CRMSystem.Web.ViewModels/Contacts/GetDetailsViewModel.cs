namespace CRMSystem.Web.ViewModels.Contacts
{
    using System.Collections.Generic;

    using CRMSystem.Data.Models;
    using CRMSystem.Data.Models.Enums;
    using CRMSystem.Services.Mapping;
    using CRMSystem.Web.ViewModels.Organizations;

    public class GetDetailsViewModel : ContactViewModel
    {
        public int Id { get; set; }

        public Title Title { get; set; }

        public string TitleAsString => this.Title.ToString();

        public string JobTitle { get; set; }
        
        public string AdditionalInfo { get; set; }

        public Address Address { get; set; }

        public ICollection<PhoneNumber> PhoneNumbers { get; set; }

        public ICollection<EmailAddress> EmailAddresses { get; set; }

        public ICollection<SocialNetwork> SocialNetworks { get; set; }
    }
}
