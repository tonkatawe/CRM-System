
using System.Collections.Generic;
using AutoMapper;
using CRMSystem.Data.Models;
using CRMSystem.Services.Mapping;

namespace CRMSystem.Web.ViewModels.Products
{
    public class IndexProductsViewModel : IMapFrom<Organization>, IHaveCustomMappings
    {
        public IEnumerable<ProductViewModel> Products { get; set; }

        public string Name { get; set; }

        public string UserEmail { get; set; }
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Organization, IndexProductsViewModel>()
                .ForMember(x => x.Products, option =>
                    option.MapFrom(x => x.Products));
        }
    }
}
