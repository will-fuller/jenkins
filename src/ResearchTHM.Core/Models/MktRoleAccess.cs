using System;
using System.Collections.Generic;

namespace ResearchTHM.Core.Models
{
    public partial class MktRoleAccess
    {
        public long Id { get; set; }
        public Guid? RoleId { get; set; }
        public Guid? PermissionId { get; set; }

        public virtual MktPermission Permission { get; set; }
        public virtual MktRole Role { get; set; }
    }
}
