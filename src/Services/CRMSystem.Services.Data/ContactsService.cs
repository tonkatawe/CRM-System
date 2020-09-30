namespace CRMSystem.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CRMSystem.Data.Common.Repositories;
    using CRMSystem.Data.Models;
    using CRMSystem.Services.Data.Contracts;
    using CRMSystem.Services.Mapping;
    using CRMSystem.Web.ViewModels.Contacts;

    public class ContactsService : IContactsService
    {
        private readonly IDeletableEntityRepository<Contact> contactsRepository;

        public ContactsService(
            IDeletableEntityRepository<Contact> contactsRepository)
        {
            this.contactsRepository = contactsRepository;
        }

        public IEnumerable<T> GetAllUserContacts<T>(string userId)
        {
            var query = this.contactsRepository.All()
                .OrderByDescending(x => x.CreatedOn)
                .Where(x => x.UserId == userId);

            return query.To<T>().ToList();
        }

        public async Task<int> CreateContactAsync(ContactCreateInputModel input)
        {
            var contact = new Contact
            {
                Title = input.Title,
                FirstName = input.FirstName,
                MiddleName = input.MiddleName,
                LastName = input.LastName,
                JobTitle = input.JobTitle,
                Organization = input.Organization,
                Industry = input.Industry,
                AdditionalInfo = input.AdditionalInfo,
                Address = input.Address,
            };
            await this.contactsRepository.AddAsync(contact);
            await this.contactsRepository.SaveChangesAsync();
            return contact.Id;
        }
    }
}
