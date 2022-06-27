using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ResearchTHM.Core.Models;
using ResearchTHM.Core.Repositories;

namespace ResearchTHM.Core.Services
{
    public interface IMktGroupService: IRepository<MktGroup>
    {
        Task<bool> UpdateGroup(MktGroup group,string userId);
        Task<bool> DeleteGroup(Guid groupid, string userid);
        Task<bool> UpdateStatus(string groupid, bool status, string userid);
    }
}
