using System;
using AutoMapper.Internal;
using CRMSystem.Web.Infrastructure;
using CRMSystem.Web.ViewModels.Customers;
using CRMSystem.Web.ViewModels.Emails;

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
        private readonly UserManager<ApplicationUser> userManager;

        public CustomersController(
            ICustomersService customersService,
            IEmailsService emailsService,
            IPhonesServices phonesService,

            UserManager<ApplicationUser> userManager)
        {
            this.customersService = customersService;
            this.emailsService = emailsService;
            this.phonesService = phonesService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (!user.HasOrganization)
            {
                return this.RedirectToAction("Create", "Organizations");
            }
            var allUserCustomers = this.customersService.GetAllUserCustomers<CustomerViewModel>(user.Id);
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



        public async Task<IActionResult> AddCustomer()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (!user.HasOrganization)
            {
                return this.RedirectToAction("Create", "Organizations");
            }

            //Todo: make service for check existed phone and mail only from current user and delete method this check by other controllers
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer(CustomerAddInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
      
            for (int i = 0; i < input.Emails.Count; i++)
            {

            }

            foreach (var email in input.Emails)
            {
                if (!this.emailsService.IsAvailableEmail(email.Email))
                {

                    ModelState.AddModelError("", $"This {email.Email} is already use");
                  
                }
            }

            if (!user.HasOrganization)
            {
                return this.RedirectToAction("Create", "Organizations");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            return Json(input);
            await this.customersService.CreateCustomerAsync(input, user.Id);

            return this.RedirectToAction("Index");
        }


        public IActionResult Edit(int id)
        {
            var viewModel = this.customersService.GetCustomerById<EditCustomerInputModel>(id);
            if (viewModel == null)
            {
                return NotFound();
            }

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCustomerInputModel input)
        {
            //todo Check for mail, phone social network, make limit for all types, try to get only changed properties

            if (ModelState.IsValid)
            {
                foreach (var email in input.EmailAddresses)
                {
                    bool test = !this.emailsService.IsAvailableEmail(email.Email);
                    if (test)
                    {

                        ViewData.ModelState.AddModelError($"{email.Email}", "e zaet");

                    }
                }
            }


            await this.customersService.UpdateCustomerAsync(input);

            return this.RedirectToAction("Details", new { id = input.Id });
        }


        public async Task<IActionResult> Details(int id)
        {
            //todo make method async

            var viewModel = this.customersService.GetCustomerById<GetDetailsViewModel>(id);

            //todo have to repair because it doesn't work :)
            //if (viewModel.SharedEditCustomerViewModel.Id != id)
            //{
            //viewModel.SharedEditCustomerViewModel = this.customersService.GetCustomerById<EditCustomerInputModel>(viewModel.Id);
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
            await this.customersService.DeleteCustomerAsync(id);

            //todo for this user check..

            return this.RedirectToAction("Index");
        }


    }
}
