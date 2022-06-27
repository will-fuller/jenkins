using System;
using System.Collections.Generic;

namespace ResearchTHM.Core.Models
{
    public partial class MktRole
    {
        public MktRole()
        {
            MktRoleAccess = new HashSet<MktRoleAccess>();
            MktUserRoleAccess = new HashSet<MktUserRoleAccess>();
        }

        public long Id { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public string CreatedById { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedById { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string DeletedById { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<MktRoleAccess> MktRoleAccess { get; set; }
        public virtual ICollection<MktUserRoleAccess> MktUserRoleAccess { get; set; }
    }
}
