namespace CRMSystem.Web.ViewModels.Organizations
{
    using CRMSystem.Data.Models;
    using CRMSystem.Services.Mapping;
    public class EditOrganizationInputModel : OrganizationCreateInputModel, IMapFrom<Organization>
    {
        public string Id { get; set; }

    }
}
