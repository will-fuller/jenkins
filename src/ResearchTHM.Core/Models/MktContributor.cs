using System;
using System.Collections.Generic;

namespace ResearchTHM.Core.Models
{
    public partial class MktContributor
    {
        public long Id { get; set; }
        public Guid ContributorId { get; set; }
        public string ContributorName { get; set; }
        public string ContributorAlias { get; set; }
        public string CompanyCategory { get; set; }
        public Guid? IndustryId { get; set; }
        public string Industry { get; set; }
        public Guid? ProfileId { get; set; }
        public string ProfileName { get; set; }
        public string ContributorUid { get; set; }
        public bool? Embargo { get; set; }
        public string Setcode { get; set; }
        public string Setcodetype { get; set; }
        public string Author { get; set; }
        public Guid? AuthorId { get; set; }
        public string Country { get; set; }
        public Guid? CountryId { get; set; }
        public string Currency { get; set; }
        public Guid? CurrencyId { get; set; }
        public string Region { get; set; }
        public Guid? RegionId { get; set; }
        public string Subject { get; set; }
        public Guid? SubjectId { get; set; }
        public string Category { get; set; }
        public Guid? CategoryId { get; set; }
        public bool IsExcluded { get; set; }
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
