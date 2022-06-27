using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchTHM.Core.TrkdModels.TrkdRequestModels
{
    public class DocSearchRequest
    {
        public DocSearchRequest1 DocSearch_Request_1 { get; set; }

        public DocSearchRequest()
        {
            DocSearch_Request_1 = new DocSearchRequest1();
        }
    }
    public class MatchStrHdln
    {
        public string value { get; set; }
    }
    public class MatchStrText
    {
        public string value { get; set; }
    }
    public class MatchStrToc
    {
        public string value { get; set; }
    }
    public class TextSearch
    {
        public MatchStrHdln matchStrHdln { get; set; }
        public MatchStrText matchStrText { get; set; }
        public MatchStrToc matchStrTOC { get; set; }
        public bool joinStrCondByOr { get; set; }
    }
    public class Ctbsm
    {
        public string category { get; set; }
        public string action { get; set; }
        public string value { get; set; }
    }
    
    public class DocSearchRequest1
    {
        public DocSearchRequest1()
        {
            //textSearch = new TextSearch();
            //ctbsm = new Ctbsm();
            //pagingHint = new PagingHint();
        }

        public DocSearchRequest1 GetNewRequestInstance(DocSearchRequest1 req)
        {
           return (DocSearchRequest1)req.MemberwiseClone();
        }
        public int maxRows { get; set; }
        public string NumPagesFrom { get; set; }
        public string NumPagesTo { get; set; }
        public string dateFrom { get; set; }
        public string dateTo { get; set; }
        public string ctbs { get; set; }
        public string industrySet { get; set; }
        public bool excludeCtbs { get; set; }
        public bool bShowPrims { get; set; }
        public bool bCP_ResponseType { get; set; }
        public bool bHierIndustries { get; set; }
        public IEnumerable<string> docID { get; set; }
        public IEnumerable<string> langID { get; set; }
        public bool calcPrice { get; set; }
        public IEnumerable<string> analyst { get; set; }
        public IEnumerable<string> region { get; set; }
        public IEnumerable<string> nRegion { get; set; }
        public IEnumerable<string> industry { get; set; }
        public IEnumerable<string> country { get; set; }
        public IEnumerable<string> nCountry { get; set; }
        public IEnumerable<string> ticker { get; set; }
        public List<string> rptStyles { get; set; }
        public IEnumerable<string> reasons { get; set; }
        public string tkrEncoding { get; set; }
        public bool tkrPrimary { get; set; }
        public TextSearch textSearch { get; set; }
        public Ctbsm ctbsm { get; set; }
        public PagingHint pagingHint { get; set; }
        public Sort sort { get; set; }
        public int req_id { get; set; }
    }
    public class PagingHint
    {
        public string hintStr { get; set; }
        public string docID { get; set; }
    }
    public class Sort
    {
        public string s_c { get; set; }
        public string s_d { get; set; }
    }

}
