namespace CRMSystem.Web.ViewModels.Products
{
    using CRMSystem.Data.Models;
    using CRMSystem.Services.Mapping;
    public class EditProductInputModel : ProductCreateInputModel, IMapFrom<Product>
    {
        public int Id { get; set; }

        public string ProductPictureUrl { get; set; }

    }
}
