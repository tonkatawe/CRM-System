using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Web.ViewModels.Statistics
{
   public class IndexViewModel
    {
        [Remote("ValidateDate", "Statistics", AdditionalFields = nameof(EndDate))]
        [Display(Name = "Start date")]
        public DateTime StartDate { get; set; }

        [Remote("ValidateDate", "Statistics", AdditionalFields = nameof(StartDate))]
        public DateTime EndDate { get; set; }
    }
}
