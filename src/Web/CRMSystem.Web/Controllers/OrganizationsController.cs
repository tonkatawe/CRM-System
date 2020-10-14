namespace CRMSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using CRMSystem.Data.Models;
    using CRMSystem.Services.Data.Contracts;
    using CRMSystem.Web.ViewModels.Organizations;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class OrganizationsController : Controller
    {
        private readonly IOrganizationsService organizationsService;
        private readonly UserManager<ApplicationUser> userManager;

        public OrganizationsController(IOrganizationsService organizationsService, UserManager<ApplicationUser> userManager)
        {
            this.organizationsService = organizationsService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Create()
        {
            return PartialView("_CreateOrgn");
            //  return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(OrganizationCreateInputModel input, string userId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.organizationsService.CreateOrganizationAsync(input, user.Id);

            return this.RedirectToAction("Create", "Contacts");
        }

        [Authorize]
        public IActionResult ConnectToOrganization()
        {
            return this.View();
            //  return this.View();
        }
    }
}
