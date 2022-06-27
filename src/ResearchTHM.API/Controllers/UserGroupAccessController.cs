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

namespace ResearchTHM.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserGroupAccessController : ControllerBase
    {
        private readonly IMktUserGroupAccessService _userGroupAccessService;

        public UserGroupAccessController(IMktUserGroupAccessService userGroupAccessService)
        {
            this._userGroupAccessService = userGroupAccessService;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<MktUserGroupAccess>>> GetUserGroupAccess()
        {
            return Ok(await _userGroupAccessService.GetuserGroupAccessFromView());
        }



    }
}