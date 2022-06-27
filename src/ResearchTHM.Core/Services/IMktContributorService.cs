using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ResearchTHM.Core.Models;
using ResearchTHM.Core.Repositories;

namespace ResearchTHM.Core.Services
{
   public interface IMktContributorService: IRepository<MktContributor>
    {
       Task<object> UpdateEnabledContributorList(string username, string userid, List<Guid> contributorid);
       Task<object> GetViewContributorGroup(bool IsDeveloper);
    }
}
