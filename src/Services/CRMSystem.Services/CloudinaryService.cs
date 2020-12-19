namespace CRMSystem.Services
{
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using CRMSystem.Services.Contracts;
    using Microsoft.AspNetCore.Http;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinary;
        private readonly string defaultImage = @"https://res.cloudinary.com/dx479nsjv/image/upload/v1608064012/CRMSystem/default-img_rftxia.gif";
        public CloudinaryService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            if (file == null || !this.IsValidFile(file))
            {
                return this.defaultImage;
            }

            string imageUrl = " ";
            byte[] fileBytes;
            using var stream = new MemoryStream();
            file.CopyTo(stream);
            fileBytes = stream.ToArray();

            using var uploadStream = new MemoryStream(fileBytes);
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, uploadStream),
            };

            var result = await this.cloudinary.UploadAsync(uploadParams);

            imageUrl = result.Url.AbsoluteUri;

            return imageUrl;
        }

        public bool IsValidFile(IFormFile file)
        {
            if (file == null)
            {
                return true;
            }

            var validTypes = new string[]
            {
                "image/x-png", "image/gif", "image/jpeg", "image/jpg", "image/png", "image/svg",
            };

            if (!validTypes.Contains(file.ContentType))
            {
                return false;
            }

            return true;
        }
    }
}
