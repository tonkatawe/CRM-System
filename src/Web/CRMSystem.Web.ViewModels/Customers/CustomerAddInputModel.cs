using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CRMSystem.Data.Models;
using CRMSystem.Data.Models.Enums;
using CRMSystem.Services.Mapping;
using CRMSystem.Web.ViewModels.Addresses;
using CRMSystem.Web.ViewModels.Emails;
using CRMSystem.Web.ViewModels.Phones;

namespace CRMSystem.Web.ViewModels.Customers
{
    public class CustomerAddInputModel : IMapFrom<Customer>
    {
        public CustomerAddInputModel()
        {
            this.Emails = new List<EmailCreateInputModel>();
            this.Phones = new List<PhoneCreateInputModel>();
        }

        [Required]
        public Title Title { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [MaxLength(20)]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        [MaxLength(30)]
        public string JobTitle { get; set; }


        [MaxLength(1000)]
        public string AdditionalInfo { get; set; }

        public AddressCreateInputModel Address { get; set; }

        public IList<EmailCreateInputModel> Emails { get; set; }

        public IList<PhoneCreateInputModel> Phones { get; set; }

    }
}
