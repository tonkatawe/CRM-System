﻿namespace CRMSystem.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using CRMSystem.Data.Common.Repositories;
    using CRMSystem.Data.Models;
    using CRMSystem.Services.Data.Contracts;
    using CRMSystem.Services.Mapping;

    public class EmailsService : IEmailsService
    {
        private readonly IDeletableEntityRepository<EmailAddress> emailRepository;

        public EmailsService(IDeletableEntityRepository<EmailAddress> emailRepository)
        {
            this.emailRepository = emailRepository;
        }

        public IEnumerable<T> GetAllContactEmails<T>(int contactId)
        {
            var query = this.emailRepository.All()
                .Where(x => x.ContactId == contactId);

            return query.To<T>().ToList();
        }
    }
}
