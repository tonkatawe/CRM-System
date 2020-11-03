using System.Reflection;
using System.Security.Cryptography.X509Certificates;

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
        private readonly IAddressesService addressesService;
        private readonly IPhonesServices phonesServices;
        private readonly IEmailsService emailsService;
        private readonly ISocialNetworksServices socialNetworkService;


        public ContactsService(
            IDeletableEntityRepository<Contact> contactsRepository,
            IAddressesService addressesService,
            IPhonesServices phonesServices,
            IEmailsService emailsService,
            ISocialNetworksServices socialNetworkService)
        {
            this.contactsRepository = contactsRepository;
            this.addressesService = addressesService;
            this.phonesServices = phonesServices;
            this.emailsService = emailsService;
            this.socialNetworkService = socialNetworkService;
        }

        public IQueryable<T> GetAllUserContacts<T>(string userId)
        {
            var query = this.contactsRepository.All()
                .OrderByDescending(x => x.CreatedOn)
                .Where(x => x.UserId == userId)
                .To<T>()
                .AsQueryable();

            return query;
        }

        public T GetContactById<T>(int contactId)
        {
            var query = this.contactsRepository
                .All()
                .Where(x => x.Id == contactId)
                .To<T>()
                .FirstOrDefault();
            return query;



        }

        public IEnumerable<T> GetByOrganization<T>(string userId, int organizationId, int skip = 0)
        {
            var query = this.contactsRepository.All()
                .Where(x => x.UserId == userId && x.OrganizationId == organizationId)
                .To<T>()
                .ToList();

            return query;
        }

        public IEnumerable<T> GetByName<T>(string userId, int skip = 0)
        {
            var query = this.contactsRepository.All()
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .Where(x => x.UserId == userId)
                .To<T>()
                .ToList();

            return query;
        }

        public int AllContactCount(string userId)
        {
            return this.contactsRepository
                .All()
                .Count(x => x.UserId == userId);
        }

        public int AllContactInOrganizationCount(int organizationId)
        {
            return this.contactsRepository.All()
                .Count(x => x.OrganizationId == organizationId);
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
            var address = await this.addressesService.CreateAddressAsync(input.Address.Country, input.Address.City,
                input.Address.Street, input.Address.ZipCode);

            var contact = new Contact
            {
                Address = address,
                UserId = userId,
                OrganizationId = null,
                Title = input.Title,
                FirstName = input.FirstName,
                MiddleName = input.MiddleName,
                LastName = input.LastName,
                JobTitle = input.JobTitle,
                Industry = input.Industry,
                AdditionalInfo = input.AdditionalInfo,
            };

            await this.contactsRepository.AddAsync(contact);
            await this.contactsRepository.SaveChangesAsync();

            var emailAddress = await this.emailsService.CreateEmailAsync(input.Email.Email, input.Email.EmailType, contact.Id);

            var phoneNumber = await this.phonesServices.CreatePhoneAsync(input.PhoneNumber.Phone, input.PhoneNumber.PhoneType,
                    contact.Id);

            var socialNetwork =
                await this.socialNetworkService.CreateSocialNetworkAsync(input.Network.Name, input.Network.SocialNetworkType,
                    contact.Id);

            contact.PhoneNumbers.Add(phoneNumber);
            contact.EmailAddresses.Add(emailAddress);
            contact.SocialNetworks.Add(socialNetwork);

            return contact.Id;
        }

        public async Task<int> AddToOrganizationAsync(int? contactId, int organizationId)
        {
            var contact = this.contactsRepository.All()
                .FirstOrDefault(x => x.Id == contactId);

            contact.OrganizationId = organizationId;

            await this.contactsRepository.SaveChangesAsync();

            return contact.Id;

        }

        public async Task<int> UpdateContact(EditContactInputModel input)
        {
       
            //todo try use reflection about properties 
            var contact = await contactsRepository.GetByIdWithDeletedAsync(input.Id);



            contact.PhoneNumbers = input.PhoneNumbers;
            contact.EmailAddresses = input.EmailAddresses;
            contact.SocialNetworks = input.SocialNetworks;
            contact.Address = input.Address;
            contact.FirstName = input.FirstName;
            contact.MiddleName = input.MiddleName;
            contact.LastName = input.LastName;
            contact.AdditionalInfo = input.AdditionalInfo;
            contact.JobTitle = input.JobTitle;

            this.contactsRepository.Update(contact);

            return await this.contactsRepository.SaveChangesAsync();
        }

        //public async Task<int> UpdateContact(EditContactInputModel input)
        //{
        //    var contact = await contactsRepository.GetByIdWithDeletedAsync(input.Id);
        //    contact.FirstName = input.
        //}
    }
}
