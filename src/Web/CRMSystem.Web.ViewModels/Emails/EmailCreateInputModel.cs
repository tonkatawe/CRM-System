namespace CRMSystem.Web.ViewModels.Emails
{
    using CRMSystem.Data.Models;
    using CRMSystem.Data.Models.Enums;
    using CRMSystem.Services.Mapping;
    using System.ComponentModel.DataAnnotations;


    public class EmailCreateInputModel : IMapFrom<EmailAddress>
    {

        public int? Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "The email is not valid")]
        public string Email { get; set; }

        public EmailType EmailType { get; set; }

    }
}
