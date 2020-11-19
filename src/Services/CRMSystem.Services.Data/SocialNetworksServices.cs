using System.Linq;
using CRMSystem.Services.Mapping;

namespace CRMSystem.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CRMSystem.Data.Common.Repositories;
    using CRMSystem.Data.Models;
    using CRMSystem.Data.Models.Enums;
    using CRMSystem.Services.Data.Contracts;

    public class SocialNetworksServices : ISocialNetworksServices
    {
        private readonly IDeletableEntityRepository<SocialNetwork> socialNetworkRepository;

        public SocialNetworksServices(IDeletableEntityRepository<SocialNetwork> socialNetworkRepository)
        {
            this.socialNetworkRepository = socialNetworkRepository;
        }

        public IEnumerable<T> GetAllContactSocialNetworks<T>(int ustomerId)
        {
            var query = this.socialNetworkRepository.All()
                .Where(x => x.CustomerId == ustomerId);
            return query.To<T>().ToList();
        }

        public async Task<SocialNetwork> CreateSocialNetworkAsync(string name, SocialNetworkType type, int customerId)
        {
            var socialNetwork = new SocialNetwork
            {
                Name = name,
                SocialNetworkType = type,
                CustomerId = customerId,
            };

            await this.socialNetworkRepository.AddAsync(socialNetwork);
            await this.socialNetworkRepository.SaveChangesAsync();

            return socialNetwork;
        }
    }
}
