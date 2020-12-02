﻿using System.Security.Claims;
using CRMSystem.Web.ViewModels.Customers;

namespace CRMSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using CRMSystem.Data.Models;
    using CRMSystem.Services.Data.Contracts;
    using CRMSystem.Web.ViewModels.Contacts;
    using CRMSystem.Web.ViewModels.Organizations;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class OrganizationsController : Controller
    {
        private readonly IOrganizationsService organizationsService;
        private readonly UserManager<ApplicationUser> userManager;

        public OrganizationsController(
            IOrganizationsService organizationsService,
            UserManager<ApplicationUser> userManager)
        {
            this.organizationsService = organizationsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var viewModel = this.organizationsService.GetById<OrganizationViewModel>(userId);
       //     viewModel.CustomerCount = user.Organization.Customers.Count;
         //   viewModel.Id = user.Organization.Id;
           // viewModel.ProductCount = user.Organization.Products.Count;
            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user.HasOrganization)
            {

                //todo make to redirect organization info
              return  RedirectToAction("Index", "Customers");
            }
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(OrganizationCreateInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.organizationsService.CreateOrganizationAsync(input, user.Id);
         
            //todo make to redirect organization info
            return RedirectToAction("Index", "Customers");
        }




    }
}
