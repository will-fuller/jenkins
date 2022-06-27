using System;
using System.Collections.Generic;

namespace ResearchTHM.Core.Models
{
    public partial class MktGroupAccess
    {
        public long Id { get; set; }
        public Guid GroupId { get; set; }
        public Guid ContributorId { get; set; }

        public virtual MktGroup Group { get; set; }
    }
}
