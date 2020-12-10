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
        private readonly IRepository<PhoneNumber> phonesRepository;

        public PhonesServices(IRepository<PhoneNumber> phonesRepository)
        {
            this.phonesRepository = phonesRepository;
        }

        public IEnumerable<T> GetAll<T>(int customerId)
        {
            var query = this.phonesRepository.All()
                .Where(x => x.CustomerId == customerId);

            return query.To<T>().ToList();
        }

        public async Task<PhoneNumber> CreateAsync(string phone, PhoneType type, int customerId)
        {
            var phoneNumber = new PhoneNumber
            {
                Phone = phone,
                PhoneType = type,
                CustomerId = customerId,
            };

            await this.phonesRepository.AddAsync(phoneNumber);
            await this.phonesRepository.SaveChangesAsync();

            return phoneNumber;
        }

        public async Task<int> UpdateAsync(PhoneCreateInputModel input)
        {
            var phone =  this.phonesRepository.All().FirstOrDefault(x=>x.Id == input.Id);

          //  var phoneOriginal = await this.phonesRepository.GetByIdWithDeletedAsync(input.Id);

            phone.Phone = input.Phone;
            phone.PhoneType = input.PhoneType;
            this.phonesRepository.Update(phone);

            return await this.phonesRepository.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var phone =  this.phonesRepository.All().FirstOrDefault(x => x.Id == id);
            //  var phone = await this.phonesRepository.GetByIdWithDeletedAsync(id);
            this.phonesRepository.Delete(phone);

            return await this.phonesRepository.SaveChangesAsync();
        }
    }
}
