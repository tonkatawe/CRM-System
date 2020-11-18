using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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
    using CRMSystem.Web.ViewModels.Contacts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly ICustomersService customersService;
        private readonly IEmailsService emailsService;
        private readonly IPhonesServices phonesService;
        private readonly IValidationService validationService;
        private readonly UserManager<ApplicationUser> userManager;

        public CustomersController(
            ICustomersService customersService,
            IEmailsService emailsService,
            IPhonesServices phonesService,
            IValidationService validationService,

            UserManager<ApplicationUser> userManager)
        {
            this.customersService = customersService;
            this.emailsService = emailsService;
            this.phonesService = phonesService;
            this.validationService = validationService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (!user.HasOrganization)
            {
                return this.RedirectToAction("Create", "Organizations");
            }
            var allUserCustomers = this.customersService.GetAll<CustomerViewModel>(user.Id);
            ViewData["CurrentSort"] = sortOrder;
            ViewData["SortByName"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["SortByOrganization"] = String.IsNullOrEmpty(sortOrder) ? "organ_desc" : "";
            ViewData["SortByIndustry"] = String.IsNullOrEmpty(sortOrder) ? "industry_desc" : "";
            ViewData["SortByDate"] = sortOrder == "Date" ? "date_desc" : "Date";

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
                case "organ_desc":
                    //todo: make it work correctly
                    //  contacts = contacts.OrderBy(x=>x.Organization.Name);
                    break;
                case "name_desc":
                    customers = customers
                        .OrderByDescending(c => c.FirstName)
                        .ThenByDescending(c => c.LastName)
                        .ThenByDescending(c => c.MiddleName);
                    break;
                case "industry_desc":
                    customers = customers
                        .OrderByDescending(c => c.Industry);
                    break;
                case "Date":
                    customers = customers.OrderBy(c => c.CreatedOn);
                    break;
                case "date_desc":
                    customers = customers.OrderByDescending(c => c.CreatedOn);
                    break;
                default:
                    customers = customers.OrderBy(c => c.LastName);
                    break;
            }

            int pageSize = 10;

            return View(await PaginatedList<CustomerViewModel>.CreateAsync(customers, pageNumber ?? 1, pageSize));
        }



        public async Task<IActionResult> Create()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (!user.HasOrganization)
            {
                return this.RedirectToAction("Create", "Organizations");
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerAddInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (!user.HasOrganization)
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
                return this.View(input);
            }


            await this.customersService.CreateSync(input, user.Id);

            return this.RedirectToAction("Index");
        }


        public async Task<IActionResult> Edit(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (!user.HasOrganization)
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

            if (!user.HasOrganization)
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
            //todo make method async

            var viewModel = this.customersService.GetById<GetDetailsViewModel>(id);

            //todo have to repair because it doesn't work :)
            //if (viewModel.SharedEditCustomerViewModel.Id != id)
            //{
            //viewModel.SharedEditCustomerViewModel = this.customersService.GetById<EditCustomerInputModel>(viewModel.Id);
            //viewModel.SharedEditCustomerViewModel.Id = viewModel.Id;

            //}

            if (viewModel == null)
            {
                return NotFound();
            }

            return this.View(viewModel);
            //return this.View("DetailsEdit", viewModel);
        }


        public async Task<IActionResult> Delete(int id)
        {
            await this.customersService.DeleteAsync(id);

            //todo for this user check..

            return this.RedirectToAction("Index");
        }


    }
}
