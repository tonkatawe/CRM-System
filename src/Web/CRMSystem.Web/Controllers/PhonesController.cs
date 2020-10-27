﻿using System.Threading.Tasks;
using CRMSystem.Services.Data.Contracts;
using CRMSystem.Web.ViewModels.Contacts;
using CRMSystem.Web.ViewModels.Phones;
using Microsoft.AspNetCore.Authorization;
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


        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyPhone(string phone, ContactCreateInputModel input = null)
        {

            if (!this.phonesServices.IsAvailablePhoneNumber(phone) || !this.phonesServices.IsAvailablePhoneNumber(input.PhoneNumber.Phone))
            {
                return Json($"Phone {phone} is already in use");
            }
            return Json(true);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PhoneCreateInputModel input)
        {
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
