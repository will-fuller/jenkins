using System;
using System.Collections.Generic;

namespace ResearchTHM.Core.Models
{
    public partial class MktIndustry
    {
        public long Id { get; set; }
        public Guid IndustryId { get; set; }
        public string IndustryName { get; set; }
        public string GlobalCode { get; set; }
        public string LocalCode { get; set; }
        public string SetCode { get; set; }
        public string IndustryUid { get; set; }
        public string GeographicCodes { get; set; }
        public string CreatedById { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedById { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string DeletedById { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
        public bool Status { get; set; }
    }
}
