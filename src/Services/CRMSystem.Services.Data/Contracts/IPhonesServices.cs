using CRMSystem.Web.ViewModels.Phones;

namespace CRMSystem.Services.Data.Contracts
{
    using CRMSystem.Data.Models;
    using CRMSystem.Data.Models.Enums;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPhonesServices
    {
        IEnumerable<T> GetAll<T>(int customerId);

        Task<PhoneNumber> CreateAsync(string phone, PhoneType type, int customerId);

        Task<int> UpdateAsync(PhoneCreateInputModel input);

        Task<int> DeleteAsync(int id);

        Task DeleteAllAsync(int customerId);
    }
}
