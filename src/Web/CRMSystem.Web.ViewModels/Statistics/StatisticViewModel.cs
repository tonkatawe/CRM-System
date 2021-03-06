﻿using CRMSystem.Web.ViewModels.Customers;
using CRMSystem.Web.ViewModels.Products;
using System;

namespace CRMSystem.Web.ViewModels.Statistics
{
    public class StatisticViewModel
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public decimal TotalBenefits { get; set; }
        public string OrganizationName { get; set; }
        public CustomerViewModel BestCustomer { get; set; }
        public CustomerViewModel CustomerWithMostOrders { get; set; }
        public ProductViewModel MostOrderedProduct { get; set; }
        public ProductViewModel MostBenefitProduct { get; set; }

        public int OrdersCount { get; set; }
    }
}
