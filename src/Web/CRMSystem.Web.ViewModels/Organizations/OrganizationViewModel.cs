namespace CRMSystem.Web.ViewModels.Organizations
{
    using AutoMapper;
    using CRMSystem.Data.Models;
    using CRMSystem.Services.Mapping;
    using CRMSystem.Web.ViewModels.Customers;
    using CRMSystem.Web.ViewModels.Products;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;

    public class OrganizationViewModel : IMapFrom<Organization>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int AddressId { get; set; }
        public string Industry { get; set; }

        public bool IsPublic { get; set; }

        public Address Address { get; set; }

        public IEnumerable<CustomerViewModel> Customers { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; }
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Organization, OrganizationViewModel>()
                .ForMember(x => x.Industry, options =>
                {
                    options.MapFrom(c => c.Industry
                        .GetType()
                        .GetMember(c.Industry.ToString())
                        .FirstOrDefault()
                        .GetCustomAttribute<DisplayAttribute>(false).Name);

                });
        }
    }
}
