using System;
using System.Collections.Generic;
using System.Text;
using ResearchTHM.Core;
using ResearchTHM.Core.Models;
using ResearchTHM.Core.Services;
using System.Threading.Tasks;
using ResearchTHM.Core.Repositories;
using System.Linq;
using Microsoft.EntityFrameworkCore;



namespace ResearchTHM.Services
{
    public class GroupAccessService : Repository<MktGroupAccess>, IMktGroupAccessService
    {
        private readonly ResearchMktContext _ResearchMktContext;
        public GroupAccessService(ResearchMktContext ResearchMktContext) : base(ResearchMktContext)
        {
            _ResearchMktContext = ResearchMktContext;
        }        
        public async Task<bool> CreateGroupAccess(string groupId,string userId,string username,List<MktGroupAccess> groupAccess)
        {            
            var processId = await _ResearchMktContext.MktProcessList.Where(c => c.ProcessName == "GroupEntitlement" && c.ProcessType == "UI").ToListAsync();
            var ctbid = groupAccess.Select(e => e.ContributorId);
            await _ResearchMktContext.MktAuditLog.AddAsync(new MktAuditLog
            {
                UserName = username,
                UserId = new Guid(userId.ToString()),
                IncludedIds = String.Join(";", ctbid),
                ExcludedIds = null,
                UpdatedOn = DateTime.UtcNow,
                ProcessId = new Guid(processId[0].ProcessId.ToString()),
                ProcessName = processId[0].ProcessName.ToString()
            });


            if (groupId != null)
            {
                _ResearchMktContext.MktGroupAccess.Where(c => c.GroupId == Guid.Parse(groupId)).DeleteFromQuery();
            }

            await _ResearchMktContext.MktGroupAccess.AddRangeAsync(groupAccess);
            if (await _ResearchMktContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }

    }
}
