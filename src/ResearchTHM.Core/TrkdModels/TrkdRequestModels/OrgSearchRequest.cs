using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchTHM.Core.TrkdModels.TrkdRequestModels
{
    public partial class OrgSearchRequest
    {
        public SearchRequest1 SearchRequest1 { get; set; }
    }

    public partial class SearchRequest1
    {
        public string Collection { get; set; }
        public Paging Paging { get; set; }
        public string Query { get; set; }
        public string Filter { get; set; }
        public string SortBy { get; set; }
        public string ResponseProperties { get; set; }
        public bool UnentitledAccess { get; set; }
    }

    public partial class Paging
    {
        public int Top { get; set; }
        public int Skip { get; set; }
    }

}
