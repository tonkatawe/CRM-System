using System;
using System.Globalization;
using AutoMapper;
using CRMSystem.Data.Models;
using CRMSystem.Data.Models.Enums;
using CRMSystem.Services.Mapping;

namespace CRMSystem.Web.ViewModels.Customers
{
    public class CustomerViewModel : IMapFrom<Customer>, IHaveCustomMappings
    {

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }


        public string FullName => string.IsNullOrEmpty(this.MiddleName)
            ? (this.FirstName + " " + this.LastName)
            : (this.FirstName + " " + this.MiddleName + " " + this.LastName);

        public string Industry { get; set; }
        public int OrdersCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
    
            configuration.CreateMap<Customer, CustomerViewModel>()
                .ForMember(x => x.Industry, options =>
                {
                    options.MapFrom(c => c.Industry.ToString());
                });

            configuration.CreateMap<Customer, CustomerViewModel>()
                .ForMember(x => x.OrdersCount, options =>
                {
                    options.MapFrom(c => c.Orders.Count);
                });
        }
    }
}
