using System;
using System.Collections.Generic;
using System.Text;

namespace ResearchTHM.Core.Models
{
    public partial class MktBoxdocInfo
    {
        public long ID { get; set; }
        public long DocId { get; set; }
        public string DocType { get; set; }
        public long Boxid { get; set; }
        public int SequenceId { get; set; }
        public string Etag { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public int? FileSize { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? ContentCreatedOn { get; set; }
        public DateTime? ContentModifiedOn { get; set; }
        public string CreatedByType { get; set; }
        public string CreatedById { get; set; }
        public string CreatedByName { get; set; }
        public string ModifiedByType { get; set; }
        public string ModifiedById { get; set; }
        public string ModifiedByName { get; set; }
        public string OwnedByType { get; set; }
        public string OwnedById { get; set; }
        public string OwnedByName { get; set; }
        public string ParentType { get; set; }
        public long? ParentId { get; set; }
        public string ParentSequenceId { get; set; }
        public string ParentEtag { get; set; }
        public string ParentName { get; set; }
        public Boolean? ItemStatus { get; set; }
    }
}
