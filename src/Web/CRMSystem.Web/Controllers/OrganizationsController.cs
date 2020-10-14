using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using CRMSystem.Web.ViewModels.Contacts;

namespace CRMSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using CRMSystem.Data.Models;
    using CRMSystem.Services.Data.Contracts;
    using CRMSystem.Web.ViewModels.Organizations;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class OrganizationsController : Controller
    {
        private readonly IOrganizationsService organizationsService;
        private readonly IContactsService contactsService;
        private readonly UserManager<ApplicationUser> userManager;

        public OrganizationsController(IOrganizationsService organizationsService,
            IContactsService contactsService,
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
        public async Task<IActionResult> Create(ConnectViewModel input, string userId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (!this.ModelState.IsValid)
            {
             return this.View(input);
            }

            await this.organizationsService.CreateOrganizationAsync(input.CreateOrganization, user.Id);

            return this.RedirectToAction("Create", "Contacts");
        }

        [Authorize]
        public async Task<IActionResult> ConnectToOrganization(int contactId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var contact = this.contactsService.GetContactById<ContactViewModel>(contactId);

            //todo: make security part for contact with not null organizationID

            if (contact == null)
            {
                return this.NotFound();
            }

            var userOrganizations = this.organizationsService.GetAll<OrganizationDropDownViewModel>(user.Id);
            var viewModel = new ConnectViewModel
            {
                Organizations = userOrganizations,
                Contact = contact,
            };

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ConnectToOrganization(int contactId, int organizationId)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            await this.contactsService.AddToOrganizationAsync(contactId, organizationId);

            //todo: make security part for contact with not null organizationID

            return this.Redirect("/Contacts/GetByUser");
        }
    }
}
