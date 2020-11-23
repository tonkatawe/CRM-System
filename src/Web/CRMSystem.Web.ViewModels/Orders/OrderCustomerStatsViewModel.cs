using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using CRMSystem.Data.Models;
using CRMSystem.Web.ViewModels.Products;

namespace CRMSystem.Web.ViewModels.Sales
{
    public class OrderCustomerStatsViewModel
    {

        public int TotalOrders { get; set; }
        
        public decimal Benefits { get; set; }

        public int DifferentProducts { get; set; }
    }

    
}
