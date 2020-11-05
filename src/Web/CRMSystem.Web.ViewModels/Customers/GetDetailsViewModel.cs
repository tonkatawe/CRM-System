using System.Collections.Generic;
using CRMSystem.Data.Models;
using CRMSystem.Web.ViewModels.Contacts;
using CRMSystem.Web.ViewModels.Emails;

namespace CRMSystem.Web.ViewModels.Customers
{
    public class GetDetailsViewModel : CustomerViewModel
    {
        public GetDetailsViewModel()
        {
            this.PhoneNumbers = new List<PhoneNumber>();
            this.EmailAddresses = new List<EmailCreateInputModel>();
            this.SocialNetworks = new List<SocialNetwork>();
        }
        public Title Title { get; set; }

        public string TitleAsString => this.Title.ToString();

        public string JobTitle { get; set; }
        
        public string AdditionalInfo { get; set; }

        public Address Address { get; set; }

        public IList<PhoneNumber> PhoneNumbers { get; set; }

        public IList<EmailCreateInputModel> EmailAddresses { get; set; }

        public IList<SocialNetwork> SocialNetworks { get; set; }

        public EditCustomerInputModel SharedEditContactViewModel { get; set; }
    }
}
