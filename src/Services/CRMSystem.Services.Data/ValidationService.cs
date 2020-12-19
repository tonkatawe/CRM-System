namespace CRMSystem.Services.Data
{
    using CRMSystem.Data.Common.Repositories;
    using CRMSystem.Data.Models;
    using CRMSystem.Services.Data.Contracts;
    using System.Linq;
    public class ValidationService : IValidationService
    {
        private readonly IDeletableEntityRepository<Customer> customersRepository;
        private readonly IRepository<PhoneNumber> phonesRepository;

        public ValidationService(IDeletableEntityRepository<Customer> customersRepository, IRepository<PhoneNumber> phonesRepository)
        {
            this.customersRepository = customersRepository;
            this.phonesRepository = phonesRepository;
        }
        public bool IsAvailableEmail(string userId, string email, int? id = null, int? customerId = null)
        {
            var result = this.customersRepository
                .All()
                .Where(x => x.UserId == userId)
                .SelectMany(x => x.Emails)
                .FirstOrDefault(x => x.Email == email && x.Id != id && x.CustomerId != customerId);

            return result == null;
        }

        public bool IsAvailablePhone(string userId, string phone, int? id = null, int? customerId = null)
        {
            var result = this.customersRepository
                .All()
                .Where(x => x.UserId == userId)
                .SelectMany(x => x.Phones)
                .FirstOrDefault(x => x.Phone == phone && x.Id != id && x.CustomerId != customerId);

            return result == null;
        }
    }
}
