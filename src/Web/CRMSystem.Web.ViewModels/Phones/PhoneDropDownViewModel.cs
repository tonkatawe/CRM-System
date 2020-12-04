using CRMSystem.Data.Models;
using CRMSystem.Services.Mapping;

namespace CRMSystem.Web.ViewModels.Phones
{
    public class PhoneDropDownViewModel : IMapFrom<PhoneNumber>
    {
        public int Id { get; set; }

        public string Phone { get; set; }
    }
}
