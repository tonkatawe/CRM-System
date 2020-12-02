using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CRMSystem.Services.Data.Contracts;
using CRMSystem.Web.ViewModels.Customers;
using CRMSystem.Web.ViewModels.Products;
using CRMSystem.Web.ViewModels.Statistics;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Web.ViewComponent
{
    [ViewComponent(Name = "GeneralStatistic")]
    public class GeneralStatisticViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly IStatisticsService statisticsService;
        private readonly IOrganizationsService organizationsService;

        public GeneralStatisticViewComponent(IStatisticsService statisticsService, IOrganizationsService organizationsService)
        {
            this.statisticsService = statisticsService;
            this.organizationsService = organizationsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var organizationId = this.organizationsService.GetId(userId);

            var startDate = await this.statisticsService.GetStartDateAsync(organizationId);
            var endDate = DateTime.UtcNow;

            //todo refactor models and const :)

            var benefits = await this.statisticsService.GetTotalBenefitsAsync(organizationId, startDate, endDate);
            var bestCustomer = await this.statisticsService.GetBestCustomersAsync<CustomerViewModel>(organizationId);
            var customerWithMostOrders = await this.statisticsService.GetCustomerByOrdersAsync<CustomerViewModel>(organizationId);
            var mostOrdered = await this.statisticsService.GetMostOrderProductAsync<ProductViewModel>(organizationId);
            var mostBenefitProduct = await this.statisticsService.GetMostBenefitProductAsync<ProductViewModel>(organizationId);
            var organizationName = this.organizationsService.GetName(userId);

            var viewModel = new StatisticViewModel
            {
                TotalBenefits = benefits,
                BestCustomer = bestCustomer,
                CustomerWithMostOrders = customerWithMostOrders,
                MostOrderedProduct = mostOrdered,
                MostBenefitProduct = mostBenefitProduct,
                OrganizationName = organizationName,
                OrdersCount = await this.statisticsService.OrdersCount(organizationId),
            };

            return View(viewModel);

        }
    }
}
