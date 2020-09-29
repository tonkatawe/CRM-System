namespace CRMSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using CRMSystem.Data.Common.Models;

    public class UserTask : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(20)]

        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]

        public string Description { get; set; }

        [Required]
        public DateTime DeadLine { get; set; }

        public bool IsComplete { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
