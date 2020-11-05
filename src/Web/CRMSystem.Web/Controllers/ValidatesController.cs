using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRMSystem.Data.Common.Repositories;
using CRMSystem.Data.Models;
using CRMSystem.Services.Data.Contracts;
using CRMSystem.Web.ViewModels.Contacts;
using CRMSystem.Web.ViewModels.Emails;
using CRMSystem.Web.ViewModels.Phones;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Web.Controllers
{
    public class ValidatesController : Controller
    {
        private readonly IPhonesServices phonesServices;
        private readonly IEmailsService emailsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<Customer> contactsRepository;

        public ValidatesController(IPhonesServices phonesServices,IEmailsService emailsService, UserManager<ApplicationUser> userManager, IDeletableEntityRepository<Customer> contactsRepository)
        {
            this.phonesServices = phonesServices;
            this.emailsService = emailsService;
            this.userManager = userManager;
            this.contactsRepository = contactsRepository;
        }


        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyPhone(string phone, CustomerAddInputModel input = null)
        {
            if (input.PhoneNumber != null)
            {
                phone = input.PhoneNumber.Phone;
                var userId = this.userManager.GetUserId(this.User);
                var contacts = this.contactsRepository.All().Where(x => x.UserId == userId);

                foreach (var contact in contacts)
                {
                    var currentPhones = this.phonesServices.GetAllContactPhones<PhoneCreateInputModel>(contact.Id);
                    if (currentPhones.Any(x => x.Phone == phone))
                    {
                        return Json($"Phone {input.PhoneNumber.Phone} is already in use (in other contact)");
                    }
                }
            }
            
            //TODO MAKE CHECK WITH CONTACTID!!!
            if (!this.phonesServices.IsAvailablePhoneNumber(phone))
            {
                return Json($"Phone {phone} is already in use");
            }
            return Json(true);
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyEmail(string email, EditContactInputModel test= null, CustomerAddInputModel input = null)
        {
            if (input.Email != null)
            {
                email = input.Email.Email;
                var userId = this.userManager.GetUserId(this.User);
                var contacts = this.contactsRepository.All().Where(x => x.UserId == userId);

                foreach (var contact in contacts)
                {
                    var currentEmails = this.emailsService.GetAllContactEmails<EmailCreateInputModel>(contact.Id);
                    if (currentEmails.Any(x => x.Email == email))
                    {
                        return Json($"Email {input.Email.Email} is already in use (in other contact)");
                    }
                }
            }
            //TODO MAKE CHECK WITH CONTACTID!!!
            if (!this.emailsService.IsAvailableEmail(email))
            {
                return Json($"Email {email} is already in use");
            }
            return Json(true);
        }
    }
}
