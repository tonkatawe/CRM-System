namespace CRMSystem.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CRMSystem.Web.ViewModels.Contacts;

    public interface IContactsService
    {
        IEnumerable<T> GetAllUserContacts<T>(string userId);

        Task<int> CreateContactAsync(ContactCreateInputModel input, string userId);
    }
}
