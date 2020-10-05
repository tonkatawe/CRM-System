﻿namespace CRMSystem.Web.ViewModels.Contacts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CRMSystem.Data.Models;
    using CRMSystem.Data.Models.Enums;
    using CRMSystem.Services.Mapping;
    using CRMSystem.Web.ViewModels.Organizations;

    public class ContactCreateInputModel : IMapFrom<Contact>, IMapFrom<OrganizationInputModel>
    {
        public ContactCreateInputModel()
        {
            this.PhoneNumbers = new HashSet<PhoneNumber>(2);
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

        [Required]
        [MaxLength(30)]
        public string JobTitle { get; set; }

        public IndustryType Industry { get; set; }

        public OrganizationInputModel Organization { get; set; }

        [MaxLength(1000)]
        public string AdditionalInfo { get; set; }

        [Required]
        public Address Address { get; set; }

        public PhoneType PhoneType { get; set; }

        public PhoneNumber PhoneNumber { get; set; }

        public ICollection<PhoneNumber> PhoneNumbers { get; set; }

        public EmailType EmailType { get; set; }

        public EmailAddress EmailAddress { get; set; }

        public ICollection<EmailAddress> EmailAddresses { get; set; }

        public SocialNetwork networkTitle { get; set; }

        public SocialNetworkType SocialNetworkType { get; set; }

        public IEnumerable<SocialNetwork> SocialNetworks { get; set; }
    }
}
