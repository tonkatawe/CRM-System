

using System.Collections.Generic;
using System.Threading.Tasks;
using CRMSystem.Web.ViewModels.Sales;

namespace CRMSystem.Services.Data.Contracts
{
    public  interface ISalesService
    {
        Task<int> CreateSale(int customerId, int productId, int quantity);

        Task<SaleCustomerStatsViewModel> GetStatsAsync(int customerId);
    }
}
