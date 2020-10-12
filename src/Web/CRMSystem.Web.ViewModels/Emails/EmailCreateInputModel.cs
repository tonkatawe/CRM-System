namespace CRMSystem.Web.ViewModels.Emails
{
    using CRMSystem.Data.Models;
    using CRMSystem.Data.Models.Enums;
    using CRMSystem.Services.Mapping;

    public class EmailCreateInputModel : IMapFrom<EmailAddress>
    {
        public int ContactId { get; set; }

        public string Email { get; set; }

        public Contact Contact { get; set; }

        public EmailType EmailType { get; set; }
    }
}
