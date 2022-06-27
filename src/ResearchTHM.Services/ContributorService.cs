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
//using Z.EntityFramework.Plus;

namespace ResearchTHM.Services
{
    public class ContributorService : Repository<MktContributor>, IMktContributorService
    {
        private readonly ResearchMktContext _ResearchMktContext;        
        public ContributorService(ResearchMktContext ResearchMktContext) : base(ResearchMktContext)
        {
            _ResearchMktContext = ResearchMktContext;
        }


        public async Task<object> UpdateEnabledContributorList(string username, string userid, List<Guid> contributorId)
        {
            var currentList = await _ResearchMktContext.MktContributor.Where(c => c.IsDeleted == false).Select(c => new { c.ContributorId, c.IsExcluded }).ToListAsync();

            var included = currentList.Where(c => c.IsExcluded == false).Select(c => c.ContributorId);
            var excluded = currentList.Where(c => c.IsExcluded == true).Select(c => c.ContributorId);
            var toBeExcluded = included.Except(contributorId);
            var toBeIncluded = excluded.Intersect(contributorId);

            var processId = await _ResearchMktContext.MktProcessList.Where(c => c.ProcessName == "ManageContributor" && c.ProcessType == "UI").ToListAsync();
            await _ResearchMktContext.MktAuditLog.AddAsync(new MktAuditLog
            {
                UserName = username,
                UserId = new Guid(userid.ToString()),
                IncludedIds = String.Join(";", toBeIncluded),
                ExcludedIds = String.Join(";", toBeExcluded),
                UpdatedOn = DateTime.UtcNow,
                ProcessId = new Guid(processId[0].ProcessId.ToString()),
                ProcessName = processId[0].ProcessName.ToString()
            });


            var x = await _ResearchMktContext.MktContributor
                .Where(c => toBeIncluded.Contains(c.ContributorId))
                .UpdateFromQueryAsync(ele => new MktContributor { IsExcluded = false });

            var y = await _ResearchMktContext.MktContributor
                .Where(c => toBeExcluded.Contains(c.ContributorId))
                .UpdateFromQueryAsync(ele => new MktContributor { IsExcluded = true });

            var m = await _ResearchMktContext.MktGroupAccess.Where(e => toBeExcluded.Contains(e.ContributorId)).DeleteFromQueryAsync();

            List<MktGroupAccess> added = new List<MktGroupAccess>();

            await _ResearchMktContext.MktGroup.ForEachAsync(g =>
              {
                  toBeIncluded.ToList().ForEach(i =>
                  {
                      
                      added.Add(new MktGroupAccess()
                      {
                          ContributorId = i,
                          GroupId = g.GroupId
                      });

                  });
              });

            await _ResearchMktContext.MktGroupAccess.AddRangeAsync(added);

            await _ResearchMktContext.SaveChangesAsync();

            return new { Included = toBeIncluded, Excluded = toBeExcluded };
        }

        //public async Task<object> UpdateEnabledContributorList(string username,string userid, List<string> contributorUid)
        //{
        //    var currentList = await _ResearchMktContext.MktContributor.Where(c => c.IsDeleted == 0).Select(c => new { c.ContributorUid, c.IsExcluded }).ToListAsync();

        //    var included = currentList.Where(c => c.IsExcluded == false).Select(c => c.ContributorUid);
        //    var excluded = currentList.Where(c => c.IsExcluded == true).Select(c => c.ContributorUid);
        //    var toBeExcluded = included.Except(contributorUid);
        //    var toBeIncluded = excluded.Intersect(contributorUid);

        //    var processId = await _ResearchMktContext.MktProcessList.Where(c => c.ProcessName=="ManageContributor" && c.ProcessType == "UI").ToListAsync();
        //    await _ResearchMktContext.MktAuditLog.AddAsync(new MktAuditLog
        //    {
        //        UserName = username,
        //        UserId = new Guid(userid.ToString()),
        //        IncludedIds = String.Join(";", toBeIncluded),
        //        ExcludedIds = String.Join(";", toBeExcluded),
        //        UpdatedOn = DateTime.Now,
        //        ProcessId = new Guid(processId[0].ProcessId.ToString()),
        //        ProcessName = processId[0].ProcessName.ToString()
        //    }) ;

        //    var x = await _ResearchMktContext.MktContributor
        //        .Where(c => toBeIncluded.Contains(c.ContributorUid))
        //        .UpdateFromQueryAsync(ele => new MktContributor { IsExcluded = false });

        //    var y = await _ResearchMktContext.MktContributor
        //        .Where(c => toBeExcluded.Contains(c.ContributorUid))
        //        .UpdateFromQueryAsync(ele => new MktContributor { IsExcluded = true });

        //    await _ResearchMktContext.SaveChangesAsync();

        //    return new { Included = toBeIncluded, Excluded = toBeExcluded };
        //}

        public async Task<object> GetViewContributorGroup(bool IsDeveloper)
        {
            VwContributorGroup vwCtbGroup = new VwContributorGroup();
            VwContributorGroupDeveloper vwCtbGroupDev = new VwContributorGroupDeveloper();
            var result = (dynamic)null;

            if(IsDeveloper == true)
            {
                result = _ResearchMktContext.VwContributorGroupDeveloper.AsQueryable().OrderBy(e => e.ContributorName);

            }else
            {
                result = _ResearchMktContext.VwContributorGroup.AsQueryable().OrderBy(e => e.ContributorName);
            }
            
            return result;
        }
    }
    
}
