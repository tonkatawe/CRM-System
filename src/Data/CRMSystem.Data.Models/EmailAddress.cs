namespace CRMSystem.Data.Models
{
    using CRMSystem.Data.Common.Models;
    using CRMSystem.Data.Models.Enums;
    using System.ComponentModel.DataAnnotations;

    public class EmailAddress : BaseModel<int>
    {
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        [Required]
        public string Email { get; set; }

        public EmailType EmailType { get; set; }
    }
}
