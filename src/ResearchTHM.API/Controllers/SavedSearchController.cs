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
    public class SavedSearchController : ControllerBase
    {
        private readonly IMktSavedSearchService _savedsearchService;
        private readonly IMapper _mapper;
        public SavedSearchController(IMktSavedSearchService savedsearchService, IMapper mapper)
        {
            this._savedsearchService = savedsearchService;
            this._mapper = mapper;
        }
    
        [HttpGet("updateSavedSearchName")]
        public async Task<ActionResult> updateSearchName([FromQuery] string userId, [FromQuery] string searchId, string searchName)
        {
            var saveSearch = await _savedsearchService.UpdateSaveSearchName(searchId, searchName, userId);
            if (saveSearch) { return Ok(true); }
            else { return Ok(false); }
        }


    }
}