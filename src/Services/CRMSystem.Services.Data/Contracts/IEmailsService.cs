namespace CRMSystem.Services.Data.Contracts
{
    using CRMSystem.Data.Models;
    using CRMSystem.Data.Models.Enums;
    using CRMSystem.Web.ViewModels.Emails;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEmailsService
    {
        IEnumerable<T> GetAll<T>(int customerId);

        Task DeleteAllAsync(int customerId);

        Task<EmailAddress> CreateAsync(string email, EmailType type, int customerId);

        Task<int> DeleteAsync(int customerId);

        Task<int> UpdateAsync(EmailCreateInputModel input);
    }
}
