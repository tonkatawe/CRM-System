﻿using System.ComponentModel.DataAnnotations;

namespace CRMSystem.Data.Models
{

    using CRMSystem.Data.Common.Models;

    public class Note : BaseDeletableModel<int>
    {
        public int ContactId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
    }
}
