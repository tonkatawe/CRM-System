using System.Threading.Tasks;
using CRMSystem.Services.Data.Contracts;
using CRMSystem.Web.ViewModels.Phones;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Web.Controllers
{
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
            await this.phonesServices.CreatePhoneAsync(input.Phone, input.PhoneType, input.ContactId);
            return this.RedirectToAction("Details", "Contacts", new {id = input.ContactId});
        }
    }
}
