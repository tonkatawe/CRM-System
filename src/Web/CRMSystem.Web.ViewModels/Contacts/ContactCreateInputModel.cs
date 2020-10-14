using CRMSystem.Web.ViewModels.Addresses;

namespace CRMSystem.Web.ViewModels.Contacts
{
    using System.ComponentModel.DataAnnotations;

    using CRMSystem.Data.Models;
    using CRMSystem.Data.Models.Enums;
    using CRMSystem.Services.Mapping;
    using CRMSystem.Web.ViewModels.Emails;
    using CRMSystem.Web.ViewModels.Organizations;
    using CRMSystem.Web.ViewModels.Phones;
    using CRMSystem.Web.ViewModels.SocialNetworks;

    public class ContactCreateInputModel : IMapFrom<Contact>
    {

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

        public AddressCreateInputModel Address { get; set; }

        [Required]
        public EmailCreateInputModel Email { get; set; }

        public EmailType EmailType { get; set; }

        public PhoneCreateInputModel PhoneNumber { get; set; }

        public PhoneType PhoneType { get; set; }

        public SocialNetworkCreateInputModel Network { get; set; }

        public SocialNetworkType SocialNetworkType { get; set; }
    }
}
