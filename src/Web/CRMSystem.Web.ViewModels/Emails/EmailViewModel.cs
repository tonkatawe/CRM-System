namespace CRMSystem.Web.ViewModels.Emails
{
    using CRMSystem.Data.Models;
    using CRMSystem.Data.Models.Enums;
    using CRMSystem.Services.Mapping;

    public class EmailViewModel : IMapFrom<EmailAddress>
    {
        public int Id { get; set; }
        public int ContactId { get; set; }

        public string Email { get; set; }

        public Customer Contact { get; set; }

        public EmailType EmailType { get; set; }
    }
}
