namespace CRMSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CRMSystem.Data.Common.Models;
    using CRMSystem.Data.Models.Enums;

    public class Customer : BaseDeletableModel<int>
    {
        public Customer()
        {
            this.Notes = new HashSet<Note>();
            this.PhoneNumbers = new HashSet<PhoneNumber>();
            this.EmailAddresses = new HashSet<EmailAddress>();
            this.SocialNetworks = new HashSet<SocialNetwork>();
            this.Orders = new HashSet<Order>();
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

        public int OrganizationId { get; set; }

        public virtual Organization Organization { get; set; }

        public IndustryType Industry { get; set; }

        [MaxLength(1000)]
        public string AdditionalInfo { get; set; }

        public int AddressId { get; set; }

        [Required]
        public virtual Address Address { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }
               
        public virtual ICollection<EmailAddress> EmailAddresses { get; set; }
               
        public virtual ICollection<SocialNetwork> SocialNetworks { get; set; }
               
        public virtual ICollection<Order> Orders { get; set; }
    }
}
