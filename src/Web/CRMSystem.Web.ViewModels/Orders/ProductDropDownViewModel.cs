namespace CRMSystem.Web.ViewModels.Orders
{
    using CRMSystem.Data.Models;
    using CRMSystem.Services.Mapping;
    public class ProductDropDownViewModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
