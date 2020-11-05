namespace CRMSystem.Data.Models
{
    using CRMSystem.Data.Common.Models;
    using CRMSystem.Data.Models.Enums;

    public class SocialNetwork : BaseDeletableModel<int>
    {
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public string Name { get; set; }

        public SocialNetworkType SocialNetworkType { get; set; }

    }
}
