﻿using Microsoft.EntityFrameworkCore;

namespace CRMSystem.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CRMSystem.Data.Common.Repositories;
    using CRMSystem.Data.Models;
    using CRMSystem.Services.Data.Contracts;
    using CRMSystem.Services.Mapping;
    using CRMSystem.Web.ViewModels.Organizations;

    public class OrganizationsService : IOrganizationsService
    {
        private readonly IAddressesService addressesService;
        private readonly IDeletableEntityRepository<Organization> organizationRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public OrganizationsService(
            IAddressesService addressesService,
            IDeletableEntityRepository<Organization> organizationRepository,
          IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.addressesService = addressesService;
            this.organizationRepository = organizationRepository;
            this.userRepository = userRepository;
        }

        public async Task<int> CreateOrganizationAsync(OrganizationCreateInputModel input, string userId)
        {
            var address = await this.addressesService.CreateAsync(input.Address.Country, input.Address.City,
                input.Address.Street, input.Address.ZipCode);
            var user = await this.userRepository.GetByIdWithDeletedAsync(userId);
            var organization = new Organization
            {
                UserId = userId,
                Name = input.Name,
                Description = input.Description,
                Address = address,
            };
            //new for test
            user.Organization = organization;
            //await this.organizationRepository.AddAsync(organization);
            //await this.organizationRepository.SaveChangesAsync();
            user.HasOrganization = true;
            this.userRepository.Update(user);

            return await this.userRepository.SaveChangesAsync();

        }



        public int GetId(string userId)
        {
            var query = this.organizationRepository
                .All()
                .FirstOrDefault(x => x.UserId == userId);

            return query.Id;
        }

        public T GetById<T>(string userId)
        {
            var query = this.organizationRepository
                .All()
                .Where(x => x.UserId == userId)
                .To<T>()
                .FirstOrDefault();

            return query;
        }

        public string GetName(string userId)
        {
            return this.organizationRepository
                .All()
                .FirstOrDefault(x => x.UserId == userId)
                .Name;
        }

        public async Task<int> GetCountAsync()
        {
            return await this.organizationRepository.All().CountAsync();
        }

        public async Task<int> UpdateAsync(EditOrganizationInputModel input)
        {
            var organization = await this.organizationRepository.GetByIdWithDeletedAsync(input.Id);

            await this.addressesService.UpdateAsync(input.Address);


            organization.Name = input.Name;
            organization.Description = input.Description;
            this.organizationRepository.Update(organization);

            return await this.organizationRepository.SaveChangesAsync();
        }
    }
}
