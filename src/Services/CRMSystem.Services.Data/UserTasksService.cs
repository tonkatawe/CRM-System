using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRMSystem.Data.Common.Repositories;
using CRMSystem.Data.Models;
using CRMSystem.Data.Models.Enums;
using CRMSystem.Services.Data.Contracts;
using CRMSystem.Services.Mapping;
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
                UserTaskStatus = UserTaskStatus.UpComing,
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

        public IEnumerable<T> GetAllUserTasks<T>(string userId, int? count = null)
        {
            var query = this.userTasksRepository.All()
                .OrderByDescending(x => x.CreatedOn)
                .Where(x => x.UserId == userId)
                .To<T>()
                .ToList();

            return query;
        }

        public async Task<int> DeleteUserTaskAsync(int userTaskId)
        {
            var userTask = await this.userTasksRepository.GetByIdWithDeletedAsync(userTaskId);
            this.userTasksRepository.Delete(userTask);

            return await this.userTasksRepository.SaveChangesAsync();
        }

        public async Task<int> ChangeUserTaskStatusAsync(int userTaskId, UserTaskStatus taskStatus)
        {
            var userTask = await this.userTasksRepository.GetByIdWithDeletedAsync(userTaskId);
            userTask.UserTaskStatus = taskStatus;
            this.userTasksRepository.Update(userTask);

            return await this.userTasksRepository.SaveChangesAsync();
        }

        public T GetTaskById<T>(int userTaskId)
        {
            var query = this.userTasksRepository.All()
                .Where(x => x.Id == userTaskId)
                .To<T>()
                .FirstOrDefault();

            return query;
        }
    }
}
