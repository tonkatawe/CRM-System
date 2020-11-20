using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CRMSystem.Data.Models;
using CRMSystem.Services.Data.Contracts;
using CRMSystem.Web.ViewModels.Products;
using CRMSystem.Web.ViewModels.SaleProducts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Web.Controllers
{
    [Authorize]
    public class SaleProductsController : Controller
    {
        private readonly ISaleProductsService saleProductsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IProductsService productsService;

        public SaleProductsController(ISaleProductsService saleProductsService,
                                      UserManager<ApplicationUser> userManager,
                                      IProductsService productsService)
        {
            this.saleProductsService = saleProductsService;
            this.userManager = userManager;
            this.productsService = productsService;
        }


        public async Task<IActionResult> Sale(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var products = this.productsService.GetAll<ProductDropDownViewModel>(user.Id).ToList();

            //todo make constrain about min 3 products and refactoring :)

            if (products.Count < 3)
            {
                return this.RedirectToAction("Create", "Products");
            }


            var viewModel = new SaleCreateInputModel
            {
                CustomerId = id,
                Products = products,
            };

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Sale(SaleCreateInputModel input)
        {

            var user = await this.userManager.GetUserAsync(this.User);
            var products = this.productsService.GetAll<ProductDropDownViewModel>(user.Id).ToList();
            var product = this.productsService.GetById<ProductViewModel>(input.ProductId);
            
            if (product.Quantity < input.Quantity)
            {
                ModelState.AddModelError("", $"You cannot add {input.Quantity} because in your storage has only {product.Quantity}");
            }

            if (!ModelState.IsValid)
            {
                input.Products = products;
            
                return this.View(input);
            }


            await this.saleProductsService.CreateSale(input.CustomerId, input.ProductId, input.Quantity);

            return this.RedirectToAction("Index", "Customers");
        }

    }
}
