using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CRMSystem.Data.Models;
using CRMSystem.Web.ViewModels.Accounts;

namespace CRMSystem.Services.Data.Contracts
{
    public interface IAccountsService
    {
        Task<KeyValuePair<string, string>> CreateAsync(CreateAccountInputModel input, ApplicationUser owner);

    }
}
