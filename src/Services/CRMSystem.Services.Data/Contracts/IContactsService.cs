namespace CRMSystem.Services.Data.Contracts
{
    using CRMSystem.Web.ViewModels.Contacts;
    using System.Threading.Tasks;
    public interface IContactsService
    {
        Task<int> CreateAsync(ContactFormViewModel input, string ip);
    }
}
