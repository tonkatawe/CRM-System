using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CRMSystem.Data.Common.Repositories;
using CRMSystem.Data.Models;
using CRMSystem.Services.Data.Contracts;
using CRMSystem.Web.ViewModels.Customers;
using CRMSystem.Web.ViewModels.Products;
using CRMSystem.Web.ViewModels.Statistics;

namespace CRMSystem.Web.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly IStatisticsService statisticsService;
        private readonly IOrganizationsService organizationsService;

        public StatisticsController(
            IStatisticsService statisticsService,
            IOrganizationsService organizationsService)
        {
            this.statisticsService = statisticsService;
            this.organizationsService = organizationsService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var organizationId = this.organizationsService.GetById(userId);
            var startDate = await this.statisticsService.GetStartDate(organizationId);
            var endDate = DateTime.UtcNow;


            var benefits = await this.statisticsService.GetTotalBenefitsAsync(organizationId,startDate, endDate);
            var bestCustomer = await this.statisticsService.GetBestCustomersAsync<CustomerViewModel>(organizationId);
            var customerWithMostOrders = await this.statisticsService.GetCustomerByOrdersAsync<CustomerViewModel>(organizationId);
            var mostOrdered = await this.statisticsService.GetMostOrderProductAsync<ProductViewModel>(organizationId);
            var mostBenefitProduct = await this.statisticsService.GetMostBenefitProductAsync<ProductViewModel>(organizationId);
            var organizationName =  this.organizationsService.GetName(userId);

            var viewModel = new StatisticsIndexViewModel
            {
               TotalBenefits = benefits,
               BestCustomer = bestCustomer,
               CustomerWithMostOrders = customerWithMostOrders,
               MostOrderedProduct = mostOrdered,
               MostBenefitProduct = mostBenefitProduct,
               OrganizationName = organizationName,
            };

            return View(viewModel);
        }

        public async Task<IActionResult> WeeklyStatistics(DateTime startDate, DateTime endDate)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var organizationId = this.organizationsService.GetById(userId);
            var todaysDate = DateTime.UtcNow;
            var sevenDaysBefore = todaysDate.AddDays(-7);
            //var totalBenefits =  await this.statisticsService.GetTotalBenefitsAsync(organizationId, sevenDaysBefore);
            //var viewmodel = new WeeklyStatsViewModel
            //{
            //    TotalBenefits =  totalBenefits,
            //};
            return this.View();
        }
    }
}
