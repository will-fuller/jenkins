using System;
using System.Collections.Generic;

namespace ResearchTHM.Core.TrkdModels.TrkdRequestModels
{
    public class AuthorSearchRequiredInfo
    {
        public string authorInfoType { get; set; }
    }

    public class BasicSearch
    {
        public string speciality { get; set; }
        public string nameSuffix { get; set; }
        public string middleName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string personID { get; set; }
        public int uid { get; set; }
        public string authorCode { get; set; }
    }

    public class CtbSearch
    {
        public bool visibleAsDisabled { get; set; }
        public bool isCurrent { get; set; }
        public int ctbID { get; set; }
    }

    public class ContribStringsSearch
    {
        public string local { get; set; }
        public string contributionString { get; set; }
        public int ctbID { get; set; }
    }

    public class AuthorSearchCriterion
    {
        public List<BasicSearch> basicSearch { get; set; }
        public List<CtbSearch> ctbSearch { get; set; }
        public List<ContribStringsSearch> contribStringsSearch { get; set; }
    }

    public class AuthorsListRequest1
    {
        public int rowcount { get; set; }
        public int startrow { get; set; }
        public string dstLangID { get; set; }
        public bool useMasterDB { get; set; }
        public bool returnStarmineID { get; set; }
        public List<AuthorSearchRequiredInfo> authorSearchRequiredInfo { get; set; }
        public AuthorSearchCriterion authorSearchCriterion { get; set; }
    }

    public class AuthorsRequest
    {
        public AuthorsListRequest1 AuthorsList_Request_1 { get; set; }
    }

}