using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Services.Data.Contracts
{
    public interface IAccountsService
    {
        Task CreateAsync(int id);
    }
}
