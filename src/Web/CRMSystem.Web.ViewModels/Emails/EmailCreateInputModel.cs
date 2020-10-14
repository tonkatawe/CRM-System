namespace CRMSystem.Web.ViewModels.Emails
{
    using System.ComponentModel.DataAnnotations;

    using CRMSystem.Data.Models;
    using CRMSystem.Data.Models.Enums;
    using CRMSystem.Services.Mapping;

    public class EmailCreateInputModel : IMapFrom<EmailAddress>
    {
        [EmailAddress(ErrorMessage = "The Emails is not valid")]
        public string Email { get; set; }

        public EmailType EmailType { get; set; }
    }
}
