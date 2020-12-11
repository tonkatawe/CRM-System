using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CRMSystem.Web.ViewModels.Accounts;

namespace CRMSystem.Services.Data.Contracts
{
    public interface IAccountsService
    {
        Task CreateAsync(CreateAccountInputModel input);
    }
}
