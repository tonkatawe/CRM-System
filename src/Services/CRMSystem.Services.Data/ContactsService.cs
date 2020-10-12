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
        private readonly IPhonesServices phonesServices;
        private readonly IEmailsService emailsService;
        private readonly ISocialNetworksServices socialNetworkService;


        public ContactsService(
            IDeletableEntityRepository<Contact> contactsRepository,
            IPhonesServices phonesServices,
            IEmailsService emailsService,
            ISocialNetworksServices socialNetworkService)
        {
            this.contactsRepository = contactsRepository;
            this.phonesServices = phonesServices;
            this.emailsService = emailsService;
            this.socialNetworkService = socialNetworkService;
        }

        public IEnumerable<T> GetAllUserContacts<T>(string userId)
        {
            var query = this.contactsRepository.All()
                .OrderByDescending(x => x.CreatedOn)
                .Where(x => x.UserId == userId);

            return query.To<T>().ToList();
        }

        public T GetContactDetails<T>(int contactId)
        {
            var query = this.contactsRepository
                .All()
                .Where(x => x.Id == contactId)
                .To<T>()
                .FirstOrDefault();

            return query;
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

        public async Task<int> DeleteContactAsync(int id)
        {
            var contact = await this.contactsRepository
                .GetByIdWithDeletedAsync(id);

            this.contactsRepository.Delete(contact);

            return await this.contactsRepository.SaveChangesAsync();
        }

        public async Task<int> CreateContactAsync(ContactCreateInputModel input, string userId)
        {
            var contact = new Contact
            {
                UserId = userId,
                Title = input.Title,
                FirstName = input.FirstName,
                MiddleName = input.MiddleName,
                OrganizationId = input.OrganizationId,
                LastName = input.LastName,
                JobTitle = input.JobTitle,
                Industry = input.Industry,
                AdditionalInfo = input.AdditionalInfo,
                Address = input.Address,
            };

            await this.contactsRepository.AddAsync(contact);
            await this.contactsRepository.SaveChangesAsync();

            var emailAddress = await this.emailsService.CreateEmailAsync(input.EmailAddress.Email, input.EmailAddress.EmailType, contact.Id);

            var phoneNumber = await this.phonesServices.CreatePhoneAsync(input.PhoneNumber.Phone, input.PhoneNumber.PhoneType,
                    contact.Id);

            var socialNetwork =
                await this.socialNetworkService.CreateSocialNetworkAsync(input.Network.Name, input.SocialNetworkType,
                    contact.Id);

            contact.PhoneNumbers.Add(phoneNumber);
            contact.EmailAddresses.Add(emailAddress);
            contact.SocialNetworks.Add(socialNetwork);

            return contact.Id;
        }
    }
}
