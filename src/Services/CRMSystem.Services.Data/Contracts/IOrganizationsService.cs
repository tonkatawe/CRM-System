namespace CRMSystem.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using CRMSystem.Web.ViewModels.Organizations;

    public interface IOrganizationsService
    {
        Task<int> CreateOrganizationAsync(OrganizationInputModel input, string userId);
    }
}
