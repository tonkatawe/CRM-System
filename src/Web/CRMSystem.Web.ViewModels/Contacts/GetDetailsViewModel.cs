namespace CRMSystem.Web.ViewModels.Contacts
{
    using System.Collections.Generic;

    using CRMSystem.Data.Models;


    public class GetDetailsViewModel : ContactViewModel
    {
        public GetDetailsViewModel()
        {
            this.PhoneNumbers = new List<PhoneNumber>();
            this.EmailAddresses = new List<EmailAddress>();
            this.SocialNetworks = new List<SocialNetwork>();
        }
        public Title Title { get; set; }

        public string TitleAsString => this.Title.ToString();

        public string JobTitle { get; set; }
        
        public string AdditionalInfo { get; set; }

        public Address Address { get; set; }

        public IList<PhoneNumber> PhoneNumbers { get; set; }

        public IList<EmailAddress> EmailAddresses { get; set; }

        public IList<SocialNetwork> SocialNetworks { get; set; }

        public EditContactInputModel SharedEditContactViewModel { get; set; }
    }
}
