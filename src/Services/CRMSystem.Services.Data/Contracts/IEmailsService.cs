namespace CRMSystem.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CRMSystem.Data.Models;
    using CRMSystem.Data.Models.Enums;

    public interface IEmailsService
    {
        IEnumerable<T> GetAllContactEmails<T>(int contactId);

        Task<EmailAddress> CreateEmailAsync(string email, EmailType type, int contactId);

        bool IsAvailableEmail(string email);

        Task<int> DeleteEmailAsync(int id);
    }
}
