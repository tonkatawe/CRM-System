using System.Collections.Generic;
using System.Linq;
using CRMSystem.Data.Common.Repositories;
using CRMSystem.Data.Models;
using CRMSystem.Services.Data.Contracts;
using CRMSystem.Web.ViewModels.Sales;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.Services.Data
{
    public class SalesService : ISalesService
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;
        private readonly IDeletableEntityRepository<Customer> customersRepository;
        private readonly IDeletableEntityRepository<Sale> salesRepository;

        public SalesService(IDeletableEntityRepository<Product> productsRepository,
            IDeletableEntityRepository<Customer> customersRepository,
            IDeletableEntityRepository<Sale> salesRepository)
        {
            this.productsRepository = productsRepository;
            this.customersRepository = customersRepository;
            this.salesRepository = salesRepository;
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

        public async Task<IEnumerable<SaleCustomerStatsViewModel>> GetStatsAsync(int customerId)
        {
            var sales = await this.salesRepository
                .All()
                .Where(x => x.CustomerId == customerId)
                .Select(x => new SaleCustomerStatsViewModel
                {
                   ProductName = x.Product.Name,
                   ProductQuantity = x.Product.Quantity,
                   ProductPrice = x.Product.Price,
                })
                .ToListAsync();



            return sales;
        }
    }
}
