using System.Collections.Generic;
using CRMSystem.Data.Models;
using CRMSystem.Web.ViewModels.Contacts;
using CRMSystem.Web.ViewModels.Emails;
using CRMSystem.Web.ViewModels.Orders;
using CRMSystem.Web.ViewModels.Phones;
using CRMSystem.Web.ViewModels.Sales;

namespace CRMSystem.Web.ViewModels.Customers
{
    public class GetDetailsViewModel : CustomerViewModel
    {
        public GetDetailsViewModel()
        {
            this.Phones = new List<PhoneCreateInputModel>();
            this.Emails = new List<EmailCreateInputModel>();
        }
        public Title Title { get; set; }

        public string TitleAsString => this.Title.ToString();

        public string JobTitle { get; set; }

        public string AdditionalInfo { get; set; }

        public Address Address { get; set; }
        public OrderCustomerStatsViewModel CustomerStats { get; set; }

        public IList<PhoneCreateInputModel> Phones { get; set; }

        public IList<EmailCreateInputModel> Emails { get; set; }


    }
}
