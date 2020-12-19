namespace CRMSystem.Web.Controllers
{
    using CRMSystem.Common;
    using CRMSystem.Data.Models;
    using CRMSystem.Services.Data.Contracts;
    using CRMSystem.Services.Messaging;
    using CRMSystem.Web.ViewModels.Accounts;
    using CRMSystem.Web.ViewModels.Customers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Authorize(Roles = "Administrator, Owner")]
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
                return NotFound();
            }


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
                    new { token = result.Key, email = customer.Email },
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
        public async Task<IActionResult> ConfirmEmail(string key, string email)
        {
            var user = await this.userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return View("Error");
            }

            var result = await this.userManager.ConfirmEmailAsync(user, key);

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

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string userNameOrEmail, string password)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return NotFound();
            }

            this.TempData["Error"] = "Invalid login attempt.";
            var user = await this.userManager.Users.FirstOrDefaultAsync(u =>
                 u.UserName == userNameOrEmail || u.Email == userNameOrEmail);

            if (user != null)
            {
                var result = await this.signInManager.PasswordSignInAsync(user.UserName, password, true, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                return this.RedirectToAction("Index", "Home", TempData["Error"]);

            }

            return this.RedirectToAction("Index", "Home", TempData["Error"]);
        }

    }
}
