
namespace CRMSystem.Services.Data.Contracts
{
  public  interface IValidationService
    {
        bool IsAvailableEmail(string userId, string email);
        bool IsAvailablePhone(string userId, string phone);
    }
}
