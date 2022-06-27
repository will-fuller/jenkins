using System;
using System.Collections.Generic;
using System.Text;

namespace ResearchTHM.Core.Models
{
    public partial class MktGroupPermission
    {
        public long Id { get; set; }
        public Guid GroupId { get; set; }
        public Guid PermissionId { get; set; }
    }
}
