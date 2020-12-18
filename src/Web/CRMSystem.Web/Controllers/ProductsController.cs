using CRMSystem.Data.Models;
using CRMSystem.Services.Data.Contracts;
using CRMSystem.Web.Infrastructure;
using CRMSystem.Web.ViewModels.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CRMSystem.Web.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;
        private readonly IOrganizationsService organizationsService;
        private readonly UserManager<ApplicationUser> userManager;


        public ProductsController(
            IProductsService productsService,
            IOrganizationsService organizationsService,
        UserManager<ApplicationUser> userManager)
        {
            this.productsService = productsService;
            this.organizationsService = organizationsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(int? pageNumber)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var userId = user.ParentId ?? user.Id;

            if (user.ParentId != null)
            {
                var owner = await this.userManager.FindByIdAsync(user.ParentId);

                ViewData["Email"] = owner.Email;
            }

            var allProducts = this.productsService.GetAll<ProductViewModel>(userId);


            var products = from c in allProducts
                           select c;
            var pageSize = 3;
            return View(await PaginatedList<ProductViewModel>.CreateAsync(products, pageNumber ?? 1, pageSize));

        }

        [Authorize(Roles = ("Owner, Administrator"))]
        public async Task<IActionResult> Create()
        {
            return View();
        }


        [HttpPost]
        [Authorize(Roles = ("Owner, Administrator"))]
        public async Task<IActionResult> Create(ProductCreateInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.productsService.CreateAsync(input, user.Id);

            this.TempData["Message"] = "Product added successfully";
            return this.RedirectToAction("Index");
        }

        [Authorize(Roles = ("Owner, Administrator"))]
        public IActionResult Edit(int id)
        {
            var viewModel = this.productsService.GetById<EditProductInputModel>(id);

            return View(viewModel);
        }


        [HttpPost]
        [Authorize(Roles = ("Owner, Administrator"))]
        public async Task<IActionResult> Edit(EditProductInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            await this.productsService.UpdateAsync(input);

            return this.RedirectToAction("Index");
        }

        [Authorize(Roles = ("Owner, Administrator"))]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

            await this.productsService.DeleteAsync(id);
            return this.RedirectToAction("Index");
        }


    }
}
