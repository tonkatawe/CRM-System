namespace CRMSystem.Web.ViewModels.Contacts
{
    using System.Collections.Generic;

    using CRMSystem.Data.Models;


    public class GetDetailsViewModel : ContactViewModel
    {
        public GetDetailsViewModel()
        {
            this.PhoneNumbers = new HashSet<PhoneNumber>();
            this.EmailAddresses = new HashSet<EmailAddress>();
            this.SocialNetworks = new HashSet<SocialNetwork>();
        }
        public Title Title { get; set; }

        public string TitleAsString => this.Title.ToString();

        public string JobTitle { get; set; }
        
        public string AdditionalInfo { get; set; }

        public Address Address { get; set; }

        public ICollection<PhoneNumber> PhoneNumbers { get; set; }

        public ICollection<EmailAddress> EmailAddresses { get; set; }

        public ICollection<SocialNetwork> SocialNetworks { get; set; }

        public EditContactInputModel SharedEditContactViewModel { get; set; }
    }
}
