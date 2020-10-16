﻿namespace CRMSystem.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using CRMSystem.Data.Models;
    using CRMSystem.Services.Data.Contracts;
    using CRMSystem.Web.ViewModels.Contacts;
    using CRMSystem.Web.ViewModels.Emails;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ContactsController : Controller
    {
        private readonly IContactsService contactsService;
        private readonly IEmailsService emailsService;
        private readonly IOrganizationsService organizationsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ContactsController(
            IContactsService contactsService,
            IEmailsService emailsService,
            IOrganizationsService organizationsService,
            UserManager<ApplicationUser> userManager)
        {
            this.contactsService = contactsService;
            this.emailsService = emailsService;
            this.organizationsService = organizationsService;
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
                return this.View(input);
            }

            var contactId = await this.contactsService.CreateContactAsync(input, user.Id);

            return this.RedirectToAction("ConnectToOrganization", "Organizations", new { contactId = contactId });
        }

        [Authorize]
        public async Task<IActionResult> GetByUser(int page = 1)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var contacts = this.contactsService.GetAllUserContacts<ContactViewModel>(user.Id);
            if (!contacts.Any())
            {
                return this.RedirectToAction("Create");
            }

            foreach (var contact in contacts)
            {
                var emails = this.emailsService.GetAllContactEmails<EmailViewModel>(contact.Id);
                foreach (var email in emails)
                {
                    contact.Emails.Add(email);
                }
            }

            var count = contacts.Count();
            

            var viewModel = new GetAllContactsViewModel
            {
                Contacts = contacts,
            };
            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Remove(int contactId)
        {

            // todo: make security and deletable entities;

            await this.contactsService.DeleteContactAsync(contactId);

            return this.RedirectToAction("GetByUser");
        }

        [Authorize]
        public IActionResult GetDetails(int contactId)
        {
            var viewModel = this.contactsService.GetContactById<GetDetailsViewModel>(contactId);
            return this.View(viewModel);
        }
    }
}
