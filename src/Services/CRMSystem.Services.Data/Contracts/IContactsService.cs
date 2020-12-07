using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CRMSystem.Web.ViewModels.Contacts;

namespace CRMSystem.Services.Data.Contracts
{
    public interface IContactsService
    {
        Task<int> CreateAsync(ContactFormViewModel input, string ip);
    }
}
