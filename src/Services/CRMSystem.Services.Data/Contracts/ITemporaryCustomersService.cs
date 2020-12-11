using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CRMSystem.Services.Data.Contracts
{
    public interface ITemporaryCustomersService
    {
        Task ApproveAsync(int id);

 
    }
}
