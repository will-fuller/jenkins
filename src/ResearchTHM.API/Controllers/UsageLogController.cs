using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ResearchTHM.Core;
using ResearchTHM.Core.Models;
using ResearchTHM.Core.Services;
using ResearchTHM.API.Resources;

using Microsoft.AspNetCore.Authorization;
using System.Linq.Expressions;
using Microsoft.AspNet.OData;

namespace ResearchTHM.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsageLogController : ControllerBase
    {
        private readonly IMktUsageLogService _usageLogService;
        private readonly IMapper _mapper;
        public UsageLogController(IMktUsageLogService usageLogService, IMapper mapper)
        {
            this._usageLogService = usageLogService;
            this._mapper = mapper;
        }

        [HttpPost("all")]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<UsageLogResource>>> GetUsageLog(DateRangeQuery dr)
        {
            //var usagelog = _usageLogService.Find(ele => ele.UserId != null);
            //var usagelogResources = _mapper.Map<IEnumerable<MktUsageLog>, IEnumerable<UsageLogResource>>(usagelog);
            var useractivitylog = await _usageLogService.GetAllUserActivity(dr.From, dr.To);
            return Ok(useractivitylog);
        }

        [HttpPost("alllog")]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<UsageLogResource>>> GetUsageLogDetail(DateRangeQuery dr)
        {           
            var useractivitylog = await _usageLogService.GetAllUserLog(dr.From, dr.To);
            return Ok(useractivitylog);
        }

        [HttpPost("alllogin")]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<UsageLogResource>>> GetLoginLog(DateRangeQuery dr)
        {
            var useractivitylog = await _usageLogService.GetAllLoginActivity(dr.From, dr.To);
            return Ok(useractivitylog);
        }

        [HttpPost("userlog")]
        [EnableQuery]
        public async Task<ActionResult<List<MktUsageLog>>> GetUserActivityLogByUser(Guid userid, DateRangeQuery dr)
        {
            var useractivitylog = await _usageLogService.GetUserActivityLogByUser(userid, dr.From, dr.To);
            return Ok(useractivitylog);
        }


        //[HttpGet("{logid}")]
        //public async Task<ActionResult<UsageLogResource>> GetApiConfigById(string logid)
        //{
        //    if (logid == "0")
        //        return Ok(_mapper.Map<IEnumerable<MktUsageLog>, IEnumerable<UsageLogResource>>(_usageLogService.Find(atb => (atb.LogId !=null))));
        //    else
        //        return Ok(_mapper.Map<MktUsageLog, UsageLogResource>(await _usageLogService.GetByIdAsync(new Guid(logid))));
        //}


    }
}