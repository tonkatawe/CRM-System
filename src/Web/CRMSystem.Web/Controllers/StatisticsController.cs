﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using CRMSystem.Services.Data.Contracts;
using CRMSystem.Web.ViewModels.Customers;
using CRMSystem.Web.ViewModels.Products;
using CRMSystem.Web.ViewModels.Statistics;
using Microsoft.AspNetCore.Authorization;

namespace CRMSystem.Web.Controllers
{
    [Authorize]
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

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(IndexViewModel input)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var organizationId = this.organizationsService.GetById(userId);
            var userStartDate = await this.statisticsService.GetStartDateAsync(organizationId);

            try
            {
                this.statisticsService.ValidationDate(input.StartDate, input.EndDate, userStartDate);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("", ex.Message);
                return this.View(input);
            }


            return this.RedirectToAction("RangeStatistic", new { startDate = input.StartDate, endDate = input.EndDate });
        }

        public async Task<IActionResult> RangeStatistic(DateTime startDate, DateTime endDate)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var organizationId = this.organizationsService.GetById(userId);
            var userStartDate = await this.statisticsService.GetStartDateAsync(organizationId);

            if (!this.statisticsService.ValidationDate(startDate, endDate, userStartDate))
            {
                return NotFound();
            }

            var benefits = await this.statisticsService.GetTotalBenefitsAsync(organizationId, startDate, endDate);
            var bestCustomer = await this.statisticsService.GetBestCustomersAsync<CustomerViewModel>(organizationId);
            var customerWithMostOrders = await this.statisticsService.GetCustomerByOrdersAsync<CustomerViewModel>(organizationId);
            var mostOrdered = await this.statisticsService.GetMostOrderProductAsync<ProductViewModel>(organizationId);
            var mostBenefitProduct = await this.statisticsService.GetMostBenefitProductAsync<ProductViewModel>(organizationId);
            var organizationName = this.organizationsService.GetName(userId);

            var viewModel = new StatisticViewModel
            {
                StartDate = startDate,
                EndDate = endDate,
                TotalBenefits = benefits,
                BestCustomer = bestCustomer,
                CustomerWithMostOrders = customerWithMostOrders,
                MostOrderedProduct = mostOrdered,
                MostBenefitProduct = mostBenefitProduct,
                OrganizationName = organizationName,

            };

            return this.View(viewModel);
        }
    }
}
