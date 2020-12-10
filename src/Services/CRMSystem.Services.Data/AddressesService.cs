using CRMSystem.Web.ViewModels.Addresses;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.Services.Data
{
    using System.Threading.Tasks;

    using CRMSystem.Data.Common.Repositories;
    using CRMSystem.Data.Models;
    using CRMSystem.Services.Data.Contracts;

    public class AddressesService : IAddressesService
    {
        private readonly IRepository<Address> addressRepository;

        public AddressesService(IRepository<Address> addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        public async Task<Address> CreateAsync(string country, string city, string street = null, int? zipCode = null)
        {
            var address = new Address
            {
                Country = country,
                City = city,
                Street = street,
                ZipCode = zipCode,
            };

            await this.addressRepository.AddAsync(address);
            await this.addressRepository.SaveChangesAsync();

            return address;
        }

        public async Task<int> UpdateAsync(AddressCreateInputModel input)
        {
            var address = await this.addressRepository.All().FirstOrDefaultAsync(x=>x.Id == input.Id);
            address.City = input.City;
            address.Country = input.Country;
            address.Street = input.Street;
            address.ZipCode = input.ZipCode;

            this.addressRepository.Update(address);

            return await this.addressRepository.SaveChangesAsync();
        }
    }
}
