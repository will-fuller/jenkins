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
using Microsoft.AspNet.OData;

namespace ResearchTHM.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IMktGroupService _groupService;
        private readonly IMktGroupAccessService _groupAccessService;
        private readonly IMapper _mapper;

        public GroupController(IMktGroupService groupService, IMktGroupAccessService groupAService, IMapper mapper)
        {
            this._groupService = groupService;
            this._groupAccessService = groupAService;
            this._mapper = mapper;
        }

        [HttpGet("")]
        [EnableQuery]
        public async Task<ActionResult<List<MktGroup>>> GetGroup(bool IsDeveloper = false)
        {
            var group = (dynamic)null;
            if (IsDeveloper == true)
            {
                group = (await _groupService.GetAllAsync()).Where(ele => ele.IsDeleted == false && ele.Status == true).OrderBy(x => x.Priority);
            }
            else
            {
                group = (await _groupService.GetAllAsync()).Where(ele => ele.IsDeleted == false && ele.Status == true && ele.isdeveloper == false).OrderBy(x => x.Priority);
            }

            var groupResources = _mapper.Map<IEnumerable<MktGroup>, IEnumerable<GroupResource>>(group);
            return Ok(groupResources);
        }

        [HttpGet("all/{IsDeveloper}")]
        [EnableQuery]
        public async Task<ActionResult<List<MktGroup>>> GetGroupAll(bool IsDeveloper)
        {
           var group = (dynamic)null;
            if (IsDeveloper == true)
            {
                group = (await _groupService.GetAllAsync()).Where(ele => ele.IsDeleted == false && ele.Status == true).OrderBy(x => x.Priority);
            }
            else
            {
                 group = (await _groupService.GetAllAsync()).Where(ele => ele.IsDeleted == false && ele.Status == true && ele.isdeveloper == false).OrderBy(x => x.Priority);
            }
            
            var groupResources = _mapper.Map<IEnumerable<MktGroup>, IEnumerable<GroupResource>>(group);
            return Ok(groupResources);
        }

        [HttpGet("{groupId}")]
        public async Task<ActionResult<GroupResource>> GetGroupById(Guid groupId)
        {
            var group = await _groupService.GetByIdAsync(groupId);
            var groupResources = _mapper.Map<MktGroup, GroupResource>(group);
            return Ok(groupResources);
        }

        [HttpPost("add")]
        public async Task<ActionResult<GroupResource>> CreateGroup(GroupResource saveGroupResources)
        {
            GroupResource saveGroup = new GroupResource();
            saveGroup.GroupId = saveGroupResources.GroupId;
            saveGroup.GroupName = saveGroupResources.GroupName;
            saveGroup.GroupDescription = saveGroupResources.GroupDescription;
            saveGroup.GroupAdName = saveGroupResources.GroupAdName;
            saveGroup.IsDeleted = false;
            saveGroup.Status = true;
            saveGroup.CreatedOn = DateTime.Now;

            var userId = saveGroupResources.userId;
            var userName = saveGroupResources.userName;
            var GroupToCreate = _mapper.Map<GroupResource, MktGroup>(saveGroup);
            await _groupService.AddAsync(GroupToCreate);
            var result = await _groupService.SaveChangesAsync();
            if (result > 0)
            {
                var ctbArray = saveGroupResources.ContributorId.Split(';');
                List<MktGroupAccess> grpList = new List<MktGroupAccess>();
                foreach (var item in ctbArray)
                {
                    grpList.Add(new MktGroupAccess
                    {
                        GroupId = saveGroupResources.GroupId,
                        ContributorId = Guid.Parse(item)
                    });
                }
                var newGroupAccess = await _groupAccessService.CreateGroupAccess(null, userId, userName, grpList);
                return Ok(newGroupAccess);
            }
            else { return Ok(result); }
        }

        [HttpPut("updatestatus")]
        public async Task<IActionResult> UpdateStatus(string groupid, bool status, string userid)
        {
            if (await _groupService.UpdateStatus(groupid, status, userid))
                return Ok(true);
            else
                return Problem();
        }

        [HttpGet("delete")]
        public async Task<IActionResult> DeleteGroup(Guid groupid, string userid)
        {
            if (await _groupService.DeleteGroup(groupid, userid))
                return Ok(true);
            else
                return Problem();
        }


        [HttpPut("update")]
        public async Task<ActionResult> UpdateGroup(Guid groupId, [FromBody] GroupResource saveGroupResources)
        {
            if (saveGroupResources.GroupId != groupId)
                return BadRequest();

            
            var group = _mapper.Map<GroupResource, MktGroup>(saveGroupResources);
            await _groupService.UpdateGroup(group, saveGroupResources.userId);           
            var result = _groupService.SaveChangesAsync();
            if (result.Status.ToString() ==  "RanToCompletion") {
                var ctbArray = saveGroupResources.ContributorId.Split(';');
                List<MktGroupAccess> grpList = new List<MktGroupAccess>();
                foreach (var item in ctbArray)
                {
                    grpList.Add(new MktGroupAccess
                    {
                        GroupId = saveGroupResources.GroupId,
                        ContributorId = Guid.Parse(item)
                    });
                }
                var newGroupAccess = await _groupAccessService.CreateGroupAccess(groupId.ToString(), saveGroupResources.userId, saveGroupResources.userName, grpList);
                return Ok();
            }
            return Ok();
        }

    }
}