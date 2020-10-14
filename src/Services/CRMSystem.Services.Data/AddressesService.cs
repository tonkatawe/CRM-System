namespace CRMSystem.Services.Data
{
    using System.Threading.Tasks;

    using CRMSystem.Data.Common.Repositories;
    using CRMSystem.Data.Models;
    using CRMSystem.Services.Data.Contracts;

    public class AddressesService : IAddressesService
    {
        private readonly IDeletableEntityRepository<Address> addressRepository;

        public AddressesService(IDeletableEntityRepository<Address> addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        public async Task<Address> CreateAddressAsync(string country, string city, string street = null, int? zipCode = null)
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
    }
}
