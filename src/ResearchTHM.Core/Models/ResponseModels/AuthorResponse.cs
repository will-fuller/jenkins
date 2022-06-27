using System;
using System.Collections.Generic;
using System.Text;

namespace ResearchTHM.Core.Models.ResponseModels
{
    public class AuthorResponse
    {
        public string name { get; set; }
        public string authorCode { get; set; }
        public int uid { get; set; }
        public string displayName { get; set; }
    }
}
