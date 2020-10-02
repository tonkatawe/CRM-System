using CRMSystem.Data.Common;

namespace CRMSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CRMSystem.Data.Models;
    using CRMSystem.Services.Data.Contracts;
    using CRMSystem.Web.ViewModels.Contacts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class ContactsController : Controller
    {
        private readonly IContactsService contactsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ContactsController(IContactsService contactsService, UserManager<ApplicationUser> userManager)
        {
            this.contactsService = contactsService;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ContactCreateInputModel input, string userId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.contactsService.CreateContactAsync(input, user.Id);

            return this.Redirect("/");
        }

        [Authorize]
        public async Task<IActionResult> GetByUser()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var contacts = this.contactsService.GetAllUserContacts<ContactViewModel>(user.Id);
            if (!contacts.Any())
            {
                return this.RedirectToAction("Create");
            }


            var viewModel = new GetAllContactsViewModel
            {
                Contacts = contacts,
            };
            return this.View(viewModel);
        }
    }
}
