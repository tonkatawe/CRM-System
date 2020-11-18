using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using CRMSystem.Data.Models;
using CRMSystem.Data.Models.Enums;
using CRMSystem.Services.Mapping;
using CRMSystem.Web.ViewModels.Contacts;
using CRMSystem.Web.ViewModels.Emails;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Web.ViewModels.Customers
{
    public class EditCustomerInputModel : CustomerAddInputModel
    {
        public int Id { get; set; }

        //public EditCustomerInputModel()
        //{
        //    this.PhoneNumbers = new List<PhoneNumber>();
        //    this.EmailAddresses = new List<EmailCreateInputModel>();
        //    this.SocialNetworks = new List<SocialNetwork>();
        //}

        //[Required]
        //public Title Title { get; set; }

        //[Required]
        //[MaxLength(20)]

        //public string FirstName { get; set; }

        //[MaxLength(20)]
        //public string MiddleName { get; set; }

        //[Required]
        //[MaxLength(20)]
        //public string LastName { get; set; }

        //[MaxLength(30)]
        //public string JobTitle { get; set; }

        //public IndustryType Industry { get; set; }

        //[MaxLength(1000)]
        //public string AdditionalInfo { get; set; }

        //[Required]
        //public Address Address { get; set; }

        //public IList<PhoneNumber> PhoneNumbers { get; set; }

        //public IList<EmailCreateInputModel> EmailAddresses { get; set; }

        //public IList<SocialNetwork> SocialNetworks { get; set; }

     

    }
}
