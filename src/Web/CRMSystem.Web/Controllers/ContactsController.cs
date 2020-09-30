using System;
using System.Linq;
using CRMSystem.Data.Models;
using CRMSystem.Services.Data.Contracts;
using CRMSystem.Web.ViewModels.Contacts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CRMSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Create()
        {
            return this.View();
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ContactCreateInputModel input)
        {

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.contactsService.CreateContactAsync(input);

            return this.RedirectToAction(this.ByContacts().ToString());
        }

        [Authorize]
        public async Task<IActionResult> ByContacts()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (!user.Contacts.Any())
            {
                return this.RedirectToPage("/NoContacts");
            }
            else
            {
                var createViewModel = this.contactsService.GetAllUserContacts<ContactViewModel>(user.Id);
                return this.View(createViewModel);
            }
        }

    }
}
