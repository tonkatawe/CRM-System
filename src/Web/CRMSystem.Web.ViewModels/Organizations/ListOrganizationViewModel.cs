
using System.Runtime.CompilerServices;
using AutoMapper;
using CRMSystem.Data.Models;
using CRMSystem.Services.Mapping;

namespace CRMSystem.Web.ViewModels.Organizations
{
    public class ListOrganizationViewModel : IMapFrom<Organization>, IHaveCustomMappings
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string Industry { get; set; }

        public string ShortDescription => this.Description.Length <= 50
            ? this.Description
            : this.Description.Substring(0, 50) + "....";

        public bool NotEqualDescription => this.ShortDescription.Length != this.Description.Length;
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Organization, ListOrganizationViewModel>()
                .ForMember(x => x.Industry, options =>
                    options.MapFrom(x => x.Industry.ToString()));
        }
    }
}
