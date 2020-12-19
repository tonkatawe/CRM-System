namespace CRMSystem.Web.ViewModels.Statistics
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class IndexViewModel
    {
        [Remote("ValidateDate", "Statistics", AdditionalFields = nameof(EndDate))]
        [Display(Name = "Start date")]
        public DateTime StartDate { get; set; }

        [Remote("ValidateDate", "Statistics", AdditionalFields = nameof(StartDate))]
        public DateTime EndDate { get; set; }
    }
}
