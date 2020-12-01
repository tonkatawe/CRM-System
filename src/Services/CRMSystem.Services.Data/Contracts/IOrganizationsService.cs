namespace CRMSystem.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CRMSystem.Web.ViewModels.Organizations;

    public interface IOrganizationsService
    {
        Task<int> CreateOrganizationAsync(OrganizationCreateInputModel input, string userId);

        int GetById(string userId);

        string GetName(string userId);

        Task<int> GetCountAsync();

    }
}
