using ResearchTHM.Core.TrkdModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ResearchTHM.Core.Models.RequestModels
{
    public class DocDownloadRequest
    {
        public long Id { get; set; }
        public string RequestId { get; set; }
        [Required]
        [RegularExpression("^[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}$", ErrorMessage = "Invalid DownloadHistId")]
        public Guid DownloadHistId { get; set; }
        [Range(0, long.MaxValue)]
        [RegularExpression("^([0-9]*)$", ErrorMessage = "Invalid DocId")]
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
        [DataType(DataType.DateTime)]
        public DateTime ArriveDate { get; set; }
        [DataType(DataType.Text)]
        public string CompanyName { get; set; }
        [Range(0, 999999999)]
        public long CtbId { get; set; }
        [DataType(DataType.Text)]
        public string DocClass { get; set; }
        [Range(0, long.MaxValue)]
        [Required]
        public bool EnhancedPdf { get; set; }
        [DataType(DataType.Text)]
        public string FileExt { get; set; }
        public bool HasEarns { get; set; }
        public bool HasFpp { get; set; }
        public bool HasSyn { get; set; }
        public bool? HasToc { get; set; }
        //public bool IndOvw { get; set; }
        [DataType(DataType.Text)]
        public string LocalCode { get; set; }
        public long PricingType { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime SubmitDate { get; set; }
        public long TocEnd { get; set; }
        public long TocStart { get; set; }
        public bool TranspRpt { get; set; }
        [DataType(DataType.Text)]
        public string Headline { get; set; }
        public Tkr PTkr { get; set; }
        public List<Tkr> Tkr { get; set; }
        public List<NameWithCode> Author { get; set; }
        public List<NameWithCode> Reg { get; set; }
        public List<NameWithCode> RegG { get; set; }
        public List<NameWithCode> DocTyp { get; set; }
        public List<NameWithCode> DocTypG { get; set; }
        public List<NameWithCode> Ind { get; set; }
        public List<NameWithCode> IndG { get; set; }
        public List<NameWithCode> Cntry { get; set; }
        public List<CtSubjectsResp> Grp { get; set; }
        public LangDesc LangDesc { get; set; }
        public List<CtSubjectsResp> ReasonsResp { get; set; }
        public List<CtSubjectsResp> RptStylesResp { get; set; }
        public NameWithCode MainAuthor { get; set; }
        [DataType(DataType.Text)]
        public string RatingType { get; set; }
        public long? RecommendRating { get; set; }
        public long? EstimateRating { get; set; }
        public IndustryTree IndustryTree { get; set; }
        [DataType(DataType.Text)]
        public string NonBillablePages { get; set; }
        [DataType(DataType.Text)]
        public string Audiences { get; set; }
        public List<CtSubjectsResp> CtSubjectsResp { get; set; }
        public List<CtSubjectsResp> DisciplinesResp { get; set; }
        public List<NameWithCode> Subj { get; set; }
        public List<NameWithCode> SubjG { get; set; }

        public float PagePrice { get; set; }
        [DataType(DataType.Text)]
        public string BillablePages { get; set; }
    }
}
