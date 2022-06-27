using System;
using System.Collections.Generic;
using System.Text;

namespace ResearchTHM.Core.Models
{
    public partial class MktProcessErrorlog
    {
        public long ID { get; set; }
        public Guid RunId { get; set; }
        public Guid ProcessId { get; set; }
        public string errorCode { get; set; }
        public string errorLog { get; set; }
        public string CreatedById { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
