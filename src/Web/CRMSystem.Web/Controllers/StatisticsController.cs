using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using CRMSystem.Data.Models;
using CRMSystem.Services.Data.Contracts;
using CRMSystem.Web.ViewModels.Customers;
using CRMSystem.Web.ViewModels.Products;
using CRMSystem.Web.ViewModels.Statistics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CRMSystem.Web.Controllers
{
    [Authorize(Roles = "Owner, Administrator")]
    public class StatisticsController : Controller
    {

        private readonly IStatisticsService statisticsService;
        private readonly IOrganizationsService organizationsService;
        private readonly UserManager<ApplicationUser> userManager;


        public StatisticsController(
            IStatisticsService statisticsService,
            IOrganizationsService organizationsService,
            UserManager<ApplicationUser> userManager)
        {
            this.statisticsService = statisticsService;
            this.organizationsService = organizationsService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(IndexViewModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            
                if (user == null)
                {
                    return NotFound();
                }
            
           
            return this.RedirectToAction("RangeStatistic", new { startDate = input.StartDate, endDate = input.EndDate });
        }

        public async Task<IActionResult> RangeStatistic(DateTime startDate, DateTime endDate)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var organizationId = this.organizationsService.GetId(userId);
       
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

        public async Task<JsonResult> ValidateDate(DateTime startDate, DateTime endDate)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userStartDate = await this.statisticsService.GetStartDateAsync(user.OrganizationId);
            var currentDate = DateTime.UtcNow;
            
            if (startDate > endDate)
            {
                return Json("Start date cannot be after end date");
            }

            if (startDate < userStartDate)
            {
                return Json(
                    $"You cannot generate this statistic because you use this application since {userStartDate:d}");
            }
            if (endDate > currentDate)
            {
                throw new Exception($"End date cannot be greater than {currentDate:d}");
            }

            return Json(true);
        }
    }
}
