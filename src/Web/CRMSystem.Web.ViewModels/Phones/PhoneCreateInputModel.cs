namespace CRMSystem.Web.ViewModels.Phones
{
    using System.ComponentModel.DataAnnotations;

    using CRMSystem.Data.Models;
    using CRMSystem.Data.Models.Enums;
    using CRMSystem.Services.Mapping;

    public class PhoneCreateInputModel : IMapFrom<PhoneNumber>
    {
        [Phone(ErrorMessage = "It is not valid phone number")]
        public string Phone { get; set; }

        public PhoneType PhoneType { get; set; }
    }
}
