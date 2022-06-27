using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResearchTHM.Core.Models;
using ResearchTHM.Core.Services;
using Microsoft.AspNetCore.Authorization;

namespace ResearchTHM.API.Controllers
{
    [Authorize]
    [Route("api/auditlog")]
    [ApiController]
    public class CtbAuditLogController : ControllerBase
    {
        private readonly IMktCtbAuditLogService _service;

        public CtbAuditLogController(IMktCtbAuditLogService service)
        {
            _service = service;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<MktAuditLog>>> GetAll(int rowCount, string type)
        {
            if (type == "Contributor Job")
            {
                return Ok(_service.Find(e => e.UpdatedOn != null && (e.ProcessId.ToString() == "115B7A5E-20AD-49E6-9BA2-8C4DE97F8469"))
                    .OrderByDescending(o => o.UpdatedOn).Take(rowCount));
            }
            else
                return Ok(_service.Find(e => e.UpdatedOn != null && (e.ProcessId.ToString() == "078D7A2E-2E33-4506-BFD6-21EAF319C1C2"))
                    .OrderByDescending(o => o.UpdatedOn).Take(rowCount));
        }
    }
}