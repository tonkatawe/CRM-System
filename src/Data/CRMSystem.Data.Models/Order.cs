namespace CRMSystem.Data.Models
{
    using CRMSystem.Data.Common.Models;
    using System.ComponentModel.DataAnnotations;

    public class Order : BaseDeletableModel<int>
    {


        [Required]
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        public string OrganizationId { get; set; }

        public Organization Organization { get; set; }

      


    }
}
