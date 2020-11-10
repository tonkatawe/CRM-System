namespace CRMSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using CRMSystem.Data.Common.Models;

    public class Sale : BaseDeletableModel<int>
    {
        public Sale()
        {
            this.Products = new HashSet<Product>();
        }

        [Required]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
        
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public IEnumerable<Product> Products { get; set; }

        public decimal Benefit => this.Products.Sum(x => x.Price);

        public int ProductCount => this.Products.Count();
    }
}
