namespace CRMSystem.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CRMSystem.Web.ViewModels.Organizations;

    public interface IOrganizationsService
    {
        Task<int> CreateOrganizationAsync(OrganizationInputModel input, string userId);

        IEnumerable<T> GetAll<T>(int? count = null);

    }
}
