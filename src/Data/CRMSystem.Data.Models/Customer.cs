namespace CRMSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CRMSystem.Data.Common.Models;

    public class Customer : BaseDeletableModel<int>
    {
        public Customer()
        {
            this.Phones = new HashSet<PhoneNumber>();
            this.Emails = new HashSet<EmailAddress>();
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

        public string FullName => string.IsNullOrEmpty(this.MiddleName)
            ? (this.FirstName + " " + this.LastName)
            : (this.FirstName + " " + this.MiddleName + " " + this.LastName);

        [MaxLength(30)]
        public string JobTitle { get; set; }

        public int OrganizationId { get; set; }

        public virtual Organization Organization { get; set; }

        
        [MaxLength(1000)]
        public string AdditionalInfo { get; set; }

        public int AddressId { get; set; }

        [Required]
        public virtual Address Address { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<PhoneNumber> Phones { get; set; }

        public virtual ICollection<EmailAddress> Emails { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
