using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ResearchTHM.Core.Models;
using ResearchTHM.Core.Repositories;

namespace ResearchTHM.Core.Services
{
    public interface IMktGroupAccessService : IRepository<MktGroupAccess>

    {
        Task<bool> CreateGroupAccess(string groupId, string userId, string username, List<MktGroupAccess> GroupAccess);

    }
}
