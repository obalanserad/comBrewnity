using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser<String>
    {
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        //public DateTime Created { get; set; }
    }
    public class ApplicationRole : IdentityRole<String>
    {

        public ApplicationRole() : base() { }

        public ApplicationRole(string roleName) : base(roleName) { }

        public ApplicationRole(string roleName, string description,
            DateTime createdDate)
            : base(roleName)
        {
            base.Name = roleName;

            this.Description = description;
            this.CreatedDate = createdDate;
        }

        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
