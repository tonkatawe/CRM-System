using System.Threading.Tasks;
using CRMSystem.Data.Models;
using CRMSystem.Data.Models.Enums;
using CRMSystem.Services.Data.Contracts;
using CRMSystem.Web.ViewModels.UserTasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Web.Controllers
{

    [Route("UserTasks")]
    public class UserTasksController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserTasksService userTasksService;

        public UserTasksController(UserManager<ApplicationUser> userManager, IUserTasksService userTasksService)
        {
            this.userManager = userManager;
            this.userTasksService = userTasksService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userTasks = this.userTasksService.GetAllUserTasks<UserTaskViewModel>(user.Id);

            var viewModel = new GetAllUserTasksViewModel
            {
                UserTasks = userTasks,
            };

            return this.View(viewModel);
        }


        [Route("GetBy")]
        public IActionResult GetAllTask()
        {
            var user = this.userManager.GetUserId(this.User);
            var products = this.userTasksService.GetAllUserTasks<UserTaskViewModel>(user);
            return new JsonResult(products);
        }

        [Route("Create")]
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

        [Route("GetByUser")]
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

        [Route("Remove")]
        [HttpPost]
        public async Task<IActionResult> Remove(int userTaskId)
        {
            await this.userTasksService.DeleteUserTaskAsync(userTaskId);

            return RedirectToAction("Index");
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int taskId, UserTaskStatus type)
        {
            await this.userTasksService.ChangeUserTaskStatusAsync(taskId, type);

            return this.RedirectToAction("GetByUser");
        }





    }
}
