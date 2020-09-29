namespace CRMSystem.Data.Models
{
    using CRMSystem.Data.Common.Models;

    public class Address : BaseDeletableModel<int>
    {
        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public int ZipCode { get; set; }

    }
}
