namespace CRMSystem.Services.Data.Contracts
{
    using CRMSystem.Data.Models;
    using CRMSystem.Web.ViewModels.Accounts;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    public interface IAccountsService
    {
        Task<KeyValuePair<string, string>> CreateAsync(CreateAccountInputModel input, ApplicationUser owner);
        
    }
}
