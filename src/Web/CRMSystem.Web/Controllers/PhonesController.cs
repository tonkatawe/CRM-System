namespace CRMSystem.Web.Controllers
{
    using CRMSystem.Services.Data.Contracts;
    using CRMSystem.Web.ViewModels.Phones;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Authorize(Roles = "Administrator, Owner")]
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
            await this.phonesServices.CreateAsync(input.Phone, input.PhoneType, input.CustomerId);
            return this.RedirectToAction("Details", "Customers", new { id = input.CustomerId });
        }
        
        [HttpPost]
        public async Task<IActionResult> Delete(int id, int customerId)
        {
            await this.phonesServices.DeleteAsync(id);

            return this.RedirectToAction("Edit", "Customers", new { id = customerId });
        }
    }
}
