namespace CRMSystem.Services.Data.Contracts
{
    using System.Collections.Generic;

    public interface IEmailsService
    {
        IEnumerable<T> GetAllContactEmails<T>(int userId);
    }
}
