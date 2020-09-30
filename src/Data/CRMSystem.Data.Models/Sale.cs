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
        public int ContactId { get; set; }

        public Contact Contact { get; set; }

        public IEnumerable<Product> Products { get; set; }

        public decimal Benefit => this.Products.Sum(x => x.Price);

        public int ProductCount => this.Products.Count();
    }
}
