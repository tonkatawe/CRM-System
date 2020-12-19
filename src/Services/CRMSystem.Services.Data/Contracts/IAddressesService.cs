namespace CRMSystem.Services.Data.Contracts
{
    using CRMSystem.Data.Models;
    using CRMSystem.Web.ViewModels.Addresses;
    using System.Threading.Tasks;

    public interface IAddressesService
    {
        Task<Address> CreateAsync(string country, string city, string street = null, int? zipCode = null);

        Task<int> UpdateAsync(AddressCreateInputModel input);

        Task DeleteAsync(int id);
    }
}
