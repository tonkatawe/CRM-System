using System;
using CRMSystem.Web.ViewModels.Customers;
using CRMSystem.Web.ViewModels.Products;

namespace CRMSystem.Web.ViewModels.Statistics
{
    public class GeneralStatisticViewModel
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public decimal TotalBenefits { get; set; }
        public string OrganizationName { get; set; }
        public CustomerViewModel BestCustomer { get; set; }
        public CustomerViewModel CustomerWithMostOrders { get; set; }
        public ProductViewModel MostOrderedProduct { get; set; }
        public ProductViewModel MostBenefitProduct { get; set; }
    }
}
