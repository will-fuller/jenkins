using System;
using System.Collections.Generic;
using System.Text;

namespace ResearchTHM.Core.Models
{
    public partial class MktAuditLog
    {
        public long ID { get; set; }
        public Guid? RunId { get; set; }
        public Guid AuditId { get; set; }
        public Guid? ProcessId { get; set; }
        public string ProcessName { get; set; }
        public Guid? UserId { get; set; }
        public string UserName { get; set; }
        public string IncludedIds { get; set; }
        public string ExcludedIds { get; set; }
        public string DeletedIds { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
