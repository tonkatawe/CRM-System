
using System.ComponentModel.DataAnnotations;
using CRMSystem.Web.Infrastructure;

namespace CRMSystem.Web.ViewModels.Contacts
{
    public class ContactFormViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Title { get; set; }

        [Required]
        [StringLength(15000, MinimumLength = 50)]
        public string Content { get; set; }

        [GoogleReCaptchaValidation]
        public string RecaptchaValue { get; set; }
    }
}
