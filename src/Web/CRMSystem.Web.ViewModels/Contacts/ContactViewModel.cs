using System;

namespace CRMSystem.Web.ViewModels.Contacts
{
    using System.Collections.Generic;

    using CRMSystem.Data.Models;
    using CRMSystem.Data.Models.Enums;
    using CRMSystem.Services.Mapping;
    using CRMSystem.Web.ViewModels.Emails;
    using CRMSystem.Web.ViewModels.Organizations;

    public class ContactViewModel : IMapFrom<Contact>
    {
      
        public int Id { get; set; }

       public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

      
        public IndustryType Industry { get; set; }

        public OrganizationCreateInputModel Organization { get; set; }

        public DateTime CreatedOn { get; set; }
        
    }
}
