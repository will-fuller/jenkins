using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ResearchTHM.Core.TrkdModels
{

    public partial class BatchResponse
    {
        public BatchResponse1 Batch_Response_1 { get; set; }
    }
    public partial class ProjectCode
    {
        [DataType(DataType.Text)]
        public string projectCode { get; set; }
        public float PagePrice { get; set; }
        public string BillingPages { get; set; }
    }

    public partial class BatchResponse1
    {
        public DocSearchResponse1 DocSearch_Response_1 { get; set; }
        public DocCountResponse1 DocCount_Response_1 { get; set; }
    }

    public partial class DocCountResponse1
    {
        public long ReqId { get; set; }
        public long Count { get; set; }
    }

    #region DocSearchResponse
    public partial class DocSearchResponse1
    {
        public long ReqId { get; set; }
        public List<DiDef> DiDef { get; set; }
    }

    public partial class DiDef: ProjectCode
    {
        [DataType(DataType.DateTime)]
        public DateTime ArriveDate { get; set; }
        [DataType(DataType.Text)]
        public string CompanyName { get; set; }
        [Range(0,999999999)]
        public long CtbId { get; set; }
        [DataType(DataType.Text)]
        public string DocClass { get; set; }
        [Range(0,long.MaxValue)]
        [Required]
        public long DocId { get; set; }
        public bool EnhancedPdf { get; set; }
        [DataType(DataType.Text)]
        public string FileExt { get; set; }
        [DataType(DataType.Text)]
        public string FileName { get; set; }
        public long FileSize { get; set; }
        [DataType(DataType.Text)]
        public string FileType { get; set; }
        public bool HasEarns { get; set; }
        public bool HasFpp { get; set; }
        public bool HasSyn { get; set; }
        public bool? HasToc { get; set; }
        //public bool IndOvw { get; set; }
        [DataType(DataType.Text)]
        public string LocalCode { get; set; }
        public long Pages { get; set; }
        public long Price { get; set; }
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
    }

    public partial class NameWithCode
    {
        public string C { get; set; }
        public string Value { get; set; }
    }

    public partial class CtSubjectsResp
    {
        public long Uid { get; set; }
        public string Value { get; set; }
    }

    public partial class IndustryTree
    {
        public List<Sector> Sector { get; set; }
    }

    public partial class Sector
    {
        public long Code { get; set; }
        public string Name { get; set; }
        public List<Subsector> Subsector { get; set; }
    }

    public partial class Subsector
    {
        public long Code { get; set; }
        public string Name { get; set; }
        public List<Industry> Industry { get; set; }
    }

    public partial class Industry
    {
        public long Code { get; set; }
        public string Name { get; set; }
        public List<Subindustry> Subindustry { get; set; }
    }

    public partial class Subindustry
    {
        public long Code { get; set; }
        public string Name { get; set; }
    }

    public partial class LangDesc
    {
        public string Lang { get; set; }
        public string Rfc1766 { get; set; }
        public string Value { get; set; }
    }

    public partial class Tkr
    {
        public string Ric { get; set; }
        public bool? BPrim { get; set; }
        public long? PrtId { get; set; }
        public string Value { get; set; }
    }
    #endregion


}