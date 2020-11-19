﻿
using System;
using System.ComponentModel.DataAnnotations;
using CRMSystem.Data.Models;
using CRMSystem.Services.Mapping;

namespace CRMSystem.Web.ViewModels.Products
{
    public class ProductCreateInputModel : IMapFrom<Product>
    {
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        [MinLength(10)]
        public string Description { get; set; }

        [Required]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int Quantity { get; set; }

        public int OrganizationId { get; set; }

        public virtual Organization Organization { get; set; }
    }
}
