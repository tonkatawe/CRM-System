using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CRMSystem.Data.Common.Repositories;
using CRMSystem.Data.Models;
using CRMSystem.Services.Data.Contracts;
using CRMSystem.Services.Mapping;
using CRMSystem.Web.ViewModels.Products;

namespace CRMSystem.Services.Data
{
    public class ProductsService : IProductsService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "jpeg", "png", "gif" };
        private readonly IDeletableEntityRepository<Product> productsRepository;
        private readonly IOrganizationsService organizationsService;

        public ProductsService(IDeletableEntityRepository<Product> productsRepository, IOrganizationsService organizationsService)
        {
            this.productsRepository = productsRepository;
            this.organizationsService = organizationsService;
        }

        public async Task<int> CreateAsync(ProductCreateInputModel input, string userId, string imagePath)
        {
            var organizationId = this.organizationsService.GetById(userId);

            var product = new Product
            {
                OrganizationId = organizationId,
                Name = input.Name,
                Price = input.Price,
                Description = input.Description,
                Quantity = input.Quantity,
            };



            Directory.CreateDirectory($"{imagePath}/products");
            foreach (var image in input.Images)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');
                if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new Exception($"Invalid image extension {extension}");
                }

                var dbImage = new Image
                {
                    Extension = extension,
                };
                product.Images.Add(dbImage);

                var physicalPath = $"{imagePath}/products/{dbImage.Id}.{extension}";
                await using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                await image.CopyToAsync(fileStream);
            }

            await this.productsRepository.AddAsync(product);

            return await this.productsRepository.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(EditProductInputModel input)
        {
            var product = await this.productsRepository.GetByIdWithDeletedAsync(input.Id);
            product.Name = input.Name;
            product.Description = input.Description;
            product.Price = input.Price;
            product.Quantity = input.Quantity;

            this.productsRepository.Update(product);

            return await this.productsRepository.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int productId)
        {
            var product = await this.productsRepository.GetByIdWithDeletedAsync(productId);
            this.productsRepository.Delete(product);

            return await this.productsRepository.SaveChangesAsync();
        }

        public T GetById<T>(int productId)
        {
            return this.productsRepository
                .All()
                .Where(x => x.Id == productId)
                .To<T>()
                .FirstOrDefault();
        }

        public IQueryable<T> GetAll<T>(string userId)
        {
            var organizationId = this.organizationsService.GetById(userId);
            return this.productsRepository
                .All()
                .Where(x => x.OrganizationId == organizationId)
                .To<T>()
                .AsQueryable();
        }

        public int ProductsCount(string userId)
        {
            var organizationId = this.organizationsService.GetById(userId);
            var products = this.productsRepository
                .All()
                .Where(x => x.OrganizationId == organizationId)
                .ToList();

            return products.Count;
        }
    }
}
