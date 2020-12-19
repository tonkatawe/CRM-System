namespace CRMSystem.Web.Controllers
{
    using CRMSystem.Data.Models;
    using CRMSystem.Services.Data.Contracts;
    using CRMSystem.Services.Messaging;
    using CRMSystem.Web.ViewModels.Emails;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;

    [Authorize(Roles = "Administrator, Owner")]
    public class EmailsController : Controller
    {
        private readonly IEmailsService emailsService;
        private readonly IEmailSender emailSender;
        private readonly UserManager<ApplicationUser> userManager;

        public EmailsController(
            IEmailsService emailsService,
            IEmailSender emailSender,
            UserManager<ApplicationUser> userManager)
        {
            this.emailsService = emailsService;
            this.emailSender = emailSender;
            this.userManager = userManager;
        }


        [HttpPost]
        public async Task<IActionResult> Add(EmailCreateInputModel input)
        {
            await this.emailsService.CreateAsync(input.Email, input.EmailType, input.CustomerId);
            return this.RedirectToAction("Edit", "Customers", new { id = input.CustomerId });
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id, int customerId)
        {
            await this.emailsService.DeleteAsync(id);
            
            return this.RedirectToAction("Edit", "Customers", new { id = customerId });
            
        }

        public IActionResult Send(int id)
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

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.emailSender.SendEmailAsync(
                user.Email,
                user.UserName,
                input.Email,
                input.Subject,
                input.Content);


            this.TempData["Message"] = "Your email was sent";

            return this.RedirectToAction("Index", "Statistics");
        }

        public IActionResult CustomSend()
        {
            return this.View();
        }

    }
}
