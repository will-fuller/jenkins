using ResearchTHM.Core.Models;
using ResearchTHM.Core.Services;
using ResearchTHM.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResearchTHM.Services
{
    public class CtbAuditLogService:Repository<MktAuditLog>, IMktCtbAuditLogService
    {
        public CtbAuditLogService(ResearchMktContext context):base(context)
        {

        }
    }
}
