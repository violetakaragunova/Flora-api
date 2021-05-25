using Microsoft.AspNetCore.Identity;
using System;

namespace DomainModel.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string FullName { get; set; }

        public int CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public bool Deleted { get; set; }
    }
}
