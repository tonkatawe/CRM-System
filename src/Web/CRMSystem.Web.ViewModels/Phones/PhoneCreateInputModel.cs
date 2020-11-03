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
        [Required]
        public int ContactId { get; set; }

        [Required]
        [Phone(ErrorMessage = "It is not valid phone number")]
        [Remote( "VerifyPhone",  "Validates")]
        public string Phone { get; set; }

        [Required]
        public PhoneType PhoneType { get; set; }
    }
}
