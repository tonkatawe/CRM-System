using System.Linq;
using CRMSystem.Web.ViewModels.Customers;

namespace CRMSystem.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CRMSystem.Web.ViewModels.Contacts;

    public interface ICustomersService
    {
        IQueryable<T> GetAllUserCustomers<T>(string userId);

        T GetCustomerById<T>(int contactId);
        
        IEnumerable<T> GetByName<T>(string userId, int skip = 0);
        
        Task<int> DeleteCustomerAsync(int id);
        
        Task<int> CreateCustomerAsync(CustomerAddInputModel input, string userId);
        
        Task<int> UpdateCustomerAsync(EditCustomerInputModel input);

    }
}
