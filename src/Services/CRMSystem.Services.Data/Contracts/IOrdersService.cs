namespace CRMSystem.Services.Data.Contracts
{
    using CRMSystem.Web.ViewModels.Orders;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public  interface IOrdersService
    {
        Task<int> CreateOrder(int customerId, int productId, int quantity);

        Task<OrderCustomerStatsViewModel> GetStatsAsync(int customerId);

        IQueryable<T> GetOrders<T>(int customerId);


        IEnumerable<T> GetOrdersType<T>(int customerId);
    }
}
