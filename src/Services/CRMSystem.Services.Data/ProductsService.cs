namespace CRMSystem.Services.Data
{
    using CRMSystem.Data.Common.Repositories;
    using CRMSystem.Data.Models;
    using CRMSystem.Services.Contracts;
    using CRMSystem.Services.Data.Contracts;
    using CRMSystem.Services.Mapping;
    using CRMSystem.Web.ViewModels.Products;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductsService : IProductsService
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;
        private readonly IOrganizationsService organizationsService;
        private readonly ICloudinaryService cloudinaryService;

        public ProductsService(
            IDeletableEntityRepository<Product> productsRepository,
            IOrganizationsService organizationsService,
            ICloudinaryService cloudinaryService)
        {
            this.productsRepository = productsRepository;
            this.organizationsService = organizationsService;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<int> CreateAsync(ProductCreateInputModel input, string userId)
        {
            var organizationId = this.organizationsService.GetId(userId);
            var productPictureUrl = await this.cloudinaryService.UploadAsync(input.ProductPicture);


            var product = new Product
            {
                OrganizationId = organizationId,
                Name = input.Name,
                Price = input.Price,
                Description = input.Description,
                Quantity = input.Quantity,
                ProductPictureUrl = productPictureUrl,
            };

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

            if (input.ProductPicture != null)
            {
                var productPictureUrl = await this.cloudinaryService.UploadAsync(input.ProductPicture);
                product.ProductPictureUrl = productPictureUrl;
            }

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
            var organizationId = this.organizationsService.GetId(userId);
            return this.productsRepository
                .All()
                .Where(x => x.OrganizationId == organizationId)
                .To<T>()
                .AsQueryable();
        }

        public int ProductsCount(string userId)
        {
            var organizationId = this.organizationsService.GetId(userId);
            var products = this.productsRepository
                .All()
                .Where(x => x.OrganizationId == organizationId)
                .ToList();

            return products.Count;
        }
    }
}
