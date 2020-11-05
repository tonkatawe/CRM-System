using CRMSystem.Web.ViewModels.Customers;

namespace CRMSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using CRMSystem.Data.Models;
    using CRMSystem.Services.Data.Contracts;
    using CRMSystem.Web.ViewModels.Contacts;
    using CRMSystem.Web.ViewModels.Organizations;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class OrganizationsController : Controller
    {
        private readonly IOrganizationsService organizationsService;
        private readonly ICustomersService contactsService;
        private readonly UserManager<ApplicationUser> userManager;

        public OrganizationsController(
            IOrganizationsService organizationsService,
            ICustomersService contactsService,
            UserManager<ApplicationUser> userManager)
        {
            this.organizationsService = organizationsService;
            this.contactsService = contactsService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        //public async Task<IActionResult> Create(AddContactToOrganizationViewModel input, int? contactId = null)
        //{
        //    var user = await this.userManager.GetUserAsync(this.User);
        //    if (!this.ModelState.IsValid)
        //    {
        //        return this.View(input);
        //    }

        //    var organizationId = await this.organizationsService.CreateOrganizationAsync(input.CreateOrganization, user.Id);

        //    if (contactId != null)
        //    {
        //        //await this.contactsService.AddToOrganizationAsync(contactId, organizationId);
        //    }

        //    return this.RedirectToAction("Index", "Contacts");
        //}

        [Authorize]
        public async Task<IActionResult> ConnectToOrganization(int contactId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var contact = this.contactsService.GetCustomerById<CustomerViewModel>(contactId);

            if (contact == null)
            {
                return this.NotFound();
            }

            var userOrganizations = this.organizationsService.GetAll<OrganizationDropDownViewModel>(user.Id);
            var viewModel = new AddContactToOrganizationViewModel
            {
                ContactId = contactId,
                Organizations = userOrganizations,
            };

            return this.View(viewModel);
        }

        //[Authorize]
        //[HttpPost]
        //public async Task<IActionResult> ConnectToOrganization(int contactId, int organizationId)
        //{
        //    var user = await this.userManager.GetUserAsync(this.User);

        //    if (!this.ModelState.IsValid)
        //    {
        //        return this.NotFound();
        //    }

        //    //todo include userId
        //    await this.contactsService.AddToOrganizationAsync(contactId, organizationId);
        //    return this.RedirectToAction("Index", "Contacts");
        //}
    }
}
