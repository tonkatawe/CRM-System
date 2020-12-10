
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        public string Username { get; set; }

     
        public string FullName { get; set; }

        public IEnumerable<EmailDropDownViewModel> Emails { get; set; }

        [Required]
        public string Email { get; set; }

        public IEnumerable<PhoneDropDownViewModel> Phones { get; set; }

        [Required]
        public string Phone { get; set; }

        public string OrganizationId { get; set; }
        public void CreateMappings(IProfileExpression configuration)
        {

            configuration.CreateMap<Customer, MakeAccountViewModel>()
                .ForMember(x => x.Username, options =>
                    options.MapFrom(x => x.FirstName[0] + "." + x.LastName));

        }
    }
}
