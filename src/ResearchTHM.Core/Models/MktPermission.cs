using System;
using System.Collections.Generic;

namespace ResearchTHM.Core.Models
{
    public partial class MktPermission
    {
        public MktPermission()
        {
            MktRoleAccess = new HashSet<MktRoleAccess>();
        }

        public long Id { get; set; }
        public Guid PermissionId { get; set; }
        public string PermissionName { get; set; }
        public string PermissionDescription { get; set; }
        public string CreatedById { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedById { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string DeletedById { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<MktRoleAccess> MktRoleAccess { get; set; }
    }
}
