using System;
using System.Linq;
using System.Threading.Tasks;
using CRMSystem.Data.Models;
using CRMSystem.Services.Data.Contracts;
using CRMSystem.Web.ViewModels.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Web.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {


        private readonly IProductsService productsService;
        private readonly IWebHostEnvironment environment;
        private readonly UserManager<ApplicationUser> userManager;


        public ProductsController(
            IProductsService productsService,
            IWebHostEnvironment environment,
            UserManager<ApplicationUser> userManager)
        {
            this.productsService = productsService;
            this.environment = environment;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {


            var user = await this.userManager.GetUserAsync(this.User);



            var userId = user.ParentId ?? user.Id;



            var viewModel = this.productsService.GetAll<ProductViewModel>(userId).ToList();

            return View(viewModel);
        }


        public async Task<IActionResult> Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.productsService.CreateAsync(input, user.Id, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("", ex.Message);
                return this.View(input);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            this.TempData["Message"] = "Product added successfully";
            return this.RedirectToAction("Index");

        }


        public IActionResult Edit(int id)
        {
            var viewModel = this.productsService.GetById<EditProductInputModel>(id);

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(EditProductInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            await this.productsService.UpdateAsync(input);

            return this.RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int id)
        {
            //todo check for security delete by other users!!! its so import

            await this.productsService.DeleteAsync(id);
            return this.RedirectToAction("Index");
        }


    }
}
