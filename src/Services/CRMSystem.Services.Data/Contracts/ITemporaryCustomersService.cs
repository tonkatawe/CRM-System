namespace CRMSystem.Services.Data.Contracts
{
    using System.Threading.Tasks;
    public interface ITemporaryCustomersService
    {
        Task ApproveAsync(int id);
        
    }
}
