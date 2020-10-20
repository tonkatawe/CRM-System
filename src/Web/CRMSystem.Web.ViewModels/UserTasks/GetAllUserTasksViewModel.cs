using System.Collections.Generic;
using CRMSystem.Data.Models.Enums;

namespace CRMSystem.Web.ViewModels.UserTasks
{
    public class GetAllUserTasksViewModel
    {
        public IEnumerable<UserTaskViewModel> UserTasks { get; set; }

        public UserTaskStatus Status { get; set; }
    }
}
