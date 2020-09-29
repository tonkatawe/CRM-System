namespace CRMSystem.Data.Models
{
    using CRMSystem.Data.Common.Models;
    using CRMSystem.Data.Models.Enums;

    public class PhoneNumber : BaseDeletableModel<int>
    {
        public int ContactId { get; set; }

        public Contact Contact { get; set; }

        public PhoneType PhoneType { get; set; }
    }
}
