using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CRMSystem.Services.Data.Contracts;
using CRMSystem.Services.Messaging;
using CRMSystem.Web.ViewModels.Customers;
using CRMSystem.Web.ViewModels.Emails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Web.Controllers
{
    [Authorize]
    public class EmailsController : Controller
    {
        private readonly IEmailsService emailsService;
        private readonly IEmailSender emailSender;

        public EmailsController(IEmailsService emailsService, IEmailSender emailSender)
        {
            this.emailsService = emailsService;
            this.emailSender = emailSender;
        }


        [HttpPost]
        public async Task<IActionResult> Add(EmailCreateInputModel input)
        {
            await this.emailsService.CreateAsync(input.Email, input.EmailType, input.CustomerId);
            return this.RedirectToAction("Edit", "Customers", new { id = input.CustomerId });
        }


        public async Task<IActionResult> Delete(int id, int customerId)
        {
            await this.emailsService.DeleteAsync(id);

            //todo for this user check..

            return this.RedirectToAction("Edit", "Customers", new { id = customerId });


        }

        public async Task<IActionResult> Send(int id)
        {
            var emails = this.emailsService.GetAll<EmailDropDownViewModel>(id).ToList();

            var viewModel = new SendEmailViewModel
            {
                Id = id,
                Emails = emails,
            };


            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Send(SendEmailViewModel input)
        {
            //todo make save all sent messages
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            //todo make get user or organization name
            var from = this.User.FindFirst(ClaimTypes.Email).Value;
            await this.emailSender.SendEmailAsync(
                from,
                "User - Name",
                input.Email,
                input.Subject,
                input.Content);

          //todo make success view with temp data :) cool redirection :)

          return this.RedirectToAction("Index", "Statistics");
        }

        public async Task<IActionResult> Update(EmailCreateInputModel input)
        {
            throw new NotImplementedException();

        }
    }
}
