namespace CRMSystem.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CRMSystem.Web.ViewModels.Organizations;

    public interface IOrganizationsService
    {
        Task<int> CreateAsync(OrganizationCreateInputModel input, string userId);

        IEnumerable<T> GetAll<T>();
        string GetId(string userId);
        T GetById<T>(string userId);

        string GetName(string userId);

        Task<int> GetCountAsync();

        Task<int> UpdateAsync(EditOrganizationInputModel input);



    }
}
