
using System.ComponentModel.DataAnnotations;

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

        public string RecaptchaValue { get; set; }
    }
}
