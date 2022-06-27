using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResearchTHM.Core.Models
{
    public partial class MktDownloadHist
    {
        public long Id { get; set; }
        [StringLength(30)]
        public string RequestId { get; set; }
        [Required]
        public Guid DownloadHistId { get; set; }
        [Range(0,long.MaxValue)]
        [RegularExpression("^([0-9]*)$", ErrorMessage = "Invalid Doc ID")]
        public long DocId { get; set; }
        [StringLength(250)]
        public string DocName { get; set; }
        [StringLength(2000)]
        public string DocTitle { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DocReleaseDate { get; set; }
        public Guid? UserId { get; set; }
        [StringLength(2000)]
        public string Analyst { get; set; }
        [StringLength(250)]
        public string ContributorName { get; set; }
        [StringLength(250)]
        public string FileName { get; set; }
        [Range(0, long.MaxValue)]
        public long? FileSize { get; set; }
        [StringLength(250)]
        public string FileType { get; set; }
        [Range(0, int.MaxValue)]
        public int? PageNo { get; set; }
        [StringLength(30)]
        public string Price { get; set; }
        [StringLength(250)]
        public string Source { get; set; }
        [StringLength(30)]
        public string Saved { get; set; }
        [StringLength(100)]
        public string Pages { get; set; }
        [StringLength(50)]
        public string DownloadType { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; }
        [StringLength(150)]
        public string DeletedById { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
        [Range(0, long.MaxValue)]
        public long? BoxFileId { get; set; }
        [StringLength(20)]
        public string ProjectCode { get; set; }
        public string RequestSource { get; set; }
    }
}
