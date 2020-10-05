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
        private readonly IDeletableEntityRepository<PhoneNumber> phonesRepository;
        private readonly IDeletableEntityRepository<EmailAddress> emailRepository;
        private readonly IDeletableEntityRepository<SocialNetwork> socialNetworkRepository;

        public ContactsService(
            IDeletableEntityRepository<Contact> contactsRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IDeletableEntityRepository<PhoneNumber> phonesRepository,
            IDeletableEntityRepository<EmailAddress> emailRepository,
            IDeletableEntityRepository<SocialNetwork> socialNetworkRepository)
        {
            this.contactsRepository = contactsRepository;
            this.phonesRepository = phonesRepository;
            this.emailRepository = emailRepository;
            this.socialNetworkRepository = socialNetworkRepository;
        }

        public IEnumerable<T> GetAllUserContacts<T>(string userId)
        {
            var query = this.contactsRepository.All()
                .OrderByDescending(x => x.CreatedOn)
                .Where(x => x.UserId == userId);

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetByOrganization<T>(int organizationId, int skip = 0)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<T> GetByCreatedOn<T>(int skip = 0)
        {
            throw new System.NotImplementedException();
        }

        public int AllContactCount()
        {
            throw new System.NotImplementedException();
        }

        public int AllContactInOrganizationCount()
        {
            throw new System.NotImplementedException();
        }

        public Task<int> DeleteContactAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> CreateContactAsync(ContactCreateInputModel input, string userId)
        {
            var contact = new Contact
            {
                UserId = userId,
                Title = input.Title,
                FirstName = input.FirstName,
                MiddleName = input.MiddleName,
                LastName = input.LastName,
                JobTitle = input.JobTitle,
                Organization = new Organization
                {
                    UserId = userId,
                    Name = input.Organization.Name,
                    Address = input.Organization.Address,
                },
                Industry = input.Industry,
                AdditionalInfo = input.AdditionalInfo,
                Address = input.Address,
            };

            await this.contactsRepository.AddAsync(contact);
            await this.contactsRepository.SaveChangesAsync();

            var phoneNumber = new PhoneNumber
            {
                ContactId = contact.Id,
                Phone = input.PhoneNumber.Phone,
                PhoneType = input.PhoneNumber.PhoneType,
            };

            await this.phonesRepository.AddAsync(phoneNumber);
            await this.phonesRepository.SaveChangesAsync();

            var emailAddress = new EmailAddress
            {
                ContactId = contact.Id,
                Email = input.EmailAddress.Email,
                EmailType = input.EmailAddress.EmailType,
            };

            await this.emailRepository.AddAsync(emailAddress);
            await this.emailRepository.SaveChangesAsync();

            var socialNetwork = new SocialNetwork
            {
                ContactId = contact.Id,
                networkTitle = input.networkTitle.networkTitle,
                SocialNetworkType = input.networkTitle.SocialNetworkType,
            };

            await this.socialNetworkRepository.AddAsync(socialNetwork);
            await this.socialNetworkRepository.SaveChangesAsync();

            return contact.Id;
        }
    }
}
