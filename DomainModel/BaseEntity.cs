using System;

namespace DomainModel
{
    public class BaseEntity
    {
        public int CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public bool Deleted { get; set; }
    }
}
