using ResearchTHM.Core.TrkdModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchTHM.Core.Models.ResponseModels
{
    public class SearchResponseResource : ProjectCode
    {
        public DateTime ArriveDate { get; set; }
        public string CompanyName { get; set; }
        public long CtbId { get; set; }
        public long DocId { get; set; }
        public string FileExt { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public string FileType { get; set; }
        public bool? HasToc { get; set; }
        public long Pages { get; set; }
        public long Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime SubmitDate { get; set; }
        public string Headline { get; set; }
        public Tkr PTkr { get; set; }
        //public string SecondaryTickers { get; set; }
        public IEnumerable<Tkr> Tkr { get; set; }
        public List<NameWithCode> Author { get; set; }
        public List<NameWithCode> Reg { get; set; }
        public List<NameWithCode> Ind { get; set; }
        public List<NameWithCode> Cntry { get; set; }
        public List<CtSubjectsResp> ReasonsResp { get; set; }
        public List<CtSubjectsResp> RptStylesResp { get; set; }
        public NameWithCode MainAuthor { get; set; }
        public string NonBillablePages { get; set; }
        public string Lang { get; set; }
    }
}
