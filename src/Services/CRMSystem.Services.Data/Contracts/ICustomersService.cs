using System.Linq;
using CRMSystem.Web.ViewModels.Customers;

namespace CRMSystem.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CRMSystem.Web.ViewModels.Contacts;

    public interface ICustomersService
    {
        IQueryable<T> GetAll<T>(string userId);

        T GetById<T>(int customerId);
        
        IEnumerable<T> GetByName<T>(string userId);
        
        Task<int> DeleteAsync(int id);
        
        Task<int> CreateAsync(CustomerAddInputModel input, string userId);
        
        Task<int> UpdateAsync(EditCustomerInputModel input);

    }
}
