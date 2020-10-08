namespace CRMSystem.Web.ViewModels.Phones
{
    using System.ComponentModel.DataAnnotations;

    using CRMSystem.Data.Models;
    using CRMSystem.Data.Models.Enums;
    using CRMSystem.Services.Mapping;

    public class PhoneInputViewModel : IMapFrom<PhoneNumber>
    {
        public int ContactId { get; set; }

        [Required]
        public string Phone { get; set; }

        public Contact Contact { get; set; }

        public PhoneType PhoneType { get; set; }
    }
}
