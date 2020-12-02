
using CRMSystem.Data.Models;
using CRMSystem.Services.Mapping;

namespace CRMSystem.Web.ViewModels.Organizations
{
    public class EditOrganizationInputModel:IMapFrom<Organization>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public Address  Address { get; set; }
    }
}
