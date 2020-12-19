namespace CRMSystem.Web.Controllers
{
    using CRMSystem.Common;
    using CRMSystem.Data.Models;
    using CRMSystem.Services.Data.Contracts;
    using CRMSystem.Services.Messaging;
    using CRMSystem.Web.Infrastructure;
    using CRMSystem.Web.ViewModels.Accounts;
    using CRMSystem.Web.ViewModels.Customers;
    using CRMSystem.Web.ViewModels.TemporaryCustomers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class TemporaryCustomersController : Controller
    {
        private const string SentData = "SentData";


        private readonly ITemporaryCustomersService temporaryCustomersService;
        private readonly ICustomersService customersService;
        private readonly IUsersService usersService;
        private readonly IAccountsService accountsService;
        private readonly IEmailSender emailSender;
        private readonly UserManager<ApplicationUser> userManager;


        public TemporaryCustomersController(
            ITemporaryCustomersService temporaryCustomersService,
            ICustomersService customersService,
            IUsersService usersService,
            IAccountsService accountsService,
            IEmailSender emailSender,
            UserManager<ApplicationUser> userManager)
        {
            this.temporaryCustomersService = temporaryCustomersService;
            this.customersService = customersService;
            this.usersService = usersService;
            this.accountsService = accountsService;
            this.emailSender = emailSender;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Create(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user != null)
            {
                return NotFound();
            }

            this.TempData["OrganizationId"] = id;

            return this.View("_CreateCustomer");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerAddInputModel input, string organizationId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user != null)
            {
                return NotFound();
            }

            var userId = this.usersService.GetUserIdByOrganizationId(organizationId);
            await this.customersService.CreateAsync(input, userId, organizationId, true);

            this.TempData["Successful"] = "Your data sent successful";
            this.TempData[SentData] = true;

            return this.RedirectToAction("Successful");
        }

        public IActionResult Successful()
        {
            if (this.TempData[SentData] == null)
            {
                return this.NotFound();
            }

            return this.View();
        }

        [Authorize(Roles = ("Owner, Administrator"))]
        public async Task<IActionResult> Index(int? pageNumber)
        {

            var user = await this.userManager.GetUserAsync(this.User);

            if (user.OrganizationId == null)
            {
                return this.RedirectToAction("Create", "Organizations");
            }
            var allUserCustomers = this.customersService.GetAll<CustomerViewModel>(user.Id, true);


            var customers = from c in allUserCustomers
                            select c;


            int pageSize = 3;

            return View(await PaginatedList<CustomerViewModel>.CreateAsync(customers, pageNumber ?? 1, pageSize));
        }

        [HttpPost]
        [Authorize(Roles = ("Owner, Administrator"))]
        public async Task<IActionResult> Approve(int id, string organizationId)
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

            await this.temporaryCustomersService.ApproveAsync(id);

            var result = new KeyValuePair<string, string>();
            try
            {
                result = await this.accountsService.CreateAsync(customer, owner);
            }
            catch (Exception e)
            {
                TempData["Error"] = "Account doesn't create - " + e.Message;

                return this.RedirectToAction("Index", "TemporaryCustomers");

            }

            var confirmationLink = Url.Action("ConfirmEmail", "Accounts",
                new { result.Key, email = customer.Email },
                Request.Scheme);


            var msg = String.Format(OutputMessages.EmailConformation, customer.FullName, customer.OrganizationName,
                customer.UserName, result.Value, confirmationLink);

            await this.emailSender.SendEmailAsync(owner.Email, owner.UserName,
                customer.Email, "Email confirm link", msg);

            TempData["Successful"] = "User approved successful and created an account.";
            return this.RedirectToAction("Index", "Customers");

        }

        [HttpPost]
        [Authorize(Roles = ("Owner, Administrator"))]
        public async Task<IActionResult> Reject(int id, string organizationId)
        {
            var owner = await this.userManager.GetUserAsync(this.User);

            if (owner.OrganizationId != organizationId)
            {
                return NotFound();
            }

            var customer = this.customersService.GetById<RejectCustomerViewModel>(id);

            await this.customersService.DeleteAsync(id);

            var msg = String.Format(OutputMessages.RejectCustomer, customer.FullName, customer.OrganizationName);

          
            await this.emailSender.SendEmailAsync(owner.Email, owner.UserName,
                customer.Email, "Email confirm link", msg);


            return this.RedirectToAction("Index");
        }
    }
}
