
using CRMSystem.Web.ViewModels.Customers;
using CRMSystem.Web.ViewModels.Products;

namespace CRMSystem.Web.ViewModels.Statistics
{
    public class StatisticsIndexViewModel
    {
        public decimal TotalBenefits { get; set; }

        public CustomerViewModel BestCustomer { get; set; }
        public CustomerViewModel CustomerWithMostOrders { get; set; }
        public ProductViewModel MostOrderedProduct { get; set; }
        public ProductViewModel MostBenefitProduct { get; set; }
    }
}
