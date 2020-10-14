namespace CRMSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CRMSystem.Data.Common.Models;
    using CRMSystem.Data.Models.Enums;

    public class Contact : BaseDeletableModel<int>
    {
        public Contact()
        {
            this.Notes = new HashSet<Note>();
            this.PhoneNumbers = new HashSet<PhoneNumber>();
            this.EmailAddresses = new HashSet<EmailAddress>();
            this.SocialNetworks = new HashSet<SocialNetwork>();
            this.Sales = new HashSet<Sale>();
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

        public int? OrganizationId { get; set; }

        public Organization Organization { get; set; }

        public IndustryType Industry { get; set; }

        [MaxLength(1000)]
        public string AdditionalInfo { get; set; }

        public int AddressId { get; set; }

        [Required]
        public Address Address { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<Note> Notes { get; set; }

        public ICollection<PhoneNumber> PhoneNumbers { get; set; }

        public ICollection<EmailAddress> EmailAddresses { get; set; }

        public ICollection<SocialNetwork> SocialNetworks { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}
