using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CRMSystem.Data.Models;
using CRMSystem.Services.Data.Contracts;
using CRMSystem.Web.Infrastructure;
using CRMSystem.Web.ViewModels.Customers;
using CRMSystem.Web.ViewModels.TemporaryCustomers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CRMSystem.Web.Controllers
{
    public class TemporaryCustomersController : Controller
    {
        private const string SentData = "Successful";


        private readonly ITemporaryCustomersService temporaryCustomersService;
        private readonly ICustomersService customersService;
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;


        public TemporaryCustomersController(
            ITemporaryCustomersService temporaryCustomersService,
            ICustomersService customersService,
            IUsersService usersService,
            UserManager<ApplicationUser> userManager)
        {
            this.temporaryCustomersService = temporaryCustomersService;
            this.customersService = customersService;
            this.usersService = usersService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Create(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user != null)
            {
                return NotFound();
            }

            this.TempData["OrganizationId"] = id;

            return this.View("_CreateCustomer");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerAddInputModel input, string organizationId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user != null)
            {
                return NotFound();
            }

            var userId = this.usersService.GetUserIdByOrganizationId(organizationId);
            await this.customersService.CreateAsync(input, userId, organizationId, true);

            this.TempData["Successful"] = "Your sent successful";
            this.TempData[SentData] = true;

            return this.RedirectToAction("Successful");
        }

        public IActionResult Successful()
        {
            if (this.TempData[SentData] == null)
            {
                return this.NotFound();
            }

            return this.View();
        }

        [Authorize(Roles = ("Owner, Admin"))]
        public async Task<IActionResult> Index(int? pageNumber)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            //todo must refactoring this!

            if (user.OrganizationId == null)
            {
                //todo make something
                return RedirectToPage("~/Account/Login");
            }







            int pageSize = 3;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = ("Owner, Admin"))]
        public async Task<IActionResult> Approve(int id, string organizationId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user.OrganizationId != organizationId)
            {
                return NotFound();
            }

            await this.temporaryCustomersService.ApproveAsync(id, organizationId);
            return this.View("Index");
        }
    }
}
