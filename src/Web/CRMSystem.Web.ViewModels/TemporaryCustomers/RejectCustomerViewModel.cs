namespace CRMSystem.Web.ViewModels.TemporaryCustomers
{
    using AutoMapper;
    using CRMSystem.Data.Models;
    using CRMSystem.Services.Mapping;
    using System.Linq;
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
