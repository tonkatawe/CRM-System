namespace CRMSystem.Web.ViewModels.Contacts
{
    using System.Collections.Generic;

    public class GetAllContactsViewModel
    {
        public IEnumerable<ContactViewModel> Contacts { get; set; }
    }
}
