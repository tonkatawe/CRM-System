namespace CRMSystem.Data.Models
{
#nullable enable
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

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

        [Required]
        public Title Title { get; set; }

        [Required]
        [MaxLength(20)]

        public string FirstName { get; set; }

        [MaxLength(20)]
        public string? MiddleName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(30)]
        public string JobTitle { get; set; }

        [Required]
        [MaxLength(30)]
        public string Company { get; set; }

        [MaxLength(20)]
        public string? Industry { get; set; }

        [MaxLength(1000)]
        public string? AdditionalInfo { get; set; }

        public int AddressId { get; set; }

        [Required]
        public Address Address { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public IEnumerable<Note> Notes { get; set; }

        public IEnumerable<PhoneNumber> PhoneNumbers { get; set; }

        public IEnumerable<EmailAddress> EmailAddresses { get; set; }

        public IEnumerable<SocialNetwork> SocialNetworks { get; set; }
    }
}
