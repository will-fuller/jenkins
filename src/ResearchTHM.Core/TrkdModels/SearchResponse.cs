using System;
using System.Collections.Generic;
using System.Text;

namespace ResearchTHM.Core.TrkdModels
{
    public partial class SearchResponse
    {
        public SearchResponse1 Search_Response_1 { get; set; }
    }

    public partial class SearchResponse1
    {
        public ResultsHeader ResultsHeader { get; set; }
        public Results Results { get; set; }
    }

    public partial class Results
    {
        public List<Result> Result { get; set; }
    }

    public partial class Result
    {
        public List<Property> Property { get; set; }
    }

    public partial class Property
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public partial class ResultsHeader
    {
        public long FirstResult { get; set; }
        public long LastResult { get; set; }
        public long Results { get; set; }
        public long TotalResults { get; set; }
    }

    


}
