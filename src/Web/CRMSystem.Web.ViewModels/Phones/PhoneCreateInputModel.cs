namespace CRMSystem.Web.ViewModels.Phones
{
    using CRMSystem.Data.Models;
    using CRMSystem.Data.Models.Enums;
    using CRMSystem.Services.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class PhoneCreateInputModel : IMapFrom<PhoneNumber>
    {
        public int? Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        [Phone(ErrorMessage = "It isn't valid phone number")]
        public string Phone { get; set; }

        [Required]
        public PhoneType PhoneType { get; set; }
    }
}
