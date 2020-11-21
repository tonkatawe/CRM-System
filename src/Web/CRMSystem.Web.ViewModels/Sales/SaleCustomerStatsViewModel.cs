using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using CRMSystem.Data.Models;
using CRMSystem.Web.ViewModels.Products;

namespace CRMSystem.Web.ViewModels.Sales
{
    public class SaleCustomerStatsViewModel
    {
        public int TotalSales { get; set; }

        public ICollection<Sale> Sales { get; set; }
        public int Quantity { get; set; }

        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public int ProductId { get; set; }

        public decimal ProductPrice { get; set; }

        }
}
