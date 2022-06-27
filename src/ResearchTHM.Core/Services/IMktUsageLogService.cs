using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ResearchTHM.Core.Models;
using ResearchTHM.Core.Repositories;

namespace ResearchTHM.Core.Services
{
    public interface IMktUsageLogService : IRepository<MktUsageLog>
    {
        Task<object> GetUserActivityLogByUser(Guid userid, DateTime fromDate, DateTime toDate);
        Task<object> GetAllUserActivity(DateTime fromDate, DateTime toDate);
        Task<object> GetAllLoginActivity(DateTime fromDate, DateTime toDate);
        Task<object> GetAllUserLog(DateTime fromDate, DateTime toDate);
    }
}
