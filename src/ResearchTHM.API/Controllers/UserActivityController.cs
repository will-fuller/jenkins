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
using System.Threading.Tasks.Sources;

namespace ResearchTHM.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserActivityController : ControllerBase
    {
        private readonly IMktUserActivityService _useractivityService;
        private readonly IMapper _mapper;
        public UserActivityController(IMktUserActivityService useractivityService, IMapper mapper)
        {
            this._useractivityService = useractivityService;
            this._mapper = mapper;
        }

        //[HttpGet("all")]
        //[EnableQuery]
        //public async Task<ActionResult<List<MktUserActivity>>> GetUserActivity()
        //{
        //    var useractivity = _useractivityService.Find(ele => ele.IsDeleted == false);
        //    var useractivityResources = _mapper.Map<IEnumerable<MktUserActivity>, IEnumerable<UserActivityResource>>(useractivity);
        //    return Ok(useractivityResources);
        //}

        [HttpGet("useractivitylog")]
        [EnableQuery]
        public async Task<ActionResult<UserActivityResource>> GetUserActivityById(string useractivityid)
        {
            if (useractivityid == "0")
                return Ok(_useractivityService.Find(atb => atb.IsDeleted == false));
            else
                return Ok(await _useractivityService.GetByIdAsync(new Guid(useractivityid)));
        }


        [HttpPost("byuser")]
        [EnableQuery]
        public async Task<ActionResult<MktUserActivity>> GetUserActivityByUserId([FromQuery]string userId, DateRangeQuery q)
        {
            return Ok(_useractivityService
                .Find(atb => atb.UserId.ToString() == userId 
                                && atb.CreatedOn > q.From && atb.CreatedOn < q.To));
        }
        [HttpPost("byuser/count")]
        [EnableQuery]
        public async Task<ActionResult<MktUserActivity>> GetUserActivityByUserIdCount([FromQuery]string userId, DateRangeQuery q)
        {
            return Ok(_useractivityService
                .Find(atb => atb.UserId.ToString() == userId 
                             && atb.CreatedOn > q.From && atb.CreatedOn < q.To).Count());
        }


    }

    public class DateRangeQuery
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

    }
}