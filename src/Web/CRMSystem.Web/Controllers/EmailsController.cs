using System;
using System.Threading.Tasks;
using CRMSystem.Services.Data.Contracts;
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
            await this.emailsService.CreateAsync(input.Email, input.EmailType, input.CustomerId);
            return this.RedirectToAction("Edit", "Customers", new { id = input.CustomerId });
        }
      
       
        public async Task<IActionResult> Delete(int id, int customerId)
        {
            await this.emailsService.DeleteAsync(id);

            //todo for this user check..

            return this.RedirectToAction("Edit", "Customers", new { id = customerId });


        }

        public async Task<IActionResult> Update(EmailCreateInputModel input)
        {
           throw new NotImplementedException();

        }
    }
}
