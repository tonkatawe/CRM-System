using System;
using CRMSystem.Data.Models.Enums;

namespace CRMSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CRMSystem.Data.Common.Models;

    public class Organization : BaseDeletableModel<string>, IAuditInfo
    {
        public Organization()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Customers = new HashSet<Customer>();
            this.Products = new HashSet<Product>();
            this.Orders = new HashSet<Order>();
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public IndustryType Industry { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public int AddressId { get; set; }

        public virtual Address Address { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
