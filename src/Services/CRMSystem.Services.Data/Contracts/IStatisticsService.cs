namespace CRMSystem.Services.Data.Contracts
{
    using System;
    using System.Threading.Tasks;
    public interface IStatisticsService
    {
        Task<decimal> GetTotalBenefitsAsync(string id, DateTime startDate, DateTime endDate);

        Task<DateTime> GetStartDateAsync(string id);

        Task<T> GetBestCustomersAsync<T>(string id);

        Task<T> GetCustomerByOrdersAsync<T>(string id);

        Task<T> GetMostOrderProductAsync<T>(string id);

        Task<T> GetMostBenefitProductAsync<T>(string id);

        Task<int> OrdersCount(string id);

    }
}
