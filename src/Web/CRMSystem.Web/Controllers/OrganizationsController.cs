using System.Security.Claims;
using CRMSystem.Web.ViewModels.Customers;

namespace CRMSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using CRMSystem.Data.Models;
    using CRMSystem.Services.Data.Contracts;
    using CRMSystem.Web.ViewModels.Contacts;
    using CRMSystem.Web.ViewModels.Organizations;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    [Authorize]
    public class OrganizationsController : Controller
    {
        private readonly IOrganizationsService organizationsService;
        private readonly UserManager<ApplicationUser> userManager;


        public OrganizationsController(IOrganizationsService organizationsService, UserManager<ApplicationUser> userManager)
        {
            this.organizationsService = organizationsService;
            this.userManager = userManager;
        }


        public IActionResult Index()
        {

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var viewModel = this.organizationsService.GetById<OrganizationViewModel>(userId);

            return this.View(viewModel);
        }

        public async Task<IActionResult> Create()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user.HasOrganization)
            {

                //todo make to redirect organization info
                return RedirectToAction("Index", "Customers");
            }
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrganizationCreateInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.organizationsService.CreateOrganizationAsync(input, user.Id);

            //todo make to redirect organization info
            return RedirectToAction("Index", "Customers");
        }

        public async Task<IActionResult> Edit()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return NotFound();
            }
            var viewModel = this.organizationsService.GetById<EditOrganizationInputModel>(user.Id);
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditOrganizationInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = this.organizationsService.GetById<EditOrganizationInputModel>(user.Id);

            await this.organizationsService.UpdateAsync(input);

            return this.RedirectToAction("Index");
        }



    }
}
