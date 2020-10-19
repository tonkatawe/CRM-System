using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRMSystem.Data.Models;
using CRMSystem.Services.Data.Contracts;
using CRMSystem.Web.ViewModels.UserTasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Web.Controllers
{
    [Authorize]
    public class UserTasksController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserTasksService userTasksService;

        public UserTasksController(UserManager<ApplicationUser> userManager, IUserTasksService userTasksService)
        {
            this.userManager = userManager;
            this.userTasksService = userTasksService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserTaskCreateInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Create");
            }

            var viewModel = await this.userTasksService.CreateUserTaskAsync(input, user.Id);

            return this.Redirect("/UserTasks/GetByUser");
        }

        public async Task<IActionResult> GetByUser()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userTasks = this.userTasksService.GetAllUserTasks<UserTaskViewModel>(user.Id);

            var viewModel = new GetAllUserTasksViewModel
            {
                UserTasks = userTasks,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Remove(int userTaskId)
        {
            await this.userTasksService.DeleteUserTaskAsync(userTaskId);

            return this.RedirectToAction("GetByUser");
        }
    }
}
