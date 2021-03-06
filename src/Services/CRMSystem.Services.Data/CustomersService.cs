﻿namespace CRMSystem.Services.Data
{
    using CRMSystem.Data.Common.Repositories;
    using CRMSystem.Data.Models;
    using CRMSystem.Services.Data.Contracts;
    using CRMSystem.Services.Mapping;
    using CRMSystem.Web.ViewModels.Customers;
    using CRMSystem.Web.ViewModels.Emails;
    using CRMSystem.Web.ViewModels.Phones;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

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
            IOrganizationsService organizationsService)
        {
            this.customersRepository = customersRepository;
            this.addressesService = addressesService;
            this.phonesServices = phonesServices;
            this.emailsService = emailsService;
            this.organizationsService = organizationsService;
        }

        public IQueryable<T> GetAll<T>(string userId, bool isTemporary)
        {
            var query = this.customersRepository.All()
                .Where(x => x.UserId == userId && x.IsTemporary == isTemporary)
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

        public async Task<string> CustomerUserIdAsync(int id)
        {
            var customer = await this.customersRepository.GetByIdWithDeletedAsync(id);

            return customer.UserId;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var customer = await this.customersRepository
                .GetByIdWithDeletedAsync(id);

            await this.emailsService.DeleteAllAsync(id);

            await this.phonesServices.DeleteAllAsync(id);

            if (customer.IsTemporary)
            {
                this.customersRepository.HardDelete(customer);

            }
            else
            {
                this.customersRepository.Delete(customer);
            }

            return await this.customersRepository.SaveChangesAsync();

        }

        public async Task<int> CreateAsync(CustomerAddInputModel input, string userId, string organizationId, bool isTemporary)
        {
            var address = await this.addressesService.CreateAsync(input.Address.Country, input.Address.City,
                input.Address.Street, input.Address.ZipCode);
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
                AdditionalInfo = input.AdditionalInfo,
                IsTemporary = isTemporary,
            };

            await this.customersRepository.AddAsync(customer);
            await this.customersRepository.SaveChangesAsync();


            foreach (var email in input.Emails)
            {
                var emailAddress = await this.emailsService.CreateAsync(email.Email, email.EmailType, customer.Id);
                customer.Emails.Add(emailAddress);
            }

            foreach (var phone in input.Phones)
            {
                var phoneNumber = await this.phonesServices.CreateAsync(phone.Phone, phone.PhoneType, customer.Id);
                customer.Phones.Add(phoneNumber);
            }


            return customer.Id;
        }


        public async Task<int> UpdateAsync(EditCustomerInputModel input)
        {
            var customer = await customersRepository.GetByIdWithDeletedAsync(input.Id);
            
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
            customer.FirstName = input.FirstName;
            customer.MiddleName = input.MiddleName;
            customer.LastName = input.LastName;
            customer.AdditionalInfo = input.AdditionalInfo;
            customer.JobTitle = input.JobTitle;

            this.customersRepository.Update(customer);

            return await this.customersRepository.SaveChangesAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await this.customersRepository
                .All()
                .CountAsync();
        }
    }
}
