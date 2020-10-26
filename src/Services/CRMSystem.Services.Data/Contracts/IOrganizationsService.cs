namespace CRMSystem.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CRMSystem.Web.ViewModels.Organizations;

    public interface IOrganizationsService
    {
        Task<int> CreateOrganizationAsync(OrganizationCreateInputModel input, string userId);

        IEnumerable<T> GetAll<T>(string userId, int? count = null);

        int AllOrganizationCount(int organizationId);

        T GetOrganizationById<T>(int contactId);

    }
}
