using System.Collections.Generic;
using System.Linq;
using CRMSystem.Data.Common.Repositories;
using CRMSystem.Data.Models;
using CRMSystem.Services.Data.Contracts;
using CRMSystem.Web.ViewModels.Sales;
using System.Threading.Tasks;
using CRMSystem.Services.Mapping;
using CRMSystem.Web.ViewModels.Orders;
using CRMSystem.Web.ViewModels.Products;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.Services.Data
{
    public class OrdersService : IOrdersService
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;
        private readonly IDeletableEntityRepository<Customer> customersRepository;
        private readonly IDeletableEntityRepository<Order> ordersRepository;

        public OrdersService(IDeletableEntityRepository<Product> productsRepository,
            IDeletableEntityRepository<Customer> customersRepository,
            IDeletableEntityRepository<Order> ordersRepository)
        {
            this.productsRepository = productsRepository;
            this.customersRepository = customersRepository;
            this.ordersRepository = ordersRepository;
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

        public async Task<OrderCustomerStatsViewModel> GetStatsAsync(int customerId)
        {
            var totalOrders = await this.ordersRepository
                .All()
                .CountAsync(x => x.CustomerId == customerId);

            var differentProducts = await this.ordersRepository
                .All()
                .Where(x => x.CustomerId == customerId)
                .Select(x => x.ProductId)
                .Distinct()
                .CountAsync();

            var benefits = await this.ordersRepository
                .All()
                .Where(x => x.CustomerId == customerId)
                .SumAsync(x => x.Product.Price * x.Quantity);

            return new OrderCustomerStatsViewModel
            {
                Id = customerId,
                TotalOrders = totalOrders,
                DifferentProducts = differentProducts,
                Benefits = benefits,

            };
        }

        public IQueryable<T> GetOrders<T>(int customerId)
        {
            var query = this.ordersRepository
                .All()
                .Where(x => x.CustomerId == customerId)
                .To<T>()
                .AsQueryable();

            return query;

        }


    }
}
