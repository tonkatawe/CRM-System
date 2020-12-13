using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using CRMSystem.Data.Models;
using CRMSystem.Services.Mapping;

namespace CRMSystem.Web.ViewModels.TemporaryCustomers
{
    public class RejectCustomerViewModel : IMapFrom<Customer>, IHaveCustomMappings
    {
        public string FullName { get; set; }
        public string OrganizationName { get; set; }
        public string Email { get; set; }
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Customer, RejectCustomerViewModel>()
                .ForMember(x => x.Email, options =>
                    options.MapFrom(x => x.Emails.FirstOrDefault().Email));
        }
    }
}
