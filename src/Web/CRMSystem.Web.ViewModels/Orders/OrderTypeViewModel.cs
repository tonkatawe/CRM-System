namespace CRMSystem.Web.ViewModels.Orders
{
    using AutoMapper;
    using CRMSystem.Data.Models;
    using CRMSystem.Services.Mapping;
    
    public class OrderTypeViewModel : IMapFrom<Order>, IHaveCustomMappings
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }
        public string ProductName { get; set; }

     
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Order, OrderTypeViewModel>()
                .ForMember(x => x.ProductName, options =>
                {
                    options.MapFrom(o => o.Product.Name);
                });


        }
        
    }
}
