using System.Net.Security;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Web.ViewModels.Phones
{
    using System.ComponentModel.DataAnnotations;

    using CRMSystem.Data.Models;
    using CRMSystem.Data.Models.Enums;
    using CRMSystem.Services.Mapping;

    public class PhoneCreateInputModel : IMapFrom<PhoneNumber>
    {
        public int? Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        [Phone(ErrorMessage = "It is not valid phone number")]
        public string Phone { get; set; }

        [Required]
        public PhoneType PhoneType { get; set; }
    }
}
