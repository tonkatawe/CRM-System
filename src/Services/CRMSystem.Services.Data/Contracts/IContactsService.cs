namespace CRMSystem.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CRMSystem.Web.ViewModels.Contacts;

    public interface IContactsService
    {
        IEnumerable<T> GetAllUserContacts<T>(string userId);

        IEnumerable<T> GetByOrganization<T>(int organizationId, int skip = 0);

        IEnumerable<T> GetByCreatedOn<T>(int skip = 0);

        int AllContactCount();

        int AllContactInOrganizationCount();

        Task<int> DeleteContactAsync(int id);

        // TODO: Make change info at contact

        Task<int> CreateContactAsync(ContactCreateInputModel input, string userId);
    }
}
