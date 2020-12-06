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
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace CRMSystem.Web.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IAccountsService accountsService;
        private readonly ICustomersService customersService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailSender emailSender;


        public AccountsController(IAccountsService accountsService, ICustomersService customersService, UserManager<ApplicationUser> userManager,  IEmailSender emailSender)
        {
            this.accountsService = accountsService;
            this.customersService = customersService;
            this.userManager = userManager;
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
            var owner = await this.userManager.GetUserAsync(this.User);
            //todo correct input
            if (!ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = input.Username,
                    Email = input.Email,
                    PhoneNumber = input.Phone,
                    OrganizationId = input.OrganizationId,

                };

                var testPassword = Guid.NewGuid().ToString().Substring(0, 8);
                

                var result = await this.userManager.CreateAsync(user, testPassword);

                var organizationName = "IT Talants alpha";

                if (result.Succeeded)
                {
                    await this.userManager.AddToRoleAsync(user, "Customer");

                    var token = await this.userManager.GenerateEmailConfirmationTokenAsync(user);

                    var confirmationLink = Url.Action("ConfirmEmail", "Accounts",
                        new { token, email = user.Email },
                           Request.Scheme);

                    var msg = String.Format(OutputMessages.EmailConformation, input.FullName, organizationName,
                        user.UserName, testPassword, confirmationLink );

                    await this.emailSender.SendEmailAsync(owner.Email, owner.UserName,
                        user.Email, "Email confirm link", msg);
                   

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
            if (result.Succeeded)
            {
                return this.View();
            }
            else
            {
                return View("Error");
            }

            //return View(result.Succeeded ? nameof(ConfirmEmail) : "Error");
        }
    
    }
}
