using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRMSystem.Web.ViewModels.Sales
{
    public class OrderCreateInputModel
    {
        public int CustomerId { get; set; }

        public IEnumerable<ProductDropDownViewModel> Products { get; set; }

        public int ProductId { get; set; }
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "You have to add minimum 1 product quantity")]
        public int Quantity { get; set; }
    }
}
