using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResearchTHM.Core.Services;
using ResearchTHM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchTHM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoxController : ControllerBase
    {
        private readonly IBoxUtilService boxService;

        public BoxController(IBoxUtilService _boxService)
        {
            boxService = _boxService;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> GetMetadata(string Id)
        {
            return Ok((await boxService.GetMetadata(Id)).Metadata);
        }
    }
}
