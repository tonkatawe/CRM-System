﻿
using System.Threading.Tasks;

namespace CRMSystem.Services.Data.Contracts
{
    public interface IStatisticsService
    {
      Task<decimal> GetTotalBenefitsAsync(int id);

      Task<T> GetBestCustomersAsync<T>(int id);

      Task<T> GetCustomerByOrdersAsync<T>(int id);
      Task<T> GetMostOrderProductAsync<T>(int id);
      Task<T> GetMostBenefitProductAsync<T>(int id);

    }
}