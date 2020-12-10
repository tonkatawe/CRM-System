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

            if (user.OrganizationId == null)
            {
                return this.RedirectToAction("Create", "Organizations");
            }
            var allUserCustomers = this.customersService.GetAll<CustomerViewModel>(user.Id, true);


            var customers = from c in allUserCustomers
                            select c;


            int pageSize = 3;

            return View(await PaginatedList<CustomerViewModel>.CreateAsync(customers, pageNumber ?? 1, pageSize));
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

            await this.temporaryCustomersService.ApproveAsync(id);

            //TODO : MAKE ACCOUNT AND SEND EMAIL


            return this.RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = ("Owner, Admin"))]

        public async Task<IActionResult> Reject(int id, string organizationId)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user.OrganizationId != organizationId)
            {
                return NotFound();
            }

            await this.temporaryCustomersService.RejectAsync(id);

            //TODO : SEND EMAIL ABOUT INFORM REJECTION

            return this.RedirectToAction("Index");
        }
    }
}
