using System;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using AutoMapper.Internal;
using CRMSystem.Data.Models.Enums;
using CRMSystem.Web.ViewModels.Customers;
using CRMSystem.Web.ViewModels.Emails;
using CRMSystem.Web.ViewModels.Phones;
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

        public T GetById<T>(int customerId)
        {
            var query = this.customersRepository
                .All()
                .Where(x => x.Id == customerId)
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
            var customer = await this.customersRepository
                .GetByIdWithDeletedAsync(id);

            this.customersRepository.Delete(customer);

            return await this.customersRepository.SaveChangesAsync();
        }

        public async Task<int> CreateAsync(CustomerAddInputModel input, string userId)
        {
            var address = await this.addressesService.CreateAsync(input.Address.Country, input.Address.City,
                input.Address.Street, input.Address.ZipCode);
            var organizationId = this.organizationsService.GetById(userId);
            var customer = new Customer
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

            await this.customersRepository.AddAsync(customer);
            await this.customersRepository.SaveChangesAsync();

            foreach (var email in input.Emails)
            {
                var emailAddress = await this.emailsService.CreateAsync(email.Email, email.EmailType, customer.Id);
                customer.EmailAddresses.Add(emailAddress);
            }

            foreach (var phone in input.Phones)
            {
                var phoneNumber = await this.phonesServices.CreateAsync(phone.Phone, phone.PhoneType, customer.Id);
                customer.PhoneNumbers.Add(phoneNumber);
            }

            return customer.Id;
        }


        public async Task<int> UpdateAsync(EditCustomerInputModel input)
        {

            //todo try use reflection about properties 
            var customer = await customersRepository.GetByIdWithDeletedAsync(input.Id);


            //todo check is it necessary
            var customerEmails = this.emailsService.GetAll<EmailCreateInputModel>(input.Id);
            var customerPhones = this.phonesServices.GetAll<PhoneCreateInputModel>(input.Id);

            foreach (var email in input.Emails)
            {
                if (email.Id != null &&
                    (email.Email != customerEmails.FirstOrDefault(x => x.Id == email.Id)?.Email ||
                     email.EmailType != customerEmails.FirstOrDefault(x => x.Id == email.Id)?.EmailType))
                {
                    await this.emailsService.UpdateAsync(email);
                }
                else if (email.Email != null && email.Id == null)
                {

                    await this.emailsService.CreateAsync(email.Email, email.EmailType, customer.Id);
                }
            }

            foreach (var phone in input.Phones)
            {
                if (phone.Id != null &&
                    (phone.Phone != customerPhones.FirstOrDefault(x => x.Id == phone.Id)?.Phone ||
                     phone.PhoneType != customerPhones.FirstOrDefault(x => x.Id == phone.Id)?.PhoneType))
                {
                    await this.phonesServices.UpdateAsync(phone);
                }
                else if (phone.Phone != null && phone.Id == null)
                {

                    await this.phonesServices.CreateAsync(phone.Phone, phone.PhoneType, customer.Id);
                }
            }

            await this.addressesService.UpdateAsync(input.Address);

            customer.Title = input.Title;
            customer.Industry = input.Industry;
            customer.FirstName = input.FirstName;
            customer.MiddleName = input.MiddleName;
            customer.LastName = input.LastName;
            customer.AdditionalInfo = input.AdditionalInfo;
            customer.JobTitle = input.JobTitle;

            this.customersRepository.Update(customer);

            return await this.customersRepository.SaveChangesAsync();
        }

    }
}
