using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CRMSystem.Services.Contracts
{
    public interface ICloudinaryService
    {
        Task<string> UploadAsync(IFormFile file);

        bool IsValidFile(IFormFile file);

    }
}
