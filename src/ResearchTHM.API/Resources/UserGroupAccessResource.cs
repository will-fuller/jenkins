using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchTHM.API.Resources
{
    public class UserGroupAccessResource
    {
        public Guid? UserId { get; set; }
        public Guid? GroupId { get; set; }
    }
}
