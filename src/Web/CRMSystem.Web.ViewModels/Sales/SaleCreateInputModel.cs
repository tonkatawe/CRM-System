using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRMSystem.Web.ViewModels.Sales
{
    public class SaleCreateInputModel
    {
        public int CustomerId { get; set; }

        public IEnumerable<ProductDropDownViewModel> Products { get; set; }

        public int ProductId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
