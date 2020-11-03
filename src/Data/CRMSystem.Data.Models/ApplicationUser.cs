// ReSharper disable VirtualMemberCallInConstructor
namespace CRMSystem.Data.Models
{
    using System;
    using System.Collections.Generic;

    using CRMSystem.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Contacts = new List<Contact>();
            this.Tasks = new HashSet<UserTask>();
            this.Organizations = new HashSet<Organization>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }

        public virtual ICollection<UserTask> Tasks { get; set; }

        public virtual ICollection<Organization> Organizations { get; set; }
    }
}
