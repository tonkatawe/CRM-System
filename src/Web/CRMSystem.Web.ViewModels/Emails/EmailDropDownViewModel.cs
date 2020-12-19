
namespace CRMSystem.Web.ViewModels.Emails
{
    using CRMSystem.Data.Models;
    using CRMSystem.Services.Mapping;

    public class EmailDropDownViewModel : IMapFrom<EmailAddress>
    {
        public int Id { get; set; }

        public string Email { get; set; }
    }
}
