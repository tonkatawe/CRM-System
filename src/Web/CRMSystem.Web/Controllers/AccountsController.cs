using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRMSystem.Common;
using CRMSystem.Data.Models;
using CRMSystem.Services.Data.Contracts;
using CRMSystem.Services.Messaging;
using CRMSystem.Web.ViewModels.Accounts;
using CRMSystem.Web.ViewModels.Customers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CRMSystem.Web.Controllers
{
    [Authorize(Roles = "Admin, Owner")]
    public class AccountsController : Controller
    {
        private readonly ICustomersService customersService;
        private readonly IAccountsService accountsService;

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IEmailSender emailSender;


        public AccountsController(
            ICustomersService customersService,
            IAccountsService accountsService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender)
        {
            this.customersService = customersService;
            this.accountsService = accountsService;

            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
        }

        public async Task<IActionResult> Index(string organizationId, int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user.OrganizationId != organizationId)
            {
                return NotFound();
            }

            var test = this.customersService.GetById<GetDetailsViewModel>(id);
            var viewModel = this.customersService.GetById<CreateAccountInputModel>(id);
            viewModel.Email = test.Emails.FirstOrDefault()?.Email;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int id, string organizationId)
        {
            var owner = await this.userManager.GetUserAsync(this.User);
            if (owner.OrganizationId != organizationId)
            {
                return NotFound();
            }

            var customer = this.customersService.GetById<CreateAccountInputModel>(id);

            if (owner.Id != customer.UserId)
            {
                return NotFound();
            }

            if (customer.HasAccount)
            {
                //todo : make error page
                return NotFound();
            }

            //todo routing data

            var result = new KeyValuePair<string, string>();
            try
            {
                result = await this.accountsService.CreateAsync(customer, owner);
            }
            catch (Exception e)
            {
                TempData["Error"] = "Account doesn't create - " + e.Message;

                return this.RedirectToAction("Index", "Customers");

            }
            
            var confirmationLink = Url.Action("ConfirmEmail", "Accounts",
                    new { result.Key, email = customer.Email },
                       Request.Scheme);

            var msg = String.Format(OutputMessages.EmailConformation, customer.FullName, customer.OrganizationName,
                customer.UserName, result.Value, confirmationLink);

            await this.emailSender.SendEmailAsync(owner.Email, owner.UserName,
                customer.Email, "Email confirm link", msg);

            TempData["Successful"] = "Account created successful.";
            return this.RedirectToAction("Index", "Customers");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            //todo make tempdata to confirm email
            var user = await this.userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return View("Error");
            }


            var result = await this.userManager.ConfirmEmailAsync(user, token);


            if (result.Succeeded)
            {
                this.TempData["ConfirmationEmail"] =
                    "Thank you for confirming your email. You've already sing in. You can list our products";
                await this.signInManager.SignInAsync(user, isPersistent: false);
                return this.RedirectToAction("Index", "Products");
            }
            else
            {
                return View("Error");
            }


        }

    }
}
