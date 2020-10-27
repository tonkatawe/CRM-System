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


        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyPhone(string phone)
        {

            if (!this.phonesServices.IsAvailablePhoneNumber(phone))
            {
                return Json($"Phone {phone} is already in use");
            }
            return Json(true);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PhoneCreateInputModel input)
        {

            //if (await this.phonesServices.IsExistPhoneNumber(input.Phone))
            //{
            //   ModelState.AddModelError("Phone", "Phone is already taken");
            //}

            await this.phonesServices.CreatePhoneAsync(input.Phone, input.PhoneType, input.ContactId);
            return this.RedirectToAction("Details", "Contacts", new { id = input.ContactId });
        }

        public async Task<IActionResult> Delete(int id, int contactId)
        {
            await this.phonesServices.DeletePhoneAsync(id);

            //todo for this user check..


            return this.RedirectToAction("Details", "Contacts", new { id = contactId });
        }
    }
}
