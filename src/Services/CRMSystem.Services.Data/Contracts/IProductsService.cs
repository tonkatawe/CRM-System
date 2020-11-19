

using System.Linq;
using System.Threading.Tasks;
using CRMSystem.Web.ViewModels.Products;

namespace CRMSystem.Services.Data.Contracts
{
    public interface IProductsService
    {
        Task<int> CreateAsync(ProductCreateInputModel input, string userId);

        Task<int> UpdateAsync(EditProductInputModel input);

        Task<int> DeleteAsync(int productId);

        T GetById<T>(int productId);
        IQueryable<T> GetAll<T>(string userId);
        int ProductsCount(string userId);
    }
}
