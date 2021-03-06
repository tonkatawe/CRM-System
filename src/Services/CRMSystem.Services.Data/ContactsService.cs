﻿namespace CRMSystem.Services.Data
{
    using CRMSystem.Data.Common.Repositories;
    using CRMSystem.Data.Models;
    using CRMSystem.Services.Data.Contracts;
    using CRMSystem.Web.ViewModels.Contacts;
    using System.Threading.Tasks;
    
    public class ContactsService : IContactsService
    {
        private readonly IRepository<ContactFormMessage> contactsRepository;

        public ContactsService(IRepository<ContactFormMessage> contactsRepository)
        {
            this.contactsRepository = contactsRepository;
        }

        public async Task<int> CreateAsync(ContactFormViewModel input, string ip)
        {
            var contactFormMessage = new ContactFormMessage
            {
                Name = input.Name,
                Email = input.Email,
                Title = input.Title,
                Content = input.Content,
                Ip = ip,
            };
            await this.contactsRepository.AddAsync(contactFormMessage);
            return await this.contactsRepository.SaveChangesAsync();
        }
    }
}
