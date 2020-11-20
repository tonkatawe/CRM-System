

using System.Threading.Tasks;
using CRMSystem.Data.Common.Repositories;
using CRMSystem.Data.Models;
using CRMSystem.Services.Data.Contracts;

namespace CRMSystem.Services.Data
{
    public class SaleProductsService : ISaleProductsService
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;
        private readonly IDeletableEntityRepository<Customer> customersRepository;

        public SaleProductsService(IDeletableEntityRepository<Product> productsRepository,
            IDeletableEntityRepository<Customer> customersRepository)
        {
            this.productsRepository = productsRepository;
            this.customersRepository = customersRepository;
        }

        public async Task<int> CreateSale(int customerId, int productId, int quantity)
        {
            var product = await this.productsRepository.GetByIdWithDeletedAsync(productId);
            var customer = await this.customersRepository.GetByIdWithDeletedAsync(customerId);

            product.Quantity -= quantity;
            this.productsRepository.Update(product);
            await this.productsRepository.SaveChangesAsync();

            var sale = new Sale
            {
                CustomerId = customerId,
                ProductId = productId,
                Quantity = quantity,
            };

            

            customer.Sales.Add(sale);

            return await this.customersRepository.SaveChangesAsync();
        }
    }
}
