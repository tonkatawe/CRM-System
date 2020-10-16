using System.Linq;
using System.Threading.Tasks;
using CRMSystem.Data.Common.Repositories;
using CRMSystem.Data.Models;
using CRMSystem.Services.Data.Contracts;
using CRMSystem.Web.ViewModels.UserTasks;

namespace CRMSystem.Services.Data
{
    public class UserTasksService : IUserTasksService
    {
        private readonly IDeletableEntityRepository<UserTask> userTasksRepository;

        public UserTasksService(IDeletableEntityRepository<UserTask> userTasksRepository)
        {
            this.userTasksRepository = userTasksRepository;
        }

        public async Task<int> CreateUserTaskAsync(UserTaskCreateInputModel input, string userId)
        {
            var task = new UserTask
            {
                UserId = userId,
                Title = input.Title,
                DeadLine = input.DeadLine,
                Description = input.Description,
            };

            await this.userTasksRepository.AddAsync(task);
            await this.userTasksRepository.SaveChangesAsync();

            return task.Id;
        }

        public int GetUserTasksCount(string userId)
        {
            return this.userTasksRepository.All()
                .Count(x => x.UserId == userId);
        }
    }
}
