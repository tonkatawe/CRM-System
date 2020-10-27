using System.Linq;

namespace CRMSystem.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CRMSystem.Web.ViewModels.Contacts;

    public interface IContactsService
    {
        IQueryable<T> GetAllUserContacts<T>(string userId);

        T GetContactById<T>(int contactId);

        IEnumerable<T> GetByOrganization<T>(string userId, int organizationId, int skip = 0);

        IEnumerable<T> GetByName<T>(string userId, int skip = 0);

        int AllContactCount(string userId);

        int AllContactInOrganizationCount(int organizationId);

        Task<int> DeleteContactAsync(int id);

        // TODO: Make change info at contact
        Task<int> CreateContactAsync(ContactCreateInputModel input, string userId);

        Task<int> AddToOrganizationAsync(int? contactId, int organizationId);

        Task<int> UpdateContact(EditContactInputModel input);

    }
}
