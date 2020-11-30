using System;
using System.Collections.Generic;
using System.Text;
using CRMSystem.Data.Models;
using CRMSystem.Services.Mapping;

namespace CRMSystem.Web.ViewModels.Emails
{
    public class EmailDropDownViewModel : IMapFrom<EmailAddress>
    {
        public string FullName { get; set; }
        public int Id { get; set; }

        public string Email { get; set; }
    }
}
