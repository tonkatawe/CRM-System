namespace CRMSystem.Data.Models
{
    using CRMSystem.Data.Common.Models;
    using CRMSystem.Data.Models.Enums;
    using System.ComponentModel.DataAnnotations;

    public class PhoneNumber : BaseModel<int>
    {
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        [Required]
        public string Phone { get; set; }

   
        public PhoneType PhoneType { get; set; }
    }
}
