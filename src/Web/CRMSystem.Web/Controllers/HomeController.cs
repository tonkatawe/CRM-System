using CRMSystem.Data.Models;
using CRMSystem.Services.Data.Contracts;
using Microsoft.AspNetCore.Identity;
using IndexViewModel = CRMSystem.Web.ViewModels.Home.IndexViewModel;

namespace CRMSystem.Web.Controllers
{
    using System.Diagnostics;

    using CRMSystem.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IContactsService contactsService;
        private readonly IUserTasksService userTasksService;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(IContactsService contactsService, IUserTasksService userTasksService, UserManager<ApplicationUser> userManager)
        {
            this.contactsService = contactsService;
            this.userTasksService = userTasksService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Identity/Account/Login");
            }

            var userId = this.userManager.GetUserId(this.User);
            var contactsCount = this.contactsService.AllContactCount(userId);
            var userTasksCount = this.userTasksService.GetUserTasksCount(userId);
     

            var viewModel = new IndexViewModel
            {
                ContactsCount = contactsCount,
                TaskCount = userTasksCount,
             
            };


            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
