namespace CRMSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using CRMSystem.Data.Common.Models;
    using CRMSystem.Data.Models.Enums;

    public class EmailAddress : BaseDeletableModel<int>
    {
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        [Required]
        public string Email { get; set; }

        public EmailType EmailType { get; set; }
    }
}
