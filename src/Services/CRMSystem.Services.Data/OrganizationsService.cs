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

        public OrganizationsService(
            IDeletableEntityRepository<Organization> organizationRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.organizationRepository = organizationRepository;
            this.userRepository = userRepository;
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
    }
}
