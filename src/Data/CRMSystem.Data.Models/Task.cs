using System;

namespace CRMSystem.Data.Models
{
    using CRMSystem.Data.Common.Models;

    public class Task : BaseDeletableModel<int>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DeadLine { get; set; }

        public bool IsComplete { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
