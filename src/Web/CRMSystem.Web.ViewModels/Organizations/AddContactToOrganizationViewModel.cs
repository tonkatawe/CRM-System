namespace CRMSystem.Web.ViewModels.Organizations
{
    using System.Collections.Generic;
    using System.ComponentModel;

    using CRMSystem.Web.ViewModels.Contacts;

    public class AddContactToOrganizationViewModel
    {
        public AddContactToOrganizationViewModel()
        {
            this.Organizations = new HashSet<OrganizationDropDownViewModel>();
        }

        public int? ContactId { get; set; }

        [DisplayName("Choice your existed organization")]
        public int OrganizationId { get; set; }

        public IEnumerable<OrganizationDropDownViewModel> Organizations { get; set; }

        public OrganizationCreateInputModel CreateOrganization { get; set; }
    }
}
