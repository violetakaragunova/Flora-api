using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace DomainModel.Identity
{
    public class Role : IdentityRole<int>
    {
        public int CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public bool Deleted { get; set; }
    }
}
