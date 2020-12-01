using System.Security.Claims;
using System.Threading.Tasks;
using CRMSystem.Data.Models;
using CRMSystem.Services.Data.Contracts;
using Microsoft.AspNetCore.Identity;
using IndexViewModel = CRMSystem.Web.ViewModels.Home.IndexViewModel;

namespace CRMSystem.Web.Controllers
{
    using System.Diagnostics;

    using CRMSystem.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly ICustomersService customersService;
        private readonly IOrganizationsService organizationsService;


        public HomeController(ICustomersService customersService, IOrganizationsService organizationsService)
        {
            this.customersService = customersService;
            this.organizationsService = organizationsService;
        }

        public async Task<IActionResult> Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var organizationId = this.organizationsService.GetById(userId);
                return this.RedirectToAction("Index", "Organizations", new {id = organizationId});
            }

            var organizationsCount = await this.organizationsService.GetCountAsync();

            var customersCount = await this.customersService.GetCountAsync();
     

            var viewModel = new IndexViewModel
            {
                OrganizationsCount = organizationsCount,
                CustomersCount = customersCount,
             
            };


            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
