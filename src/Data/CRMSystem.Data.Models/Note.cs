namespace CRMSystem.Data.Models
{

    using CRMSystem.Data.Common.Models;

    public class Note : BaseDeletableModel<int>
    {
        public int ContactId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
