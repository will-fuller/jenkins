using System;
using System.Collections.Generic;
using System.Text;

namespace ResearchTHM.Core.TrkdModels
{
public class AuthorsResponse
    {
        public AuthorsListResponse1 AuthorsList_Response_1 { get; set; }
    }

    public class Basic
    {
        public string biography { get; set; }
        public DateTime creationDate { get; set; }
        public string displayableName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string lastUpdateTime { get; set; }
        public string middleName { get; set; }
        public string namePrefix { get; set; }
        public string origin { get; set; }
        public string speciality { get; set; }
    }

    public class Ctb
    {
        public string ctbID { get; set; }
        public bool isCurrent { get; set; }
        public bool visibleAsDisabled { get; set; }
    }

    public partial class Author
    {
        public string authorCode { get; set; }
        public int uid { get; set; }
        public Basic basic { get; set; }
        public List<Ctb> ctb { get; set; }
    }

    public class AuthorsListResponse1
    {
        public List<Author> author { get; set; }
    }

}
