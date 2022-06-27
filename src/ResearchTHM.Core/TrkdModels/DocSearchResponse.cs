using System;
using System.Collections.Generic;
using System.Text;

namespace ResearchTHM.Core.TrkdModels
{
    public partial class DocSearchResponse
    {
        public DocSearchResponse1 DocSearch_Response_1 { get; set; }
    }

    public partial class DocSearchResponse1
    {
        public List<DiDef> DiDef { get; set; }
    }

    public partial class ProjectCode
    {
        public string projectCode { get; set; }
    }

    public partial class DiDef : ProjectCode
    {
        public DateTime ArriveDate { get; set; }
        public string CompanyName { get; set; }
        public long CtbId { get; set; }
        public DocClass DocClass { get; set; }
        public long DocId { get; set; }
        public bool EnhancedPdf { get; set; }
        public string FileExt { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public string FileType { get; set; }
        public bool HasEarns { get; set; }
        public bool HasFpp { get; set; }
        public bool HasSyn { get; set; }
        public bool? HasToc { get; set; }
        public bool IndOvw { get; set; }
        public string LocalCode { get; set; }
        public long Pages { get; set; }
        public long Price { get; set; }
        public long PricingType { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime SubmitDate { get; set; }
        public long TocEnd { get; set; }
        public long TocStart { get; set; }
        public bool TranspRpt { get; set; }
        public string Headline { get; set; }
        public Tkr PTkr { get; set; }
        public List<Tkr> Tkr { get; set; }
        public List<NameWithCode> Author { get; set; }
        public List<NameWithCode> Reg { get; set; }
        public List<NameWithCode> RegG { get; set; }
        public List<NameWithCode> Subj { get; set; }
        public List<NameWithCode> SubjG { get; set; }
        public List<NameWithCode> Ind { get; set; }
        public List<NameWithCode> IndG { get; set; }
        public List<NameWithCode> Cntry { get; set; }
        public List<CtSubjectsResp> Grp { get; set; }
        public LangDesc LangDesc { get; set; }
        public List<CtSubjectsResp> ReasonsResp { get; set; }
        public List<CtSubjectsResp> RptStylesResp { get; set; }
        public NameWithCode MainAuthor { get; set; }
        public string RatingType { get; set; }
        public long? RecommendRating { get; set; }
        public long? EstimateRating { get; set; }
        public string NonBillablePages { get; set; }
        public string Audiences { get; set; }
        public List<NameWithCode> DocTyp { get; set; }
        public List<NameWithCode> DocTypG { get; set; }
        public List<CtSubjectsResp> CtSubjectsResp { get; set; }
        public List<CtSubjectsResp> DisciplinesResp { get; set; }
        public List<NameWithCode> Crncy { get; set; }
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
    public enum DocClass { Mn, Mr };
    public enum File { pdf, url, txt, web, activeDoc };
    public enum Lang { En };
    public enum Value { English };

}
