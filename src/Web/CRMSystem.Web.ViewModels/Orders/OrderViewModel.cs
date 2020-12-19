namespace CRMSystem.Web.ViewModels.Orders
{
    using AutoMapper;
    using CRMSystem.Data.Models;
    using CRMSystem.Services.Mapping;
    using System;
    using System.Globalization;
    public class OrderViewModel : IMapFrom<Order>, IHaveCustomMappings
    {
        public DateTime CreatedOn { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public string OrderedDateAsString => this.CreatedOn.ToString("D", CultureInfo.InvariantCulture);
        public void CreateMappings(IProfileExpression configuration)
        {
         
            configuration.CreateMap<Order, OrderViewModel>()
                .ForMember(x => x.ProductName, options =>
                {
              //      options.MapFrom(o => o.Product.Name);
                });

        }
    }
}
