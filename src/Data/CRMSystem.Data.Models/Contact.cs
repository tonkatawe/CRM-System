namespace CRMSystem.Data.Models
{
    using System.Collections.Generic;

    using CRMSystem.Data.Common.Models;

    public class Contact : BaseDeletableModel<int>
    {
        public Contact()
        {
            this.Notes = new HashSet<Note>();
            this.PhoneNumbers = new HashSet<PhoneNumber>();
            this.EmailAddresses = new HashSet<EmailAddress>();
            this.SocialNetworks = new HashSet<SocialNetwork>();
        }

        public Title Title { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string JobTitle { get; set; }

        public string Company { get; set; }

        public string Industry { get; set; }

        public string AdditionalInfo { get; set; }

        public int AddressId { get; set; }

        public Address Address { get; set; }

        public string UserId { get; set; }


        public ApplicationUser User { get; set; }

        public IEnumerable<Note> Notes { get; set; }

        public IEnumerable<PhoneNumber> PhoneNumbers { get; set; }

        public IEnumerable<EmailAddress> EmailAddresses { get; set; }

        public IEnumerable<SocialNetwork> SocialNetworks { get; set; }
    }
}
