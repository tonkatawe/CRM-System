﻿using System.Security.Claims;
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
        private readonly UserManager<ApplicationUser> userManager;


        public HomeController(ICustomersService customersService, IOrganizationsService organizationsService, UserManager<ApplicationUser> userManager)
        {
            this.customersService = customersService;
            this.organizationsService = organizationsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
         
            var organizationsCount = await this.organizationsService.GetCountAsync();
            var customersCount = await this.customersService.GetCountAsync();
            
            var viewModel = new IndexViewModel
            {
                OrganizationsCount = organizationsCount,
                CustomersCount = customersCount,

            };

            var user = await this.userManager.GetUserAsync(this.User);

            if (user == null)
            {
                return this.View(viewModel);
            }

            if (user.HasOrganization)
            {
                return this.RedirectToAction("Index", "Organizations");
            }

         




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
