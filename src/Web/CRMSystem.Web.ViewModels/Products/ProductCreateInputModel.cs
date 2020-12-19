namespace CRMSystem.Web.ViewModels.Products
{
    using CRMSystem.Data.Models;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.ComponentModel.DataAnnotations;
    public class ProductCreateInputModel
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
        [Range(typeof(decimal), "0", "79228162514264337593543950335", ErrorMessage = "The price is out of range")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "The quantity is out of range")]
        public int Quantity { get; set; }

        public string OrganizationId { get; set; }

        public virtual Organization Organization { get; set; }
        
        public IFormFile ProductPicture { get; set; }
    }
}
