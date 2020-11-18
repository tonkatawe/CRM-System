
namespace CRMSystem.Services.Data.Contracts
{
    public interface IValidationService
    {
        bool IsAvailableEmail(string userId, string email, int? id = null, int? customerId = null);
        bool IsAvailablePhone(string userId, string phone, int? id = null, int? customerId = null);
    }
}
