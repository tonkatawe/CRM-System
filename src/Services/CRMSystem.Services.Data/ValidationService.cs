using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRMSystem.Data.Common.Repositories;
using CRMSystem.Data.Models;
using CRMSystem.Services.Data.Contracts;

namespace CRMSystem.Services.Data
{
    public class ValidationService : IValidationService
    {
        private readonly IDeletableEntityRepository<Customer> customersRepository;
        private readonly IDeletableEntityRepository<PhoneNumber> phonesRepository;

        public ValidationService(IDeletableEntityRepository<Customer> customersRepository, IDeletableEntityRepository<PhoneNumber> phonesRepository)
        {
            this.customersRepository = customersRepository;
            this.phonesRepository = phonesRepository;
        }
        public bool IsAvailableEmail(string userId, string email, int? id = null, int? customerId = null)
        {
            var result = this.customersRepository
                .All()
                .Where(x => x.UserId == userId)
                .SelectMany(x => x.EmailAddresses)
                .FirstOrDefault(x => x.Email == email && x.Id != id && x.CustomerId != customerId);

            return result == null;

        }

        public bool IsAvailablePhone(string userId, string phone, int? id = null, int? customerId = null)
        {
            var result = this.customersRepository
                .All()
                .Where(x => x.UserId == userId)
                .SelectMany(x => x.PhoneNumbers)
                .FirstOrDefault(x => x.Phone == phone && x.Id != id && x.CustomerId != customerId);

            return result == null;
        }
    }
}
