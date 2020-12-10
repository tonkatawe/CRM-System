
using CRMSystem.Data.Models;
using CRMSystem.Services.Mapping;

namespace CRMSystem.Web.ViewModels.Organizations
{
    public class EditOrganizationInputModel : OrganizationCreateInputModel
    {
        public string Id { get; set; }
    }
}
