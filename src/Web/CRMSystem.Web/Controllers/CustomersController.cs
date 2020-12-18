using System;
using System.Collections.Generic;
using CRMSystem.Web.Infrastructure;
using CRMSystem.Web.ViewModels.Customers;
using CRMSystem.Web.ViewModels.Emails;
using CRMSystem.Web.ViewModels.Phones;

namespace CRMSystem.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using CRMSystem.Data.Models;
    using CRMSystem.Services.Data.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    [Authorize(Roles = "Administrator, Owner")]
    public class CustomersController : Controller
    {
        private readonly ICustomersService customersService;
        private readonly IEmailsService emailsService;
        private readonly IPhonesServices phonesService;
        private readonly IValidationService validationService;
        private readonly IOrdersService salesService;
        private readonly UserManager<ApplicationUser> userManager;

        public CustomersController(
            ICustomersService customersService,
            IEmailsService emailsService,
            IPhonesServices phonesService,
            IValidationService validationService,
            IOrdersService salesService,
            UserManager<ApplicationUser> userManager)
        {
            this.customersService = customersService;
            this.emailsService = emailsService;
            this.phonesService = phonesService;
            this.validationService = validationService;
            this.salesService = salesService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            //todo must refactoring this!

            if (user == null)
            {
                return RedirectToPage("~/Account/Login");
            }

            if (user.OrganizationId == null)
            {
                return this.RedirectToAction("Create", "Organizations");
            }

            var allUserCustomers = this.customersService.GetAll<CustomerViewModel>(user.Id, false);
            ViewData["CurrentSort"] = sortOrder;
            ViewData["SortByName"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var customers = from c in allUserCustomers
                            select c;
            if (!String.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(s => s.LastName.Contains(searchString)
                                               || s.FirstName.Contains(searchString));
            }
            switch (sortOrder)
            {
            
                case "name_desc":
                    customers = customers
                        .OrderByDescending(c => c.FirstName)
                        .ThenByDescending(c => c.LastName)
                        .ThenByDescending(c => c.MiddleName);
                    break;

                case "Date":
                    customers = customers.OrderBy(c => c.OrdersCount);
                    break;
                case "date_desc":
                    customers = customers.OrderByDescending(c => c.OrdersCount);
                    break;
                default:
                    customers = customers.OrderBy(c => c.LastName);
                    break;
            }

            int pageSize = 3;

            return View(await PaginatedList<CustomerViewModel>.CreateAsync(customers, pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> Create()
        {

            var user = await this.userManager.GetUserAsync(this.User);
            if (user.OrganizationId == null)
            {
                return this.RedirectToAction("Create", "Organizations");
            }

            return this.View("_CreateCustomer");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerAddInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user.OrganizationId == null)
            {
                return this.RedirectToAction("Create", "Organizations");
            }

            foreach (var email in input.Emails)
            {
                if (!this.validationService.IsAvailableEmail(user.Id, email.Email))
                {
                    ModelState.AddModelError("", $"This {email.Email} is already taken by other customer in your list");
                }
            }

            foreach (var phone in input.Phones)
            {
                if (!this.validationService.IsAvailablePhone(user.Id, phone.Phone))
                {
                    ModelState.AddModelError("", $"This {phone.Phone} is already taken by other customer in your list");
                }
            }

            if (!this.ModelState.IsValid)
            {
                return this.View("_CreateCustomer", input);
            }

            await this.customersService.CreateAsync(input, user.Id, user.OrganizationId, false);

            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user.OrganizationId == null)
            {
                return this.RedirectToAction("Create", "Organizations");
            }

            var viewModel = this.customersService.GetById<EditCustomerInputModel>(id);
            if (viewModel == null)
            {
                return NotFound();
            }

            viewModel.Phones = this.phonesService.GetAll<PhoneCreateInputModel>(id).ToList();
            viewModel.Emails = this.emailsService.GetAll<EmailCreateInputModel>(id).ToList();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCustomerInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user.OrganizationId == null)
            {
                return this.RedirectToAction("Create", "Organizations");
            }


            foreach (var email in input.Emails)
            {

                if (!this.validationService.IsAvailableEmail(user.Id, email.Email, email.Id, email.CustomerId))
                {
                    ModelState.AddModelError("", $"This {email.Email} is already taken by other customer in your list");
                }
            }

            foreach (var phone in input.Phones)
            {
                if (!this.validationService.IsAvailablePhone(user.Id, phone.Phone, phone.Id, phone.CustomerId))
                {
                    ModelState.AddModelError("", $"This {phone.Phone} is already taken by other customer in your list");
                }
            }


            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }


            await this.customersService.UpdateAsync(input);

            return this.RedirectToAction("Details", new { id = input.Id });
        }

        public async Task<IActionResult> Details(int id)
        {
            //todo check for security

            var viewModel = this.customersService.GetById<GetDetailsViewModel>(id);
            viewModel.CustomerStats = await this.salesService.GetStatsAsync(id);


            return this.View(viewModel);

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var customerUserId = await this.customersService.CustomerUserIdAsync(id);

            if (user.Id != customerUserId)
            {
                return NotFound();
            }

            await this.customersService.DeleteAsync(id);

            return this.RedirectToAction("Index");
        }
    }
}
