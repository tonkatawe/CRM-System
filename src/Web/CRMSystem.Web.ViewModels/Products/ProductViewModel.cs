namespace CRMSystem.Web.ViewModels.Products
{
    using CRMSystem.Data.Models;
    using CRMSystem.Services.Mapping;
    
    public class ProductViewModel : IMapFrom<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string ProductPictureUrl { get; set; }

    }
}
