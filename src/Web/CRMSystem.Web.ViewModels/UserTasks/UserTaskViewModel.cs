using System;
using System.Collections.Generic;
using CRMSystem.Data.Models;
using CRMSystem.Data.Models.Enums;
using CRMSystem.Services.Mapping;

namespace CRMSystem.Web.ViewModels.UserTasks
{
    public class UserTaskViewModel : IMapFrom<UserTask>
    {

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DeadLine { get; set; }

        public bool IsComplete { get; set; }

        public bool InProgress { get; set; }

        public string UserId { get; set; }

        public bool IsDeleted { get; set; }

        public int Id { get; set; }

        public UserTaskStatus UserTaskStatus { get; set; }
        
        public DateTime CreatedOn { get; set; }
    }
}
