namespace CRMSystem.Web.ViewModels.UserTasks
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using CRMSystem.Data.Models;
    using CRMSystem.Services.Mapping;
    public class UserTaskCreateInputModel : IMapFrom<UserTask>
    {
        [Required]
        [MaxLength(80)]
        public string Title { get; set; }

        [Required(ErrorMessage = "You should write any task's description...")]
        [MaxLength(1500)]
        public string Description { get; set; }

        [Required]
        public DateTime DeadLine { get; set; }

        public bool IsComplete { get; set; }
    }
}

