namespace CRMSystem.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CRMSystem.Data.Models;
    using CRMSystem.Data.Models.Enums;

    public interface IPhonesServices
    {
        IEnumerable<T> GetAllContactPhones<T>(int contactId);

        Task<PhoneNumber> CreatePhoneAsync(string phone, PhoneType type, int contactId);
    }
}
