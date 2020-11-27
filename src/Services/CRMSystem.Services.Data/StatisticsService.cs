

using System;
using System.Linq;
using System.Threading.Tasks;
using CRMSystem.Data.Common.Repositories;
using CRMSystem.Data.Models;
using CRMSystem.Services.Data.Contracts;
using CRMSystem.Services.Mapping;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.Services.Data
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IDeletableEntityRepository<Order> ordersRepository;
        private readonly IDeletableEntityRepository<Customer> customerRepository;
        private readonly IDeletableEntityRepository<Product> productsRepository;

        public StatisticsService(
            IDeletableEntityRepository<Order> ordersRepository,
            IDeletableEntityRepository<Customer> customerRepository,
            IDeletableEntityRepository<Product> productsRepository)
        {
            this.ordersRepository = ordersRepository;
            this.customerRepository = customerRepository;
            this.productsRepository = productsRepository;
        }



        public async Task<decimal> GetTotalBenefitsAsync(int id, DateTime startDate, DateTime endDate)
        {
        return await this.ordersRepository
                .All()
                .Where(x => x.OrganizationId == id && x.CreatedOn >=startDate && x.CreatedOn <= endDate)
                .SumAsync(x => x.Product.Price * x.Quantity);
        }

        public async Task<DateTime> GetStartDateAsync(int id)
        {
            var startDate = await this.ordersRepository
                .All()
                .Where(x => x.OrganizationId == id)
                .OrderBy(x => x.CreatedOn)
                .Select(x=>x.CreatedOn)
                .FirstOrDefaultAsync();

            return startDate;
        }

        public async Task<T> GetBestCustomersAsync<T>(int id)
        {
            return await this.customerRepository
                .All()
                 .Where(x => x.OrganizationId == id)
                 .OrderByDescending(x => x.Orders.Sum(y => y.Product.Price * y.Quantity))
                 .To<T>()
                 .FirstOrDefaultAsync();
        }

        public async Task<T> GetCustomerByOrdersAsync<T>(int id)
        {
            return await this.customerRepository
                .All()
                .Where(x => x.OrganizationId == id)
                .OrderByDescending(x => x.Orders.Count)
                .To<T>()
                .FirstOrDefaultAsync();
        }

        public async Task<T> GetMostOrderProductAsync<T>(int id)
        {
            return await this.productsRepository
                .All()
                .Where(x => x.OrganizationId == id)
                .OrderByDescending(x => x.Orders.Count())
                .To<T>()
                .FirstOrDefaultAsync();
        }

        public async Task<T> GetMostBenefitProductAsync<T>(int id)
        {
            return await this.productsRepository
                .All()
                .Where(x => x.OrganizationId == id)
                .OrderByDescending(x => x.Orders.Max(x => x.Product.Price * x.Quantity))
                .To<T>()
                .FirstOrDefaultAsync();
        }

        public bool ValidationDate(DateTime startDate, DateTime endDate, DateTime userStartDate)
        {
            var cuurentDate = DateTime.UtcNow;
            if (startDate > endDate)
            {
                throw new Exception($"Star date ({startDate:d}) cannot be greater than end date ({endDate:d}).");
            }

            if (endDate > cuurentDate)
            {
                throw new Exception($"End date cannot be greater than {cuurentDate:d}");
            }

            if (startDate < userStartDate)
            {
                throw new Exception($"Start date cannot be before {userStartDate:d}");
            }
            return  true;
        }
    }
}
