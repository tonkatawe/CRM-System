using System.Collections.Generic;
using System.Threading.Tasks;
using CRMSystem.Web.ViewModels.UserTasks;

namespace CRMSystem.Services.Data.Contracts
{
    public interface IUserTasksService
    {
        Task<int> CreateUserTaskAsync(UserTaskCreateInputModel input, string userId);

        int GetUserTasksCount(string userId);
        IEnumerable<T> GetAllUserTasks<T>(string userId, int? count = null);

        Task<int> DeleteUserTaskAsync(int userTaskId);
    }
}
