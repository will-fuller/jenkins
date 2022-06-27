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

using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;


namespace ResearchTHM.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GroupAccessController : ControllerBase
    {
        private readonly IMktGroupAccessService _groupAccessService;
        private readonly IMapper _mapper;

        public GroupAccessController(IMktGroupAccessService groupAccessService, IMapper mapper)
        {
            this._groupAccessService = groupAccessService;
            this._mapper = mapper;
        }

        [HttpGet("")]
        [EnableQuery]
        public async Task<ActionResult<List<MktGroupAccess>>> GetGroupAccess()
        {
            var groupaccess =  await _groupAccessService.GetAllAsync();
            var groupaccessResources = _mapper.Map<IEnumerable<MktGroupAccess>, IEnumerable<GroupAccessResource>>(groupaccess);
            return Ok(groupaccessResources);
        }

        [HttpPost("")]
        public async Task<ActionResult<GroupAccessResource>> CreateGroupAccess([FromBody] GroupAccessResource saveGroupAccessResources)
        {
            var groupId = saveGroupAccessResources.GroupId;
            var userId = saveGroupAccessResources.userId;
            var username = saveGroupAccessResources.userName;
            var ctbArray = saveGroupAccessResources.ContributorId.Split(';');


            List<MktGroupAccess> grpList = new List<MktGroupAccess>();
            foreach (var item in ctbArray)
            {
                grpList.Add(new MktGroupAccess
                {
                    GroupId = Guid.Parse(groupId),
                    ContributorId = Guid.Parse(item)
                });
            }
            var newGroupAccess = await _groupAccessService.CreateGroupAccess(groupId, userId, username, grpList);
            return Ok(newGroupAccess);
        }

    }
}