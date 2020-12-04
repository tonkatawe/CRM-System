
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CRMSystem.Data.Models;
using CRMSystem.Services.Mapping;
using CRMSystem.Web.ViewModels.Emails;
using CRMSystem.Web.ViewModels.Phones;

namespace CRMSystem.Web.ViewModels.Accounts
{
    public class MakeAccountViewModel : IMapFrom<Customer>, IHaveCustomMappings
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public IEnumerable<EmailDropDownViewModel> Emails { get; set; }
        public string Email { get; set; }

        public IEnumerable<PhoneDropDownViewModel> Phones { get; set; }

        public string Phone { get; set; }

        public int OrganizationId { get; set; }
        public void CreateMappings(IProfileExpression configuration)
        {

            configuration.CreateMap<Customer, MakeAccountViewModel>()
                .ForMember(x => x.Username, options =>
                    options.MapFrom(x => x.FirstName[0] + "." + x.LastName));

        }
    }
}
