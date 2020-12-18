namespace CRMSystem.Services.Data.Contracts
{
    using CRMSystem.Web.ViewModels.Customers;
    using System.Linq;
    using System.Threading.Tasks;

    public interface ICustomersService
    {
        IQueryable<T> GetAll<T>(string userId, bool isTemporary);

        T GetById<T>(int customerId);

        Task<string> CustomerUserIdAsync(int id);

        Task<int> DeleteAsync(int id);

        Task<int> CreateAsync(CustomerAddInputModel input, string userId, string organizationId, bool isTemporary);

        Task<int> UpdateAsync(EditCustomerInputModel input);

        Task<int> GetCountAsync();


    }
}
