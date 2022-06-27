using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResearchTHM.API.Resources
{
    public class DownloadHistResource
    {
        [Required]
        public Guid DownloadHistId { get; set; }
        [DataType(DataType.Text)]
        public string DocId { get; set; }
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
        [Range(0, int.MaxValue)]
        public int? FileSize { get; set; }
        [StringLength(250)]
        public string FileType { get; set; }
        [Range(0, int.MaxValue)]
        public int? PageNo { get; set; }
        [StringLength(30)]
        public string Price { get; set; }
        [StringLength(250)]
        public string Source { get; set; }
        [StringLength(250)]
        public string ReportNo { get; set; }
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

        [DataType(DataType.DateTime)]
        public DateTime? From { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? To { get; set; }
    }
}
