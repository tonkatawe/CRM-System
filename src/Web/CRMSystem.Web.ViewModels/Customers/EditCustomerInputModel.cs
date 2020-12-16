using CRMSystem.Data.Models;
using CRMSystem.Services.Mapping;

namespace CRMSystem.Web.ViewModels.Customers
{
    public class EditCustomerInputModel : CustomerAddInputModel, IMapFrom<Customer>
    {
        public int Id { get; set; }

        public string FullName { get; set; }
    }
}
