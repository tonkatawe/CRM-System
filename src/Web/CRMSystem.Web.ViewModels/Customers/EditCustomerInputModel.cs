using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using CRMSystem.Data.Models;
using CRMSystem.Data.Models.Enums;
using CRMSystem.Services.Mapping;
using CRMSystem.Web.ViewModels.Contacts;
using CRMSystem.Web.ViewModels.Emails;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Web.ViewModels.Customers
{
    public class EditCustomerInputModel : CustomerAddInputModel
    {
        public int Id { get; set; }

    }
}
