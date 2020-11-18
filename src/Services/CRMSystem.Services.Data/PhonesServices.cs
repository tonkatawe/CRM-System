using CRMSystem.Data.Common.Repositories;
using CRMSystem.Data.Models;
using CRMSystem.Data.Models.Enums;
using CRMSystem.Services.Data.Contracts;
using CRMSystem.Services.Mapping;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRMSystem.Web.ViewModels.Phones;

namespace CRMSystem.Services.Data
{
    public class PhonesServices : IPhonesServices
    {
        private readonly IDeletableEntityRepository<PhoneNumber> phonesRepository;

        public PhonesServices(IDeletableEntityRepository<PhoneNumber> phonesRepository)
        {
            this.phonesRepository = phonesRepository;
        }

        public IEnumerable<T> GetAll<T>(int contactId)
        {
            var query = this.phonesRepository.All()
                .Where(x => x.CustomerId == contactId);

            return query.To<T>().ToList();
        }

        public async Task<PhoneNumber> CreateAsync(string phone, PhoneType type, int contactId)
        {
            var phoneNumber = new PhoneNumber
            {
                Phone = phone,
                PhoneType = type,
                CustomerId = contactId,
            };

            await this.phonesRepository.AddAsync(phoneNumber);
            await this.phonesRepository.SaveChangesAsync();

            return phoneNumber;
        }

        public async Task<int> UpdateAsync(PhoneCreateInputModel input)
        {
            var phone = await this.phonesRepository.GetByIdWithDeletedAsync(input.Id);
            phone.Phone = input.Phone;
            phone.PhoneType = input.PhoneType;
            this.phonesRepository.Update(phone);

            return await this.phonesRepository.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var phone = await this.phonesRepository.GetByIdWithDeletedAsync(id);
            this.phonesRepository.Delete(phone);

            return await this.phonesRepository.SaveChangesAsync();
        }
    }
}
