namespace CRMSystem.Data.Models
{
    using CRMSystem.Data.Common.Models;
    public class ContactFormMessage : BaseModel<int>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Ip { get; set; }
    }
}
