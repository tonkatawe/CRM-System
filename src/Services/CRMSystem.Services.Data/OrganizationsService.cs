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

        public async Task<int> CreateOrganizationAsync(OrganizationCreateInputModel input, string userId)
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

        public IEnumerable<T> GetAll<T>(string userId, int? count = null)
        {
            IQueryable<Organization> query =
                this.organizationRepository.All()
                    .Where(x => x.UserId == userId)
                    .OrderBy(x => x.Name);

            return query.To<T>().ToList();
        }
    }
}
