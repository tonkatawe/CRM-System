using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CRMSystem.Data.Models;
using CRMSystem.Services.Data.Contracts;
using CRMSystem.Web.Infrastructure;
using CRMSystem.Web.ViewModels.Orders;
using CRMSystem.Web.ViewModels.Products;
using CRMSystem.Web.ViewModels.Sales;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

namespace CRMSystem.Web.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrdersService ordersService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IProductsService productsService;

        public OrdersController(IOrdersService ordersService,
                                      UserManager<ApplicationUser> userManager,
                                      IProductsService productsService)
        {
            this.ordersService = ordersService;
            this.userManager = userManager;
            this.productsService = productsService;
        }


        public async Task<IActionResult> Create(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var products = this.productsService.GetAll<ProductDropDownViewModel>(user.Id).ToList();

            //todo make constrain about min 3 products and refactoring :)

            if (products.Count < 3)
            {
                return this.RedirectToAction("Create", "Products");
            }


            var viewModel = new OrderCreateInputModel
            {
                CustomerId = id,
                Products = products,
            };

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateInputModel input)
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


            await this.ordersService.CreateOrder(input.CustomerId, input.ProductId, input.Quantity);

            return this.RedirectToAction("Index", "Customers");
        }

        public async Task<IActionResult> GetOrders(string sortOrder, int id, int? pageNumber)
        {
            var allOrders = this.ordersService.GetOrders<OrderViewModel>(id);
            int pageSize = 3;

            ViewData["CustomerId"] = id;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["SortByDate"] = String.IsNullOrEmpty(sortOrder) ? "dateSorted" : "";
            ViewData["SortByQuantity"] = String.IsNullOrEmpty(sortOrder) ? "quantitySorted" : "";
            ViewData["SortByName"] = String.IsNullOrEmpty(sortOrder) ? "nameSorted" : "";

            var orders = from o in allOrders select o;

            switch (sortOrder)
            {
                case "quantitySorted":
                    orders = orders.OrderByDescending(x => x.Quantity);
                    break;
                case "dateSorted":
                    orders = orders.OrderBy(x => x.CreatedOn);
                    break;
                case "nameSorted":
                    orders = orders.OrderBy(x => x.ProductName);
                    break;

            }



            return View(await PaginatedList<OrderViewModel>.CreateAsync(orders, pageNumber ?? 1, pageSize));
        }

        public IActionResult OrderTypes(int id)
        {
            var viewModel = this.ordersService.GetOrdersType<OrderTypeViewModel>(id).Distinct();
            return this.View(viewModel);
        }

    }
}
