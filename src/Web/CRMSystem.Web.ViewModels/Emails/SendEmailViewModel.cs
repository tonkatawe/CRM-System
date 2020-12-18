using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRMSystem.Web.ViewModels.Emails
{
    public class SendEmailViewModel
    {
        public int Id { get; set; }
        public IEnumerable<EmailDropDownViewModel> Emails { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(3), MaxLength(100)]
        public string Subject { get; set; }

        [Required]
        [MinLength(10), MaxLength(2000)]
        public string Content { get; set; }

    }
}
