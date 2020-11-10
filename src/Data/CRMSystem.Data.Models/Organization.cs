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
            this.Products = new HashSet<Product>();
            this.Sales = new HashSet<Sale>();
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
        public ICollection<Product> Products { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}
