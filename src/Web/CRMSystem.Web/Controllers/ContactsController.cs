using System;
using System.Linq;
using CRMSystem.Data.Models;
using CRMSystem.Services.Data.Contracts;
using CRMSystem.Web.ViewModels.Contacts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class ContactsController : Controller
    {
        private readonly IContactsService contactsService;
        private readonly UserManager<ApplicationUser> userManager;


        public ContactsController(IContactsService contactsService, UserManager<ApplicationUser> userManager, ILogger<ContactsController> contactLogger)
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
        public async Task<IActionResult> Create(ContactCreateInputModel input, string userId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (!this.ModelState.IsValid)
            {
                Console.WriteLine(this.ModelState.ErrorCount);
                foreach (var value in this.ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        Console.WriteLine(error.Exception);
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
                return this.View(input);
            }

            await this.contactsService.CreateContactAsync(input, user.Id);

            return this.Redirect("/");
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
