using System.Threading.Tasks;
using CRMSystem.Services.Data.Contracts;
using CRMSystem.Web.ViewModels.Contacts;
using CRMSystem.Web.ViewModels.Emails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Web.Controllers
{
    [Authorize]
    public class EmailsController : Controller
    {
        private readonly IEmailsService emailsService;

        public EmailsController(IEmailsService emailsService)
        {
            this.emailsService = emailsService;
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyEmail(string email, ContactCreateInputModel input = null)
        {

            if (!this.emailsService.IsAvailableEmail(email) || !this.emailsService.IsAvailableEmail(input.Email.Email))
            {
                return Json($"Email {email} is already in use");
            }
            return Json(true);
        }

        [HttpPost]
        public async Task<IActionResult> Add(EmailCreateInputModel input)
        {
            await this.emailsService.CreateEmailAsync(input.Email, input.EmailType, input.ContactId);
            return this.RedirectToAction("Details", "Contacts", new { id = input.ContactId });
        }

        public async Task<IActionResult> Delete(int id, int contactId)
        {
            await this.emailsService.DeleteEmailAsync(id);

            //todo for this user check..

            return this.RedirectToAction("Details", "Contacts", new { id = contactId });
        }
    }
}
