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
            this.SaleProducts = new HashSet<SaleProducts>();
        }

        [Required]
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<SaleProducts> SaleProducts { get; set; }

        //public decimal Benefit => this.Products.Sum(x => x.Price);

        //public int ProductCount => this.Products.Count();
    }
}
