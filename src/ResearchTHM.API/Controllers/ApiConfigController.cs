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
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace ResearchTHM.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApiConfigController : ControllerBase
    {
        private readonly IMktApiConfigService _apiconfigService;
        private readonly IMapper _mapper;
        public ApiConfigController(IMktApiConfigService apiconfigService, IMapper mapper)
        {
            this._apiconfigService = apiconfigService;
            this._mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<MktApiConfig>>> GetApiConfig()
        {
            var apiconfig = _apiconfigService.Find(ele => ele.IsDeleted == false);
            var apiconfigResources = _mapper.Map<IEnumerable<MktApiConfig>, IEnumerable<ApiConfigResource>>(apiconfig);
            return Ok(apiconfigResources);
        }

        [HttpPost("")]
        public async Task<ActionResult<MktApiConfig>> CreateApiConfig([FromBody] MktApiConfig saveMktApiConfig)
        {
            await _apiconfigService.AddAsync(saveMktApiConfig);
            await _apiconfigService.SaveChangesAsync();
            return saveMktApiConfig;
        }


        [HttpGet("{appconfigid}")]
        public async Task<ActionResult<SavedSearchResource>> GetApiConfigById(string appconfigid)
        {
            if(appconfigid == "0")
                return Ok(_mapper.Map<IEnumerable<MktApiConfig>, IEnumerable<ApiConfigResource>>(_apiconfigService.Find(atb => (atb.Status == true && atb.IsDeleted == false))));
            else
                return Ok(_mapper.Map<MktApiConfig, ApiConfigResource>(await _apiconfigService.GetByIdAsync(new Guid(appconfigid))));
        }

    }
}