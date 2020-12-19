namespace CRMSystem.Web.ViewModels.Phones
{
    using CRMSystem.Data.Models;
    using CRMSystem.Services.Mapping;
    public class PhoneDropDownViewModel : IMapFrom<PhoneNumber>
    {
        public int Id { get; set; }

        public string Phone { get; set; }
    }
}
