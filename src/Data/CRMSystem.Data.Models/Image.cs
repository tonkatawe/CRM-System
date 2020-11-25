using System;
using System.Collections.Generic;
using System.Text;
using CRMSystem.Data.Common.Models;

namespace CRMSystem.Data.Models
{
    public class Image : BaseModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public string Extension { get; set; }

    }
}
