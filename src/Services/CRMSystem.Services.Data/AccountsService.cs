﻿namespace CRMSystem.Services.Data
{
    using CRMSystem.Data.Common.Repositories;
    using CRMSystem.Data.Models;
    using CRMSystem.Services.Data.Contracts;
    using CRMSystem.Web.ViewModels.Accounts;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public class AccountsService : IAccountsService
    {
        private readonly IDeletableEntityRepository<Customer> customersRepository;
        private readonly UserManager<ApplicationUser> userManager;


        public AccountsService(
            IDeletableEntityRepository<Customer> customersRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.customersRepository = customersRepository;
            this.userManager = userManager;
        }

        public async Task<KeyValuePair<string, string>> CreateAsync(CreateAccountInputModel input, ApplicationUser owner)
        {
            var user = new ApplicationUser
            {
                UserName = input.UserName,
                Email = input.Email,
                PhoneNumber = input.Phone,
                Parent = owner,
            };

            var userPassword = Guid.NewGuid().ToString().Substring(0, 8);

            var result = await this.userManager.CreateAsync(user, userPassword);

            if (result.Succeeded)
            {
                await this.userManager.AddToRoleAsync(user, "Customer");
                var token = await this.userManager.GenerateEmailConfirmationTokenAsync(user);

                var customer = await this.customersRepository.GetByIdWithDeletedAsync(input.Id);

                customer.HasAccount = true;
                this.customersRepository.Update(customer);
                await this.customersRepository.SaveChangesAsync();

                return new KeyValuePair<string, string>(token, userPassword);

            }
            else
            {

                throw new Exception(result.ToString());

            }
        }
    }
}
