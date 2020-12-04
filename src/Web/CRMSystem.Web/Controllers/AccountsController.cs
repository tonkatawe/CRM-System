﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CRMSystem.Data.Common.Repositories;
using CRMSystem.Data.Models;
using CRMSystem.Services.Data.Contracts;
using CRMSystem.Web.ViewModels.Accounts;
using CRMSystem.Web.ViewModels.Customers;
using Microsoft.AspNetCore.Identity;

namespace CRMSystem.Web.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IOrganizationsService organizationsService;
        private readonly ICustomersService customersService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<ApplicationUser> applicationUserRepository;


        public AccountsController(IOrganizationsService organizationsService, ICustomersService customersService, UserManager<ApplicationUser> userManager, IDeletableEntityRepository<ApplicationUser> applicationUserRepository)
        {
            this.organizationsService = organizationsService;
            this.customersService = customersService;
            this.userManager = userManager;
            this.applicationUserRepository = applicationUserRepository;
        }

        public async Task<IActionResult> Index(int organizationId, int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user.OrganizationId != organizationId)
            {
                return NotFound();
            }

            var test = this.customersService.GetById<GetDetailsViewModel>(id);
            var viewModel = this.customersService.GetById<MakeAccountViewModel>(id);
            viewModel.Email = test.Emails.FirstOrDefault()?.Email;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(MakeAccountViewModel input)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = input.Email,
                    Email = input.Email,
                    PhoneNumber = input.Phone,
                    OrganizationId = input.OrganizationId,

                };
                var result = await this.userManager.CreateAsync(user, input.Password);

                var test = await this.userManager.AddToRoleAsync(user, "Customer");

                if (!test.Succeeded)
                {
                    return this.Content($"{test.Errors}");
                }

                if (result.Succeeded)
                {

                    return this.RedirectToAction("Index", "Customers");

                }

            }

            return this.RedirectToAction("Index", "Customers");
        }
    }
}
