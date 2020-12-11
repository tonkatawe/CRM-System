using Microsoft.AspNetCore.Mvc;
using System;
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

            await this.accountsService.CreateAsync(customer);
            //todo correct input
            //if (!ModelState.IsValid)
            //{
            //    var user = new ApplicationUser
            //    {
            //        UserName = input.Username,
            //        Email = input.Email,
            //        PhoneNumber = input.Phone,
            //        OrganizationId = input.OrganizationId,
            //        Parent = owner,

            //    };

            //    var userPassword = Guid.NewGuid().ToString().Substring(0, 8);


            //    var result = await this.userManager.CreateAsync(user, userPassword);

            //    var organizationName = this.organizationsService.GetName(owner.Id);

            //    if (result.Succeeded)
            //    {
            //        await this.userManager.AddToRoleAsync(user, "Customer");

            //        var token = await this.userManager.GenerateEmailConfirmationTokenAsync(user);

            //        var confirmationLink = Url.Action("ConfirmEmail", "Accounts",
            //            new { token, email = user.Email },
            //               Request.Scheme);

            //        var msg = String.Format(OutputMessages.EmailConformation, input.FullName, organizationName,
            //            user.UserName, userPassword, confirmationLink);

            //        await this.emailSender.SendEmailAsync(owner.Email, owner.UserName,
            //            user.Email, "Email confirm link", msg);


            //        return this.RedirectToAction("Index", "Customers");


            //    }




            //}

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
