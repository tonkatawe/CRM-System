namespace CRMSystem.Services.Data.Contracts
{
    using CRMSystem.Web.ViewModels.Organizations;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IOrganizationsService
    {
        Task<int> CreateAsync(OrganizationCreateInputModel input, string userId);

        IQueryable<T> GetAll<T>(bool isPublic);
        
        string GetId(string userId);
        
        T GetById<T>(string userId);

        string GetName(string userId);

        Task<int> GetCountAsync();

        Task<int> UpdateAsync(EditOrganizationInputModel input);

        Task<int> ChangeStatusAsync(string id, bool isPublic);
        
    }
}
