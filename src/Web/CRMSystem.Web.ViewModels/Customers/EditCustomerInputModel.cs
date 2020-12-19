namespace CRMSystem.Web.ViewModels.Customers
{
    using CRMSystem.Data.Models;
    using CRMSystem.Services.Mapping;
    public class EditCustomerInputModel : CustomerAddInputModel, IMapFrom<Customer>
    {
        public int Id { get; set; }

        public string FullName { get; set; }
    }
}
