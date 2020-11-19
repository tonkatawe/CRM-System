namespace CRMSystem.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CRMSystem.Data.Models;
    using CRMSystem.Data.Models.Enums;

    public interface ISocialNetworksServices
    {
        IEnumerable<T> GetAllContactSocialNetworks<T>(int customerId);

        Task<SocialNetwork> CreateSocialNetworkAsync(string name, SocialNetworkType type, int customerId);
    }
}
