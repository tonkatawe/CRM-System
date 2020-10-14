using CRMSystem.Data.Models;

namespace CRMSystem.Services.Data.Contracts
{
    using System;
    using System.Threading.Tasks;

    public interface IAddressesService
    {
        Task<Address> CreateAddressAsync(string country, string city, string street = null, int? zipCode = null);
    }
}
