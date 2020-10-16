using System.Threading.Tasks;
using CRMSystem.Web.ViewModels.UserTasks;

namespace CRMSystem.Services.Data.Contracts
{
    public interface IUserTasksService
    {
        Task<int> CreateUserTaskAsync(UserTaskCreateInputModel input, string userId);

        int GetUserTasksCount(string userId);
    }
}
