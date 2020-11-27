using System;
using System.Collections.Generic;
using System.Text;

namespace CRMSystem.Web.ViewModels.Statistics
{
    public class RangeStatsViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalBenefits { get; set; }

    }
}
