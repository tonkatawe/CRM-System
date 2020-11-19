using CRMSystem.Web.ViewModels.Phones;

namespace CRMSystem.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CRMSystem.Data.Models;
    using CRMSystem.Data.Models.Enums;

    public interface IPhonesServices
    {
        IEnumerable<T> GetAll<T>(int customerId);

        Task<PhoneNumber> CreateAsync(string phone, PhoneType type, int customerId);

        Task<int> UpdateAsync(PhoneCreateInputModel input);

        Task<int> DeleteAsync(int id);
    }
}
