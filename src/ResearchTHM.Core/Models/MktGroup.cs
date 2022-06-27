using System;
using System.Collections.Generic;

namespace ResearchTHM.Core.Models
{
    public partial class MktGroup
    {
        public MktGroup()
        {
            MktGroupAccess = new HashSet<MktGroupAccess>();
            MktUserGroupAccess = new HashSet<MktUserGroupAccess>();
        }

        public long Id { get; set; }
        public Guid GroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public string CreatedById { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedById { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string DeletedById { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
        public bool Status { get; set; }
        public string GroupAdName { get; set; }
        public int? Priority { get; set; }

        public bool isdeveloper { get; set; }

        public virtual ICollection<MktGroupAccess> MktGroupAccess { get; set; }
        public virtual ICollection<MktUserGroupAccess> MktUserGroupAccess { get; set; }
    }
}
