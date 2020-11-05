namespace CRMSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CRMSystem.Data.Common.Models;

    public class Organization : BaseDeletableModel<int>
    {
        public Organization()
        {
            this.Customers = new HashSet<Customer>();
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public int AddressId { get; set; }

        public Address Address { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<Customer> Customers { get; set; }
    }
}
