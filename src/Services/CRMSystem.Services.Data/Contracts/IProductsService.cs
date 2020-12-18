namespace CRMSystem.Services.Data.Contracts
{
    using CRMSystem.Web.ViewModels.Products;
    using System.Linq;
    using System.Threading.Tasks;

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
