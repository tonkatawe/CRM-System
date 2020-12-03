
using System.Linq;
using AutoMapper;
using CRMSystem.Data.Models;
using CRMSystem.Services.Mapping;

namespace CRMSystem.Web.ViewModels.Accounts
{
    public class MakeAccountViewModel : IMapFrom<Customer>, IHaveCustomMappings
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }
        public void CreateMappings(IProfileExpression configuration)
        {
          
            configuration.CreateMap<Customer, MakeAccountViewModel>()
                .ForMember(x => x.Username, options =>
                    options.MapFrom(x => x.FirstName[0] + "." + x.LastName));
        }
    }
}
