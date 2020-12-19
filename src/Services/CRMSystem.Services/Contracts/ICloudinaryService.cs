namespace CRMSystem.Services.Contracts
{
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;
    public interface ICloudinaryService
    {
        Task<string> UploadAsync(IFormFile file);

        bool IsValidFile(IFormFile file);

    }
}
