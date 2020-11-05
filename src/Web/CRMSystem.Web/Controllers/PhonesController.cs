using System.Threading.Tasks;
using CRMSystem.Data.Models;
using CRMSystem.Services.Data.Contracts;
using CRMSystem.Web.ViewModels.Contacts;
using CRMSystem.Web.ViewModels.Phones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Web.Controllers
{
    [Authorize]
    public class PhonesController : Controller
    {
        private readonly IPhonesServices phonesServices;
       
        public PhonesController(IPhonesServices phonesServices)
        {
            this.phonesServices = phonesServices;
        }
        

        [HttpPost]
        public async Task<IActionResult> Add(PhoneCreateInputModel input)
        {
            await this.phonesServices.CreatePhoneAsync(input.Phone, input.PhoneType, input.CustomerId);
            return this.RedirectToAction("Details", "Customers", new { id = input.CustomerId });
        }

        public async Task<IActionResult> Delete(int id, int contactId)
        {
            await this.phonesServices.DeletePhoneAsync(id);

            //todo for this user check..

            return this.RedirectToAction("Details", "Customers", new { id = contactId });
        }
    }
}
