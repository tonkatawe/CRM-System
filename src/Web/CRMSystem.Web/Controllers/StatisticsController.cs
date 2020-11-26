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

            var benefits = await this.statisticsService.GetTotalBenefitsAsync(organizationId);
            var bestCustomer = await this.statisticsService.GetBestCustomersAsync<CustomerViewModel>(organizationId);
            var customerWithMostOrders = await this.statisticsService.GetCustomerByOrdersAsync<CustomerViewModel>(organizationId);
            var mostOrdered = await this.statisticsService.GetMostOrderProductAsync<ProductViewModel>(organizationId);
            var mostBenefitProduct = await this.statisticsService.GetMostBenefitProductAsync<ProductViewModel>(organizationId);


            var viewModel = new StatisticsIndexViewModel
            {
               TotalBenefits = benefits,
               BestCustomer = bestCustomer,
               CustomerWithMostOrders = customerWithMostOrders,
               MostOrderedProduct = mostOrdered,
               MostBenefitProduct = mostBenefitProduct,
            };

            return View(viewModel);
        }
    }
}
