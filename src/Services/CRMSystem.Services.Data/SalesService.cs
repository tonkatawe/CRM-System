using System.Collections.Generic;
using System.Linq;
using CRMSystem.Data.Common.Repositories;
using CRMSystem.Data.Models;
using CRMSystem.Services.Data.Contracts;
using CRMSystem.Web.ViewModels.Sales;
using System.Threading.Tasks;
using CRMSystem.Web.ViewModels.Products;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.Services.Data
{
    public class SalesService : ISalesService
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;
        private readonly IDeletableEntityRepository<Customer> customersRepository;
        private readonly IDeletableEntityRepository<Order> salesRepository;

        public SalesService(IDeletableEntityRepository<Product> productsRepository,
            IDeletableEntityRepository<Customer> customersRepository,
            IDeletableEntityRepository<Order> salesRepository)
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

            var order = new Order
            {
                OrganizationId = customer.OrganizationId,
                CustomerId = customerId,
                ProductId = productId,
                Quantity = quantity,
            };



            customer.Sales.Add(order);

            return await this.customersRepository.SaveChangesAsync();
        }

        public async Task<SaleCustomerStatsViewModel> GetStatsAsync(int customerId)
        {
            var totalOrders = await this.salesRepository
                .All()
                .CountAsync(x => x.CustomerId == customerId);

            var differentProducts = await this.salesRepository
                .All()
                .Where(x => x.CustomerId == customerId)
                .Select(x => x.ProductId)
                .Distinct()
                .CountAsync();

            var benefits = await this.salesRepository
                .All()
                .Where(x => x.CustomerId == customerId)
                .SumAsync(x => x.Product.Price * x.Quantity);

            return new SaleCustomerStatsViewModel
            {
                TotalOrders = totalOrders,
                DifferentProducts = differentProducts,
                Benefits = benefits,

            };
        }
    }
}
