﻿namespace CRMSystem.Data.Models
{
    using CRMSystem.Data.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Product : BaseDeletableModel<int>
    {
        public Product()
        {
            this.Orders = new HashSet<Order>();
            this.Customers = new HashSet<Customer>();
        }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        [Range(0, Int32.MaxValue)]
        public int Quantity { get; set; }

        public string OrganizationId { get; set; }


        public virtual Organization Organization { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }

        public string ProductPictureUrl { get; set; }
    }
}
