namespace CRMSystem.Services.Data
{
    using CRMSystem.Data.Common.Repositories;
    using CRMSystem.Data.Models;
    using CRMSystem.Services.Data.Contracts;
    using System.Threading.Tasks;
    public class TemporaryCustomersService : ITemporaryCustomersService
    {
        private readonly IDeletableEntityRepository<Customer> customerRepository;

        public TemporaryCustomersService(IDeletableEntityRepository<Customer> customerRepository)
        {
            this.customerRepository = customerRepository;

        }

        public async Task ApproveAsync(int id)
        {
            var customer = await this.customerRepository.GetByIdWithDeletedAsync(id);

            customer.IsTemporary = false;
            this.customerRepository.Update(customer);

            await this.customerRepository.SaveChangesAsync();
        }
    }
}
