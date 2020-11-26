namespace CRMSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using CRMSystem.Data.Common.Models;

    public class Order : BaseDeletableModel<int>
    {


        [Required]
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        public int OrganizationId { get; set; }

        public Organization Organization { get; set; }

      


    }
}
