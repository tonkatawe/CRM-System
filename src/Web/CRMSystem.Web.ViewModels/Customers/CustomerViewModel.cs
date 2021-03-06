﻿namespace CRMSystem.Web.ViewModels.Customers
{
    using AutoMapper;
    using CRMSystem.Data.Models;
    using CRMSystem.Services.Mapping;
    public class CustomerViewModel : IMapFrom<Customer>, IHaveCustomMappings
    {

        public int Id { get; set; }

        public string OrganizationId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public int OrdersCount { get; set; }

        public bool IsTemporary { get; set; }

        public bool HasAccount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {



            configuration.CreateMap<Customer, CustomerViewModel>()
                .ForMember(x => x.OrdersCount, options =>
                {
                    options.MapFrom(c => c.Orders.Count);
                });
        }
    }
}
