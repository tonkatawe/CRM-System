using System.Collections.Generic;
using AutoMapper;
using CRMSystem.Web.ViewModels.Customers;
using CRMSystem.Web.ViewModels.Products;

namespace CRMSystem.Web.ViewModels.Organizations
{
    using CRMSystem.Data.Models;
    using CRMSystem.Services.Mapping;

    public class OrganizationViewModel : IMapFrom<Organization>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int AddressId { get; set; }

        public Address Address { get; set; }
        
        public IEnumerable<CustomerViewModel> Customers { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; }
     
    }
}
