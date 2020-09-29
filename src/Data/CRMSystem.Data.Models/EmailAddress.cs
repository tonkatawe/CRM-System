namespace CRMSystem.Data.Models
{

    using CRMSystem.Data.Common.Models;
    using CRMSystem.Data.Models.Enums;

    public class EmailAddress : BaseDeletableModel<int>
    {
        public int ContactId { get; set; }

        public Contact Contact { get; set; }

        public EmailType EmailType { get; set; }
    }
}
