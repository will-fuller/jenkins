using System;
using System.Collections.Generic;

namespace ResearchTHM.Core.Models
{
    public partial class MktUserGroupAccess
    {
        public Guid? UserId { get; set; }
        public Guid? GroupId { get; set; }

        public virtual MktGroup Group { get; set; }
        public virtual MktUser User { get; set; }
    }
}
