using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CRMSystem.Data.Common.Repositories;
using CRMSystem.Data.Models;
using CRMSystem.Data.Models.Enums;
using CRMSystem.Services.Data.Contracts;
using CRMSystem.Services.Mapping;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.Services.Data
{
 public   class PhonesServices:IPhonesServices
    {
        private readonly IDeletableEntityRepository<PhoneNumber> phonesRepository;

        public PhonesServices(IDeletableEntityRepository<PhoneNumber> phonesRepository)
        {
            this.phonesRepository = phonesRepository;
        }

        public IEnumerable<T> GetAllContactPhones<T>(int contactId)
        {
            var query = this.phonesRepository.All()
                .Where(x => x.ContactId == contactId);

            return query.To<T>().ToList();
        }

        public async Task<PhoneNumber> CreatePhoneAsync(string phone, PhoneType type, int contactId)
        {
           var phoneNumber = new PhoneNumber
           {
               Phone = phone,
               PhoneType = type,
               ContactId = contactId,
           };

           await this.phonesRepository.AddAsync(phoneNumber);
           await this.phonesRepository.SaveChangesAsync();

           return phoneNumber;
        }

        public bool IsAvailablePhoneNumber(string phoneNumber)
        {
            var phone =  this.phonesRepository.All().FirstOrDefault(x=>x.Phone == phoneNumber);
            return phone == null;
        }
    }
}
