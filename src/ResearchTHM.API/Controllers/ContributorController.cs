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
    public class ContributorController : ControllerBase
    {
        private readonly IMktContributorService _contributorService;
        private readonly IMapper _mapper;
        public ContributorController(IMktContributorService contributorService, IMapper mapper)
        {
            this._contributorService = contributorService;
            this._mapper = mapper;
        }

        [HttpGet("all")]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<ContributorResource>>> GetContributors(bool includeDeleted = false)
        {
            IOrderedEnumerable<MktContributor> contributor;
            if (includeDeleted)
            {
                contributor = (await _contributorService.GetAllAsync()).OrderBy(e => e.ContributorName);
            }
            else
                contributor = _contributorService.Find(ele => ele.IsDeleted == false).OrderBy(e => e.ContributorName);

            var contributorResources = _mapper.Map<IEnumerable<MktContributor>, IEnumerable<ContributorResource>>(contributor);
            //return Ok(contributorResources.Select(Helpers.DynamicSelectGenerator<ContributorResource>(selectedCols)));
            return Ok(contributorResources);
        }

        [HttpGet("ctbGroup")]
        [EnableQuery]
        public async Task<ActionResult<object>> GetViewContributorGroups(bool IsDeveloper = false)
        {
            var contributorGroup = _contributorService.GetViewContributorGroup(IsDeveloper);

            return Ok(contributorGroup);
        }

        [HttpGet("allgroup")]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<ContributorResource>>> GetGroupContributors()
        {
            var contributor = _contributorService.Find(ele => ele.IsDeleted == false && ele.IsExcluded == false).OrderBy(e => e.ContributorName);
            var contributorResources = _mapper.Map<IEnumerable<MktContributor>, IEnumerable<ContributorResource>>(contributor);
            //return Ok(contributorResources.Select(Helpers.DynamicSelectGenerator<ContributorResource>(selectedCols)));
            return Ok(contributorResources);
        }

        //[HttpGet]
        //[EnableQuery]
        //public async Task<ActionResult<IEnumerable<ContributorResource>>> GetContributorById(string contributorId)
        //{
        //    if (contributorId == "0")
        //        return Ok(_mapper.Map<IEnumerable<MktContributor>, IEnumerable<ContributorResource>>(_contributorService.Find(ctb => (ctb.IsExcluded == false && ctb.IsDeleted == false))).OrderBy(e => e.ContributorName));
        //    else
        //        return Ok(_mapper.Map<MktContributor, ContributorResource>(await _contributorService.GetByIdAsync(new Guid(contributorId))));
        //}


        //[HttpPut("exclude")]
        //public async Task<IActionResult> ExcludeContributor(Guid contributorId, bool status)
        //{
        //    if (await _contributorService.ExcludeContributor(contributorId, status))
        //        return Ok();
        //    else
        //        return Problem();
        //}

        [HttpPut("enable")]
        public async Task<IActionResult> UpdateEnabledContributorList([FromQuery] string username, string userid, [FromBody] List<Guid> contributorid)
        {
            return Ok(await _contributorService.UpdateEnabledContributorList(username, userid, contributorid));
        }

    }
}