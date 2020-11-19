namespace CRMSystem.Data.Models
{
   public class SaleProducts
    {
        public int Id { get; set; }

        public int SaleId { get; set; }

        public virtual Sale Sale { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
