namespace CRMSystem.Web.ViewModels.Orders
{
    public class OrderCustomerStatsViewModel
    {
        public int Id  { get; set; }
        public int TotalOrders { get; set; }
        
        public decimal Benefits { get; set; }

        public int DifferentProducts { get; set; }

       
    }

    
}
