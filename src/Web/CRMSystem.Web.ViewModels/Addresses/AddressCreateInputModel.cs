namespace CRMSystem.Web.ViewModels.Addresses
{
    using CRMSystem.Data.Models;
    using CRMSystem.Services.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class AddressCreateInputModel : IMapFrom<Address>
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Country should be maximum 30 letters")]
        public string Country { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "City should be maximum 30 letters")]
        public string City { get; set; }

        [MaxLength(50, ErrorMessage = "Street should be maximum 30 letters")]
        public string Street { get; set; }

        [Range(1, 10000, ErrorMessage = "Zip code should be between 1 and 10000")]
        public int? ZipCode { get; set; }
    }
}
