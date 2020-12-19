using CRMSystem.Data.Models;
using CRMSystem.Web.ViewModels.Addresses;
using CRMSystem.Web.ViewModels.Emails;
using CRMSystem.Web.ViewModels.Phones;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CRMSystem.Data.Models.Enums;

namespace CRMSystem.Web.ViewModels.Customers
{
    public class CustomerAddInputModel
    {
        public CustomerAddInputModel()
        {
            this.Emails = new List<EmailCreateInputModel>();
            this.Phones = new List<PhoneCreateInputModel>();
        }

        [Required]
        public Title Title { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "The first name should be maximum 20 letters")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use latin letters only please")]
        public string FirstName { get; set; }

        [MaxLength(20, ErrorMessage = "The middle name should be maximum 20 letters")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use latin letters only please")]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "The last name should be maximum 20 letters")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use latin letters only please")]
        public string LastName { get; set; }

        [MaxLength(30, ErrorMessage = "Job title should be maximum 30 letters")]
        public string JobTitle { get; set; }


        [MaxLength(1000, ErrorMessage = "Additional info for customer should be maximum 1000 letters")]
        public string AdditionalInfo { get; set; }

        public AddressCreateInputModel Address { get; set; }

        public IList<EmailCreateInputModel> Emails { get; set; }

        public IList<PhoneCreateInputModel> Phones { get; set; }

    }
}
