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

        public OrganizationsService(
            IAddressesService addressesService,
            IDeletableEntityRepository<Organization> organizationRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IDeletableEntityRepository<Contact> contactRepository)
        {
            this.addressesService = addressesService;
            this.organizationRepository = organizationRepository;
        }

        public async Task<int> CreateOrganizationAsync(OrganizationCreateInputModel input, string userId)
        {
            var address = await this.addressesService.CreateAddressAsync(input.Address.Country, input.Address.City,
                input.Address.Street, input.Address.ZipCode);

            var organization = new Organization
            {
                UserId = userId,
                Name = input.Name,
                Description = input.Description,
                Address = address,
            };
            await this.organizationRepository.AddAsync(organization);
            await this.organizationRepository.SaveChangesAsync();
            return organization.Id;
        }

        public IEnumerable<T> GetAll<T>(string userId, int? count = null)
        {
            var query = this.organizationRepository.All()
                    .Where(x => x.UserId == userId)
                    .OrderBy(x => x.Name)
                    .To<T>()
                    .ToList();

            return query;
        }

        public int AllOrganizationCount(int organizationId)
        {
            return this.organizationRepository.All()
                .Count(x => x.Id == organizationId);
        }
    }
}
