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
        private readonly IDeletableEntityRepository<EmailAddress> emailRepository;

        public EmailsService(IDeletableEntityRepository<EmailAddress> emailRepository)
        {
            this.emailRepository = emailRepository;
        }

        public IEnumerable<T> GetAllCustomerEmails<T>(int contactId)
        {
            var query = this.emailRepository.All()
                .Where(x => x.CustomerId == contactId);

            return query.To<T>().ToList();
        }

        public async Task<EmailAddress> CreateEmailAsync(string email, EmailType type, int contactId)
        {
            var emailAddress = new EmailAddress
            {
                CustomerId = contactId,
                Email = email,
                EmailType = type,
            };
            await this.emailRepository.AddAsync(emailAddress);
            await this.emailRepository.SaveChangesAsync();

            return emailAddress;
        }

       

        public async Task<int> DeleteEmailAsync(int contactId)
        {
            var phone = await this.emailRepository.GetByIdWithDeletedAsync(contactId);
            this.emailRepository.Delete(phone);

            return await this.emailRepository.SaveChangesAsync();
        }

        public async Task<int> UpdateEmailAsync(EmailCreateInputModel input)
        {
            var email = await this.emailRepository.GetByIdWithDeletedAsync(input.Id);
            email.Email = input.Email;
            email.EmailType = input.EmailType;
            this.emailRepository.Undelete(email);

            return await this.emailRepository.SaveChangesAsync();
        }
    }
}
