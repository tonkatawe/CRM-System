

using System.Threading.Tasks;
using CRMSystem.Data.Common.Repositories;
using CRMSystem.Data.Models;
using CRMSystem.Services.Data.Contracts;
using CRMSystem.Web.ViewModels.Accounts;

namespace CRMSystem.Services.Data
{
  public  class AccountsService:IAccountsService
    {
        private readonly IDeletableEntityRepository<Customer> customersRepository;

        public AccountsService(IDeletableEntityRepository<Customer> customersRepository)
        {
            this.customersRepository = customersRepository;
        }

        public Task CreateAsync(CreateAccountInputModel input)
        {
            throw new System.NotImplementedException();
        }
    }
}
