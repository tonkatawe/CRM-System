

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRMSystem.Web.ViewModels.Sales;

namespace CRMSystem.Services.Data.Contracts
{
    public  interface IOrdersService
    {
        Task<int> CreateOrder(int customerId, int productId, int quantity);

        Task<OrderCustomerStatsViewModel> GetStatsAsync(int customerId);

        IQueryable<T> GetOrders<T>(int customerId);


        IEnumerable<T> GetOrdersType<T>(int customerId);
    }
}
