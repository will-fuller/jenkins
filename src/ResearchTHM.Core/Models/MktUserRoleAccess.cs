using System;
using System.Collections.Generic;

namespace ResearchTHM.Core.Models
{
    public partial class MktUserRoleAccess
    {
        public Guid? UserId { get; set; }
        public Guid? RoleId { get; set; }

        public virtual MktRole Role { get; set; }
        public virtual MktUser User { get; set; }
    }
}
