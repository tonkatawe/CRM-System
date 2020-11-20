

using System.Threading.Tasks;

namespace CRMSystem.Services.Data.Contracts
{
    public  interface ISaleProductsService
    {
        Task<int> CreateSale(int customerId, int productId, int quantity);
    }
}
