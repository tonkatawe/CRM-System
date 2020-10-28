using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CRMSystem.Data.Models;
using CRMSystem.Data.Models.Enums;
using CRMSystem.Services.Mapping;
using CRMSystem.Web.ViewModels.Organizations;

namespace CRMSystem.Web.ViewModels.Contacts
{
    public class EditContactInputModel : IMapFrom<Contact>
    {
        public int Id { get; set; }

        public EditContactInputModel()
        {
            this.PhoneNumbers = new HashSet<PhoneNumber>();
            this.EmailAddresses = new HashSet<EmailAddress>();
            this.SocialNetworks = new HashSet<SocialNetwork>();
        }

        [Required]
        public Title Title { get; set; }

        [Required]
        [MaxLength(20)]

        public string FirstName { get; set; }

        [MaxLength(20)]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        [MaxLength(30)]
        public string JobTitle { get; set; }

        public IndustryType Industry { get; set; }

        [MaxLength(1000)]
        public string AdditionalInfo { get; set; }

        [Required]
        public Address Address { get; set; }

        public ICollection<PhoneNumber> PhoneNumbers { get; set; }

        public ICollection<EmailAddress> EmailAddresses { get; set; }

        public ICollection<SocialNetwork> SocialNetworks { get; set; }

    }
}
