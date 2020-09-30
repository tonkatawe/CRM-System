namespace CRMSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using CRMSystem.Data.Common.Models;

    public class Product : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }
    }
}
