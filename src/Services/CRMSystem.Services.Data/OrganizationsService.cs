using System.Collections.Generic;
using System.Linq;
using CRMSystem.Services.Mapping;

namespace CRMSystem.Services.Data
{
    using System.Threading.Tasks;

    using CRMSystem.Data.Common.Repositories;
    using CRMSystem.Data.Models;
    using CRMSystem.Services.Data.Contracts;
    using CRMSystem.Web.ViewModels.Organizations;

    public class OrganizationsService : IOrganizationsService
    {
        private readonly IDeletableEntityRepository<Organization> organizationRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<Contact> contactRepository;

        public OrganizationsService(
            IDeletableEntityRepository<Organization> organizationRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IDeletableEntityRepository<Contact> contactRepository)
        {
            this.organizationRepository = organizationRepository;
            this.userRepository = userRepository;
            this.contactRepository = contactRepository;
        }

        public async Task<int> CreateOrganizationAsync(OrganizationInputModel input, string userId)
        {
            var organization = new Organization
            {
                UserId = userId,
                Name = input.Name,
                Description = input.Description,

                Address = input.Address,
            };
            await this.organizationRepository.AddAsync(organization);
            await this.organizationRepository.SaveChangesAsync();
            return organization.Id;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Organization> query =
                this.organizationRepository.All().OrderBy(x => x.Name);

            return query.To<T>().ToList();
        }
    }
}
