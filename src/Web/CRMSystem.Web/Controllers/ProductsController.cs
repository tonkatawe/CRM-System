using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRMSystem.Data.Models;
using CRMSystem.Services.Data.Contracts;
using CRMSystem.Web.ViewModels.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Web.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;
        private readonly UserManager<ApplicationUser> userManager;


        public ProductsController(IProductsService productsService, UserManager<ApplicationUser> userManager)
        {
            this.productsService = productsService;
            this.userManager = userManager;
        }

        // GET: ProductsController
        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var viewModel = this.productsService.GetAll<ProductViewModel>(user.Id).ToList();

            return View(viewModel);
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductsController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            await this.productsService.CreateAsync(input, user.Id);


            return this.RedirectToAction("Index");

        }

        // GET: ProductsController/Edit/5
        public IActionResult Edit(int id)
        {
            var viewModel = this.productsService.GetById<EditProductInputModel>(id);

            return View(viewModel);
        }

        // POST: ProductsController/Edit/5
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

        // GET: ProductsController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            //todo check for security delete by other users!!! 

            await this.productsService.DeleteAsync(id);
            return this.RedirectToAction("Index");
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
