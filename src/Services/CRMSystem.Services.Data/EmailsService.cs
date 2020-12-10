using CRMSystem.Web.ViewModels.Emails;

namespace CRMSystem.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CRMSystem.Data.Common.Repositories;
    using CRMSystem.Data.Models;
    using CRMSystem.Data.Models.Enums;
    using CRMSystem.Services.Data.Contracts;
    using CRMSystem.Services.Mapping;

    public class EmailsService : IEmailsService
    {
        private readonly IRepository<EmailAddress> emailRepository;

        public EmailsService(IRepository<EmailAddress> emailRepository)
        {
            this.emailRepository = emailRepository;
        }

        public IEnumerable<T> GetAll<T>(int customerId)
        {
            var query = this.emailRepository.All()
                .Where(x => x.CustomerId == customerId);

            return query.To<T>().ToList();
        }

        public async Task<EmailAddress> CreateAsync(string email, EmailType type, int customerId)
        {
            var emailAddress = new EmailAddress
            {
                CustomerId = customerId,
                Email = email,
                EmailType = type,
            };
            await this.emailRepository.AddAsync(emailAddress);
            await this.emailRepository.SaveChangesAsync();

            return emailAddress;
        }



        public async Task<int> DeleteAsync(int customerId)
        {
            var email =  this.emailRepository.All().FirstOrDefault(x=>x.Id == customerId);
            this.emailRepository.Delete(email);

            return await this.emailRepository.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(EmailCreateInputModel input)
        {
            var email =  this.emailRepository.All().FirstOrDefault(x=>x.Id == input.Id);
            email.Email = input.Email;
            email.EmailType = input.EmailType;
            this.emailRepository.Update(email);

            return await this.emailRepository.SaveChangesAsync();
        }
    }
}
