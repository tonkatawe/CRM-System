using System;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using AutoMapper.Internal;
using CRMSystem.Data.Models.Enums;
using CRMSystem.Web.ViewModels.Customers;
using CRMSystem.Web.ViewModels.Emails;
using Microsoft.EntityFrameworkCore.Infrastructure;

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

    public class CustomersService : ICustomersService
    {
        private readonly IDeletableEntityRepository<Customer> customersRepository;
        private readonly IAddressesService addressesService;
        private readonly IPhonesServices phonesServices;
        private readonly IEmailsService emailsService;
        private readonly ISocialNetworksServices socialNetworkService;
        private readonly IOrganizationsService organizationsService;


        public CustomersService(
            IDeletableEntityRepository<Customer> customersRepository,
            IAddressesService addressesService,
            IPhonesServices phonesServices,
            IEmailsService emailsService,
            ISocialNetworksServices socialNetworkService,
            IOrganizationsService organizationsService)
        {
            this.customersRepository = customersRepository;
            this.addressesService = addressesService;
            this.phonesServices = phonesServices;
            this.emailsService = emailsService;
            this.socialNetworkService = socialNetworkService;
            this.organizationsService = organizationsService;
        }

        public IQueryable<T> GetAll<T>(string userId)
        {
            var query = this.customersRepository.All()
                .OrderByDescending(x => x.CreatedOn)
                .Where(x => x.UserId == userId)
                .To<T>()
                .AsQueryable();

            return query;
        }

        public T GetById<T>(int contactId)
        {
            var query = this.customersRepository
                .All()
                .Where(x => x.Id == contactId)
                .To<T>()
                .FirstOrDefault();
            return query;



        }

        public IEnumerable<T> GetByName<T>(string userId, int skip = 0)
        {
            var query = this.customersRepository.All()
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .Where(x => x.UserId == userId)
                .To<T>()
                .ToList();

            return query;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var contact = await this.customersRepository
                .GetByIdWithDeletedAsync(id);

            this.customersRepository.Delete(contact);

            return await this.customersRepository.SaveChangesAsync();
        }

        public async Task<int> CreateSync(CustomerAddInputModel input, string userId)
        {
            var address = await this.addressesService.CreateAddressAsync(input.Address.Country, input.Address.City,
                input.Address.Street, input.Address.ZipCode);
            var organizationId = this.organizationsService.GetOrganizationId(userId);
            var contact = new Customer
            {
                Address = address,
                UserId = userId,
                OrganizationId = organizationId,
                Title = input.Title,
                FirstName = input.FirstName,
                MiddleName = input.MiddleName,
                LastName = input.LastName,
                JobTitle = input.JobTitle,
                Industry = input.Industry,
                AdditionalInfo = input.AdditionalInfo,
            };

            await this.customersRepository.AddAsync(contact);
            await this.customersRepository.SaveChangesAsync();

            foreach (var email in input.Emails)
            {
                var emailAddress = await this.emailsService.CreateEmailAsync(email.Email, email.EmailType, contact.Id);
                contact.EmailAddresses.Add(emailAddress);
            }

            foreach (var phone in input.Phones)
            {
                var phoneNumber = await this.phonesServices.CreateAsync(phone.Phone, phone.PhoneType, contact.Id);
                contact.PhoneNumbers.Add(phoneNumber);
            }

        

            return contact.Id;
        }


        public async Task<int> UpdateAsync(EditCustomerInputModel input)
        {
         
            //todo try use reflection about properties 
            var customer = await customersRepository.GetByIdWithDeletedAsync(input.Id);
            var customerEmails = this.emailsService.GetAllCustomerEmails<EmailCreateInputModel>(input.Id);
            foreach (var email in input.Emails)
            {
                if (email.Id != null &&
                    (email.Email != customerEmails.FirstOrDefault(x => x.Id == email.Id)?.Email ||
                     email.EmailType != customerEmails.FirstOrDefault(x => x.Id == email.Id).EmailType))
                {
                    await this.emailsService.UpdateEmailAsync(email);
                }
                else if (email.Email != null && email.Id == null)
                {

                    await this.emailsService.CreateEmailAsync(email.Email, email.EmailType, customer.Id);
                }
            }

            //customer.PhoneNumbers = input.Phones;
            //customer.Industry = input.Industry;
            //customer.SocialNetworks = input.SocialNetworks;
            //customer.Address = input.Address;
            //customer.FirstName = input.FirstName;
            //customer.MiddleName = input.MiddleName;
            //customer.LastName = input.LastName;
            //customer.AdditionalInfo = input.AdditionalInfo;
            //customer.JobTitle = input.JobTitle;

            this.customersRepository.Update(customer);

            return await this.customersRepository.SaveChangesAsync();
        }

    }
}
