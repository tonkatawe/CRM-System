namespace CRMSystem.Web.ViewModels.SocialNetworks
{
    using CRMSystem.Data.Models;
    using CRMSystem.Data.Models.Enums;
    using CRMSystem.Services.Mapping;

    public class SocialNetworkCreateInputModel : IMapFrom<SocialNetwork>
    {
        public string Name { get; set; }

        public SocialNetworkType SocialNetworkType { get; set; }
    }
}
