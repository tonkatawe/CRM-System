using System;
using System.Collections.Generic;
using System.Text;

namespace CRMSystem.Web.ViewModels.SaleProducts
{
    public class SaleCreateInputModel
    {
        public int CustomerId { get; set; }

        public IEnumerable<ProductDropDownViewModel> Products { get; set; }

        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
