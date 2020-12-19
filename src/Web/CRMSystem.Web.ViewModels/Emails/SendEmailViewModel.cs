namespace CRMSystem.Web.ViewModels.Emails
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class SendEmailViewModel
    {
        public int Id { get; set; }
        public IEnumerable<EmailDropDownViewModel> Emails { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Subject should be minimum 3 letters"), MaxLength(100, ErrorMessage = "Subject should be maximum 100 letters")]
        public string Subject { get; set; }

        [Required]
        [MinLength(10), MaxLength(2000)]
        public string Content { get; set; }

    }
}
