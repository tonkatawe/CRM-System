using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using CRMSystem.Common;
using CRMSystem.Data.Models;
using CRMSystem.Services.Data.Contracts;
using CRMSystem.Services.Messaging;
using CRMSystem.Web.ViewModels.Contacts;
using Microsoft.AspNetCore.Identity;

namespace CRMSystem.Web.Controllers
{
    public class ContactsController : Controller
    {
        private const string Successful = "Successful";

        private readonly IContactsService contactsService;
        private readonly IEmailSender emailSender;
        private readonly UserManager<ApplicationUser> userManager;

        public ContactsController(
            IContactsService contactsService,
            IEmailSender emailSender,
            UserManager<ApplicationUser> userManager)
        {
            this.contactsService = contactsService;
            this.emailSender = emailSender;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user != null)
            {
                var viewModel = new ContactFormViewModel
                {
                    Email = user.Email,
                    Name = user.UserName,

                };
                return this.View(viewModel);

            }
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactFormViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var ip = this.HttpContext.Connection.RemoteIpAddress.ToString();

            await this.contactsService.CreateAsync(input, ip);

            await this.emailSender.SendEmailAsync(
                input.Email,
                input.Name,
                GlobalConstants.SystemEmail,
                input.Title,
                input.Content);

            this.TempData[Successful] = true;
            this.TempData["Messages"] = "Your email was sent";

            return this.RedirectToAction("ThankYou");
        }

        public IActionResult ThankYou()
        {
            if (this.TempData[Successful] == null)
            {
                return this.NotFound();
            }

            return this.View();
        }
    }
}
