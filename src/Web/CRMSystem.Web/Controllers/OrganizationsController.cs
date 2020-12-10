using System.Security.Claims;

namespace CRMSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using CRMSystem.Data.Models;
    using CRMSystem.Services.Data.Contracts;
    using CRMSystem.Web.ViewModels.Organizations;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = ("Administrator, Owner"))]
    public class OrganizationsController : Controller
    {

        private readonly IOrganizationsService organizationsService;
        private readonly UserManager<ApplicationUser> userManager;


        public OrganizationsController(IOrganizationsService organizationsService, UserManager<ApplicationUser> userManager)
        {
            this.organizationsService = organizationsService;
            this.userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user.OrganizationId == null)
            {
                return this.RedirectToAction("Create");
            }


            var viewModel = this.organizationsService.GetById<OrganizationViewModel>(user.Id);

            return this.View(viewModel);
        }

        public async Task<IActionResult> Create()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user.OrganizationId != null)
            {
                return RedirectToAction("Index");
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

            await this.organizationsService.CreateAsync(input, user.Id);

            
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user.OrganizationId == null)
            {
                return this.RedirectToAction("Create");
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

        [AllowAnonymous]
        public IActionResult List()
        {
            var viewModel = this.organizationsService.GetAll<ListOrganizationViewModel>();
            return this.View(viewModel);
        }

    }
}
