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
    public class UsageLogService : Repository<MktUsageLog>, IMktUsageLogService
    {
        private readonly ResearchMktContext _ResearchMktContext;
        public UsageLogService(ResearchMktContext ResearchMktContext) : base(ResearchMktContext)
        {
            _ResearchMktContext = ResearchMktContext;
        }

        public async Task<object> GetAllUserActivity(DateTime fromDate, DateTime toDate)
        {

            var results = await (from ga in _ResearchMktContext.MktUsageLogs
                                 join c in _ResearchMktContext.MktUser on ga.UserId equals c.UserId into ps
                                 from c in ps.DefaultIfEmpty()
                                 where (ga.LogType ==  "2" || ga.LogType == "3") && (ga.LogDate >= fromDate && ga.LogDate <= toDate)
                                 orderby c.FirstName, ga.LogDate descending
                                 select new
                                 {
                                     LogId = ga.LogId,
                                     LogType = ga.LogType,
                                     BrowserName = ga.BrowserName,
                                     BrowserVersion = ga.BrowserVersion,
                                     Location = ga.Location,
                                     LogDate = ga.LogDate,
                                     Device = ga.Device,
                                     Related = ga.Related,
                                     UserID = ga.UserId,
                                     Id = c.Id,
                                     Name = c.FirstName + " " + c.LastName,
                                     ga.IP
                                 }).ToListAsync();

            return results;
        }

        public async Task<object> GetAllLoginActivity(DateTime fromDate, DateTime toDate)
        {

            var results = await (from ga in _ResearchMktContext.MktUsageLogs
                                 join c in _ResearchMktContext.MktUser on ga.UserId equals c.UserId into ps
                                 from c in ps.DefaultIfEmpty()
                                 where ga.LogType == "1" && (ga.LogDate >= fromDate && ga.LogDate <= toDate)
                                 orderby ga.LogDate descending
                                 select new
                                 {
                                     LogId = ga.LogId,
                                     LogType = ga.LogType,
                                     BrowserName = ga.BrowserName,
                                     BrowserVersion = ga.BrowserVersion,
                                     Location = ga.Location,
                                     LogDate = ga.LogDate,
                                     Device = ga.Device,
                                     Related = ga.Related,
                                     UserID = ga.UserId,
                                     Name = c.FirstName + " " + c.LastName,
                                     ga.IP
                                 }).ToListAsync();

            return results;
        }


        public async Task<object> GetUserActivityLogByUser(Guid userid, DateTime fromDate, DateTime toDate)
        {

            var results = await (from ga in _ResearchMktContext.MktUsageLogs
                                 join c in _ResearchMktContext.MktUser on ga.UserId equals c.UserId into ps
                                 from c in ps
                                 where (ga.LogDate >= fromDate && ga.LogDate <= toDate && userid == c.UserId)
                                 //group c by c.FirstName into grouped
                                 orderby c.FirstName,ga.LogDate descending
                                 select new
                                 {
                                     LogId = ga.LogId,
                                     LogType = ga.LogType,
                                     BrowserName = ga.BrowserName,
                                     BrowserVersion = ga.BrowserVersion,
                                     Location = ga.Location,
                                     LogDate = ga.LogDate,
                                     Device = ga.Device,
                                     Related = ga.Related,
                                     Name = c.FirstName + " " + c.LastName
                                 }).ToListAsync();

            return results;
        }

        public async Task<object> GetAllUserLog(DateTime fromDate, DateTime toDate)
        {

            var results = await (from ga in _ResearchMktContext.MktUsageLogs
                                 join c in _ResearchMktContext.MktUser on ga.UserId equals c.UserId into ps
                                 from c in ps.DefaultIfEmpty()
                                 where (ga.LogDate >= fromDate && ga.LogDate <= toDate)
                                 orderby ga.LogDate descending
                                 select new
                                 {
                                     LogId = ga.LogId,
                                     LogType = ga.LogType,
                                     BrowserName = ga.BrowserName,
                                     BrowserVersion = ga.BrowserVersion,
                                     Location = ga.Location,
                                     LogDate = ga.LogDate,
                                     Device = ga.Device,
                                     Related = ga.Related,
                                     UserID = ga.UserId,
                                     Id = c.Id,
                                     Name = c.FirstName + " " + c.LastName,
                                     ga.IP
                                 }).ToListAsync();

            return results;
        }
    }
}
