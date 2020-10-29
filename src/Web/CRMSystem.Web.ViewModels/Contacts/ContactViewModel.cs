using CRMSystem.Data.Models;
using CRMSystem.Services.Mapping;

namespace CRMSystem.Web.ViewModels.Contacts
{
    using System;
    using System.Globalization;
    using CRMSystem.Data.Models.Enums;
    using CRMSystem.Web.ViewModels.Organizations;

    public class ContactViewModel:IMapFrom<Contact>
    {

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }


        public string FullName => string.IsNullOrEmpty(this.MiddleName)
            ? (this.FirstName + " " + this.LastName)
            : (this.FirstName + " " + this.MiddleName + " " + this.LastName);


        public IndustryType Industry { get; set; }

        public string IndustryAsString => this.Industry.ToString();

        public OrganizationViewModel Organization { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedOnAsString => this.CreatedOn.ToString("d", CultureInfo.InvariantCulture);

    }
}
