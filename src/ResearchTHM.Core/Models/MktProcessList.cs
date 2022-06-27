using System;
using System.Collections.Generic;
using System.Text;

namespace ResearchTHM.Core.Models
{
    public partial class MktProcessList
    {
        public long ID { get; set; }
        public Guid? ProcessId { get; set; }
        public string ProcessName { get; set; }
        public string ProcessType { get; set; }
        public string schFlag { get; set; }
        public string notifyFlag { get; set; }
        public string notifyList { get; set; }
        public string CreatedById { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Status { get; set; }
    }
}
