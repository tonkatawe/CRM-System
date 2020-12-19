namespace CRMSystem.Web.ViewModels.Accounts
{
    using AutoMapper;
    using CRMSystem.Data.Models;
    using CRMSystem.Services.Mapping;
    using System.Linq;
    public class CreateAccountInputModel : IMapFrom<Customer>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }


        public string Email { get; set; }

        public string Phone { get; set; }

        public string UserId { get; set; }

        public string OrganizationName { get; set; }
        
        public string OrganizationId { get; set; }

        public bool HasAccount { get; set; }
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Customer, CreateAccountInputModel>()
                .ForMember(u => u.UserName, options =>
                    options.MapFrom(u => u.FirstName[0] + "." + u.LastName))
                .ForMember(e => e.Email, options =>
                     options.MapFrom(e => e.Emails.FirstOrDefault().Email))
                .ForMember(p => p.Phone, options =>
                options.MapFrom(p => p.Phones.FirstOrDefault().Phone));
        }
    }
}
