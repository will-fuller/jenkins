using System;
using System.Collections.Generic;
using System.Text;

namespace ResearchTHM.Core.Models
{
    public partial class MktProcessRunlog
    {
        public long ID { get; set; }
        public Guid RunId { get; set; }
        public Guid ProcessId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string notifyFlag { get; set; }
        public string notifyList { get; set; }
        public string msgLog { get; set; }
        public string userId { get; set; }
        public string userName { get; set; }
        public string runStatus { get; set; }
        public string CreatedById { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
