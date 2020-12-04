using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Claims;
using System.Threading.Tasks;
using CRMSystem.Common;
using CRMSystem.Data.Common.Repositories;
using CRMSystem.Data.Models;
using CRMSystem.Services.Data.Contracts;
using CRMSystem.Services.Messaging;
using CRMSystem.Web.ViewModels.Accounts;
using CRMSystem.Web.ViewModels.Customers;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace CRMSystem.Web.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IOrganizationsService organizationsService;
        private readonly ICustomersService customersService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<ApplicationUser> applicationUserRepository;
        private readonly IEmailSender emailSender;


        public AccountsController(IOrganizationsService organizationsService, ICustomersService customersService, UserManager<ApplicationUser> userManager, IDeletableEntityRepository<ApplicationUser> applicationUserRepository, IEmailSender emailSender)
        {
            this.organizationsService = organizationsService;
            this.customersService = customersService;
            this.userManager = userManager;
            this.applicationUserRepository = applicationUserRepository;
            this.emailSender = emailSender;
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
            //todo correct input
            if (!ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = "test",
                    Email = "test@tessssst.teeest",
                    PhoneNumber = "4322342343",
                    OrganizationId = input.OrganizationId,

                };
                var result = await this.userManager.CreateAsync(user, input.Password);

                if (result.Succeeded)
                {
                    var token = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Accounts",
                        new { token, email = GlobalConstants.SystemEmail },
                           Request.Scheme);
                    await this.emailSender.SendEmailAsync(GlobalConstants.SystemEmail, "Anton",
                        GlobalConstants.SystemEmail, "Email confirm link", $"Confirm link {confirmationLink}");
                    var test = await this.userManager.AddToRoleAsync(user, "Customer");

                    return this.RedirectToAction("Index", "Customers");


                }


            

            }

            return this.RedirectToAction("Index", "Customers");
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await this.userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return View("Error");
            }

            var result = await this.userManager.ConfirmEmailAsync(user, token);
            return View(result.Succeeded ? nameof(ConfirmEmail) : "Error");
        }
    }
}
