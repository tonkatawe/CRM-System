﻿using CRMSystem.Web.ViewModels.Emails;

namespace CRMSystem.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CRMSystem.Data.Models;
    using CRMSystem.Data.Models.Enums;

    public interface IEmailsService
    {
        IEnumerable<T> GetAll<T>(int contactId);

        Task<EmailAddress> CreateAsync(string email, EmailType type, int customerId);

        Task<int> DeleteAsync(int id);

        Task<int> UpdateAsync(EmailCreateInputModel input);
    }
}
