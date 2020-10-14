namespace CRMSystem.Web.ViewModels.Addresses
{
    using System.ComponentModel.DataAnnotations;

    using CRMSystem.Data.Models;
    using CRMSystem.Services.Mapping;

    public class AddressCreateInputModel : IMapFrom<Address>
    {
        [Required]
        [MaxLength(30)]
        public string Country { get; set; }

        [Required]
        [MaxLength(30)]
        public string City { get; set; }

        [MaxLength(50)]
        public string Street { get; set; }

        [Range(0, 9999999)]
        public int? ZipCode { get; set; }
    }
}
