using System.Collections.Generic;
using System.Linq;
using CRMSystem.Data.Common.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Web.ViewModels.Emails
{
    using System.ComponentModel.DataAnnotations;

    using CRMSystem.Data.Models;
    using CRMSystem.Data.Models.Enums;
    using CRMSystem.Services.Mapping;


    public class EmailCreateInputModel : IMapFrom<EmailAddress>
    {

        public int? Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "The Emails is not valid")]

        public string Email { get; set; }

        public EmailType EmailType { get; set; }

    }
}
