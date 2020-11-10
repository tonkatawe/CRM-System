using System;
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

       
        [HttpPost]
        public async Task<IActionResult> Add(EmailCreateInputModel input)
        {
            await this.emailsService.CreateEmailAsync(input.Email, input.EmailType, input.CustomerId);
            return this.RedirectToAction("Details", "Customers", new { id = input.CustomerId });
        }
      
       
        public async Task<IActionResult> Delete(int id, int contactId)
        {
            await this.emailsService.DeleteEmailAsync(id);

            //todo for this user check..

            //return this.RedirectToAction("Details", "Contacts", new { id = contactId });
            return Json(id);

        }

        public async Task<IActionResult> Update(EmailCreateInputModel input)
        {
           throw new NotImplementedException();

        }
    }
}
